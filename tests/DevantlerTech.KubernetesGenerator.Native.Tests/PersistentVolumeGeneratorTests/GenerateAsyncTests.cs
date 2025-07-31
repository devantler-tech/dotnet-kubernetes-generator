
namespace DevantlerTech.KubernetesGenerator.Native.Tests.PersistentVolumeGeneratorTests;

/// <summary>
/// Tests for the <see cref="PersistentVolumeGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PersistentVolume object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "persistent-volume"
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "1Gi"
        },
        ClaimRef = new PersistentVolumeClaimRef
        {
          Name = "pvc",
          Namespace = "default"
        },
        PersistentVolumeReclaimPolicy = PersistentVolumeReclaimPolicy.Retain,
        StorageClassName = "storage-class",
        MountOptions = ["option"],
        NodeAffinity = new PersistentVolumeNodeAffinity
        {
          Required = new PersistentVolumeNodeAffinityNodeSelector
          {
            NodeSelectorTerms =
            [
              new NodeSelectorTerm
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
  /// Verifies the generated PersistentVolume with host path configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithHostPath_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-hostpath",
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "5Gi"
        },
        PersistentVolumeReclaimPolicy = PersistentVolumeReclaimPolicy.Delete,
        HostPath = new PersistentVolumeHostPath
        {
          Path = "/mnt/data",
          Type = PersistentVolumeHostPathType.DirectoryOrCreate
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
  /// Verifies the generated PersistentVolume with NFS configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNfs_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-nfs",
        Labels = new Dictionary<string, string>
        {
          ["app"] = "storage"
        }
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteMany],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "10Gi"
        },
        PersistentVolumeReclaimPolicy = PersistentVolumeReclaimPolicy.Recycle,
        Nfs = new PersistentVolumeNfs
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
  /// Verifies the generated PersistentVolume with Local volume source.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLocal_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-local"
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOncePod],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "50Gi"
        },
        PersistentVolumeReclaimPolicy = PersistentVolumeReclaimPolicy.Delete,
        StorageClassName = "local-storage",
        Local = new PersistentVolumeLocal
        {
          Path = "/mnt/local-storage",
          FsType = "ext4"
        },
        NodeAffinity = new PersistentVolumeNodeAffinity
        {
          Required = new PersistentVolumeNodeAffinityNodeSelector
          {
            NodeSelectorTerms =
            [
              new NodeSelectorTerm
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
  /// Verifies the generated PersistentVolume with multiple access modes.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleAccessModes_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-multi-access"
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [
          PersistentVolumeAccessMode.ReadOnlyMany,
          PersistentVolumeAccessMode.ReadWriteMany
        ],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "100Gi"
        },
        PersistentVolumeReclaimPolicy = PersistentVolumeReclaimPolicy.Retain,
        Nfs = new PersistentVolumeNfs
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
  /// Verifies the generated PersistentVolume with different HostPath types.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDifferentHostPathTypes_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-hostpath-file"
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "1Gi"
        },
        PersistentVolumeReclaimPolicy = PersistentVolumeReclaimPolicy.Delete,
        HostPath = new PersistentVolumeHostPath
        {
          Path = "/var/log/app.log",
          Type = PersistentVolumeHostPathType.File
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
  /// Verifies the generated PersistentVolume with advanced node affinity operators.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAdvancedNodeAffinity_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-advanced-affinity"
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "20Gi"
        },
        PersistentVolumeReclaimPolicy = PersistentVolumeReclaimPolicy.Retain,
        HostPath = new PersistentVolumeHostPath
        {
          Path = "/mnt/ssd-storage",
          Type = PersistentVolumeHostPathType.Directory
        },
        NodeAffinity = new PersistentVolumeNodeAffinity
        {
          Required = new PersistentVolumeNodeAffinityNodeSelector
          {
            NodeSelectorTerms =
            [
              new NodeSelectorTerm
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
  /// Verifies the generated PersistentVolume with minimal configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalConfiguration_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pv-minimal"
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "1Gi"
        },
        HostPath = new PersistentVolumeHostPath
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
