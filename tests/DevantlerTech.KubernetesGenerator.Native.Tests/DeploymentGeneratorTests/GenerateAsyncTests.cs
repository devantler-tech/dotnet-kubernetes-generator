using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DeploymentGeneratorTests;

/// <summary>
/// Tests for the <see cref="DeploymentGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Deployment object with single image and command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithSingleImageAndCommand_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new Deployment("deployment")
    {
      Metadata = { Namespace = "default" },
      Replicas = 3,
      Port = 8080
    };
    model.Images.Add("nginx:latest");
    model.Command = "sh";
    model.Args.Add("-c");
    model.Args.Add("echo hello");

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

  /// <summary>
  /// Verifies the generated Deployment object with multiple images (no command).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleImages_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new Deployment("multi-image-deployment")
    {
      Metadata = { Namespace = "default" },
      Replicas = 2
    };
    model.Images.Add("busybox:latest");
    model.Images.Add("ubuntu:latest");
    model.Images.Add("nginx");

    // Act
    string fileName = "multi-image-deployment.yaml";
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
  /// Verifies the generated Deployment object with minimal properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new Deployment("simple-deployment");
    model.Images.Add("nginx");

    // Act
    string fileName = "simple-deployment.yaml";
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
  /// Verifies the generated Deployment object with command only.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithCommandOnly_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new Deployment("deployment-with-command")
    {
      Metadata = { Namespace = "default" },
      Command = "date"
    };
    model.Images.Add("busybox");

    // Act
    string fileName = "deployment-with-command.yaml";
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
  /// Verifies that a <see cref="ArgumentNullException"/> is thrown when the model is null.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new DeploymentGenerator();

    // Act & Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(() => generator.GenerateAsync(null!, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when no images are specified.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNoImages_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new Deployment("deployment-no-images");

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
    Assert.Contains("At least one image must be specified", exception.Message);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when multiple images are used with commands.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithMultipleImagesAndCommand_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new Deployment("deployment-multi-image-command");
    model.Images.Add("nginx");
    model.Images.Add("busybox");
    model.Command = "sh";

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
    Assert.Contains("Multiple images cannot be specified when using command arguments", exception.Message);
  }
}

