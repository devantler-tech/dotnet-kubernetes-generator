using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.SelfSubjectReviewGeneratorTests;


/// <summary>
/// Tests for the <see cref="SelfSubjectReviewGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated SelfSubjectReview object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidSelfSubjectReview()
  {
    // Arrange
    var generator = new SelfSubjectReviewGenerator();
    var model = new V1SelfSubjectReview
    {
      ApiVersion = "authentication.k8s.io/v1",
      Kind = "SelfSubjectReview",
      Metadata = new V1ObjectMeta
      {
        Name = "self-subject-review",
        NamespaceProperty = "default"
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "self-subject-review.yaml");
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
