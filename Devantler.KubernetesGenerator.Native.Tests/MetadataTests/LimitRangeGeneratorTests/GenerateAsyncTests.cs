using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.LimitRangeGeneratorTests;


/// <summary>
/// Tests for the <see cref="LimitRangeGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated LimitRange object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidLimitRange()
  {
    // Arrange
    var generator = new LimitRangeGenerator();
    var model = new V1LimitRange
    {
      ApiVersion = "v1",
      Kind = "LimitRange",
      Metadata = new V1ObjectMeta
      {
        Name = "limit-range",
        NamespaceProperty = "default"
      },
      Spec = new V1LimitRangeSpec
      {
        Limits =
        [
          new V1LimitRangeItem
          {
            Type = "Pod",
            Max = new Dictionary<string, ResourceQuantity>
            {
              ["cpu"] = new ResourceQuantity("1"),
              ["memory"] = new ResourceQuantity("1Gi")
            },
            Min = new Dictionary<string, ResourceQuantity>
            {
              ["cpu"] = new ResourceQuantity("100m"),
              ["memory"] = new ResourceQuantity("100Mi")
            },
            DefaultRequest = new Dictionary<string, ResourceQuantity>
            {
              ["cpu"] = new ResourceQuantity("100m"),
              ["memory"] = new ResourceQuantity("100Mi")
            },
            MaxLimitRequestRatio = new Dictionary<string, ResourceQuantity>
            {
              ["cpu"] = new ResourceQuantity("10"),
              ["memory"] = new ResourceQuantity("10")
            },
            DefaultProperty = new Dictionary<string, ResourceQuantity>
            {
              ["cpu"] = new ResourceQuantity("100m"),
              ["memory"] = new ResourceQuantity("100Mi")
            }
          }
        ]
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "limit-range.yaml");
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
