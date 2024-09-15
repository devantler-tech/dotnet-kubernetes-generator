using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.SelfSubjectAccessReviewGeneratorTests;


/// <summary>
/// Tests for the <see cref="SelfSubjectAccessReviewGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated SelfSubjectAccessReview object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidSelfSubjectAccessReview()
  {
    // Arrange
    var generator = new SelfSubjectAccessReviewGenerator();
    var model = new V1SelfSubjectAccessReview
    {
      ApiVersion = "authorization.k8s.io/v1",
      Kind = "SelfSubjectAccessReview",
      Metadata = new V1ObjectMeta
      {
        Name = "self-subject-access-review",
        NamespaceProperty = "default"
      },
      Spec = new V1SelfSubjectAccessReviewSpec
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
        }
      },
    };

    // Act
    string fileName = "self-subject-access-review.yaml";
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
