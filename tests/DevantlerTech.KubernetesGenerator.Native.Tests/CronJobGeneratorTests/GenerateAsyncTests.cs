using DevantlerTech.KubernetesGenerator.Core;
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

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithCronJobWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new CronJobGenerator();

    var model = new V1CronJob
    {
      ApiVersion = "batch/v1",
      Kind = "CronJob",
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
                    Image = "nginx"
                  }
                ]
              }
            }
          }
        }
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
    Assert.Contains("model.Metadata.Name must be set", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a schedule set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithCronJobWithoutSchedule_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new CronJobGenerator();

    var model = new V1CronJob
    {
      ApiVersion = "batch/v1",
      Kind = "CronJob",
      Metadata = new V1ObjectMeta
      {
        Name = "test-cronjob"
      },
      Spec = new V1CronJobSpec
      {
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
                    Image = "nginx"
                  }
                ]
              }
            }
          }
        }
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
    Assert.Contains("model.Spec.Schedule must be set", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have containers.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithCronJobWithoutContainers_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new CronJobGenerator();

    var model = new V1CronJob
    {
      ApiVersion = "batch/v1",
      Kind = "CronJob",
      Metadata = new V1ObjectMeta
      {
        Name = "test-cronjob"
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
                Containers = []
              }
            }
          }
        }
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
    Assert.Contains("model.Spec.JobTemplate.Spec.Template.Spec.Containers must contain at least one container", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the container does not have an image.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithCronJobWithoutImage_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new CronJobGenerator();

    var model = new V1CronJob
    {
      ApiVersion = "batch/v1",
      Kind = "CronJob",
      Metadata = new V1ObjectMeta
      {
        Name = "test-cronjob"
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
                    Name = "container"
                  }
                ]
              }
            }
          }
        }
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
    Assert.Contains("first container in model.Spec.JobTemplate.Spec.Template.Spec.Containers must have an image", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies the generated CronJob object without commands.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithCronJobWithoutCommand_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new V1CronJob
    {
      ApiVersion = "batch/v1",
      Kind = "CronJob",
      Metadata = new V1ObjectMeta
      {
        Name = "simple-cron-job",
        NamespaceProperty = "default"
      },
      Spec = new V1CronJobSpec
      {
        Schedule = "0 2 * * *",
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
                    Image = "busybox"
                  }
                ]
              }
            }
          }
        }
      }
    };

    // Act
    string fileName = "simple-cron-job.yaml";
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

