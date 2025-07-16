namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents an ingress rule for use with kubectl create ingress.
/// </summary>
public class IngressRule
{
  /// <summary>
  /// Gets or sets the host for the rule.
  /// </summary>
  public string? Host { get; set; }

  /// <summary>
  /// Gets or sets the path for the rule.
  /// Paths containing the leading character '*' are considered pathType=Prefix.
  /// </summary>
  public string? Path { get; set; }

  /// <summary>
  /// Gets or sets the service name for the rule.
  /// </summary>
  public required string ServiceName { get; set; }

  /// <summary>
  /// Gets or sets the service port for the rule.
  /// </summary>
  public required string ServicePort { get; set; }

  /// <summary>
  /// Gets or sets the TLS secret name for the rule.
  /// </summary>
  public string? TlsSecretName { get; set; }

  /// <summary>
  /// Formats the rule as expected by kubectl create ingress.
  /// </summary>
  /// <returns>Rule in format host/path=service:port[,tls=secretname]</returns>
  public string ToKubectlFormat()
  {
    // Build the host/path part
    string hostPath = "";
    if (!string.IsNullOrEmpty(Host))
    {
      hostPath = Host;
    }
    hostPath += "/";
    if (!string.IsNullOrEmpty(Path))
    {
      hostPath += Path;
    }

    // If neither host nor path is specified, just use empty string
    if (string.IsNullOrEmpty(hostPath))
    {
      hostPath = "";
    }

    string rule = $"{hostPath}={ServiceName}:{ServicePort}";

    if (!string.IsNullOrEmpty(TlsSecretName))
    {
      rule += $",tls={TlsSecretName}";
    }

    return rule;
  }
}
