using System.Text;
using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.SecretGeneratorTests;


/// <summary>
/// Tests for the <see cref="SecretGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Secret object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new V1Secret
    {
      ApiVersion = "v1",
      Kind = "Secret",
      Metadata = new V1ObjectMeta
      {
        Name = "secret",
        NamespaceProperty = "default"
      },
      Type = "Opaque",
      Immutable = true,
      Data = new Dictionary<string, byte[]>
      {
        ["key"] = Encoding.UTF8.GetBytes("value")
      },
      StringData = new Dictionary<string, string>
      {
        ["key"] = "value"
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "secret.yaml");
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
