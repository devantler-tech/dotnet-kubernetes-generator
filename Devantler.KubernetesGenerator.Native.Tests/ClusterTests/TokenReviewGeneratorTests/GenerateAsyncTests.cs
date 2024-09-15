using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.TokenReviewGeneratorTests;


/// <summary>
/// Tests for the <see cref="TokenReviewGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated TokenReview object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidTokenReview()
  {
    // Arrange
    var generator = new TokenReviewGenerator();
    var model = new V1TokenReview
    {
      ApiVersion = "authentication.k8s.io/v1",
      Kind = "TokenReview",
      Metadata = new V1ObjectMeta
      {
        Name = "token-review",
        NamespaceProperty = "default"
      },
      Spec = new V1TokenReviewSpec
      {
        Token = "token",
        Audiences = ["audience"]
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "token-review.yaml");
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
