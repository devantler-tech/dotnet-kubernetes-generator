using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.ValidatingWebhookConfigurationGeneratorTests;


/// <summary>
/// Tests for the <see cref="ValidatingWebhookConfigurationGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ValidatingWebhookConfiguration object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidValidatingWebhookConfiguration()
  {
    // Arrange
    var generator = new ValidatingWebhookConfigurationGenerator();
    var model = new V1ValidatingWebhookConfiguration
    {
      ApiVersion = "admissionregistration.k8s.io/v1",
      Kind = "ValidatingWebhookConfiguration",
      Metadata = new V1ObjectMeta
      {
        Name = "validating-webhook-configuration",
        NamespaceProperty = "default"
      },
      Webhooks =
      [
        new V1ValidatingWebhook
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
    string fileName = "validating-webhook-configuration.yaml";
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
