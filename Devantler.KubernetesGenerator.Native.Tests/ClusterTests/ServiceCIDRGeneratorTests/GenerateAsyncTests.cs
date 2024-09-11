using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.ServiceCIDRGeneratorTests;


/// <summary>
/// Tests for the <see cref="ServiceCIDRGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ServiceCIDR object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidServiceCIDR()
  {
    // Arrange
    var generator = new ServiceCIDRGenerator();
    var model = new V1beta1ServiceCIDR
    {
      ApiVersion = "v1beta1",
      Kind = "ServiceCIDR",
      Metadata = new V1ObjectMeta
      {
        Name = "service-cidr",
        NamespaceProperty = "default"
      },
      Spec = new V1beta1ServiceCIDRSpec
      {
        Cidrs = ["cidr"]
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "service-cidr.yaml");
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
