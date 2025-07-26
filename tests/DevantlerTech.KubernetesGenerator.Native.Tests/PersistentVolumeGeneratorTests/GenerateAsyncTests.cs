using DevantlerTech.KubernetesGenerator.Native.Models;

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
      Metadata = new Metadata
      {
        Name = "persistent-volume",
        Namespace = "default"
      },
      Spec = new PersistentVolumeSpec
      {
        AccessModes = ["ReadWriteOnce"],
        Capacity = new Dictionary<string, string>
        {
          ["storage"] = "1Gi"
        },
        ClaimRef = new PersistentVolumeClaimRef
        {
          Name = "pvc",
          Namespace = "default"
        },
        PersistentVolumeReclaimPolicy = "Retain",
        StorageClassName = "storage-class",
        MountOptions = ["option"],
        NodeAffinity = new PersistentVolumeNodeAffinity
        {
          Required = new PersistentVolumeNodeSelector
          {
            NodeSelectorTerms =
            [
              new PersistentVolumeNodeSelectorTerm
              {
                MatchExpressions =
                [
                  new PersistentVolumeNodeSelectorRequirement
                  {
                    Key = "key",
                    Operator = "In",
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
}
