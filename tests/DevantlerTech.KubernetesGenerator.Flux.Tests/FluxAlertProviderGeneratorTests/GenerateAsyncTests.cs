using DevantlerTech.KubernetesGenerator.Flux.Models.AlertProvider;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxAlertProviderGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxAlertProviderGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxAlertProviderGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxAlertProviderGenerator"/> generates a valid Flux Alert Provider.
  /// </summary>
  [Theory]
  [ClassData(typeof(ClassData))]
  public async Task GenerateAsync_GeneratesValidFluxAlertProvider(FluxAlertProvider model, string fileName)
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
