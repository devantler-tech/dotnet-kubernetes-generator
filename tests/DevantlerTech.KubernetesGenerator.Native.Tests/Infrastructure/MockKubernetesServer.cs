using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.Infrastructure;

/// <summary>
/// Simple mock Kubernetes API server for testing kubectl commands
/// </summary>
sealed class MockKubernetesServer : IDisposable
{
  HttpListener? _listener;
  Task? _serverTask;
  readonly CancellationTokenSource _cancellationTokenSource = new();
  readonly string _kubeconfigPath;
  readonly string _originalKubeconfigPath;
  bool _disposed;

  public string ServerUrl { get; private set; } = "";

  public MockKubernetesServer()
  {
    _kubeconfigPath = Path.Combine(Path.GetTempPath(), $"kubeconfig-test-{Guid.NewGuid()}");
    _originalKubeconfigPath = Environment.GetEnvironmentVariable("KUBECONFIG") ?? "";
  }

  public void Start()
  {
    // Find an available port
    using TcpListener listener = new(IPAddress.Loopback, 0);
    listener.Start();
    int port = ((IPEndPoint)listener.LocalEndpoint).Port;
    listener.Stop();

    ServerUrl = $"http://localhost:{port}";
    _listener = new HttpListener();
    _listener.Prefixes.Add($"{ServerUrl}/");
    _listener.Start();

    // Start handling requests
    _serverTask = Task.Run(HandleRequestsAsync, _cancellationTokenSource.Token);

    // Create kubeconfig pointing to mock server
    CreateMockKubeconfig();

    // Set environment variable
    Environment.SetEnvironmentVariable("KUBECONFIG", _kubeconfigPath);
  }

  public void Stop()
  {
    if (_disposed) return;

    _cancellationTokenSource.Cancel();
    _listener?.Stop();
    _ = (_serverTask?.Wait(TimeSpan.FromSeconds(5)));
    _listener?.Close();

    // Restore original KUBECONFIG
    Environment.SetEnvironmentVariable("KUBECONFIG", _originalKubeconfigPath);

    // Clean up temporary kubeconfig
    if (File.Exists(_kubeconfigPath))
    {
      File.Delete(_kubeconfigPath);
    }
  }

  async Task HandleRequestsAsync()
  {
    while (!_cancellationTokenSource.Token.IsCancellationRequested && _listener != null)
    {
      try
      {
        var context = await _listener.GetContextAsync().ConfigureAwait(false);
        _ = Task.Run(() => ProcessRequest(context), _cancellationTokenSource.Token);
      }
      catch (HttpListenerException)
      {
        // Expected when stopping
        break;
      }
      catch (ObjectDisposedException)
      {
        // Expected when stopping
        break;
      }
    }
  }

  static void ProcessRequest(HttpListenerContext context)
  {
    var request = context.Request;
    var response = context.Response;

    try
    {
      // Log the request for debugging  
      Console.WriteLine($"Mock server received: {request.HttpMethod} {request.Url?.AbsolutePath}");

      if (request.HttpMethod == "GET" && request.Url?.AbsolutePath == "/version")
      {
        HandleVersionRequest(response);
      }
      else if (request.HttpMethod == "GET" && request.Url?.AbsolutePath == "/api/v1")
      {
        HandleApiDiscoveryRequest(response);
      }
      else if (request.HttpMethod == "GET" && request.Url?.AbsolutePath == "/api")
      {
        HandleApiRequest(response);
      }
      else if (request.HttpMethod == "POST" &&
               request.Url?.AbsolutePath?.Contains("/api/v1/namespaces/", StringComparison.OrdinalIgnoreCase) == true &&
               request.Url?.AbsolutePath?.EndsWith("/pods", StringComparison.OrdinalIgnoreCase) == true)
      {
        HandlePodCreateRequest(response);
      }
      else
      {
        response.StatusCode = 404;
        response.Close();
      }
    }
    catch (HttpListenerException)
    {
      response.StatusCode = 500;
      response.Close();
    }
    catch (ObjectDisposedException)
    {
      response.StatusCode = 500;
      response.Close();
    }
  }

  static void HandleVersionRequest(HttpListenerResponse response)
  {
    object version = new
    {
      major = "1",
      minor = "28",
      gitVersion = "v1.28.0-mock"
    };

    string json = JsonSerializer.Serialize(version);
    byte[] buffer = Encoding.UTF8.GetBytes(json);

    response.StatusCode = 200;
    response.ContentType = "application/json";
    response.ContentLength64 = buffer.Length;
    response.OutputStream.Write(buffer, 0, buffer.Length);
    response.Close();
  }

  static void HandleApiDiscoveryRequest(HttpListenerResponse response)
  {
    object apiResources = new
    {
      kind = "APIResourceList",
      apiVersion = "v1",
      groupVersion = "v1",
      resources = new[]
        {
                new { name = "pods", singularName = "pod", namespaced = true, kind = "Pod" }
            }
    };

    string json = JsonSerializer.Serialize(apiResources);
    byte[] buffer = Encoding.UTF8.GetBytes(json);

    response.StatusCode = 200;
    response.ContentType = "application/json";
    response.ContentLength64 = buffer.Length;
    response.OutputStream.Write(buffer, 0, buffer.Length);
    response.Close();
  }

  static void HandleApiRequest(HttpListenerResponse response)
  {
    object apiVersions = new
    {
      kind = "APIVersions",
      versions = new[] { "v1" },
      serverAddressByClientCIDRs = new[]
      {
        new { clientCIDR = "0.0.0.0/0", serverAddress = "localhost:8080" }
      }
    };

    string json = JsonSerializer.Serialize(apiVersions);
    byte[] buffer = Encoding.UTF8.GetBytes(json);
    
    response.StatusCode = 200;
    response.ContentType = "application/json";
    response.ContentLength64 = buffer.Length;
    response.OutputStream.Write(buffer, 0, buffer.Length);
    response.Close();
  }

  static void HandlePodCreateRequest(HttpListenerResponse response)
  {
    string podYaml = """
            apiVersion: v1
            kind: Pod
            metadata:
              creationTimestamp: null
              labels:
                run: test-pod
              name: test-pod
              namespace: default
            spec:
              containers:
              - image: nginx
                name: test-pod
                resources: {}
              dnsPolicy: ClusterFirst
              restartPolicy: Always
            status: {}
            """;

    byte[] buffer = Encoding.UTF8.GetBytes(podYaml);

    response.StatusCode = 200;
    response.ContentType = "application/yaml";
    response.ContentLength64 = buffer.Length;
    response.OutputStream.Write(buffer, 0, buffer.Length);
    response.Close();
  }

  void CreateMockKubeconfig()
  {
    string kubeconfig = $$"""
            apiVersion: v1
            clusters:
            - cluster:
                server: {{ServerUrl}}
                insecure-skip-tls-verify: true
              name: mock-cluster
            contexts:
            - context:
                cluster: mock-cluster
                user: mock-user
              name: mock-context
            current-context: mock-context
            kind: Config
            preferences: {}
            users:
            - name: mock-user
              user:
                token: mock-token
            """;

    File.WriteAllText(_kubeconfigPath, kubeconfig);
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  void Dispose(bool disposing)
  {
    if (!_disposed && disposing)
    {
      Stop();
      _cancellationTokenSource.Dispose();
      _disposed = true;
    }
  }
}
