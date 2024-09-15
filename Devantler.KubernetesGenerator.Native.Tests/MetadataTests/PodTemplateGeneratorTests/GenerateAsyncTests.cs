using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.PodTemplateGeneratorTests;


/// <summary>
/// Tests for the <see cref="PodTemplateGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PodTemplate object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPodTemplate()
  {
    // Arrange
    var generator = new PodTemplateGenerator();
    var model = new V1PodTemplate
    {
      ApiVersion = "v1",
      Kind = "PodTemplate",
      Metadata = new V1ObjectMeta
      {
        Name = "pod-template",
        NamespaceProperty = "default"
      },
      Template = new V1PodTemplateSpec
      {
        Metadata = new V1ObjectMeta
        {
          Labels = new Dictionary<string, string>
          {
            ["app"] = "pod-template"
          }
        },
        Spec = new V1PodSpec
        {
          Containers =
          [
            new V1Container
            {
              Name = "container",
              Image = "nginx",
              Command = ["echo", "hello"]
            }
          ]
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "pod-template.yaml");
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

