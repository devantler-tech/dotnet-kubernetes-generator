using Devantler.KubernetesGenerator.Native.Workloads;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.WorkloadTests.ReplicaSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="ReplicaSetGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ReplicaSet object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidReplicaSet()
  {
    // Arrange
    var generator = new ReplicaSetGenerator();
    var model = new V1ReplicaSet
    {
      ApiVersion = "apps/v1",
      Kind = "ReplicaSet",
      Metadata = new V1ObjectMeta
      {
        Name = "replica-set",
        NamespaceProperty = "default"
      },
      Spec = new V1ReplicaSetSpec
      {
        Replicas = 1,
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "replica-set"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "replica-set"
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
    string fileName = "replica-set.yaml";
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

