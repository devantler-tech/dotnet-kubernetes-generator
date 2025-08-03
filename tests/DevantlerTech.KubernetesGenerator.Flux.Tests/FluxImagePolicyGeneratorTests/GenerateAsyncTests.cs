using DevantlerTech.KubernetesGenerator.Core.Models;
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

  /// <summary>
  /// Tests that <see cref="FluxImagePolicyGenerator"/> throws an exception for invalid sort by option.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithInvalidSortBy_ThrowsArgumentOutOfRangeException()
  {
    // Arrange
    var model = new FluxImagePolicy
    {
      Metadata = new NamespacedMetadata
      {
        Name = "test-policy"
      },
      Spec = new FluxImagePolicySpec
      {
        ImageRef = "test-image",
        SortBy = (FluxImagePolicySortBy)999 // Invalid enum value
      }
    };
    string outputPath = Path.Combine(Path.GetTempPath(), "test.yaml");

    // Act & Assert
    var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
      () => _generator.GenerateAsync(model, outputPath, true));

    Assert.Contains("Invalid sort by option", exception.Message, StringComparison.Ordinal);
    Assert.Equal("model", exception.ParamName);
  }
}