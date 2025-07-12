using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PodGeneratorTests;


/// <summary>
/// Tests for the <see cref="PodGenerator"/> class.
/// </summary>
internal class GenerateAsyncTests
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
    var model = new V1Pod
    {
      ApiVersion = "v1",
      Kind = "Pod",
      Metadata = new V1ObjectMeta
      {
        Name = "pod",
        NamespaceProperty = "default"
      },
      Spec = new V1PodSpec
      {
        Containers =
        [
          new V1Container
          {
            Name = "container",
            Image = "nginx",
            Command = ["echo", "hello"]
          }
        ]
      }
    };

    // Act
    string fileName = "pod.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}

