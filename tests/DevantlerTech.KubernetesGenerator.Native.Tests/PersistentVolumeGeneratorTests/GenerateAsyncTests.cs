using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PersistentVolumeGeneratorTests;


/// <summary>
/// Tests for the <see cref="PersistentVolumeGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PersistentVolume object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new V1ObjectMeta
      {
        Name = "persistent-volume",
        NamespaceProperty = "default"
      },
      Capacity = new Dictionary<string, ResourceQuantity>
      {
        ["storage"] = new ResourceQuantity("1Gi")
      },
      AccessModes = ["ReadWriteOnce"],
      ClaimRef = new V1ObjectReference
      {
        ApiVersion = "v1",
        Kind = "PersistentVolumeClaim",
        Name = "pvc",
        NamespaceProperty = "default"
      },
      PersistentVolumeReclaimPolicy = "Retain",
      StorageClassName = "storage-class",
      MountOptions = ["option"],
      NodeAffinity = new V1VolumeNodeAffinity
      {
        Required = new V1NodeSelector
        {
          NodeSelectorTerms =
          [
            new V1NodeSelectorTerm
            {
              MatchExpressions =
              [
                new V1NodeSelectorRequirement
                {
                  Key = "key",
                  OperatorProperty = "In",
                  Values = ["value"]
                }
              ]
            }
          ]
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
  /// Verifies the generated PersistentVolume object with minimal required properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new V1ObjectMeta
      {
        Name = "persistent-volume-minimal"
      },
      Capacity = new Dictionary<string, ResourceQuantity>
      {
        ["storage"] = new ResourceQuantity("1Gi")
      },
      AccessModes = ["ReadWriteOnce"],
      HostPath = new V1HostPathVolumeSource
      {
        Path = "/var/data"
      }
    };

    // Act
    string fileName = "persistent-volume-minimal.yaml";
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
  /// Verifies the generated PersistentVolume object with HostPath volume source.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithHostPathVolumeSource_ShouldGenerateAValidPersistentVolume()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new V1ObjectMeta
      {
        Name = "persistent-volume-hostpath"
      },
      Capacity = new Dictionary<string, ResourceQuantity>
      {
        ["storage"] = new ResourceQuantity("1Gi")
      },
      AccessModes = ["ReadWriteOnce"],
      HostPath = new V1HostPathVolumeSource
      {
        Path = "/var/data"
      }
    };

    // Act
    string fileName = "persistent-volume-hostpath.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the PersistentVolume model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithPersistentVolumeWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new PersistentVolumeGenerator();
    var model = new PersistentVolume
    {
      Metadata = new V1ObjectMeta
      {
        NamespaceProperty = "default"
      },
      Capacity = new Dictionary<string, ResourceQuantity>
      {
        ["storage"] = new ResourceQuantity("1Gi")
      },
      AccessModes = ["ReadWriteOnce"],
      HostPath = new V1HostPathVolumeSource
      {
        Path = "/tmp/data"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
