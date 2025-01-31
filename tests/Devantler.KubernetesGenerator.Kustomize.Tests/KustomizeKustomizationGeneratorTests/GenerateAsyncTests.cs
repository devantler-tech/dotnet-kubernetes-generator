using Devantler.KubernetesGenerator.Kustomize.Models;
using Devantler.KubernetesGenerator.Kustomize.Models.Generators;
using Devantler.KubernetesGenerator.Kustomize.Models.Patches;

namespace Devantler.KubernetesGenerator.Kustomize.Tests.KustomizeKustomizationGeneratorTests;

/// <summary>
/// Tests for the <see cref="KustomizeKustomization"/> generator.
/// </summary>
public class GenerateAsyncTests
{
  readonly KustomizeKustomizationGenerator _generator = new();

  /// <summary>
  /// Tests that <see cref="KustomizeKustomizationGenerator"/> generates a valid Kustomize Kustomization with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullKustomizeKustomizationFile()
  {
    // Arrange
    var component = new KustomizeKustomization
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
            LabelSelector = "app=nginx",
            AnnotationSelector = "zone=west"
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
      ],
      SecretGenerator =
      [
        new KustomizeSecretGenerator
        {
          Name = "my-secret",
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
      ],
      Components = [
        "component1.yaml",
        "component2.yaml"
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "kustomization.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(component, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName("kustomization.full");

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="KustomizeKustomizationGenerator"/> generates a valid Kustomize Kustomization with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalKustomizeKustomizationFile()
  {
    // Arrange
    var component = new KustomizeKustomization
    {
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "kustomization.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(component, outputPath, true);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName("kustomization.minimal");

    // Cleanup
    File.Delete(outputPath);
  }
}
