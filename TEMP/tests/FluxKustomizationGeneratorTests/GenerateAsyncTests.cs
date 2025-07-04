using DevantlerTech.KubernetesGenerator.Flux.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models.Dependencies;
using DevantlerTech.KubernetesGenerator.Flux.Models.Images;
using DevantlerTech.KubernetesGenerator.Flux.Models.KubeConfig;
using DevantlerTech.KubernetesGenerator.Flux.Models.Metadata;
using DevantlerTech.KubernetesGenerator.Flux.Models.Patches;
using DevantlerTech.KubernetesGenerator.Flux.Models.SecretRef;
using DevantlerTech.KubernetesGenerator.Kustomize.Models.Patches;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxKustomizationGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxKustomizationGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxKustomizationGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="FluxKustomizationGenerator"/> generates a valid Flux Kustomization object with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullKustomization()
  {
    // Arrange
    var fluxKustomization = new FluxKustomization
    {
      Metadata = new V1ObjectMeta
      {
        Name = "my-kustomization",
        NamespaceProperty = "my-namespace",
        Labels = new Dictionary<string, string> { { "key", "value" } },
        Annotations = new Dictionary<string, string> { { "key", "value" } }
      },
      Spec = new FluxKustomizationSpec
      {
        Interval = "10m",
        RetryInterval = "5m",
        Timeout = "30m",
        Suspend = false,
        TargetNamespace = "my-target-namespace",
        DependsOn =
        [
          new FluxDependsOn
          {
            Name = "my-dependency",
            Namespace = "my-dependency-namespace"
          }
        ],
        SourceRef = new FluxKustomizationSpecSourceRef
        {
          Kind = FluxSource.GitRepository,
          Name = "my-git-repo",
          Namespace = "my-git-repo-namespace"
        },
        Path = "my-path",
        Prune = true,
        Wait = true,
        HealthChecks =
        [
          new FluxKustomizationSpecHealthCheck
          {
            ApiVersion = "v1",
            Kind = "Pod",
            Name = "my-pod",
            Namespace = "my-pod-namespace",
          }
        ],
        ServiceAccountName = "my-service-account",
        CommonMetadata =
        [
          new FluxMetadata
          {
            Annotations = new Dictionary<string, string> { { "key", "value" } },
            Labels = new Dictionary<string, string> { { "key", "value" } },
          }
        ],
        NamePrefix = "my-prefix",
        NameSuffix = "my-suffix",
        Patches =
        [
          new FluxPatch
          {
            Target = new KustomizeTarget
            {
              Kind = "Deployment",
              Version = "v1",
              Name = "my-deployment",
              Namespace = "my-deployment-namespace",
              Group = "my-group",
              LabelSelector = "key=value",
              AnnotationSelector = "key=value"
            },
            Patch = """
            apiVersion: apps/v1
            kind: Deployment
            metadata:
              name: my-deployment
            spec:
              replicas: 3
            """
          }
        ],
        Images =
        [
          new FluxImage
          {
            Name = "my-image",
            NewName = "my-new-image",
            NewTag = "latest",
            Digest = "sha256:1234567890abcdef",
          }
        ],
        Components = ["my-component"],
        PostBuild = new FluxKustomizationSpecPostBuild
        {
          Substitute = new Dictionary<string, string> { { "key", "value" } },
          SubstituteFrom = [
              new FluxKustomizationSpecPostBuildSubstituteFrom {
                Kind =  FluxKustomizationSpecPostBuildSubstituteFromKind.ConfigMap,
                Name = "my-configmap",
                Optional = false
              }
           ]
        },
        Force = true,
        KubeConfig = new FluxKubeConfig
        {
          SecretRef = new FluxSecretRef
          {
            Name = "my-secret",
            Key = "value.yaml"
          }
        },
        Decryption = new FluxKustomizationSpecDecryption
        {
          Provider = FluxKustomizationSpecDecryptionProvider.SOPS,
          SecretRef = new FluxSecretRef
          {
            Name = "my-secret",
            Key = "sops-age",
          }
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "flux-kustomization.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(fluxKustomization, outputPath);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("flux-kustomization.full");

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="FluxKustomizationGenerator"/> generates a valid Flux Kustomization object with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalK3dConfigFile()
  {
    // Arrange
    var fluxKustomization = new FluxKustomization
    {
      Metadata = new V1ObjectMeta
      {
        Name = "my-kustomization",
      },
      Spec = new FluxKustomizationSpec
      {
        Interval = "10m",
        Path = "my-path",
        SourceRef = new FluxKustomizationSpecSourceRef
        {
          Kind = FluxSource.GitRepository,
          Name = "my-git-repo",
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "flux-kustomization.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(fluxKustomization, outputPath, true);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("flux-kustomization.minimal");

    // Cleanup
    File.Delete(outputPath);
  }
}
