using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.FlowSchemaGeneratorTests;


/// <summary>
/// Tests for the <see cref="FlowSchemaGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated FlowSchema object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFlowSchema()
  {
    // Arrange
    var generator = new FlowSchemaGenerator();
    var model = new V1FlowSchema
    {
      ApiVersion = "flowcontrol.apiserver.k8s.io/v1",
      Kind = "FlowSchema",
      Metadata = new V1ObjectMeta
      {
        Name = "flow-schema",
        NamespaceProperty = "default"
      },
      Spec = new V1FlowSchemaSpec
      {
        DistinguisherMethod = new V1FlowDistinguisherMethod
        {
          Type = "ByUser"
        },
        MatchingPrecedence = 100,
        Rules =
        [
          new V1PolicyRulesWithSubjects
          {
            NonResourceRules =
            [
              new V1NonResourcePolicyRule
              {
                Verbs = ["get", "list"],
                NonResourceURLs = ["/healthz"]
              }
            ],
            ResourceRules =
            [
              new V1ResourcePolicyRule
              {
                Verbs = ["get", "list"],
                Resources = ["pods"]
              }
            ],
            Subjects =
            [
              new Flowcontrolv1Subject
              {
                Kind = "User",
                Group = new V1GroupSubject
                {
                  Name = "system:authenticated"
                },
                ServiceAccount = new V1ServiceAccountSubject
                {
                  NamespaceProperty = "default",
                  Name = "default"
                },
                User = new V1UserSubject
                {
                  Name = "admin"
                }
              }
            ]
          }
        ],
        PriorityLevelConfiguration = new V1PriorityLevelConfigurationReference
        {
          Name = "priority-level-configuration"
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "flow-schema.yaml");
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
