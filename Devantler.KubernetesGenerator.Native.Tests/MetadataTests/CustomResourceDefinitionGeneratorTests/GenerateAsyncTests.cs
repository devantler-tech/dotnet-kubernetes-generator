using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.CustomResourceDefinitionGeneratorTests;


/// <summary>
/// Tests for the <see cref="CustomResourceDefinitionGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated CustomResourceDefinition object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCustomResourceDefinition()
  {
    // Arrange
    var generator = new CustomResourceDefinitionGenerator();
    var model = new V1CustomResourceDefinition
    {
      ApiVersion = "apiextensions.k8s.io/v1",
      Kind = "CustomResourceDefinition",
      Metadata = new V1ObjectMeta
      {
        Name = "custom-resource-definition",
        NamespaceProperty = "default"
      },
      Spec = new V1CustomResourceDefinitionSpec
      {
        Group = "example.com",
        Names = new V1CustomResourceDefinitionNames
        {
          Kind = "Example",
          ListKind = "ExampleList",
          Plural = "examples",
          Singular = "example"
        },
        Scope = "Namespaced",
        Conversion = new V1CustomResourceConversion
        {
          Strategy = "Webhook",
          Webhook = new V1WebhookConversion
          {
            ConversionReviewVersions = ["v1"],
            ClientConfig = new Apiextensionsv1WebhookClientConfig
            {
              Service = new Apiextensionsv1ServiceReference
              {
                Name = "webhook-service",
                NamespaceProperty = "default",
                Path = "/convert"
              },
              CaBundle = [0x01, 0x02, 0x03],
              Url = "https://webhook-service.default.svc:443/convert"
            }
          }
        },
        PreserveUnknownFields = false,
        Versions =
        [
          new V1CustomResourceDefinitionVersion
          {
            Name = "v1",
            Served = true,
            Storage = true,
            Schema = new V1CustomResourceValidation
            {
              OpenAPIV3Schema = new V1JSONSchemaProps
              {
                Type = "object",
                Properties = new Dictionary<string, V1JSONSchemaProps>
                {
                  ["spec"] = new V1JSONSchemaProps
                  {
                    Type = "object",
                    Properties = new Dictionary<string, V1JSONSchemaProps>
                    {
                      ["replicas"] = new V1JSONSchemaProps
                      {
                        Type = "integer",
                        Format = "int32"
                      }
                    }
                  },
                  ["status"] = new V1JSONSchemaProps
                  {
                    Type = "object",
                    Properties = new Dictionary<string, V1JSONSchemaProps>
                    {
                      ["replicas"] = new V1JSONSchemaProps
                      {
                        Type = "integer",
                        Format = "int32"
                      }
                    }
                  }
                }
              }
            }
          }
        ]
      }
    };

    // Act
    string fileName = "custom-resource-definition.yaml";
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
