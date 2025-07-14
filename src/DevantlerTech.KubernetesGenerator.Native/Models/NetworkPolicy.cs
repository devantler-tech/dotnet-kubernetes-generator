namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a NetworkPolicy for use with kubectl create commands.
/// </summary>
public class NetworkPolicy(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the network policy.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the pod selector for the network policy.
  /// </summary>
  public NetworkPolicyPodSelector? PodSelector { get; set; }

  /// <summary>
  /// Gets or sets the policy types (Ingress, Egress, or both).
  /// </summary>
  public List<string>? PolicyTypes { get; set; }

  /// <summary>
  /// Gets or sets the ingress rules for the network policy.
  /// </summary>
  public List<NetworkPolicyIngressRule>? Ingress { get; set; }

  /// <summary>
  /// Gets or sets the egress rules for the network policy.
  /// </summary>
  public List<NetworkPolicyEgressRule>? Egress { get; set; }
}

/// <summary>
/// Represents a pod selector for network policies.
/// </summary>
public class NetworkPolicyPodSelector
{
  /// <summary>
  /// Gets or sets the match labels for the pod selector.
  /// </summary>
  public Dictionary<string, string>? MatchLabels { get; set; }

  /// <summary>
  /// Gets or sets the match expressions for the pod selector.
  /// </summary>
  public List<NetworkPolicyLabelSelectorRequirement>? MatchExpressions { get; set; }
}

/// <summary>
/// Represents a label selector requirement for network policies.
/// </summary>
public class NetworkPolicyLabelSelectorRequirement
{
  /// <summary>
  /// Gets or sets the label key.
  /// </summary>
  public string Key { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the operator (In, NotIn, Exists, DoesNotExist).
  /// </summary>
  public string Operator { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the values for the requirement.
  /// </summary>
  public List<string>? Values { get; set; }
}

/// <summary>
/// Represents an ingress rule for network policies.
/// </summary>
public class NetworkPolicyIngressRule
{
  /// <summary>
  /// Gets or sets the ports allowed by this rule.
  /// </summary>
  public List<NetworkPolicyPort>? Ports { get; set; }

  /// <summary>
  /// Gets or sets the sources that can access the selected pods.
  /// </summary>
  public List<NetworkPolicyPeer>? From { get; set; }
}

/// <summary>
/// Represents an egress rule for network policies.
/// </summary>
public class NetworkPolicyEgressRule
{
  /// <summary>
  /// Gets or sets the ports allowed by this rule.
  /// </summary>
  public List<NetworkPolicyPort>? Ports { get; set; }

  /// <summary>
  /// Gets or sets the destinations that can be accessed by the selected pods.
  /// </summary>
  public List<NetworkPolicyPeer>? To { get; set; }
}

/// <summary>
/// Represents a port specification for network policies.
/// </summary>
public class NetworkPolicyPort
{
  /// <summary>
  /// Gets or sets the port number or name.
  /// </summary>
  public string? Port { get; set; }

  /// <summary>
  /// Gets or sets the protocol (TCP, UDP, SCTP).
  /// </summary>
  public string? Protocol { get; set; }

  /// <summary>
  /// Gets or sets the end port for port ranges.
  /// </summary>
  public int? EndPort { get; set; }
}

/// <summary>
/// Represents a peer specification for network policies.
/// </summary>
public class NetworkPolicyPeer
{
  /// <summary>
  /// Gets or sets the pod selector for this peer.
  /// </summary>
  public NetworkPolicyPodSelector? PodSelector { get; set; }

  /// <summary>
  /// Gets or sets the namespace selector for this peer.
  /// </summary>
  public NetworkPolicyPodSelector? NamespaceSelector { get; set; }

  /// <summary>
  /// Gets or sets the IP block for this peer.
  /// </summary>
  public NetworkPolicyIPBlock? IPBlock { get; set; }
}

/// <summary>
/// Represents an IP block specification for network policies.
/// </summary>
public class NetworkPolicyIPBlock
{
  /// <summary>
  /// Gets or sets the CIDR block.
  /// </summary>
  public string CIDR { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the list of CIDR blocks to exclude from the main CIDR block.
  /// </summary>
  public List<string>? Except { get; set; }
}