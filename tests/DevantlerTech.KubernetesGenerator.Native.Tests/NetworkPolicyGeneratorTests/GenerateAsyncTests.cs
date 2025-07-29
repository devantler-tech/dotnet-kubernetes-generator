using DevantlerTech.KubernetesGenerator.Native.Models;

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
      Metadata = new Metadata
      {
        Name = "network-policy",
        Namespace = "default"
      },
      Spec = new NetworkPolicySpec
      {
        PodSelector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "nginx"
          }
        },
        PolicyTypes = [NetworkPolicyType.Ingress, NetworkPolicyType.Egress],
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
        ],
        Ingress =
        [
          new NetworkPolicyIngressRule
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
}
