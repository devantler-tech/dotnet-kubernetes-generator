using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents an Ingress for use with kubectl create ingress.
/// </summary>
public class Ingress
{
  /// <summary>
  /// Gets or sets the metadata for the ingress.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the ingress class name.
  /// </summary>
  public string? IngressClassName { get; set; }

  /// <summary>
  /// Gets or sets the default backend for the ingress.
  /// </summary>
  public IngressBackend? DefaultBackend { get; set; }

  /// <summary>
  /// Gets or sets the rules for the ingress.
  /// </summary>
  public IList<IngressRule>? Rules { get; init; }
}

/// <summary>
/// Represents an ingress backend.
/// </summary>
public class IngressBackend
{
  /// <summary>
  /// Gets or sets the service name.
  /// </summary>
  public required string ServiceName { get; set; }

  /// <summary>
  /// Gets or sets the service port.
  /// </summary>
  public required string ServicePort { get; set; }
}

/// <summary>
/// Represents an ingress rule.
/// </summary>
public class IngressRule
{
  /// <summary>
  /// Gets or sets the host for the rule.
  /// </summary>
  public string? Host { get; set; }

  /// <summary>
  /// Gets or sets the path for the rule.
  /// </summary>
  public string? Path { get; set; }

  /// <summary>
  /// Gets or sets the backend for the rule.
  /// </summary>
  public required IngressBackend Backend { get; set; }

  /// <summary>
  /// Gets or sets the TLS secret name for the rule.
  /// </summary>
  public string? TlsSecretName { get; set; }
}