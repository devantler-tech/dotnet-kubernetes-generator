using Devantler.KubernetesGenerator.Flux.Models;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxHelmRepositoryGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxHelmRepositoryGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxHelmRepositoryGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxHelmRepositoryGenerator"/> generates a valid Flux HelmRepository object with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullHelmRepository()
  {
    // Arrange
    var fluxHelmRepository = new FluxHelmRepository
    {
      Metadata = new V1ObjectMeta
      {
        Name = "helm-repository",
        NamespaceProperty = "default",
        Labels = new Dictionary<string, string> { { "key", "value" } },
        Annotations = new Dictionary<string, string> { { "key", "value" } }
      },
      Spec = new FluxHelmRepositorySpec
      {
        Interval = "10m",
        Url = new Uri("https://my-helm-repo.com"),
        Type = FluxHelmRepositorySpecType.Default
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "helm-repository.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(fluxHelmRepository, outputPath);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("helm-repository.full.yaml");

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="FluxHelmRepositoryGenerator"/> generates a valid Flux HelmRepository object with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalHelmRepository()
  {
    // Arrange
    var fluxHelmRepository = new FluxHelmRepository
    {
      Metadata = new V1ObjectMeta
      {
        Name = "helm-repository"
      },
      Spec = new FluxHelmRepositorySpec
      {
        Url = new Uri("https://my-helm-repo.com")
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "helm-repository.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(fluxHelmRepository, outputPath, true);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("helm-repository.minimal.yaml");

    // Cleanup
    File.Delete(outputPath);
  }
}
