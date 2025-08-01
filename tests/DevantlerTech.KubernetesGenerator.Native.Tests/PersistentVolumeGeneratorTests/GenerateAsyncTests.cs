
using DevantlerTech.KubernetesGenerator.Native.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;
using DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolumeClaim;
using DevantlerTech.KubernetesGenerator.Native.Models.PriorityClass;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PersistentVolumeGeneratorTests;

/// <summary>
/// Tests for the <see cref="PersistentVolumeGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativePersistentVolume object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "persistent-volume"
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [NativePersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "1Gi"
        },
        ClaimRef = new NativePersistentVolumeClaimRef
        {
          Name = "pvc",
          Namespace = "default"
        },
        PersistentVolumeReclaimPolicy = NativePersistentVolumeReclaimPolicy.Retain,
        StorageClassName = "storage-class",
        MountOptions = ["option"],
        NodeAffinity = new NativePersistentVolumeNodeAffinity
        {
          Required = new NativePersistentVolumeNodeAffinityNodeSelector
          {
            NodeSelectorTerms =
            [
              new NativeNodeSelectorTerm
              {
                MatchExpressions =
                [
                  new MatchExpression
                  {
                    Key = "key",
                    Operator = MatchExpressionOperator.In,
                    Values = ["value"]
                  }
                ]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "persistent-volume.yaml";
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
  /// Verifies the generated NativePersistentVolume with host path configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithHostPath_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-hostpath",
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [NativePersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "5Gi"
        },
        PersistentVolumeReclaimPolicy = NativePersistentVolumeReclaimPolicy.Delete,
        HostPath = new NativePersistentVolumeHostPath
        {
          Path = "/mnt/data",
          Type = NativePersistentVolumeHostPathType.DirectoryOrCreate
        }
      }
    };

    // Act
    string fileName = "pv-hostpath.yaml";
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
  /// Verifies the generated NativePersistentVolume with NFS configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNfs_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-nfs",
        Labels = new Dictionary<string, string>
        {
          ["app"] = "storage"
        }
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [NativePersistentVolumeAccessMode.ReadWriteMany],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "10Gi"
        },
        PersistentVolumeReclaimPolicy = NativePersistentVolumeReclaimPolicy.Recycle,
        Nfs = new NativePersistentVolumeNfs
        {
          Server = "nfs-server.example.com",
          Path = "/shared/data",
          ReadOnly = false
        }
      }
    };

    // Act
    string fileName = "pv-nfs.yaml";
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
  /// Verifies the generated NativePersistentVolume with Local volume source.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLocal_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-local"
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [NativePersistentVolumeAccessMode.ReadWriteOncePod],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "50Gi"
        },
        PersistentVolumeReclaimPolicy = NativePersistentVolumeReclaimPolicy.Delete,
        StorageClassName = "local-storage",
        Local = new NativePersistentVolumeLocal
        {
          Path = "/mnt/local-storage",
          FsType = "ext4"
        },
        NodeAffinity = new NativePersistentVolumeNodeAffinity
        {
          Required = new NativePersistentVolumeNodeAffinityNodeSelector
          {
            NodeSelectorTerms =
            [
              new NativeNodeSelectorTerm
              {
                MatchExpressions =
                [
                  new MatchExpression
                  {
                    Key = "kubernetes.io/hostname",
                    Operator = MatchExpressionOperator.Exists
                  }
                ]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "pv-local.yaml";
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
  /// Verifies the generated NativePersistentVolume with multiple access modes.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleAccessModes_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-multi-access"
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [
          NativePersistentVolumeAccessMode.ReadOnlyMany,
          NativePersistentVolumeAccessMode.ReadWriteMany
        ],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "100Gi"
        },
        PersistentVolumeReclaimPolicy = NativePersistentVolumeReclaimPolicy.Retain,
        Nfs = new NativePersistentVolumeNfs
        {
          Server = "shared-nfs.example.com",
          Path = "/shared/readonly",
          ReadOnly = true
        }
      }
    };

    // Act
    string fileName = "pv-multi-access.yaml";
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
  /// Verifies the generated NativePersistentVolume with different HostPath types.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDifferentHostPathTypes_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-hostpath-file"
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [NativePersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "1Gi"
        },
        PersistentVolumeReclaimPolicy = NativePersistentVolumeReclaimPolicy.Delete,
        HostPath = new NativePersistentVolumeHostPath
        {
          Path = "/var/log/app.log",
          Type = NativePersistentVolumeHostPathType.File
        }
      }
    };

    // Act
    string fileName = "pv-hostpath-file.yaml";
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
  /// Verifies the generated NativePersistentVolume with advanced node affinity operators.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAdvancedNodeAffinity_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-advanced-affinity"
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [NativePersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "20Gi"
        },
        PersistentVolumeReclaimPolicy = NativePersistentVolumeReclaimPolicy.Retain,
        HostPath = new NativePersistentVolumeHostPath
        {
          Path = "/mnt/ssd-storage",
          Type = NativePersistentVolumeHostPathType.Directory
        },
        NodeAffinity = new NativePersistentVolumeNodeAffinity
        {
          Required = new NativePersistentVolumeNodeAffinityNodeSelector
          {
            NodeSelectorTerms =
            [
              new NativeNodeSelectorTerm
              {
                MatchExpressions =
                [
                  new MatchExpression
                  {
                    Key = "node-type",
                    Operator = MatchExpressionOperator.NotIn,
                    Values = ["small", "micro"]
                  },
                  new MatchExpression
                  {
                    Key = "ssd-storage",
                    Operator = MatchExpressionOperator.DoesNotExist
                  },
                  new MatchExpression
                  {
                    Key = "storage-size",
                    Operator = MatchExpressionOperator.Gt,
                    Values = ["100"]
                  }
                ]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "pv-advanced-affinity.yaml";
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
  /// Verifies the generated NativePersistentVolume with minimal configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalConfiguration_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new NativePersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-minimal"
      },
      Spec = new NativePersistentVolumeSpec
      {
        AccessModes = [NativePersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "1Gi"
        },
        HostPath = new NativePersistentVolumeHostPath
        {
          Path = "/mnt/data"
        }
      }
    };

    // Act
    string fileName = "pv-minimal.yaml";
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
