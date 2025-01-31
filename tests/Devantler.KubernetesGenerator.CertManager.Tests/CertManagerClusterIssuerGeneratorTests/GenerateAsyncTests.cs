using Devantler.KubernetesGenerator.CertManager.Models;
using k8s.Models;

namespace Devantler.KubernetesGenerator.CertManager.Tests.CertManagerClusterIssuerGeneratorTests;

/// <summary>
/// Tests for <see cref="CertManagerClusterIssuerGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly CertManagerClusterIssuerGenerator _generator = new();

  /// <summary>
  /// Tests that <see cref="CertManagerClusterIssuerGenerator"/> generates a valid Cert Manager ClusterIssuer object.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCertManagerClusterIssuerFile()
  {
    // Arrange
    var certManagerClusterIssuer = new CertManagerClusterIssuer
    {
      Metadata = new V1ObjectMeta
      {
        Name = "cluster-issuer",
        NamespaceProperty = "default"
      },
      Spec = new CertManagerClusterIssuerSpec
      {
        SelfSigned = new object()
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "cert-manager-cluster-issuer.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(certManagerClusterIssuer, outputPath);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("cert-manager-cluster-issuer.yaml");

    // Cleanup
    File.Delete(outputPath);
  }
}
