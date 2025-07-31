using DevantlerTech.KubernetesGenerator.Core;

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

  /// <summary>
  /// Verifies that an ArgumentNullException is thrown when the model is null.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new CronJobGenerator();

    // Act & Assert
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
    _ = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
      await generator.GenerateAsync(null!, "test.yaml"));
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
  }

  /// <summary>
  /// Verifies that a KubernetesGeneratorException is thrown when the name is null.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNullName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = null!, // Null name
        Namespace = "default"
      },
      Spec = new CronJobSpec
      {
        Schedule = "*/1 * * * *",
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "test",
                  Image = "nginx"
                }
              ]
            }
          }
        }
      }
    };

    // Act & Assert
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(async () =>
      await generator.GenerateAsync(model, "test.yaml"));
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task

    Assert.Contains("CronJob name is required and cannot be null or empty", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that a KubernetesGeneratorException is thrown when the name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "", // Empty name
        Namespace = "default"
      },
      Spec = new CronJobSpec
      {
        Schedule = "*/1 * * * *",
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "test",
                  Image = "nginx"
                }
              ]
            }
          }
        }
      }
    };

    // Act & Assert
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(async () =>
      await generator.GenerateAsync(model, "test.yaml"));
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task

    Assert.Contains("CronJob name is required and cannot be null or empty", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that a KubernetesGeneratorException is thrown when no containers are defined.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNoContainers_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "test-cronjob",
        Namespace = "default"
      },
      Spec = new CronJobSpec
      {
        Schedule = "*/1 * * * *",
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [] // Empty containers list
            }
          }
        }
      }
    };

    // Act & Assert
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(async () =>
      await generator.GenerateAsync(model, "test.yaml"));
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task

    Assert.Contains("CronJob must have at least one container defined", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that a KubernetesGeneratorException is thrown when an invalid cron schedule is provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public void CronJobSpec_WithInvalidSchedule_ShouldThrowKubernetesGeneratorException()
  {
    // Act & Assert
    var exception = Assert.Throws<KubernetesGeneratorException>(() =>
      new CronJobSpec
      {
        Schedule = "invalid-cron", // Invalid cron format
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "test",
                  Image = "nginx"
                }
              ]
            }
          }
        }
      });

    Assert.Contains("Invalid cron schedule format", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies the generated CronJob object without namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutNamespace_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new CronJob
    {
      Metadata = new Metadata
      {
        Name = "cron-job-no-namespace"
        // No namespace specified
      },
      Spec = new CronJobSpec
      {
        Schedule = "0 1 * * *",
        JobTemplate = new JobTemplate
        {
          Template = new PodTemplate
          {
            Spec = new PodSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = "cron-job-no-namespace",
                  Image = "alpine"
                }
              ]
            }
          }
        }
      }
    };

    // Act
    string fileName = "cron-job-no-namespace.yaml";
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

