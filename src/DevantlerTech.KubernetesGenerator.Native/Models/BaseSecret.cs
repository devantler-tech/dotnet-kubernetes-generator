namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Base class for Kubernetes secret models.
/// </summary>
public abstract class BaseSecret
{
  string _name = string.Empty;

  /// <summary>
  /// Gets or sets the name of the secret.
  /// </summary>
  public required string Name
  {
    get => _name;
    init
    {
      _name = value;
      Metadata = new() { Name = value };
    }
  }

  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = string.Empty };
}
