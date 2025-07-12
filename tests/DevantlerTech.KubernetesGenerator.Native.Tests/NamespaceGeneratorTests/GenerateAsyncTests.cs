using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.NamespaceGeneratorTests;


/// <summary>
/// Tests for the <see cref="NamespaceGenerator"/> class.
/// </summary>
internal class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Namespace object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidNamespace()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new V1Namespace
    {
      ApiVersion = "v1",
      Kind = "Namespace",
      Metadata = new V1ObjectMeta
      {
        Name = "namespace",
      },
      Spec = new V1NamespaceSpec
      {
        Finalizers = ["kubernetes"]
      },
    };

    // Act
    string fileName = "namespace.yaml";
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
