using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DaemonSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="DaemonSetGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated DaemonSet object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new DaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new Metadata
      {
        Name = "daemon-set",
        Namespace = "default"
      },
      Spec = new DaemonSetSpec
      {
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set"
          }
        },
        Template = new PodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set"
            }
          },
          Spec = new PodSpec
          {
            Containers =
            [
              new PodContainer
              {
                Name = "container",
                Image = "nginx",
                Command = ["echo", "hello"]
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "daemon-set.yaml";
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
  /// Verifies a minimal DaemonSet can be generated.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new DaemonSet
    {
      Metadata = new Metadata
      {
        Name = "minimal-daemon-set"
      },
      Spec = new DaemonSetSpec
      {
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "minimal"
          }
        },
        Template = new PodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "minimal"
            }
          },
          Spec = new PodSpec
          {
            Containers =
            [
              new PodContainer
              {
                Name = "app",
                Image = "nginx"
              }
            ]
          }
        }
      }
    };

    // Act
    string fileName = "minimal-daemon-set.yaml";
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
  /// Verifies a DaemonSet with rolling update strategy can be generated.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithRollingUpdateStrategy_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new DaemonSet
    {
      Metadata = new Metadata
      {
        Name = "rolling-update-daemon-set",
        Namespace = "default"
      },
      Spec = new DaemonSetSpec
      {
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "rolling-update"
          }
        },
        Template = new PodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "rolling-update"
            }
          },
          Spec = new PodSpec
          {
            Containers =
            [
              new PodContainer
              {
                Name = "app",
                Image = "nginx:1.21"
              }
            ]
          }
        },
        UpdateStrategy = new DaemonSetUpdateStrategy
        {
          Type = UpdateStrategyType.RollingUpdate,
          RollingUpdate = new DaemonSetRollingUpdateStrategy
          {
            MaxUnavailable = "1",
            MaxSurge = "1"
          }
        },
        MinReadySeconds = 30,
        RevisionHistoryLimit = 5
      }
    };

    // Act
    string fileName = "rolling-update-daemon-set.yaml";
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

