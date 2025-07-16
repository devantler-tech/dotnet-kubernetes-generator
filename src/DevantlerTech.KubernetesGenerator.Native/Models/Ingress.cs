namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents an Ingress for use with kubectl create ingress.
/// </summary>
public class Ingress
{
  /// <summary>
  /// Gets or sets the metadata for the ingress.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the ingress class to be used.
  /// </summary>
  public string? Class { get; set; }

  /// <summary>
  /// Gets or sets the default backend service in format of svcname:port.
  /// </summary>
  public string? DefaultBackend { get; set; }

  /// <summary>
  /// Gets or sets the rules for the ingress.
  /// Rules are in format host/path=service:port[,tls=secretname].
  /// </summary>
  public IList<IngressRule> Rules { get; init; } = [];

  /// <summary>
  /// Gets or sets the annotations for the ingress.
  /// </summary>
  public IDictionary<string, string>? Annotations { get; init; }
}
