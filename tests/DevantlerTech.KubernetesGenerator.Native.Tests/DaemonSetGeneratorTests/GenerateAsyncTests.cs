
namespace DevantlerTech.KubernetesGenerator.Native.Tests.DaemonSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="DaemonSetGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeDaemonSet object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new NativeDaemonSet
    {
      ApiVersion = "apps/v1",
      Kind = "DaemonSet",
      Metadata = new Metadata
      {
        Name = "daemon-set",
        Namespace = "default"
      },
      Spec = new NativeDaemonSetSpec
      {
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "daemon-set"
          }
        },
        Template = new NativePodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "daemon-set"
            }
          },
          Spec = new NativePodSpec
          {
            Containers =
            [
              new NativePodContainer
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
  /// Verifies a minimal NativeDaemonSet can be generated.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new NativeDaemonSet
    {
      Metadata = new Metadata
      {
        Name = "minimal-daemon-set"
      },
      Spec = new NativeDaemonSetSpec
      {
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "minimal"
          }
        },
        Template = new NativePodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "minimal"
            }
          },
          Spec = new NativePodSpec
          {
            Containers =
            [
              new NativePodContainer
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
  /// Verifies a NativeDaemonSet with rolling update strategy can be generated.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithRollingUpdateStrategy_ShouldGenerateAValidDaemonSet()
  {
    // Arrange
    var generator = new DaemonSetGenerator();
    var model = new NativeDaemonSet
    {
      Metadata = new Metadata
      {
        Name = "rolling-update-daemon-set",
        Namespace = "default"
      },
      Spec = new NativeDaemonSetSpec
      {
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "rolling-update"
          }
        },
        Template = new NativePodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "rolling-update"
            }
          },
          Spec = new NativePodSpec
          {
            Containers =
            [
              new NativePodContainer
              {
                Name = "app",
                Image = "nginx:1.21"
              }
            ]
          }
        },
        UpdateStrategy = new NativeDaemonSetUpdateStrategy
        {
          Type = UpdateStrategyType.RollingUpdate,
          RollingUpdate = new NativeDaemonSetRollingUpdateStrategy
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

