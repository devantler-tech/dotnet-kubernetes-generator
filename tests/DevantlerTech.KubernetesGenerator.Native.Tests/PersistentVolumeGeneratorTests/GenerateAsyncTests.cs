using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PersistentVolumeGeneratorTests;

/// <summary>
/// Tests for the <see cref="PersistentVolumeGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PersistentVolume object with basic properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicProperties_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume("test-pv")
    {
      Metadata = new Metadata
      {
        Name = "test-pv",
        Namespace = "default",
        Labels = new Dictionary<string, string>
        {
          ["app"] = "test"
        }
      },
      Capacity = new ResourceQuantity("1Gi"),
      AccessModes = ["ReadWriteOnce"],
      ReclaimPolicy = "Retain",
      StorageClassName = "standard",
      VolumeMode = "Filesystem",
      HostPath = new HostPathVolumeSource
      {
        Path = "/tmp/data",
        Type = "DirectoryOrCreate"
      }
    };

    // Act
    string fileName = "persistent-volume-basic.yaml";
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
  /// Verifies the generated PersistentVolume object with NFS storage.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNfsStorage_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume("nfs-pv")
    {
      Metadata = new Metadata
      {
        Name = "nfs-pv",
        Namespace = "default"
      },
      Capacity = new ResourceQuantity("5Gi"),
      AccessModes = ["ReadWriteMany"],
      ReclaimPolicy = "Retain",
      StorageClassName = "nfs",
      Nfs = new NfsVolumeSource
      {
        Server = "nfs-server.example.com",
        Path = "/exports/data",
        ReadOnly = false
      }
    };

    // Act
    string fileName = "persistent-volume-nfs.yaml";
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
  /// Verifies the generated PersistentVolume object with local storage and node affinity.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLocalStorageAndNodeAffinity_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume("local-pv")
    {
      Metadata = new Metadata
      {
        Name = "local-pv",
        Namespace = "default",
        Annotations = new Dictionary<string, string>
        {
          ["description"] = "Local storage persistent volume"
        }
      },
      Capacity = new ResourceQuantity("10Gi"),
      AccessModes = ["ReadWriteOnce"],
      ReclaimPolicy = "Delete",
      StorageClassName = "local-storage",
      Local = new LocalVolumeSource
      {
        Path = "/mnt/local-disk",
        FsType = "ext4"
      },
      NodeAffinity = new NodeAffinity
      {
        Required = new List<NodeSelectorTerm>
        {
          new NodeSelectorTerm
          {
            MatchExpressions = new List<NodeSelectorRequirement>
            {
              new NodeSelectorRequirement
              {
                Key = "kubernetes.io/hostname",
                Operator = "In",
                Values = ["worker-node-1"]
              }
            }
          }
        }
      }
    };

    // Act
    string fileName = "persistent-volume-local.yaml";
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
  /// Verifies that an exception is thrown when model is null.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    PersistentVolume model = null!;

    // Act & Assert
    await Assert.ThrowsAsync<ArgumentNullException>(() =>
      generator.GenerateAsync(model, "test-output.yaml"));
  }

  /// <summary>
  /// Verifies that an exception is thrown when persistent volume name is not provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume("test-pv")
    {
      Metadata = new Metadata
      {
        Name = "" // Empty name
      }
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, "test-output.yaml"));
    
    Assert.Contains("The model.Metadata.Name must be set", exception.Message);
  }
}
