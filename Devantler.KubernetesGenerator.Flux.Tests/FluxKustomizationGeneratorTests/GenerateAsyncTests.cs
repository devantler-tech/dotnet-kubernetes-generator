using Devantler.KubernetesGenerator.Flux.Models.Kustomization;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxKustomizationGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxKustomizationGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxKustomizationGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxKustomizationGenerator"/> generates a valid Flux Kustomization.
  /// </summary>
  [Theory]
  [ClassData(typeof(ClassData))]
  public async Task GenerateAsync_GeneratesValidFluxKustomization(FluxKustomization model, string fileName)
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
