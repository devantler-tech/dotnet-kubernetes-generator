using DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxImagePolicyGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxImagePolicyGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxImagePolicyGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxImagePolicyGenerator"/> generates a valid Flux ImagePolicy.
  /// </summary>
  [Theory]
  [ClassData(typeof(ClassData))]
  public async Task GenerateAsync_GeneratesValidFluxImagePolicy(FluxImagePolicy model, string fileName)
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