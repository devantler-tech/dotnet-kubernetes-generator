using DevantlerTech.KubernetesGenerator.Flux.Models.Alert;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxAlertGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxAlertGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxAlertGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxAlertGenerator"/> generates a valid Flux Alert.
  /// </summary>
  [Theory]
  [ClassData(typeof(ClassData))]
  public async Task GenerateAsync_GeneratesValidFluxAlert(FluxAlert model, string fileName)
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
