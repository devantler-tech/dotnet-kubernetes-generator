namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes ServiceAccount for use with kubectl create serviceaccount.
/// </summary>
public class ServiceAccount
{
  /// <summary>
  /// Gets or sets the metadata for the service account.
  /// </summary>
  public required ObjectMeta Metadata { get; set; }
}