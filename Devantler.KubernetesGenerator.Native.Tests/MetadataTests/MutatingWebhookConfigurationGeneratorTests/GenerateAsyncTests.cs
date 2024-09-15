using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.MutatingWebhookConfigurationGeneratorTests;


/// <summary>
/// Tests for the <see cref="MutatingWebhookConfigurationGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated MutatingWebhookConfiguration object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidMutatingWebhookConfiguration()
  {
    // Arrange
    var generator = new MutatingWebhookConfigurationGenerator();
    var model = new V1MutatingWebhookConfiguration
    {
      ApiVersion = "admissionregistration.k8s.io/v1",
      Kind = "MutatingWebhookConfiguration",
      Metadata = new V1ObjectMeta
      {
        Name = "mutating-webhook-configuration",
        NamespaceProperty = "default"
      },
      Webhooks =
      [
        new V1MutatingWebhook
        {
          Name = "mutating-webhook",
          ClientConfig = new Admissionregistrationv1WebhookClientConfig
          {
            Service = new Admissionregistrationv1ServiceReference
            {
              Name = "mutating-webhook-service",
              NamespaceProperty = "default",
              Path = "/mutate"
            }
          },
          Rules =
          [
            new V1RuleWithOperations
            {
              Operations = ["CREATE"],
              ApiGroups = ["apps"],
              ApiVersions = ["v1"],
              Resources = ["deployments"]
            }
          ],
          SideEffects = "None",
          AdmissionReviewVersions = ["v1"],
          TimeoutSeconds = 30,
          FailurePolicy = "Fail",
          MatchPolicy = "Exact",
          ReinvocationPolicy = "Never",
          MatchConditions =
          [
            new V1MatchCondition
            {
              Expression = "request.object.metadata.labels['app'] == 'nginx'",
              Name = "app-label"
            }
          ],
          NamespaceSelector = new V1LabelSelector
          {
            MatchLabels = new Dictionary<string, string>
            {
              ["app"] = "nginx"
            }
          },
          ObjectSelector = new V1LabelSelector
          {
            MatchLabels = new Dictionary<string, string>
            {
              ["app"] = "nginx"
            }
          }
        }
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "mutating-webhook-configuration.yaml");
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
