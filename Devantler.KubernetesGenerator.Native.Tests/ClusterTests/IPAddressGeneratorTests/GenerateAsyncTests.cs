using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.IPAddressGeneratorTests;


/// <summary>
/// Tests for the <see cref="IPAddressGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated IPAddress object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidIPAddress()
  {
    // Arrange
    var generator = new IPAddressGenerator();
    var model = new V1beta1IPAddress
    {
      ApiVersion = "networking.k8s.io/v1beta1",
      Kind = "IPAddress",
      Metadata = new V1ObjectMeta
      {
        Name = "ip-address",
        NamespaceProperty = "default"
      },
      Spec = new V1beta1IPAddressSpec
      {
        ParentRef = new V1beta1ParentReference
        {
          Name = "pod",
          NamespaceProperty = "default",
          Group = "group",
          Resource = "resource",
        },
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "ip-address.yaml");
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
