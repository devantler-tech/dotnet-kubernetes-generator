namespace DevantlerTech.KubernetesGenerator.Native.Models.Ingress;

/// <summary>
/// Represents an ingress rule for use with kubectl create ingress.
/// </summary>
public class NativeIngressRule
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
    string rule = $"{Host}/{Path}={ServiceName}:{ServicePort}";

    if (!string.IsNullOrEmpty(TlsSecretName))
    {
      rule += $",tls={TlsSecretName}";
    }

    return rule;
  }
}
