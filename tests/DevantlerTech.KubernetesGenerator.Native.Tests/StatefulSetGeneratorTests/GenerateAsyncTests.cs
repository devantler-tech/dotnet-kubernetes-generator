using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.StatefulSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="StatefulSetGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated StatefulSet object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidStatefulSet()
  {
    // Arrange
    var generator = new StatefulSetGenerator();
    var model = new StatefulSet
    {
      Metadata = new V1ObjectMeta
      {
        Name = "stateful-set",
        NamespaceProperty = "default"
      },
      Replicas = 1,
      ServiceName = "stateful-set",
      Containers =
      [
        new StatefulSetContainer
        {
          Name = "container",
          Image = "nginx",
          Command = ["echo", "hello"]
        }
      ]
    };

    // Act
    string fileName = "stateful-set.yaml";
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

