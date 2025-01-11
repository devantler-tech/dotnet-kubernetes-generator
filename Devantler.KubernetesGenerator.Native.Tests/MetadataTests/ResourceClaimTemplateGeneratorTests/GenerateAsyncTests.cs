using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.ResourceClaimTemplateGeneratorTests;


/// <summary>
/// Tests for the <see cref="ResourceClaimTemplateGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ResourceClaimTemplate object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidResourceClaimTemplate()
  {
    // Arrange
    var generator = new ResourceClaimTemplateGenerator();
    var model = new V1alpha3ResourceClaimTemplate
    {
      ApiVersion = "resource.k8s.io/v1alpha3",
      Kind = "ResourceClaimTemplate",
      Metadata = new V1ObjectMeta
      {
        Name = "resource-claim-template",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha3ResourceClaimTemplateSpec
      {
        Metadata = new V1ObjectMeta
        {
          Name = "resource-claim",
          NamespaceProperty = "default"
        },
        Spec = new V1alpha3ResourceClaimSpec
        {
          Controller = "controller",
          Devices = new V1alpha3DeviceClaim
          {
            Config = [
              new V1alpha3DeviceClaimConfiguration
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
            Constraints = [
              new V1alpha3DeviceConstraint
              {
                MatchAttribute = "match-attribute",
                Requests = [
                  "request"
                ]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "resource-claim-template.yaml";
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

