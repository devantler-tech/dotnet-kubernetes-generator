using Devantler.KubernetesGenerator.K3d.Models;
using k8s.Models;

namespace Devantler.KubernetesGenerator.K3d.Tests.K3dConfigGeneratorTests;

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
      KubeAPI = new K3dConfigKubeAPI
      {
        Host = "myhost.my.domain",
        HostIP = "127.0.0.1",
        HostPort = "6445"
      },
      Image = "rancher/k3s:v1.20.4-k3s1",
      Network = "my-custom-net",
      Subnet = "172.28.0.0/16",
      Token = "superSecretToken",
      Volumes = [
        new K3dConfigVolume
        {
          Volume = "/my/host/path:/path/in/node",
          NodeFilters = [
            "server:0",
            "agent:*"
          ]
        }
      ],
      Ports = [
        new K3dConfigPort
        {
          Port = "8080:80",
          NodeFilters = [
            "loadbalancer"
          ]
        }
      ],
      Env = [
        new K3dConfigEnv
        {
          EnvVar = "bar=baz",
          NodeFilters = [
            "server:0"
          ]
        }
      ],
      Files = [
        new K3dConfigFile
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
        new K3dConfigFile
        {
          Description = "Source: Relative, Destination: Absolute path, Node: Servers only",
          Source = "ns-baz.yaml",
          Destination = "/var/lib/rancher/k3s/server/manifests/baz.yaml",
          NodeFilters = [
            "server:*"
          ]
        }
      ],
      Registries = new K3dConfigRegistries
      {
        Create = new K3dConfigRegistriesCreate
        {
          Name = "registry.localhost",
          Host = "0.0.0.0",
          HostPort = "5000",
          Proxy = new K3dConfigRegistriesCreateProxy
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
        new K3dConfigHostAlias
        {
          Ip = "1.2.3.4",
          Hostnames = [
            "my.host.local",
            "that.other.local"
          ]
        },
        new K3dConfigHostAlias
        {
          Ip = "1.1.1.1",
          Hostnames = [
            "cloud.flare.dns"
          ]
        }
      ],
      Options = new K3dConfigOptions
      {
        K3d = new K3dConfigOptionsK3d
        {
          Wait = true,
          Timeout = "60s",
          DisableLoadbalancer = false,
          DisableImageVolume = false,
          DisableRollback = false,
          Loadbalancer = new K3dConfigOptionsK3dLoadbalancer
          {
            ConfigOverrides = [
              "settings.workerConnections=2048"
            ]
          }
        },
        K3s = new K3dConfigOptionsK3s
        {
          ExtraArgs = [
            new K3dConfigOptionsK3sExtraArg
            {
              Arg = "--tls-san=my.host.domain",
              NodeFilters = [
                "server:*"
              ]
            }
          ],
          NodeLabels = [
            new K3dConfigLabel
            {
              Label = "foo=bar",
              NodeFilters = [
                "agent:1"
              ]
            }
          ]
        },
        Kubeconfig = new K3dConfigOptionsKubeconfig
        {
          UpdateDefaultKubeconfig = true,
          SwitchCurrentContext = true
        },
        Runtime = new K3dConfigOptionsRuntime
        {
          GPURequest = "all",
          Labels = [
            new K3dConfigLabel
            {
              Label = "bar=baz",
              NodeFilters = [
                "agent:1"
              ]
            }
          ],
          Ulimits = [
            new K3dConfigOptionsRuntimeUlimit
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
    string outputPath = Path.Combine(Path.GetTempPath(), "k3d-config.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await K3dConfigKubernetesGenerator.GenerateAsync(config, outputPath);
    string k3dConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(k3dConfigFromFile, extension: "yaml").UseFileName("k3d-config.full.yaml");

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
    string outputPath = Path.Combine(Path.GetTempPath(), "k3d-config.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await K3dConfigKubernetesGenerator.GenerateAsync(config, outputPath, true);
    string k3dConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(k3dConfigFromFile, extension: "yaml").UseFileName("k3d-config.minimal.yaml");

    // Cleanup
    File.Delete(outputPath);
  }
}
