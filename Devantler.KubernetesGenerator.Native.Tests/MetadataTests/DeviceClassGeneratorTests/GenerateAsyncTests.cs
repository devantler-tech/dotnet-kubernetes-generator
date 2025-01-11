using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.DeviceClassGeneratorTests;


/// <summary>
/// Tests for the <see cref="DeviceClassGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated DeviceClass object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidDeviceClass()
  {
    // Arrange
    var generator = new DeviceClassGenerator();
    var model = new V1alpha3DeviceClass
    {
      ApiVersion = "resource.k8s.io/v1alpha3",
      Kind = "DeviceClass",
      Metadata = new V1ObjectMeta
      {
        Name = "device-class",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha3DeviceClassSpec
      {
        Config = [
          new V1alpha3DeviceClassConfiguration
          {
            Opaque = new V1alpha3OpaqueDeviceConfiguration
            {
              Driver = "driver",
              Parameters = new Dictionary<string, string>
              {
                { "key", "value" }
              },
            }
          }
        ],
        SuitableNodes = new V1NodeSelector
        {
          NodeSelectorTerms =
          [
            new V1NodeSelectorTerm
            {
              MatchExpressions =
              [
                new V1NodeSelectorRequirement
                {
                  Key = "key",
                  OperatorProperty = "operator",
                  Values =
                  [
                    "value"
                  ]
                }
              ]
            }
          ]
        }
      }
    };

    // Act
    string fileName = "device-class.yaml";
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
