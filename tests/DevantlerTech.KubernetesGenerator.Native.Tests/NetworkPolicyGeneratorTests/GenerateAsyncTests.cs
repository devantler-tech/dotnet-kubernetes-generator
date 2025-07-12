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
    var model = new V1NetworkPolicy
    {
      ApiVersion = "v1",
      Kind = "NetworkPolicy",
      Metadata = new V1ObjectMeta
      {
        Name = "network-policy",
        NamespaceProperty = "default"
      },
      Spec = new V1NetworkPolicySpec
      {
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
        PolicyTypes = ["Ingress", "Egress"],
        PodSelector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "nginx"
          }
        }
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
