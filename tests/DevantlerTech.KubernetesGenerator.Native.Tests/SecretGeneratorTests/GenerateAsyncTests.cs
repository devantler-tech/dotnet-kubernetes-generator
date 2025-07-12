using DevantlerTech.KubernetesGenerator.Native.Models;
using Xunit;
using VerifyXunit;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.SecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="SecretGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Secret object using generic secret.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecret_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new SecretCreateOptions
    {
      Generic = new GenericSecretOptions
      {
        Name = "secret",
        Namespace = "default",
        Type = "Opaque"
      }
    };

    // Add a literal value to match the expected output
    model.Generic.FromLiterals["key"] = "value";

    // Act
    string fileName = "secret.yaml";
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
