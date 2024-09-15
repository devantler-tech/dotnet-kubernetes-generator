using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.ClusterTrustBundleGeneratorTests;


/// <summary>
/// Tests for the <see cref="ClusterTrustBundleGenerator"/> method.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ClusterTrustBundle object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidClusterTrustBundle()
  {
    // Arrange
    var generator = new ClusterTrustBundleGenerator();
    var model = new V1alpha1ClusterTrustBundle
    {
      ApiVersion = "certificates.k8s.io/v1alpha1",
      Kind = "ClusterTrustBundle",
      Metadata = new V1ObjectMeta
      {
        Name = "test-cluster-trust-bundle",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha1ClusterTrustBundleSpec
      {
        SignerName = "signer",
        TrustBundle = "trust-bundle"
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "cluster-trust-bundle.yaml");
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
