using Devantler.KubernetesGenerator.Native.Workloads;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.WorkloadTests.DaemonSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="DaemonSetGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
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
    var model = new V1DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new V1ObjectMeta
      {
        Name = "daemon-set",
        NamespaceProperty = "default"
      },
      Spec = new V1DaemonSetSpec
      {
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set"
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
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "daemon-set.yaml");
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

