using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.PriorityLevelConfigurationGeneratorTests;


/// <summary>
/// Tests for the <see cref="PriorityLevelConfigurationGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PriorityLevelConfiguration object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPriorityLevelConfiguration()
  {
    // Arrange
    var generator = new PriorityLevelConfigurationGenerator();
    var model = new V1PriorityLevelConfiguration
    {
      ApiVersion = "flowcontrol.apiserver.k8s.io/v1",
      Kind = "PriorityLevelConfiguration",
      Metadata = new V1ObjectMeta
      {
        Name = "priority-level-configuration",
        NamespaceProperty = "default"
      },
      Spec = new V1PriorityLevelConfigurationSpec
      {
        Limited = new V1LimitedPriorityLevelConfiguration
        {
          BorrowingLimitPercent = 1,
          LendablePercent = 1,
          NominalConcurrencyShares = 1,
          LimitResponse = new V1LimitResponse
          {
            Type = "Reject"
          },
        },
        Type = "Limited",
        Exempt = new V1ExemptPriorityLevelConfiguration
        {
          LendablePercent = 1,
          NominalConcurrencyShares = 1
        }
      }
    };

    // Act
    string fileName = "priority-level-configuration.yaml";
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

