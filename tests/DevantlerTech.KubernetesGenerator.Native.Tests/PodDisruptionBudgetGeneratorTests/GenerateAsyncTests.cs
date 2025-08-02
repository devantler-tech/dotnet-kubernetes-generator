using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.Pod;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PodDisruptionBudgetGeneratorTests;

/// <summary>
/// Tests for the <see cref="PodDisruptionBudgetGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativePodDisruptionBudget object with MinAvailable.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinAvailable_ShouldGenerateAValidPodDisruptionBudget()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = "pdb-min-available",
        Namespace = "default"
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "app=nginx",
        MinAvailable = "1"
      }
    };

    // Act
    string fileName = "pdb-min-available.yaml";
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

  /// <summary>
  /// Verifies the generated NativePodDisruptionBudget object with MaxUnavailable.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMaxUnavailable_ShouldGenerateAValidPodDisruptionBudget()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = "pdb-max-unavailable",
        Namespace = "default"
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "app=nginx",
        MaxUnavailable = "1"
      }
    };

    // Act
    string fileName = "pdb-max-unavailable.yaml";
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

  /// <summary>
  /// Verifies the generated NativePodDisruptionBudget object with percentage values.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithPercentage_ShouldGenerateAValidPodDisruptionBudget()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = "pdb-percentage",
        Namespace = "default"
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "app=nginx",
        MinAvailable = "50%"
      }
    };

    // Act
    string fileName = "pdb-percentage.yaml";
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

  /// <summary>
  /// Verifies the generated NativePodDisruptionBudget object without namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutNamespace_ShouldGenerateAValidPodDisruptionBudget()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = "pdb-no-namespace"
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "app=nginx",
        MinAvailable = "2"
      }
    };

    // Act
    string fileName = "pdb-no-namespace.yaml";
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

  /// <summary>
  /// Verifies that an exception is thrown when both MinAvailable and MaxUnavailable are specified.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBothMinAndMax_ShouldThrowException()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = "pdb-both"
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "app=nginx",
        MinAvailable = "1",
        MaxUnavailable = "1"
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "pdb-both.yaml");
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, outputPath));
  }

  /// <summary>
  /// Verifies that an exception is thrown when neither MinAvailable nor MaxUnavailable are specified.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutMinOrMax_ShouldThrowException()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = "pdb-none"
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "app=nginx"
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "pdb-none.yaml");
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, outputPath));
  }

  /// <summary>
  /// Verifies that an exception is thrown when selector is not specified.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutSelector_ShouldThrowException()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = "pdb-no-selector"
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "",
        MinAvailable = "1"
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "pdb-no-selector.yaml");
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, outputPath));
  }

  /// <summary>
  /// Verifies that an exception is thrown when name is not specified.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowException()
  {
    // Arrange
    var generator = new PodDisruptionBudgetGenerator();
    var model = new NativePodDisruptionBudget
    {
      Metadata = new NamespacedMetadata
      {
        Name = ""
      },
      Spec = new NativePodDisruptionBudgetSpec
      {
        Selector = "app=nginx",
        MinAvailable = "1"
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "pdb-no-name.yaml");
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, outputPath));
  }
}
