using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.LeaseGeneratorTests;


/// <summary>
/// Tests for the <see cref="LeaseGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Lease object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidLease()
  {
    // Arrange
    var generator = new LeaseGenerator();
    var model = new V1Lease
    {
      ApiVersion = "v1",
      Kind = "Lease",
      Metadata = new V1ObjectMeta
      {
        Name = "lease",
        NamespaceProperty = "default"
      },
      Spec = new V1LeaseSpec
      {
        HolderIdentity = "holder",
        LeaseDurationSeconds = 10,
        // use a date that does not change
        AcquireTime = DateTime.UnixEpoch,
        RenewTime = DateTime.UnixEpoch,
        LeaseTransitions = 1
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "lease.yaml");
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
