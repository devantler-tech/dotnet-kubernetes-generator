using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.CronJobGeneratorTests;

/// <summary>
/// Tests for the <see cref="CronJobGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated CronJob object with image and schedule.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageAndSchedule_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "cron-job",
        Namespace = "default"
      },
      Spec = new CronJobSpec
      {
        Schedule = "*/1 * * * *", // Every minute
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "cron-job",
                  Image = "nginx"
                }
              ]
            }
          }
        }
      }
    };

    // Act
    string fileName = "cron-job-basic.yaml";
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
  /// Verifies the generated CronJob object with image, schedule, and command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageScheduleAndCommand_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "cron-job-with-command",
        Namespace = "default"
      },
      Spec = new CronJobSpec
      {
        Schedule = "0 0 * * *", // Daily at midnight
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "cron-job-with-command",
                  Image = "busybox",
                  Command = ["echo", "hello", "world"]
                }
              ]
            }
          }
        }
      }
    };

    // Act
    string fileName = "cron-job-with-command.yaml";
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
  /// Verifies the generated CronJob object with restart policy.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithRestartPolicy_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "cron-job-with-restart",
        Namespace = "default"
      },
      Spec = new CronJobSpec
      {
        Schedule = "*/5 * * * *",
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "cron-job-with-restart",
                  Image = "alpine"
                }
              ],
              RestartPolicy = PodRestartPolicy.OnFailure
            }
          }
        }
      }
    };

    // Act
    string fileName = "cron-job-with-restart-policy.yaml";
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
  /// Verifies the generated CronJob object with all properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllProperties_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "cron-job-complete",
        Namespace = "production"
      },
      Spec = new CronJobSpec
      {
        Schedule = "0 2 * * *", // Daily at 2 AM
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "cron-job-complete",
                  Image = "nginx:latest",
                  Command = ["sh", "-c", "echo 'Running daily backup'"]
                }
              ],
              RestartPolicy = PodRestartPolicy.Never
            }
          }
        }
      }
    };

    // Act
    string fileName = "cron-job-complete.yaml";
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
  /// Verifies the generated CronJob object using the hierarchical API directly.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithHierarchicalStructure_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "cron-job-hierarchical",
        Namespace = "test"
      },
      Spec = new CronJobSpec
      {
        Schedule = "30 3 * * *", // 3:30 AM daily
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "backup-container",
                  Image = "backup:v1.2.3",
                  Command = ["backup", "--full"]
                }
              ],
              RestartPolicy = PodRestartPolicy.OnFailure
            }
          }
        }
      }
    };

    // Act
    string fileName = "cron-job-hierarchical.yaml";
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

