using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ServiceAccountGeneratorTests;

/// <summary>
/// Tests for the <see cref="ServiceAccountGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Test data for valid ServiceAccount generation scenarios.
  /// </summary>
  public static TheoryData<string, string?, string> ValidServiceAccountData =>
    new()
    {
      { "self-subject-review", "default", "service-account.yaml" },
      { "simple-service-account", null, "service-account-no-namespace.yaml" }
    };

  /// <summary>
  /// Verifies the generated ServiceAccount object with different configurations.
  /// </summary>
  /// <param name="name">The ServiceAccount name.</param>
  /// <param name="namespaceName">The namespace name (can be null).</param>
  /// <param name="fileName">The expected output file name.</param>
  /// <returns></returns>
  [Theory]
  [MemberData(nameof(ValidServiceAccountData))]
  public async Task GenerateAsync_WithValidConfiguration_ShouldGenerateAValidServiceAccount(string name, string? namespaceName, string fileName)
  {
    // Arrange
    var generator = new ServiceAccountGenerator();
    var model = new V1ServiceAccount
    {
      ApiVersion = "v1",
      Kind = "ServiceAccount",
      Metadata = new V1ObjectMeta
      {
        Name = name,
        NamespaceProperty = namespaceName
      }
    };

    // Act & Assert
    await GenerateAndVerifyAsync(generator, model, fileName);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ServiceAccountGenerator();
    var model = new V1ServiceAccount
    {
      ApiVersion = "v1",
      Kind = "ServiceAccount"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Helper method to generate a file and verify its content.
  /// </summary>
  /// <typeparam name="TModel">The model type.</typeparam>
  /// <typeparam name="TGenerator">The generator type.</typeparam>
  /// <param name="generator">The generator instance.</param>
  /// <param name="model">The model to generate.</param>
  /// <param name="fileName">The file name for verification.</param>
  /// <returns>A task representing the async operation.</returns>
  static async Task GenerateAndVerifyAsync<TModel, TGenerator>(
    TGenerator generator,
    TModel model,
    string fileName)
    where TGenerator : IKubernetesGenerator<TModel>
  {
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);

    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    File.Delete(outputPath);
  }
}
