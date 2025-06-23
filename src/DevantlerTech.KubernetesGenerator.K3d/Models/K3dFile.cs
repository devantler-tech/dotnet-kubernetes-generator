namespace DevantlerTech.KubernetesGenerator.K3d.Models;

/// <summary>
/// A file that is mounted onto nodes.
/// </summary>
public class K3dFile
{
  /// <summary>
  /// Description of the file.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// Source of the file. Either a file path or the content of the file.
  /// </summary>
  public required string Source { get; set; }

  /// <summary>
  /// The path where the file should be mounted.
  /// </summary>
  public required string Destination { get; set; }

  /// <summary>
  /// Node filters for the file. (e.g. "server:*" or "agent:0")
  /// </summary>
  public IEnumerable<string>? NodeFilters { get; set; }
}
