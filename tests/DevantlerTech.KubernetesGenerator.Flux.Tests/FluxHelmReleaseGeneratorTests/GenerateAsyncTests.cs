using DevantlerTech.KubernetesGenerator.Flux.Models.HelmRelease;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxHelmReleaseGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxHelmReleaseGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxHelmReleaseGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxHelmReleaseGenerator"/> generates a valid Flux HelmRelease.
  /// </summary>
  [Theory]
  [ClassData(typeof(ClassData))]
  public async Task GenerateAsync_GeneratesValidFluxHelmRelease(FluxHelmRelease model, string fileName)
  {
    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    await _generator.GenerateAsync(model, outputPath, true);
    string fileContents = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContents, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}
