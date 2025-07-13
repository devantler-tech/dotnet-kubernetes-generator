using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ConfigMapGeneratorTests;


/// <summary>
/// Tests for the <see cref="ConfigMapGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ConfigMap object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new V1ConfigMap
    {
      ApiVersion = "v1",
      Kind = "ConfigMap",
      Metadata = new V1ObjectMeta
      {
        Name = "config-map",
        NamespaceProperty = "default"
      },
      Data = new Dictionary<string, string>
      {
        ["key"] = "value"
      },
      BinaryData = new Dictionary<string, byte[]>
      {
        ["key"] = [1, 2, 3]
      },
      Immutable = true
    };

    // Act
    string fileName = "config-map.yaml";
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
