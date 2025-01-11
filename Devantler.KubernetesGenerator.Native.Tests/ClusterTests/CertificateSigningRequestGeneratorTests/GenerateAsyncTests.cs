using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.CertificateSigningRequestGeneratorTests;


/// <summary>
/// Tests for the <see cref="CertificateSigningRequestGenerator"/> method.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated certificate signing request.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCertificateSigningRequest()
  {
    // Arrange
    var generator = new CertificateSigningRequestGenerator();
    var model = new V1CertificateSigningRequest
    {
      ApiVersion = "certificates.k8s.io/v1",
      Kind = "CertificateSigningRequest",
      Metadata = new V1ObjectMeta
      {
        Name = "test-csr",
        NamespaceProperty = "default"
      },
      Spec = new V1CertificateSigningRequestSpec
      {
        ExpirationSeconds = 3600,
        Extra = new Dictionary<string, IList<string>>
      {
        { "extra", new List<string> { "extra" } }
      },
        Request = [0x01, 0x02, 0x03],
        Usages = ["client auth"],
        Groups = ["group"],
        SignerName = "signer",
        Uid = "uid",
        Username = "username"
      }
    };

    // Act
    string fileName = "csr.yaml";
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
