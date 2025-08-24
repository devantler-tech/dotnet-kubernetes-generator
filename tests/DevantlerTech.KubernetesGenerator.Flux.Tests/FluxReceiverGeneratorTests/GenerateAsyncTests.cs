using DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxReceiverGeneratorTests;

/// <summary>
/// Tests for <see cref="FluxReceiverGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly FluxReceiverGenerator _generator = new();

  /// <summary>
  /// Tests that <see cref="FluxReceiverGenerator"/> generates a valid Flux Receiver.
  /// </summary>
  [Theory]
  [ClassData(typeof(ClassData))]
  public async Task GenerateAsync_GeneratesValidFluxReceiver(FluxReceiver model, string fileName)
  {
    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    await _generator.GenerateAsync(model, outputPath, true);
    string fileContents = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContents, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}