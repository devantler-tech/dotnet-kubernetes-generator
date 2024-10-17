using Devantler.K9sCLI;
using k8s.Models;
using KSail.Models;
using KSail.Models.Commands.Check;
using KSail.Models.Commands.Debug;
using KSail.Models.Commands.Down;
using KSail.Models.Commands.Gen;
using KSail.Models.Commands.Init;
using KSail.Models.Commands.Lint;
using KSail.Models.Commands.List;
using KSail.Models.Commands.Sops;
using KSail.Models.Commands.Start;
using KSail.Models.Commands.Stop;
using KSail.Models.Commands.Up;
using KSail.Models.Commands.Update;
using KSail.Models.Registry;

namespace Devantler.KubernetesGenerator.KSail.Tests.KSailClusterGeneratorTests;

/// <summary>
/// Tests for <see cref="KSailClusterGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly KSailClusterGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="KSailClusterGenerator"/> generates a valid KSail Cluster configuration with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullKSailClusterFile()
  {
    // Arrange
    string name = "my-cluster";
    var cluster = new KSailCluster
    {
      Metadata = new V1ObjectMeta
      {
        Name = name
      },
      Spec = new KSailClusterSpec(name)
      {
        Kubeconfig = "./.kube/config",
        Context = "my-cluster",
        Timeout = 300,
        ManifestsDirectory = "./k8s",
        KustomizationDirectory = "./clusters/my-cluster/flux-system",
        ConfigPath = "./k3d-config.yaml",
        Distribution = KSailKubernetesDistribution.K3d,
        GitOpsTool = KSailGitOpsTool.Flux,
        ContainerEngine = KSailContainerEngine.Docker,
        Sops = true,
        Registries =
        [
          new KSailRegistry
          {
            Name = "ksail-registry",
            HostPort = 5000,
            IsGitOpsOCISource = true,
          },
          new KSailRegistry
          {
            Name = "mirror-docker.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://registry-1.docker.io")
            },
            HostPort = 5001
          },
          new KSailRegistry
          {
            Name = "mirror-registry.k8s.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://registry.k8s.io")
            },
            HostPort = 5002
          },
          new KSailRegistry
          {
            Name = "mirror-gcr.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://gcr.io")
            },
            HostPort = 5002
          },
          new KSailRegistry
          {
            Name = "mirror-ghcr.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://ghcr.io")
            },
            HostPort = 5003
          },
          new KSailRegistry
          {
            Name = "mirror-mcr.microsoft.com",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://mcr.microsoft.com")
            },
            HostPort = 5004
          },
          new KSailRegistry
          {
            Name = "mirror-quay.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://quay.io")
            },
            HostPort = 5005
          }
        ],
        CheckOptions = new KSailCheckOptions
        {
        },
        DebugOptions = new KSailDebugOptions
        {
          Editor = Editor.Nano
        },
        DownOptions = new KSailDownOptions
        {
          Registries = true
        },
        GenOptions = new KSailGenOptions
        {
        },
        InitOptions = new KSailInitOptions
        {
          Template = KSailInitTemplate.Simple,
          Components = true,
          HelmReleases = true,
          PostBuildVariables = true
        },
        LintOptions = new KSailLintOptions
        {
        },
        ListOptions = new KSailListOptions
        {
        },
        SopsOptions = new KSailSopsOptions
        {
        },
        StartOptions = new KSailStartOptions
        {
        },
        StopOptions = new KSailStopOptions
        {
        },
        UpOptions = new KSailUpOptions
        {
          Lint = true,
          Reconcile = true,
        },
        UpdateOptions = new KSailUpdateOptions
        {
          Lint = true,
          Reconcile = true,
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "ksail-config.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await _generator.GenerateAsync(cluster, outputPath, true);
    string ksailClusterConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(ksailClusterConfigFromFile, extension: "yaml").UseFileName("ksail-config.full.yaml");

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="KSailClusterGenerator"/> generates a valid KSail cluster configuration with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalKSailClusterFile()
  {
    // Arrange
    var cluster = new KSailCluster();

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "ksail-config.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await _generator.GenerateAsync(cluster, outputPath, true);
    string ksailClusterConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(ksailClusterConfigFromFile, extension: "yaml").UseFileName("ksail-config.minimal.yaml");

    // Cleanup
    File.Delete(outputPath);
  }
}
