using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.PersistentVolumeClaimGeneratorTests;


/// <summary>
/// Tests for the <see cref="PersistentVolumeClaimGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PersistentVolumeClaim object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPersistentVolumeClaim()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new V1PersistentVolumeClaim
    {
      ApiVersion = "v1",
      Kind = "PersistentVolumeClaim",
      Metadata = new V1ObjectMeta
      {
        Name = "persistent-volume-claim",
        NamespaceProperty = "default"
      },
      Spec = new V1PersistentVolumeClaimSpec
      {
        AccessModes = new List<string> { "ReadWriteOnce" },
        DataSource = new V1TypedLocalObjectReference
        {
          ApiGroup = "storage.k8s.io",
          Kind = "StorageClass",
          Name = "storage-class"
        },
        DataSourceRef = new V1TypedObjectReference
        {
          ApiGroup = "storage.k8s.io",
          Kind = "PersistentVolumeClaim",
          Name = "pvc",
          NamespaceProperty = "default"
        },
        Resources = new V1VolumeResourceRequirements
        {
          Requests = new Dictionary<string, ResourceQuantity>
          {
            ["storage"] = new ResourceQuantity("1Gi")
          }
        },
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["key"] = "value"
          }
        },
        StorageClassName = "storage-class",
        VolumeMode = "Filesystem",
        VolumeName = "volume-name"
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "persistent-volume-claim.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}
