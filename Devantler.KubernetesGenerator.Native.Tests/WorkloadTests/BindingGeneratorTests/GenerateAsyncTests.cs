using Devantler.KubernetesGenerator.Native.Workload;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.WorkloadTests.BindingGeneratorTests;


/// <summary>
/// Tests for the <see cref="BindingGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Binding object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidBinding()
  {
    // Arrange
    var generator = new BindingGenerator();
    var model = new V1Binding
    {
      ApiVersion = "v1",
      Kind = "Binding",
      Metadata = new V1ObjectMeta
      {
        Name = "binding",
        NamespaceProperty = "default"
      },
      Target = new V1ObjectReference
      {
        ApiVersion = "v1",
        Kind = "Pod",
        Name = "pod-name",
        NamespaceProperty = "default",
        ResourceVersion = "1",
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "binding.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}

