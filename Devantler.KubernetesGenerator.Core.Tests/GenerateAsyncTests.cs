namespace Devantler.KubernetesGenerator.Core.Tests;

/// <summary>
/// Tests for <see cref="BaseKubernetesGenerator{T}"/>.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Tests that <see cref="BaseKubernetesGenerator{T}"/> generates a valid Kubernetes object excluding null and default values.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithOmitDefaults_OmitsNullAndDefaults()
  {
    // Arrange
    var generator = new BaseKubernetesGenerator<object>(omitDefaults: true);
    var model = new { Name = "my-name", Value = default(string), Enabled = default(bool), Items = (object?)null };
    string outputPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

    // Act
    await generator.GenerateAsync(model, outputPath);

    // Assert
    string yaml = await File.ReadAllTextAsync(outputPath);
    _ = await Verify(yaml, extension: "yaml").UseFileName("omit-defaults");
  }
}
