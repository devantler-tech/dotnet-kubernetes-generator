using System.Net;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.NetworkPolicyGeneratorTests;


/// <summary>
/// Tests for the <see cref="NetworkPolicyGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NetworkPolicy object with comprehensive features.
  /// Tests ingress and egress rules, multiple ports, pod selectors, policy types, and EndPort ranges.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithComprehensiveConfiguration_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NetworkPolicy
    {
      Metadata = new Metadata
      {
        Name = "comprehensive-policy",
        Namespace = "production"
      },
      Spec = new NetworkPolicySpec
      {
        PodSelector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["tier"] = "backend"
          }
        },
        PolicyTypes = [NetworkPolicyType.Ingress, NetworkPolicyType.Egress],
        Ingress =
        [
          new NetworkPolicyIngressRule
          {
            Ports =
            [
              new NetworkPolicyPort
              {
                Port = "8080",
                Protocol = NetworkPolicyProtocol.TCP
              },
              new NetworkPolicyPort
              {
                Port = "9090",
                Protocol = NetworkPolicyProtocol.TCP,
                EndPort = 9099
              }
            ],
            From =
            [
              new NetworkPolicyPeer
              {
                PodSelector = new LabelSelector
                {
                  MatchLabels = new Dictionary<string, string>
                  {
                    ["tier"] = "frontend"
                  }
                }
              }
            ]
          }
        ],
        Egress =
        [
          new NetworkPolicyEgressRule
          {
            Ports =
            [
              new NetworkPolicyPort
              {
                Port = "80",
                Protocol = NetworkPolicyProtocol.TCP
              }
            ]
          }
        ]
      }
    };

    // Act
    string fileName = "comprehensive-network-policy.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated NetworkPolicy object with IP block restrictions.
  /// Tests distinct IPNetwork functionality for CIDR-based network access control.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithIPBlockRestrictions_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NetworkPolicy
    {
      Metadata = new Metadata
      {
        Name = "ip-block-policy",
        Namespace = "secure"
      },
      Spec = new NetworkPolicySpec
      {
        PodSelector = new LabelSelector(), // Empty selector selects all pods
        PolicyTypes = [NetworkPolicyType.Ingress, NetworkPolicyType.Egress],
        Ingress =
        [
          new NetworkPolicyIngressRule
          {
            From =
            [
              new NetworkPolicyPeer
              {
                IPBlock = new NetworkPolicyIPBlock
                {
                  CIDR = IPNetwork.Parse("10.0.0.0/8"),
                  Except = ["10.0.1.0/24", "10.0.2.0/24"]
                }
              }
            ]
          }
        ],
        Egress =
        [
          new NetworkPolicyEgressRule
          {
            To =
            [
              new NetworkPolicyPeer
              {
                IPBlock = new NetworkPolicyIPBlock
                {
                  CIDR = IPNetwork.Parse("192.168.0.0/16")
                }
              }
            ],
            Ports =
            [
              new NetworkPolicyPort
              {
                Port = "53",
                Protocol = NetworkPolicyProtocol.UDP
              }
            ]
          }
        ]
      }
    };

    // Act
    string fileName = "ip-block-policy.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated NetworkPolicy object with namespace selector.
  /// Tests distinct cross-namespace policy functionality for allowing traffic between namespaces.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNamespaceSelector_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NetworkPolicy
    {
      Metadata = new Metadata
      {
        Name = "namespace-selector-policy",
        Namespace = "staging"
      },
      Spec = new NetworkPolicySpec
      {
        PodSelector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "api"
          }
        },
        PolicyTypes = [NetworkPolicyType.Ingress],
        Ingress =
        [
          new NetworkPolicyIngressRule
          {
            From =
            [
              new NetworkPolicyPeer
              {
                NamespaceSelector = new LabelSelector
                {
                  MatchLabels = new Dictionary<string, string>
                  {
                    ["environment"] = "production"
                  }
                },
                PodSelector = new LabelSelector
                {
                  MatchLabels = new Dictionary<string, string>
                  {
                    ["role"] = "client"
                  }
                }
              }
            ],
            Ports =
            [
              new NetworkPolicyPort
              {
                Port = "8080",
                Protocol = NetworkPolicyProtocol.TCP
              }
            ]
          }
        ]
      }
    };

    // Act
    string fileName = "namespace-selector-policy.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the NetworkPolicy name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NetworkPolicy
    {
      Metadata = new Metadata
      {
        Name = ""
      },
      Spec = new NetworkPolicySpec
      {
        PodSelector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "webapp"
          }
        }
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
