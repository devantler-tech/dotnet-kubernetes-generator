using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PersistentVolumeClaimGeneratorTests;


/// <summary>
/// Tests for the <see cref="PersistentVolumeClaimGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PersistentVolumeClaim object with comprehensive properties set.
  /// Tests all major PersistentVolumeClaim features including multiple access modes, volume modes,
  /// data sources, resource requirements, selectors, and storage class configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithComprehensiveProperties_ShouldGenerateAValidPersistentVolumeClaim()
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
        AccessModes = [
          PersistentVolumeAccessMode.ReadWriteOnce,
          PersistentVolumeAccessMode.ReadOnlyMany
        ],
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
            ["storage"] = "5Gi"
          },
          Limits = new Dictionary<string, string>
          {
            ["storage"] = "10Gi"
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
        VolumeMode = VolumeMode.Block,
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the PersistentVolumeClaim name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new PersistentVolumeClaim
    {
      Metadata = new Metadata
      {
        Name = ""
      },
      Spec = new PersistentVolumeClaimSpec
      {
        AccessModes = [PersistentVolumeAccessMode.ReadWriteOnce]
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
