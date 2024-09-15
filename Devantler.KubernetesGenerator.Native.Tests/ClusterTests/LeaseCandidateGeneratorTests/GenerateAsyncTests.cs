using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.LeaseCandidateGeneratorTests;


/// <summary>
/// Tests for the <see cref="LeaseCandidateGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated LeaseCandidate object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidLeaseCandidate()
  {
    // Arrange
    var generator = new LeaseCandidateGenerator();
    var model = new V1alpha1LeaseCandidate
    {
      ApiVersion = "coordination.k8s.io/v1",
      Kind = "LeaseCandidate",
      Metadata = new V1ObjectMeta
      {
        Name = "lease-candidate",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha1LeaseCandidateSpec
      {
        BinaryVersion = "1.0.0",
        EmulationVersion = "1.0.0",
        LeaseName = "lease-name",
        PingTime = DateTime.UnixEpoch,
        PreferredStrategies =
        [
          "strategy1",
          "strategy2"
        ],
        RenewTime = DateTime.UnixEpoch,
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "lease-candidate.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}
