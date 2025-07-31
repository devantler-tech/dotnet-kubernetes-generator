using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DeploymentGeneratorTests;


/// <summary>
/// Tests for the <see cref="DeploymentGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeDeployment object with basic configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicConfiguration_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new NativeDeployment
    {
      Metadata = new Metadata
      {
        Name = "my-deployment",
        Namespace = "default"
      },
      Spec = new NativeDeploymentSpec
      {
        Images = ["nginx"],
        Replicas = 3
      }
    };

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
  /// Verifies the generated NativeDeployment with port and command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithPortAndCommand_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new NativeDeployment
    {
      Metadata = new Metadata
      {
        Name = "my-app-deployment"
      },
      Spec = new NativeDeploymentSpec
      {
        Images = ["busybox"],
        Replicas = 2,
        Port = 8080,
        Command = ["echo", "hello", "world"]
      }
    };

    // Act
    string fileName = "app-deployment.yaml";
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
  /// Verifies the generated NativeDeployment with multiple images (without command).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleImages_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new NativeDeployment
    {
      Metadata = new Metadata
      {
        Name = "multi-container-deployment"
      },
      Spec = new NativeDeploymentSpec
      {
        Images = ["nginx", "busybox", "ubuntu"],
        Replicas = 2
      }
    };

    // Act
    string fileName = "multi-deployment.yaml";
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
  /// Verifies that an exception is thrown when no name is provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyName_ShouldThrowException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new NativeDeployment
    {
      Metadata = new Metadata
      {
        Name = string.Empty
      },
      Spec = new NativeDeploymentSpec
      {
        Images = ["nginx"]
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(
      () => generator.GenerateAsync(model, "/tmp/test.yaml"));

    Assert.Contains("A non-empty Deployment name must be provided", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that an exception is thrown when multiple images are provided with a command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleImagesAndCommand_ShouldThrowException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new NativeDeployment
    {
      Metadata = new Metadata
      {
        Name = "test-deployment"
      },
      Spec = new NativeDeploymentSpec
      {
        Images = ["nginx", "busybox"],
        Command = ["echo", "hello"]
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(
      () => generator.GenerateAsync(model, "/tmp/test.yaml"));

    Assert.Contains("Cannot specify multiple images with a command", exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Verifies that an exception is thrown when no images are provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNoImages_ShouldThrowException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new NativeDeployment
    {
      Metadata = new Metadata
      {
        Name = "test-deployment"
      },
      Spec = new NativeDeploymentSpec
      {
        Images = []
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(
      () => generator.GenerateAsync(model, "/tmp/test.yaml"));

    Assert.Contains("At least one container image must be provided", exception.Message, StringComparison.Ordinal);
  }
}

