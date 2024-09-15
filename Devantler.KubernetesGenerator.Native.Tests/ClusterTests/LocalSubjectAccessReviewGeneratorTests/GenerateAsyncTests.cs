using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.LocalSubjectAccessReviewGeneratorTests;


/// <summary>
/// Tests for the <see cref="LocalSubjectAccessReviewGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated LocalSubjectAccessReview object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidLocalSubjectAccessReview()
  {
    // Arrange
    var generator = new LocalSubjectAccessReviewGenerator();
    var model = new V1LocalSubjectAccessReview
    {
      ApiVersion = "authorization.k8s.io/v1",
      Kind = "LocalSubjectAccessReview",
      Metadata = new V1ObjectMeta
      {
        Name = "local-subject-access-review",
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
        User = "user"
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "local-subject-access-review.yaml");
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
