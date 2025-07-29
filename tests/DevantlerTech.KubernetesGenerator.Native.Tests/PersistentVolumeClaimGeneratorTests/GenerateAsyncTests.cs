using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PersistentVolumeClaimGeneratorTests;


/// <summary>
/// Tests for the <see cref="PersistentVolumeClaimGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PersistentVolumeClaim object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPersistentVolumeClaim()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new PersistentVolumeClaim
    {
      ApiVersion = "v1",
      Kind = "PersistentVolumeClaim",
      Metadata = new Metadata
      {
        Name = "persistent-volume-claim",
        Namespace = "default"
      },
      Spec = new PersistentVolumeClaimSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce],
        DataSource = new TypedLocalObjectReference
        {
          ApiGroup = "storage.k8s.io",
          Kind = "StorageClass",
          Name = "storage-class"
        },
        DataSourceRef = new TypedObjectReference
        {
          ApiGroup = "storage.k8s.io",
          Kind = "PersistentVolumeClaim",
          Name = "pvc",
          Namespace = "default"
        },
        Resources = new VolumeResourceRequirements
        {
          Requests = new Dictionary<string, string>
          {
            ["storage"] = "1Gi"
          }
        },
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["key"] = "value"
          }
        },
        StorageClassName = "storage-class",
        VolumeMode = VolumeMode.Filesystem,
        VolumeName = "volume-name"
      }
    };

    // Act
    string fileName = "persistent-volume-claim.yaml";
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
  /// Verifies the generated PersistentVolumeClaim object with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidPersistentVolumeClaim()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new PersistentVolumeClaim
    {
      Metadata = new Metadata
      {
        Name = "minimal-pvc"
      },
      Spec = new PersistentVolumeClaimSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce]
      }
    };

    // Act
    string fileName = "minimal-persistent-volume-claim.yaml";
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
  /// Verifies the generated PersistentVolumeClaim object with multiple access modes.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleAccessModes_ShouldGenerateAValidPersistentVolumeClaim()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new PersistentVolumeClaim
    {
      Metadata = new Metadata
      {
        Name = "multi-access-pvc",
        Namespace = "test-namespace"
      },
      Spec = new PersistentVolumeClaimSpec
      {
        AccessModes = [
          PersistentVolumeAccessMode.ReadWriteOnce,
          PersistentVolumeAccessMode.ReadOnlyMany
        ],
        Resources = new VolumeResourceRequirements
        {
          Requests = new Dictionary<string, string>
          {
            ["storage"] = "5Gi"
          },
          Limits = new Dictionary<string, string>
          {
            ["storage"] = "10Gi"
          }
        }
      }
    };

    // Act
    string fileName = "multi-access-persistent-volume-claim.yaml";
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
  /// Verifies the generated PersistentVolumeClaim object with Block volume mode.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBlockVolumeMode_ShouldGenerateAValidPersistentVolumeClaim()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new PersistentVolumeClaim
    {
      Metadata = new Metadata
      {
        Name = "block-volume-pvc"
      },
      Spec = new PersistentVolumeClaimSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOncePod],
        VolumeMode = VolumeMode.Block,
        Resources = new VolumeResourceRequirements
        {
          Requests = new Dictionary<string, string>
          {
            ["storage"] = "100Gi"
          }
        }
      }
    };

    // Act
    string fileName = "block-volume-persistent-volume-claim.yaml";
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
