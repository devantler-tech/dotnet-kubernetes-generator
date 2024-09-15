using Devantler.KubernetesGenerator.Native.Workloads;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.WorkloadTests.ReplicationControllerGeneratorTests;


/// <summary>
/// Tests for the <see cref="ReplicationControllerGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ReplicationController object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidReplicationController()
  {
    // Arrange
    var generator = new ReplicationControllerGenerator();
    var model = new V1ReplicationController
    {
      ApiVersion = "v1",
      Kind = "ReplicationController",
      Metadata = new V1ObjectMeta
      {
        Name = "replication-controller",
        NamespaceProperty = "default"
      },
      Spec = new V1ReplicationControllerSpec
      {
        Replicas = 1,
        MinReadySeconds = 1,
        Selector = new Dictionary<string, string>
        {
          ["app"] = "replication-controller"
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "replication-controller"
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
    string outputPath = Path.Combine(Path.GetTempPath(), "replication-controller.yaml");
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

