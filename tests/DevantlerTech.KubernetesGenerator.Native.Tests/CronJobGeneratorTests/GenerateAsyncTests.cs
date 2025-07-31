using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.CronJobGeneratorTests;

/// <summary>
/// Tests for the <see cref="CronJobGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeCronJob object with image and schedule.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageAndSchedule_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "cron-job",
        Namespace = "default"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "*/1 * * * *", // Every minute
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
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
  /// Verifies the generated NativeCronJob object with image, schedule, and command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageScheduleAndCommand_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "cron-job-with-command",
        Namespace = "default"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "0 0 * * *", // Daily at midnight
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
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
  /// Verifies the generated NativeCronJob object with restart policy.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithRestartPolicy_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "cron-job-with-restart",
        Namespace = "default"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "*/5 * * * *",
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
                {
                  Name = "cron-job-with-restart",
                  Image = "alpine"
                }
              ],
              RestartPolicy = NativePodRestartPolicy.OnFailure
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
  /// Verifies the generated NativeCronJob object with all properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllProperties_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "cron-job-complete",
        Namespace = "production"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "0 2 * * *", // Daily at 2 AM
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
                {
                  Name = "cron-job-complete",
                  Image = "nginx:latest",
                  Command = ["sh", "-c", "echo 'Running daily backup'"]
                }
              ],
              RestartPolicy = NativePodRestartPolicy.Never
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
  /// Verifies the generated NativeCronJob object using the hierarchical API directly.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithHierarchicalStructure_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "cron-job-hierarchical",
        Namespace = "test"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "30 3 * * *", // 3:30 AM daily
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
                {
                  Name = "backup-container",
                  Image = "backup:v1.2.3",
                  Command = ["backup", "--full"]
                }
              ],
              RestartPolicy = NativePodRestartPolicy.OnFailure
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
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = null!, // Null name
        Namespace = "default"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "*/1 * * * *",
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
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

    Assert.Contains("NativeCronJob name is required and cannot be null or empty", exception.Message, StringComparison.Ordinal);
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
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "", // Empty name
        Namespace = "default"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "*/1 * * * *",
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
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

    Assert.Contains("NativeCronJob name is required and cannot be null or empty", exception.Message, StringComparison.Ordinal);
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
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "test-cronjob",
        Namespace = "default"
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "*/1 * * * *",
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
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

    Assert.Contains("NativeCronJob must have at least one container defined", exception.Message, StringComparison.Ordinal);
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
      new NativeCronJobSpec
      {
        Schedule = "invalid-cron", // Invalid cron format
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
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
  /// Verifies the generated NativeCronJob object without namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutNamespace_ShouldGenerateAValidCronJob()
  {
    // Arrange
    var generator = new CronJobGenerator();
    var model = new NativeCronJob
    {
      Metadata = new NativeMetadata
      {
        Name = "cron-job-no-namespace"
        // No namespace specified
      },
      Spec = new NativeCronJobSpec
      {
        Schedule = "0 1 * * *",
        JobTemplate = new NativeJobTemplate
        {
          Template = new NativePodTemplate
          {
            Spec = new NativePodSpec
            {
              Containers = [
                new NativePodContainer
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

