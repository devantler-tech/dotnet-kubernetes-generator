using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.ValidatingAdmissionPolicyBindingGeneratorTests;


/// <summary>
/// Tests for the <see cref="ValidatingAdmissionPolicyBindingGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ValidatingAdmissionPolicyBinding object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidValidatingAdmissionPolicyBinding()
  {
    // Arrange
    var generator = new ValidatingAdmissionPolicyBindingGenerator();
    var model = new V1ValidatingAdmissionPolicyBinding
    {
      ApiVersion = "admissionregistration.k8s.io/v1",
      Kind = "ValidatingAdmissionPolicyBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "validating-admission-policy-binding",
        NamespaceProperty = "default"
      },
      Spec = new V1ValidatingAdmissionPolicyBindingSpec
      {
        PolicyName = "validating-admission-policy",
        ValidationActions = ["create", "update"],
        MatchResources = new V1MatchResources
        {
          MatchPolicy = "Exact",
          NamespaceSelector = new V1LabelSelector
          {
            MatchExpressions =
            [
              new V1LabelSelectorRequirement
              {
                Key = "env",
                OperatorProperty = "In",
                Values = ["dev"]
              }
            ]
          },
        }

      }
    };

    // Act
    string fileName = "validating-admission-policy-binding.yaml";
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
