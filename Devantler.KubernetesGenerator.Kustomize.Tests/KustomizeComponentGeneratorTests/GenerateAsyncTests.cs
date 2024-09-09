using Devantler.KubernetesGenerator.Kustomize.Models;
using Devantler.KubernetesGenerator.Kustomize.Models.Generators;
using Devantler.KubernetesGenerator.Kustomize.Models.Patches;

namespace Devantler.KubernetesGenerator.Kustomize.Tests.KustomizeComponentGeneratorTests;

/// <summary>
/// Tests for the <see cref="KustomizeComponent"/> generator.
/// </summary>
public class GenerateAsyncTests
{
  readonly KustomizeComponentGenerator _generator = new();

  /// <summary>
  /// Tests that <see cref="KustomizeComponentGenerator"/> generates a valid Kustomize component configuration with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullKustomizeComponentFile()
  {
    // Arrange
    var component = new KustomizeComponent
    {
      Resources =
      [
        "resource1.yaml",
        "resource2.yaml"
      ],
      Patches =
      [
        new KustomizePatch
        {
          Path = "patch1.yaml",
          Target = new KustomizeTarget
          {
            Name = "deployment1",
            Kind = "Deployment",
            Version = "v1",
            Namespace = "default",
            Group = "apps",
            LabelSelector = new Dictionary<string, string> { { "app", "nginx" } },
            AnnotationSelector = new Dictionary<string, string> { { "zone", "west" } }
          },
          Patch = """
          apiVersion: apps/v1
          kind: Deployment
          metadata:
            name: deployment1
            labels:
              app.kubernetes.io/version: 1.21.1
          """,
          Options = new KustomizePatchOptions
          {
            AllowNameChange = true,
            AllowKindChange = true,
          }
        }
      ],
      ConfigMapGenerator = [
        new KustomizeConfigMapGenerator
        {
          Name = "my-configmap",
          Behavior = KustomizeGeneratorBehavior.Create,
          Files = [
            "data.txt",
            "config.json"
          ],
          Envs = [
            ".env"
          ],
          Literals = [
            "KEY1=VALUE1",
            "KEY2=VALUE2"
          ],
          Options = new KustomizeGeneratorOptions
          {
            DisableNameSuffixHash = true,
            Labels = ["app=nginx"],
            Annotations = ["zone=west"]
          }
        }
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "kustomization.yaml");
    File.Delete(outputPath);
    await _generator.GenerateAsync(component, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="KustomizeComponentGenerator"/> generates a valid Kustomize component configuration with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalKustomizeComponentFile()
  {
    // Arrange
    var component = new KustomizeComponent
    {
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "kustomization.yaml");
    File.Delete(outputPath);
    await _generator.GenerateAsync(component, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}
