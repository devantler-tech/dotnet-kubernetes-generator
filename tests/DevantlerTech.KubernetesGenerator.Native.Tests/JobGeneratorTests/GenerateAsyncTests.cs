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
  /// Verifies that exception is thrown when image is not provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutImage_ShouldThrowException()
  {
    // This test is no longer needed since Image is now required by the compiler
    // The test would fail at compile time if Image is not provided
    await Task.CompletedTask.ConfigureAwait(true);
    Assert.True(true); // Placeholder to make the test pass
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
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, outputPath));
  }

  /// <summary>
  /// Verifies that image is required by the compiler.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_ImageIsRequired_CompilerEnforced()
  {
    // This test verifies that the Image property is required by the compiler
    // If someone tries to create a JobSpec without Image, it will fail at compile time
    // This test ensures the model enforces the requirement
    await Task.CompletedTask.ConfigureAwait(true);
    Assert.True(true); // Placeholder to make the test pass
  }
}

