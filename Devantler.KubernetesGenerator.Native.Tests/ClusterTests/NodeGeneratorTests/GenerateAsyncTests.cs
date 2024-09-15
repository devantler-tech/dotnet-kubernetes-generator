using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.NodeGeneratorTests;


/// <summary>
/// Tests for the <see cref="NodeGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Node object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidNode()
  {
    // Arrange
    var generator = new NodeGenerator();
    var model = new V1Node
    {
      ApiVersion = "v1",
      Kind = "Node",
      Metadata = new V1ObjectMeta
      {
        Name = "node",
        NamespaceProperty = "default"
      },
      Spec = new V1NodeSpec
      {
        PodCIDR = "pod-cidr",
        ConfigSource = new V1NodeConfigSource
        {
          ConfigMap = new V1ConfigMapNodeConfigSource
          {
            KubeletConfigKey = "kubelet-config-key",
            Name = "config-map-name",
            NamespaceProperty = "default"
          }
        },
        ExternalID = "external-id",
        ProviderID = "provider-id",
        PodCIDRs = ["pod-cidr"],
        Taints =
         [
           new V1Taint
           {
             Effect = "NoSchedule",
             Key = "key",
             Value = "value"
           }
         ],
        Unschedulable = true,
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "node.yaml");
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
