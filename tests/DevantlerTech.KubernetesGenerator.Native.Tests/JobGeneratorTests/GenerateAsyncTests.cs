
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.Job;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.JobGeneratorTests;

/// <summary>
/// Tests for the <see cref="JobGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeJob object with image.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageAndCommand_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new NativeJob
    {
      Metadata = new Metadata
      {
        Name = "job-with-image-and-command",
        Namespace = "default"
      },
      Spec = new NativeJobSpec
      {
        Image = "busybox",
        Command = ["echo", "hello"]
      }
    };

    // Act
    string fileName = "job-with-image-and-command.yaml";
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
  /// Verifies the generated NativeJob object with image only.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithImageOnly_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new NativeJob
    {
      Metadata = new Metadata
      {
        Name = "job-with-image",
        Namespace = "default"
      },
      Spec = new NativeJobSpec
      {
        Image = "nginx"
      }
    };

    // Act
    string fileName = "job-with-image.yaml";
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

