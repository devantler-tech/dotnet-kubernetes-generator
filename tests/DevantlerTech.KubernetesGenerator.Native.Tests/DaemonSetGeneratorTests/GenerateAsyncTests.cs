using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DaemonSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="DaemonSetGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated DaemonSet object with basic properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicProperties_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new V1DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new V1ObjectMeta
      {
        Name = "daemon-set",
        NamespaceProperty = "default"
      },
      Spec = new V1DaemonSetSpec
      {
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set"
            }
          },
          Spec = new V1PodSpec
          {
            Containers =
            [
              new V1Container
              {
                Name = "container",
                Image = "nginx",
                Command = ["echo", "hello"]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "daemon-set.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated DaemonSet object with rolling update strategy.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithRollingUpdateStrategy_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new V1DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new V1ObjectMeta
      {
        Name = "daemon-set-rolling",
        NamespaceProperty = "default"
      },
      Spec = new V1DaemonSetSpec
      {
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set-rolling"
          }
        },
        UpdateStrategy = new V1DaemonSetUpdateStrategy
        {
          Type = "RollingUpdate",
          RollingUpdate = new V1RollingUpdateDaemonSet
          {
            MaxSurge = "25%",
            MaxUnavailable = "25%"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set-rolling"
            }
          },
          Spec = new V1PodSpec
          {
            Containers =
            [
              new V1Container
              {
                Name = "app",
                Image = "nginx:latest",
                Ports =
                [
                  new V1ContainerPort
                  {
                    ContainerPortProperty = 80,
                    Name = "http"
                  }
                ]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "daemon-set-rolling-update.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated DaemonSet object with resource limits and node selector.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithResourceLimitsAndNodeSelector_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new V1DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new V1ObjectMeta
      {
        Name = "daemon-set-resources",
        NamespaceProperty = "kube-system",
        Labels = new Dictionary<string, string>
        {
          ["app"] = "daemon-set-resources",
          ["version"] = "v1.0.0"
        }
      },
      Spec = new V1DaemonSetSpec
      {
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set-resources"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set-resources"
            }
          },
          Spec = new V1PodSpec
          {
            NodeSelector = new Dictionary<string, string>
            {
              ["kubernetes.io/os"] = "linux"
            },
            Tolerations =
            [
              new V1Toleration
              {
                Key = "node-role.kubernetes.io/control-plane",
                Operator = "Exists",
                Effect = "NoSchedule"
              }
            ],
            Containers =
            [
              new V1Container
              {
                Name = "app",
                Image = "nginx:1.21",
                Resources = new V1ResourceRequirements
                {
                  Requests = new Dictionary<string, ResourceQuantity>
                  {
                    ["cpu"] = new ResourceQuantity("100m"),
                    ["memory"] = new ResourceQuantity("128Mi")
                  },
                  Limits = new Dictionary<string, ResourceQuantity>
                  {
                    ["cpu"] = new ResourceQuantity("500m"),
                    ["memory"] = new ResourceQuantity("256Mi")
                  }
                },
                Env =
                [
                  new V1EnvVar
                  {
                    Name = "NODE_NAME",
                    ValueFrom = new V1EnvVarSource
                    {
                      FieldRef = new V1ObjectFieldSelector
                      {
                        FieldPath = "spec.nodeName"
                      }
                    }
                  }
                ]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "daemon-set-resources.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated DaemonSet object with volumes and security context.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithVolumesAndSecurityContext_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new V1DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new V1ObjectMeta
      {
        Name = "daemon-set-volumes",
        NamespaceProperty = "default"
      },
      Spec = new V1DaemonSetSpec
      {
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set-volumes"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set-volumes"
            }
          },
          Spec = new V1PodSpec
          {
            SecurityContext = new V1PodSecurityContext
            {
              RunAsNonRoot = true,
              RunAsUser = 1000,
              FsGroup = 2000
            },
            Containers =
            [
              new V1Container
              {
                Name = "app",
                Image = "nginx:alpine",
                SecurityContext = new V1SecurityContext
                {
                  ReadOnlyRootFilesystem = true,
                  AllowPrivilegeEscalation = false,
                  Capabilities = new V1Capabilities
                  {
                    Drop = ["ALL"]
                  }
                },
                VolumeMounts =
                [
                  new V1VolumeMount
                  {
                    Name = "config",
                    MountPath = "/etc/nginx/conf.d",
                    ReadOnlyProperty = true
                  },
                  new V1VolumeMount
                  {
                    Name = "tmp",
                    MountPath = "/tmp"
                  }
                ]
              }
            ],
            Volumes =
            [
              new V1Volume
              {
                Name = "config",
                ConfigMap = new V1ConfigMapVolumeSource
                {
                  Name = "nginx-config"
                }
              },
              new V1Volume
              {
                Name = "tmp",
                EmptyDir = new V1EmptyDirVolumeSource()
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "daemon-set-volumes.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated DaemonSet object with minimal configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalConfiguration_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new V1DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new V1ObjectMeta
      {
        Name = "minimal-daemon-set"
      },
      Spec = new V1DaemonSetSpec
      {
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "minimal"
          }
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "minimal"
            }
          },
          Spec = new V1PodSpec
          {
            Containers =
            [
              new V1Container
              {
                Name = "app",
                Image = "busybox"
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "daemon-set-minimal.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}

