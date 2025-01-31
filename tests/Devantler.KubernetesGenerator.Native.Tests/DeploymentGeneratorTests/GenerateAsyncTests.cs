using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.DeploymentGeneratorTests;


/// <summary>
/// Tests for the <see cref="DeploymentGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Deployment object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new V1Deployment
    {
      ApiVersion = "apps/v1",
      Kind = "Deployment",
      Metadata = new V1ObjectMeta
      {
        Name = "deployment",
        NamespaceProperty = "default"
      },
      Spec = new V1DeploymentSpec
      {
        Replicas = 1,
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "deployment"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "deployment"
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
    string fileName = "deployment.yaml";
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

