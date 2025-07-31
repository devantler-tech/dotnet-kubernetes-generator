using System.Net;

namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Represents an IP block for NetworkPolicy rules.
/// </summary>
public class NativeNetworkPolicyIPBlock
{
  /// <summary>
  /// Gets or sets the CIDR block for the IP range.
  /// </summary>
  public required IPNetwork CIDR { get; init; }

  /// <summary>
  /// Gets or sets the list of IP addresses or CIDR blocks to exclude from the CIDR block.
  /// </summary>
  public IReadOnlyList<string>? Except { get; init; }
}
