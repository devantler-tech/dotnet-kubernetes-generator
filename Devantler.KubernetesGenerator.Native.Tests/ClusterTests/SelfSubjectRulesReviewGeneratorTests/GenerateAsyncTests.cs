using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.SelfSubjectRulesReviewGeneratorTests;


/// <summary>
/// Tests for the <see cref="SelfSubjectRulesReviewGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated SelfSubjectRulesReview object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidSelfSubjectRulesReview()
  {
    // Arrange
    var generator = new SelfSubjectRulesReviewGenerator();
    var model = new V1SelfSubjectRulesReview
    {
      ApiVersion = "authorization.k8s.io/v1",
      Kind = "SelfSubjectRulesReview",
      Metadata = new V1ObjectMeta
      {
        Name = "self-subject-rules-review",
        NamespaceProperty = "default"
      },
      Spec = new V1SelfSubjectRulesReviewSpec
      {
        NamespaceProperty = "default",
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "self-subject-rules-review.yaml");
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
