using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.JobGeneratorTests;

/// <summary>
/// Tests for the <see cref="JobGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Job object with image.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageAndCommand_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new Metadata
      {
        Name = "job",
        Namespace = "default"
      },
      Spec = new JobSpec
      {
        Image = "busybox",
        Command = ["echo", "hello"]
      }
    };

    // Act
    string fileName = "job.yaml";
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
  /// Verifies the generated Job object with image only.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageOnly_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new Metadata
      {
        Name = "simple-job",
        Namespace = "default"
      },
      Spec = new JobSpec
      {
        Image = "nginx"
      }
    };

    // Act
    string fileName = "simple-job.yaml";
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
  /// Verifies the generated Job object from a cronjob.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_FromCronJob_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new Metadata
      {
        Name = "from-cronjob",
        Namespace = "default"
      },
      Spec = new JobSpec
      {
        Image = "", // Not used when creating from cronjob
        From = "cronjob/my-cronjob"
      }
    };

    // Act
    string fileName = "from-cronjob.yaml";
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
  /// Verifies that exception is thrown when job name is not provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutJobName_ShouldThrowException()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new Metadata
      {
        Name = "", // Empty name
        Namespace = "default"
      },
      Spec = new JobSpec
      {
        Image = "nginx"
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "test.yaml");
    await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, outputPath));
  }

  /// <summary>
  /// Verifies that exception is thrown when image is not provided and not creating from cronjob.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutImageAndNotFromCronJob_ShouldThrowException()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new Metadata
      {
        Name = "test-job",
        Namespace = "default"
      },
      Spec = new JobSpec
      {
        Image = "" // Empty image and no From
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "test.yaml");
    await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, outputPath));
  }
}

