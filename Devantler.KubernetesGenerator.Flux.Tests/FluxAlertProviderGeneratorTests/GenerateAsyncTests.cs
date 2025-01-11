using Devantler.KubernetesGenerator.Flux.Models.AlertProvider;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxAlertProviderGeneratorTests;

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
    string file = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(file, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}
