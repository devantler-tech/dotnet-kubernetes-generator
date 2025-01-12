namespace Devantler.KubernetesGenerator.Core.Tests;

/// <summary>
/// Tests for <see cref="YamlFileWriter"/>.
/// </summary>
public class YamlFileWriterTests
{
  /// <summary>
  /// Tests the <see cref="YamlFileWriter.WriteToFileAsync(string, string, bool, CancellationToken)"/> method with an invalid file extension.
  /// </summary>
  [Fact]
  public async Task WriteToFileAsync_WithInvalidFileExtension_ShouldThrowKubernetesGeneratorExceptionAsync()
  {
    // Arrange
    string outputPath = Path.Combine(Path.GetTempPath(), "test.txt");
    string output = "test";
    bool overwrite = true;

    // Act
    async Task Act() => await YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite);

    // Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(Act);
  }
}
