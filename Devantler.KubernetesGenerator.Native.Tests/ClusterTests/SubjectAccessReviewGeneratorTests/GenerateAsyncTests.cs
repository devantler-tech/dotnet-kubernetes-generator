using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.SubjectAccessReviewGeneratorTests;


/// <summary>
/// Tests for the <see cref="SubjectAccessReviewGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated SubjectAccessReview object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidSubjectAccessReview()
  {
    // Arrange
    var generator = new SubjectAccessReviewGenerator();
    var model = new V1SubjectAccessReview
    {
      ApiVersion = "authorization.k8s.io/v1",
      Kind = "SubjectAccessReview",
      Metadata = new V1ObjectMeta
      {
        Name = "subject-access-review",
        NamespaceProperty = "default"
      },
      Spec = new V1SubjectAccessReviewSpec
      {
        ResourceAttributes = new V1ResourceAttributes
        {
          Group = "group",
          Name = "name",
          NamespaceProperty = "namespace",
          Resource = "resource",
          Subresource = "subresource",
          Verb = "verb",
          Version = "version"
        },
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "subject-access-review.yaml");
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
