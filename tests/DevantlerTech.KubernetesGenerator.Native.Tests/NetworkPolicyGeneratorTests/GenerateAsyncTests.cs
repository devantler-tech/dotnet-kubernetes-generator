using System.Net;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.NetworkPolicyGeneratorTests;


/// <summary>
/// Tests for the <see cref="NetworkPolicyGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeNetworkPolicy object with comprehensive features.
  /// Tests ingress and egress rules, multiple ports, pod selectors, policy types, and EndPort ranges.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithComprehensiveConfiguration_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NativeNetworkPolicy
    {
      Metadata = new NamespacedMetadata
      {
        Name = "comprehensive-policy",
        Namespace = "production"
      },
      Spec = new NativeNetworkPolicySpec
      {
        PodSelector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["tier"] = "backend"
          }
        },
        PolicyTypes = [NativeNetworkPolicyType.Ingress, NativeNetworkPolicyType.Egress],
        Ingress =
        [
          new NativeNetworkPolicyIngressRule
          {
            Ports =
            [
              new NativeNetworkPolicyPort
              {
                Port = "8080",
                Protocol = NativeNetworkPolicyProtocol.TCP
              },
              new NativeNetworkPolicyPort
              {
                Port = "9090",
                Protocol = NativeNetworkPolicyProtocol.TCP,
                EndPort = 9099
              }
            ],
            From =
            [
              new NativeNetworkPolicyPeer
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
          new NativeNetworkPolicyEgressRule
          {
            Ports =
            [
              new NativeNetworkPolicyPort
              {
                Port = "80",
                Protocol = NativeNetworkPolicyProtocol.TCP
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
  /// Verifies the generated NativeNetworkPolicy object with IP block restrictions.
  /// Tests distinct IPNetwork functionality for CIDR-based network access control.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithIPBlockRestrictions_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NativeNetworkPolicy
    {
      Metadata = new NamespacedMetadata
      {
        Name = "ip-block-policy",
        Namespace = "secure"
      },
      Spec = new NativeNetworkPolicySpec
      {
        PodSelector = new LabelSelector(), // Empty selector selects all pods
        PolicyTypes = [NativeNetworkPolicyType.Ingress, NativeNetworkPolicyType.Egress],
        Ingress =
        [
          new NativeNetworkPolicyIngressRule
          {
            From =
            [
              new NativeNetworkPolicyPeer
              {
                IPBlock = new NativeNetworkPolicyIPBlock
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
          new NativeNetworkPolicyEgressRule
          {
            To =
            [
              new NativeNetworkPolicyPeer
              {
                IPBlock = new NativeNetworkPolicyIPBlock
                {
                  CIDR = IPNetwork.Parse("192.168.0.0/16")
                }
              }
            ],
            Ports =
            [
              new NativeNetworkPolicyPort
              {
                Port = "53",
                Protocol = NativeNetworkPolicyProtocol.UDP
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
  /// Verifies the generated NativeNetworkPolicy object with namespace selector.
  /// Tests distinct cross-namespace policy functionality for allowing traffic between namespaces.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNamespaceSelector_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NativeNetworkPolicy
    {
      Metadata = new NamespacedMetadata
      {
        Name = "namespace-selector-policy",
        Namespace = "staging"
      },
      Spec = new NativeNetworkPolicySpec
      {
        PodSelector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "api"
          }
        },
        PolicyTypes = [NativeNetworkPolicyType.Ingress],
        Ingress =
        [
          new NativeNetworkPolicyIngressRule
          {
            From =
            [
              new NativeNetworkPolicyPeer
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
              new NativeNetworkPolicyPort
              {
                Port = "8080",
                Protocol = NativeNetworkPolicyProtocol.TCP
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the NativeNetworkPolicy name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NativeNetworkPolicy
    {
      Metadata = new NamespacedMetadata
      {
        Name = ""
      },
      Spec = new NativeNetworkPolicySpec
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
