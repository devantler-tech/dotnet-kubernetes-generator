using Devantler.KubernetesGenerator.Flux.Models;
using Devantler.KubernetesGenerator.Flux.Models.Dependencies;
using Devantler.KubernetesGenerator.Flux.Models.Sources;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxHelmReleaseGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxHelmReleaseGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxHelmReleaseGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxHelmReleaseGenerator"/> generates a valid Flux HelmRelease object with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullHelmRelease()
  {
    // Arrange
    var fluxHelmRelease = new FluxHelmRelease
    {
      Metadata = new V1ObjectMeta
      {
        Name = "helm-release",
        NamespaceProperty = "default",
        Labels = new Dictionary<string, string> { { "key", "value" } },
        Annotations = new Dictionary<string, string> { { "key", "value" } }
      },
      Spec = new FluxHelmReleaseSpec
      {
        Interval = "10m",
        DependsOn =
        [
          new FluxDependsOn
          {
            Name = "dependency1",
            Namespace = "default"
          }
        ],
        Chart = new FluxHelmReleaseSpecChart
        {
          Spec = new FluxHelmReleaseSpecChartSpec
          {
            Chart = "my-chart",
            Version = "1.0.0",
            SourceRef = new FluxSourceRef
            {
              Kind = FluxSource.HelmRepository,
              Name = "my-helm-repo",
              Namespace = "my-namespace"
            }
          }
        },
        Values = new Dictionary<string, object> { { "key", "value" } }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "helm-release.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(fluxHelmRelease, outputPath);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("helm-release.full.yaml");

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="FluxHelmReleaseGenerator"/> generates a valid Flux HelmRelease object with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalHelmRelease()
  {
    // Arrange
    var fluxHelmRelease = new FluxHelmRelease
    {
      Metadata = new V1ObjectMeta
      {
        Name = "helm-release"
      },
      Spec = new FluxHelmReleaseSpec
      {
        Interval = "10m",
        Chart = new FluxHelmReleaseSpecChart
        {
          Spec = new FluxHelmReleaseSpecChartSpec
          {
            Chart = "my-chart",
            Version = "1.0.0",
            SourceRef = new FluxSourceRef
            {
              Kind = FluxSource.HelmRepository,
              Name = "my-helm-repo"
            }
          }
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "helm-release.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(fluxHelmRelease, outputPath, true);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("helm-release.minimal.yaml");

    // Cleanup
    File.Delete(outputPath);
  }
}
