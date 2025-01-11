using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.ValidatingAdmissionPolicyGeneratorTests;


/// <summary>
/// Tests for the <see cref="ValidatingAdmissionPolicyGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ValidatingAdmissionPolicy object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidValidatingAdmissionPolicy()
  {
    // Arrange
    var generator = new ValidatingAdmissionPolicyGenerator();
    var model = new V1ValidatingAdmissionPolicy
    {
      ApiVersion = "admissionregistration.k8s.io/v1",
      Kind = "ValidatingAdmissionPolicy",
      Metadata = new V1ObjectMeta
      {
        Name = "validating-admission-policy",
        NamespaceProperty = "default"
      },
      Spec = new V1ValidatingAdmissionPolicySpec
      {
        AuditAnnotations =
        [
          new V1AuditAnnotation
          {
            Key = "audit.k8s.io/level",
            ValueExpression = "RequestReceived"
          }
        ],
        Validations =
        [
          new V1Validation
          {
            Message = "The request is not allowed",
            Reason = "Forbidden",
          }
        ]
      }
    };

    // Act
    string fileName = "validating-admission-policy.yaml";
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
