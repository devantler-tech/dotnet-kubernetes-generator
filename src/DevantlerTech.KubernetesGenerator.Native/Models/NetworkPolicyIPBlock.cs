namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents an IP block for NetworkPolicy rules.
/// </summary>
public class NetworkPolicyIPBlock
{
  /// <summary>
  /// Gets or sets the CIDR block for the IP range.
  /// </summary>
  public required string CIDR { get; init; }

  /// <summary>
  /// Gets or sets the list of IP addresses or CIDR blocks to exclude from the CIDR block.
  /// </summary>
  public IList<string>? Except { get; init; }
}