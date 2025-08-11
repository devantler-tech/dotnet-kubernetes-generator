using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.Deployment;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DeploymentGeneratorTests;

/// <summary>
/// Tests for the <see cref="DeploymentGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Test data for valid deployment generation scenarios.
  /// </summary>
  public static TheoryData<NativeDeployment, string> ValidDeploymentData =>
    new()
    {
      {
        new NativeDeployment
        {
          Metadata = new Metadata { Name = "my-deployment", Namespace = "default" },
          Spec = new NativeDeploymentSpec { Images = ["nginx"], Replicas = 3 }
        },
        "deployment.yaml"
      },
      {
        new NativeDeployment
        {
          Metadata = new Metadata { Name = "my-app-deployment" },
          Spec = new NativeDeploymentSpec
          {
            Images = ["busybox"],
            Replicas = 2,
            Port = 8080,
            Command = ["echo", "hello", "world"]
          }
        },
        "app-deployment.yaml"
      },
      {
        new NativeDeployment
        {
          Metadata = new Metadata { Name = "multi-container-deployment" },
          Spec = new NativeDeploymentSpec { Images = ["nginx", "busybox", "ubuntu"], Replicas = 2 }
        },
        "multi-deployment.yaml"
      }
    };

  /// <summary>
  /// Verifies the generated NativeDeployment object with different configurations.
  /// </summary>
  /// <param name="model">The deployment model to test.</param>
  /// <param name="fileName">The expected output file name.</param>
  /// <returns></returns>
  [Theory]
  [MemberData(nameof(ValidDeploymentData))]
  public async Task GenerateAsync_WithValidConfiguration_ShouldGenerateAValidDeployment(NativeDeployment model, string fileName)
  {
    // Arrange
    var generator = new DeploymentGenerator();

    // Act & Assert
    await GenerateAndVerifyAsync(generator, model, fileName);
  }

  /// <summary>
  /// Test data for exception scenarios.
  /// </summary>
  public static TheoryData<NativeDeployment, string> ExceptionData =>
    new()
    {
      {
        new NativeDeployment
        {
          Metadata = new Metadata { Name = string.Empty },
          Spec = new NativeDeploymentSpec { Images = ["nginx"] }
        },
        "A non-empty Deployment name must be provided"
      },
      {
        new NativeDeployment
        {
          Metadata = new Metadata { Name = "test-deployment" },
          Spec = new NativeDeploymentSpec { Images = ["nginx", "busybox"], Command = ["echo", "hello"] }
        },
        "Cannot specify multiple images with a command"
      },
      {
        new NativeDeployment
        {
          Metadata = new Metadata { Name = "test-deployment" },
          Spec = new NativeDeploymentSpec { Images = [] }
        },
        "At least one container image must be provided"
      }
    };

  /// <summary>
  /// Verifies that exceptions are thrown for invalid configurations.
  /// </summary>
  /// <param name="model">The deployment model to test.</param>
  /// <param name="expectedMessagePart">Expected part of the exception message.</param>
  /// <returns></returns>
  [Theory]
  [MemberData(nameof(ExceptionData))]
  public async Task GenerateAsync_WithInvalidConfiguration_ShouldThrowException(NativeDeployment model, string expectedMessagePart)
  {
    // Arrange
    var generator = new DeploymentGenerator();

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, "/tmp/test.yaml"));

    Assert.Contains(expectedMessagePart, exception.Message, StringComparison.Ordinal);
  }

  /// <summary>
  /// Helper method to generate a file and verify its content.
  /// </summary>
  /// <typeparam name="TModel">The model type.</typeparam>
  /// <typeparam name="TGenerator">The generator type.</typeparam>
  /// <param name="generator">The generator instance.</param>
  /// <param name="model">The model to generate.</param>
  /// <param name="fileName">The file name for verification.</param>
  /// <returns>A task representing the async operation.</returns>
  static async Task GenerateAndVerifyAsync<TModel, TGenerator>(
    TGenerator generator,
    TModel model,
    string fileName)
    where TGenerator : IKubernetesGenerator<TModel>
  {
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);

    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    File.Delete(outputPath);
  }
}

