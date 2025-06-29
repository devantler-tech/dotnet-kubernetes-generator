using DevantlerTech.KubernetesGenerator.K3d.Models;
using DevantlerTech.KubernetesGenerator.K3d.Models.Options;
using DevantlerTech.KubernetesGenerator.K3d.Models.Options.K3d;
using DevantlerTech.KubernetesGenerator.K3d.Models.Options.K3s;
using DevantlerTech.KubernetesGenerator.K3d.Models.Options.Runtime;
using DevantlerTech.KubernetesGenerator.K3d.Models.Registries;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.K3d.Tests.K3dConfigGeneratorTests;

/// <summary>
/// Tests for <see cref="K3dConfigKubernetesGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  K3dConfigGenerator K3dConfigKubernetesGenerator { get; } = new();

  /// <summary>
  /// Tests that <see cref="K3dConfigKubernetesGenerator"/> generates a valid K3d cluster configuration with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullK3dConfigFile()
  {
    // Arrange
    var config = new K3dConfig
    {
      Metadata = new V1ObjectMeta
      {
        Name = "mycluster"
      },
      Servers = 1,
      Agents = 2,
      KubeAPI = new K3dKubeAPI
      {
        Host = "myhost.my.domain",
        HostIP = "127.0.0.1",
        HostPort = "6445"
      },
      Network = "my-custom-net",
      Subnet = "172.28.0.0/16",
      Token = "superSecretToken",
      Volumes = [
        new K3dVolume
        {
          Volume = "/my/host/path:/path/in/node",
          NodeFilters = [
            "server:0",
            "agent:*"
          ]
        }
      ],
      Ports = [
        new K3dPort
        {
          Port = "8080:80",
          NodeFilters = [
            "loadbalancer"
          ]
        }
      ],
      Env = [
        new K3dEnv
        {
          EnvVar = "bar=baz",
          NodeFilters = [
            "server:0"
          ]
        }
      ],
      Files = [
        new K3dFile
        {
          Description = "Source: Embedded, Destination: Magic shortcut path",
          Source = """
            apiVersion: v1
            kind: Namespace
            metadata:
              name: foo
          """,
          Destination = "k3s-manifests-custom/foo.yaml"
        },
        new K3dFile
        {
          Description = "Source: Relative, Destination: Absolute path, Node: Servers only",
          Source = "ns-baz.yaml",
          Destination = "/var/lib/rancher/k3s/server/manifests/baz.yaml",
          NodeFilters = [
            "server:*"
          ]
        }
      ],
      Registries = new K3dRegistries
      {
        Create = new K3dRegistriesCreate
        {
          Name = "registry.localhost",
          Host = "0.0.0.0",
          HostPort = "5000",
          Proxy = new K3dRegistriesCreateProxy
          {
            RemoteURL = new Uri("https://registry-1.docker.io"),
            Username = "",
            Password = ""
          },
          Volumes = [
            "/some/path:/var/lib/registry"
          ]
        },
        Use = [
          "k3d-myotherregistry:5000"
        ],
        Config = """
          mirrors:
            "my.company.registry":
              endpoint:
                - http://my.company.registry:5000
        """,
      },
      HostAliases = [
        new K3dHostAlias
        {
          Ip = "1.2.3.4",
          Hostnames = [
            "my.host.local",
            "that.other.local"
          ]
        },
        new K3dHostAlias
        {
          Ip = "1.1.1.1",
          Hostnames = [
            "cloud.flare.dns"
          ]
        }
      ],
      Options = new K3dOptions
      {
        K3d = new K3dOptionsK3d
        {
          Wait = true,
          Timeout = "60s",
          DisableLoadbalancer = false,
          DisableImageVolume = false,
          DisableRollback = false,
          Loadbalancer = new K3dOptionsK3dLoadbalancer
          {
            ConfigOverrides = [
              "settings.workerConnections=2048"
            ]
          }
        },
        K3s = new K3dOptionsK3s
        {
          ExtraArgs = [
            new K3dOptionsK3sExtraArg
            {
              Arg = "--tls-san=my.host.domain",
              NodeFilters = [
                "server:*"
              ]
            }
          ],
          NodeLabels = [
            new K3dOptionsLabel
            {
              Label = "foo=bar",
              NodeFilters = [
                "agent:1"
              ]
            }
          ]
        },
        Kubeconfig = new K3dOptionsKubeconfig
        {
          UpdateDefaultKubeconfig = true,
          SwitchCurrentContext = true
        },
        Runtime = new K3dOptionsRuntime
        {
          GPURequest = "all",
          Labels = [
            new K3dOptionsLabel
            {
              Label = "bar=baz",
              NodeFilters = [
                "agent:1"
              ]
            }
          ],
          Ulimits = [
            new K3dOptionsRuntimeUlimit
            {
              Name = "nofile",
              Soft = 26677,
              Hard = 26677
            }
          ]
        },
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "k3d.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await K3dConfigKubernetesGenerator.GenerateAsync(config, outputPath);
    string k3dConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(k3dConfigFromFile, extension: "yaml").UseFileName("k3d.full");

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="K3dConfigKubernetesGenerator"/> generates a valid K3d cluster configuration with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalK3dConfigFile()
  {
    // Arrange
    var config = new K3dConfig
    {
      Metadata = new V1ObjectMeta
      {
        Name = "mycluster"
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "k3d.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await K3dConfigKubernetesGenerator.GenerateAsync(config, outputPath, true);
    string k3dConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(k3dConfigFromFile, extension: "yaml").UseFileName("k3d.minimal");

    // Cleanup
    File.Delete(outputPath);
  }
}
