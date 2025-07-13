using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PodGeneratorTests;


/// <summary>
/// Tests for the <see cref="PodGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Pod object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPod()
  {
    // Arrange
    var generator = new PodGenerator();
    var model = new Pod
    {
      Metadata = new V1ObjectMeta
      {
        Name = "pod",
        NamespaceProperty = "default"
      },
      Image = "nginx",
      Command = ["echo", "hello"],
      Environment = new Dictionary<string, string>
      {
        { "ENV_VAR", "value" }
      },
      Port = 80,
      RestartPolicy = "Never",
      Labels = new Dictionary<string, string>
      {
        { "app", "test" }
      }
    };

    // Act
    string fileName = "pod.yaml";
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
  /// Verifies the generated Pod object with minimal required fields.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalRequiredFields_ShouldGenerateAValidPod()
  {
    // Arrange
    var generator = new PodGenerator();
    var model = new Pod
    {
      Metadata = new V1ObjectMeta
      {
        Name = "minimal-pod"
      },
      Image = "nginx"
    };

    // Act
    string fileName = "minimal-pod.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the Pod model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithPodWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new PodGenerator();
    var model = new Pod
    {
      Metadata = new V1ObjectMeta
      {
        NamespaceProperty = "default"
      },
      Image = "nginx"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}

