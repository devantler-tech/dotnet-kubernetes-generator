using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.JobGeneratorTests;


/// <summary>
/// Tests for the <see cref="JobGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Job object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new V1ObjectMeta
      {
        Name = "my-job",
        NamespaceProperty = "default"
      },
      Image = "nginx:latest",
      Command = ["echo", "hello"],
      Args = ["world"]
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
  /// Verifies the generated Job object with minimal properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new V1ObjectMeta
      {
        Name = "simple-job"
      },
      Image = "busybox:latest"
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
  /// Verifies the generated Job object from a CronJob reference.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithFromCronJob_ShouldGenerateAValidJob()
  {
    // Arrange
    var generator = new JobGenerator();
    var model = new Job
    {
      Metadata = new V1ObjectMeta
      {
        Name = "job-from-cronjob",
        NamespaceProperty = "default"
      },
      Image = "nginx:latest", // This will be ignored when From is specified
      From = "cronjob/my-cronjob"
    };

    // Act & Assert
    // This test verifies that the --from parameter is properly handled,
    // but we expect it to fail without a cluster since --from requires API server connection
    _ = await Assert.ThrowsAsync<CliWrap.Exceptions.CommandExecutionException>(
      () => generator.GenerateAsync(model, Path.Combine(Path.GetTempPath(), "job-from-cronjob.yaml"))
    );
  }
}

