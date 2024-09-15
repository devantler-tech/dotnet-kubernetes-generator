using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.ServiceAccountGeneratorTests;


/// <summary>
/// Tests for the <see cref="ServiceAccountGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ServiceAccount object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidServiceAccount()
  {
    // Arrange
    var generator = new ServiceAccountGenerator();
    var model = new V1ServiceAccount
    {
      ApiVersion = "v1",
      Kind = "ServiceAccount",
      Metadata = new V1ObjectMeta
      {
        Name = "self-subject-review",
        NamespaceProperty = "default"
      },
      AutomountServiceAccountToken = true,
      ImagePullSecrets =
      [
        new V1LocalObjectReference
        {
          Name = "image-pull-secret"
        }
      ],
      Secrets =
      [
        new V1ObjectReference
        {
          ApiVersion = "v1",
          Kind = "Secret",
          Name = "secret",
          NamespaceProperty = "default",
          FieldPath = "secret"
        }
      ]
    };

    // Act
    string fileName = "service-account.yaml";
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
