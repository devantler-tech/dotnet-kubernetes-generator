using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DaemonSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="DaemonSetGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated DaemonSet object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new Metadata
      {
        Name = "daemon-set",
        Namespace = "default"
      },
      Spec = new DaemonSetSpec
      {
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set"
          }
        },
        Template = new PodTemplateSpec
        {
          Metadata = new PodTemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set"
            }
          },
          Spec = new PodSpec
          {
            Containers =
            [
              new PodContainer
              {
                Name = "container",
                Image = "nginx",
                Command = ["echo", "hello"]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "daemon-set.yaml";
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

