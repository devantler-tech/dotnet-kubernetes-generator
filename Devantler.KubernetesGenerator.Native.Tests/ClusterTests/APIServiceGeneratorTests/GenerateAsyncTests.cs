using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.APIServiceGeneratorTests;


/// <summary>
/// Tests for the <see cref="APIServiceGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated APIService object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidAPIService()
  {
    // Arrange
    var generator = new APIServiceGenerator();
    var model = new V1APIService
    {
      ApiVersion = "v1",
      Kind = "APIService",
      Metadata = new V1ObjectMeta
      {
        Name = "api-service",
        NamespaceProperty = "default"
      },
      Spec = new V1APIServiceSpec
      {
        CaBundle = [1, 2, 3],
        Group = "group",
        GroupPriorityMinimum = 1,
        InsecureSkipTLSVerify = true,
        Service = new Apiregistrationv1ServiceReference
        {
          Name = "service",
          NamespaceProperty = "namespace"
        },
        Version = "version",
        VersionPriority = 1
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "api-service.yaml");
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
