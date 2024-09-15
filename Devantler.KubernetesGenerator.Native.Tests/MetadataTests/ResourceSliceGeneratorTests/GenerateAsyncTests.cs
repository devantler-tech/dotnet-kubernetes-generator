using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.ResourceSliceGeneratorTests;


/// <summary>
/// Tests for the <see cref="ResourceSliceGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ResourceSlice object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidResourceSlice()
  {
    // Arrange
    var generator = new ResourceSliceGenerator();
    var model = new V1alpha3ResourceSlice
    {
      ApiVersion = "resource.k8s.io/v1alpha3",
      Kind = "ResourceSlice",
      Metadata = new V1ObjectMeta
      {
        Name = "resource-slice",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha3ResourceSliceSpec
      {
        AllNodes = true,
        Devices = [
          new V1alpha3Device {
            Name = "device",
            Basic = new V1alpha3BasicDevice
            {
              Attributes = new Dictionary<string, V1alpha3DeviceAttribute>
              {
                { "key", new V1alpha3DeviceAttribute
                  {
                    BoolValue = true,
                    StringValue = "string",
                    IntValue = 1,
                  }
                }
              }
            }
          }
        ]
      }
    };

    // Act
    string fileName = "resource-slice.yaml";
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

