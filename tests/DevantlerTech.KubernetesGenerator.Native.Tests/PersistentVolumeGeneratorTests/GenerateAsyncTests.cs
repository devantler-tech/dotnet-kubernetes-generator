using DevantlerTech.KubernetesGenerator.Native.Models;

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
      Metadata = new Metadata
      {
        Name = "persistent-volume",
        Namespace = "default"
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
        PersistentVolumeReclaimPolicy = Models.PersistentVolumeReclaimPolicy.Retain,
        StorageClassName = "storage-class",
        MountOptions = ["option"],
        NodeAffinity = new PersistentVolumeNodeAffinity
        {
          Required = new PersistentVolumeNodeAffinityNodeSelector
          {
            NodeSelectorTerms =
            [
              new PersistentVolumeNodeAffinityNodeSelectorTerm
              {
                MatchExpressions =
                [
                  new PersistentVolumeNodeAffinityNodeSelectorRequirement
                  {
                    Key = "key",
                    Operator = PersistentVolumeNodeAffinityNodeSelectorRequirementOperator.In,
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
      Metadata = new Metadata
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
        PersistentVolumeReclaimPolicy = Models.PersistentVolumeReclaimPolicy.Delete,
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
      Metadata = new Metadata
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
        PersistentVolumeReclaimPolicy = Models.PersistentVolumeReclaimPolicy.Recycle,
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
}
