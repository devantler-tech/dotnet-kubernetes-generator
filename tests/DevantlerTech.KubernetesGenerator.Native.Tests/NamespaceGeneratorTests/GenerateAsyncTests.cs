using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.NamespaceGeneratorTests;


/// <summary>
/// Tests for the <see cref="NamespaceGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeNamespace object with kubectl create namespace functionality.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNamespaceModel_ShouldGenerateAValidNamespace()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new NativeNamespace
    {
      Metadata = new NativeClusterScopedMetadata
      {
        Name = "test-namespace"
      }
    };

    // Act
    string fileName = "namespace.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the namespace name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyNamespaceName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new NativeNamespace
    {
      Metadata = new NativeClusterScopedMetadata
      {
        Name = ""
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
