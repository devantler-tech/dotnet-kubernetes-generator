using Devantler.KubernetesGenerator.Flux.Models.HelmRepository;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxHelmRepositoryGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxHelmRepositoryGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxHelmRepositoryGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxHelmRepositoryGenerator"/> generates a valid Flux HelmRepository.
  /// </summary>
  [Theory]
  [ClassData(typeof(ClassData))]
  public async Task GenerateAsync_GeneratesValidFluxHelmRepository(FluxHelmRepository model, string fileName)
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
