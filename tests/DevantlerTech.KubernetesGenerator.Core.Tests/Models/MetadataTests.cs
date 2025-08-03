using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Core.Tests.Models;

/// <summary>
/// Tests for Core metadata classes.
/// </summary>
public class MetadataTests
{
  /// <summary>
  /// Tests the parameterless constructor of <see cref="ClusterScopedMetadata"/>.
  /// </summary>
  [Fact]
  public void ClusterScopedMetadata_ParameterlessConstructor_ShouldCreateInstance()
  {
    // Act
    var metadata = new ClusterScopedMetadata { Name = "test" };

    // Assert
    Assert.NotNull(metadata);
    Assert.Null(metadata.Labels);
    Assert.Null(metadata.Annotations);
    Assert.Equal("test", metadata.Name);
  }

  /// <summary>
  /// Tests the constructor with labels parameter of <see cref="ClusterScopedMetadata"/>.
  /// </summary>
  [Fact]
  public void ClusterScopedMetadata_ConstructorWithLabels_ShouldSetLabels()
  {
    // Arrange
    var labels = new Dictionary<string, string>
    {
      ["app"] = "test",
      ["version"] = "1.0.0"
    };

    // Act
    var metadata = new ClusterScopedMetadata(labels) { Name = "test" };

    // Assert
    Assert.NotNull(metadata);
    Assert.Equal(labels, metadata.Labels);
    Assert.Null(metadata.Annotations);
    Assert.Equal("test", metadata.Name);
  }

  /// <summary>
  /// Tests the constructor with null labels parameter of <see cref="ClusterScopedMetadata"/>.
  /// </summary>
  [Fact]
  public void ClusterScopedMetadata_ConstructorWithNullLabels_ShouldSetLabelsToNull()
  {
    // Act
    var metadata = new ClusterScopedMetadata(null) { Name = "test" };

    // Assert
    Assert.NotNull(metadata);
    Assert.Null(metadata.Labels);
    Assert.Null(metadata.Annotations);
    Assert.Equal("test", metadata.Name);
  }

  /// <summary>
  /// Tests setting annotations through object initializer on <see cref="ClusterScopedMetadata"/>.
  /// </summary>
  [Fact]
  public void ClusterScopedMetadata_SettingAnnotationsInInitializer_ShouldSetCorrectly()
  {
    // Arrange
    var annotations = new Dictionary<string, string> { ["description"] = "test" };

    // Act
    var metadata = new ClusterScopedMetadata
    {
      Name = "test-name",
      Annotations = annotations
    };

    // Assert
    Assert.Equal("test-name", metadata.Name);
    Assert.Equal(annotations, metadata.Annotations);
  }
}