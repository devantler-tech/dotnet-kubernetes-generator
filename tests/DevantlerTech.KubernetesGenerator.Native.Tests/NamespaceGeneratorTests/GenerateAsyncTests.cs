using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.Namespace;

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
      Metadata = new ClusterScopedMetadata
      {
        Name = "test-namespace"
      }
    };

    // Act & Assert
    await GenerateAndVerifyAsync(generator, model, "namespace.yaml");
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the namespace name is invalid.
  /// </summary>
  /// <param name="name">The namespace name to test.</param>
  /// <returns></returns>
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public async Task GenerateAsync_WithInvalidNamespaceName_ShouldThrowKubernetesGeneratorException(string? name)
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new NativeNamespace
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = name!
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => 
      generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  private static async Task GenerateAndVerifyAsync<TModel, TGenerator>(
    TGenerator generator,
    TModel model,
    string fileName)
    where TGenerator : IKubernetesGenerator<TModel>
  {
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);

    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    File.Delete(outputPath);
  }
}
