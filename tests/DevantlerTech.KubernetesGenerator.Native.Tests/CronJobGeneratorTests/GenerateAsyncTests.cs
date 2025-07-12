using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.CronJobGeneratorTests;


/// <summary>
/// Tests for the <see cref="CronJobGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated CronJob object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new V1CronJob
    {
      ApiVersion = "batch/v1",
      Kind = "CronJob",
      Metadata = new V1ObjectMeta
      {
        Name = "cron-job",
        NamespaceProperty = "default"
      },
      Spec = new V1CronJobSpec
      {
        Schedule = "*/1 * * * *",
        JobTemplate = new V1JobTemplateSpec
        {
          Spec = new V1JobSpec
          {
            Template = new V1PodTemplateSpec
            {
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
        }
      }
    };

    // Act
    string fileName = "cron-job.yaml";
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

