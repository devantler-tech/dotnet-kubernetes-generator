using Devantler.KubernetesGenerator.Native.Workloads;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.WorkloadTests.StatefulSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="StatefulSetGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated StatefulSet object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidStatefulSet()
  {
    // Arrange
    var generator = new StatefulSetGenerator();
    var model = new V1StatefulSet
    {
      ApiVersion = "apps/v1",
      Kind = "StatefulSet",
      Metadata = new V1ObjectMeta
      {
        Name = "stateful-set",
        NamespaceProperty = "default"
      },
      Spec = new V1StatefulSetSpec
      {
        Replicas = 1,
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "stateful-set"
          }
        },
        ServiceName = "stateful-set",
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "stateful-set"
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
    string fileName = "stateful-set.yaml";
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

