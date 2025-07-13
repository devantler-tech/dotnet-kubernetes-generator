using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.NetworkPolicyGeneratorTests;


/// <summary>
/// Tests for the <see cref="NetworkPolicyGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NetworkPolicy object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NetworkPolicy
    {
      Metadata = new V1ObjectMeta
      {
        Name = "network-policy",
        NamespaceProperty = "default"
      },
      PodSelector = new V1LabelSelector
      {
        MatchLabels = new Dictionary<string, string>
        {
          ["app"] = "nginx"
        }
      },
      Ingress =
      [
        new V1NetworkPolicyIngressRule
        {
          Ports =
          [
            new V1NetworkPolicyPort
            {
              Port = new IntstrIntOrString("80")
            }
          ]
        }
      ],
      Egress =
      [
        new V1NetworkPolicyEgressRule
        {
          Ports =
          [
            new V1NetworkPolicyPort
            {
              Port = new IntstrIntOrString("80")
            }
          ]
        }
      ],
      PolicyTypes = ["Ingress", "Egress"]
    };

    // Act
    string fileName = "network-policy.yaml";
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
  /// Verifies the generated NetworkPolicy object with minimal required fields.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalRequiredFields_ShouldGenerateAValidNetworkPolicy()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();
    var model = new NetworkPolicy
    {
      Metadata = new V1ObjectMeta
      {
        Name = "network-policy-minimal"
      },
      PodSelector = new V1LabelSelector
      {
        MatchLabels = new Dictionary<string, string>
        {
          ["app"] = "nginx"
        }
      }
    };

    // Act
    string fileName = "network-policy-minimal.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the NetworkPolicy model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNetworkPolicyWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new NetworkPolicyGenerator();

    var model = new NetworkPolicy
    {
      Metadata = new V1ObjectMeta
      {
        NamespaceProperty = "default"
      },
      PodSelector = new V1LabelSelector
      {
        MatchLabels = new Dictionary<string, string>
        {
          ["app"] = "nginx"
        }
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
