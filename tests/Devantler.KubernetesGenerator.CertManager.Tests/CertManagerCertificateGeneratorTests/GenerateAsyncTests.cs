using Devantler.KubernetesGenerator.CertManager.Models;
using Devantler.KubernetesGenerator.CertManager.Models.IssuerRef;
using k8s.Models;

namespace Devantler.KubernetesGenerator.CertManager.Tests.CertManagerCertificateGeneratorTests;

/// <summary>
/// Tests for <see cref="CertManagerCertificateGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly CertManagerCertificateGenerator _generator = new();

  /// <summary>
  /// Tests that <see cref="CertManagerCertificateGenerator"/> generates a valid Cert Manager Certificate object.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCertManagerCertificateFile()
  {
    // Arrange
    var certManagerCertificate = new CertManagerCertificate
    {
      Metadata = new V1ObjectMeta
      {
        Name = "certificate",
        NamespaceProperty = "default"
      },
      Spec = new CertManagerCertificateSpec
      {
        SecretName = "certificate-secret",
        DnsNames = [
          "example.com",
          "*.example.com"
        ],
        IssuerRef = new CertManagerIssuerRef
        {
          Name = "issuer",
          Kind = "ClusterIssuer"
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "some-path", "cert-manager-certificate.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await _generator.GenerateAsync(certManagerCertificate, outputPath);
    string kustomizationFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kustomizationFromFile, extension: "yaml").UseFileName("cert-manager-certificate.yaml");

    // Cleanup
    File.Delete(outputPath);
  }
}
