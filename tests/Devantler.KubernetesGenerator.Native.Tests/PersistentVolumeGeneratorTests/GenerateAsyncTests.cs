using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.PersistentVolumeGeneratorTests;


/// <summary>
/// Tests for the <see cref="PersistentVolumeGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
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
    var model = new V1PersistentVolume
    {
      ApiVersion = "v1",
      Kind = "PersistentVolume",
      Metadata = new V1ObjectMeta
      {
        Name = "persistent-volume",
        NamespaceProperty = "default"
      },
      Spec = new V1PersistentVolumeSpec
      {
        AccessModes = ["ReadWriteOnce"],
        Capacity = new Dictionary<string, ResourceQuantity>
        {
          ["storage"] = new ResourceQuantity("1Gi")
        },
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
        },
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
}
