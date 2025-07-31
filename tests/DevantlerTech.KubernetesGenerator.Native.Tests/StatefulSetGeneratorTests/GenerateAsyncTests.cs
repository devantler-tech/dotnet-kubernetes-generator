
namespace DevantlerTech.KubernetesGenerator.Native.Tests.StatefulSetGeneratorTests;


/// <summary>
/// Tests for the <see cref="StatefulSetGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeStatefulSet object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidStatefulSet()
  {
    // Arrange
    var generator = new StatefulSetGenerator();
    var model = new NativeStatefulSet
    {
      Metadata = new Metadata
      {
        Name = "stateful-set",
        Namespace = "default"
      },
      Spec = new NativeStatefulSetSpec
      {
        Replicas = 1,
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "stateful-set"
          }
        },
        ServiceName = "stateful-set",
        Template = new NativePodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "stateful-set"
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
    string fileName = "stateful-set.yaml";
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
  /// Verifies the generated NativeStatefulSet object with rolling update strategy.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithRollingUpdateStrategy_ShouldGenerateAValidStatefulSet()
  {
    // Arrange
    var generator = new StatefulSetGenerator();
    var model = new NativeStatefulSet
    {
      Metadata = new Metadata
      {
        Name = "rolling-update-statefulset",
        Namespace = "default"
      },
      Spec = new NativeStatefulSetSpec
      {
        Replicas = 3,
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "rolling-app"
          }
        },
        ServiceName = "rolling-service",
        Template = new NativePodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "rolling-app"
            }
          },
          Spec = new NativePodSpec
          {
            Containers =
            [
              new NativePodContainer
              {
                Name = "app-container",
                Image = "nginx:latest",
                Ports =
                [
                  new NativePodContainerPort
                  {
                    ContainerPort = 80,
                    Name = "http"
                  }
                ]
              }
            ]
          }
        },
        UpdateStrategy = new NativeStatefulSetUpdateStrategy
        {
          Type = UpdateStrategyType.RollingUpdate,
          RollingUpdate = new NativeStatefulSetRollingUpdateStrategy
          {
            Partition = 1
          }
        },
        PodManagementPolicy = NativeStatefulSetPodManagementPolicy.Parallel
      }
    };

    // Act
    string fileName = "rolling-update-statefulset.yaml";
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
  /// Verifies the generated NativeStatefulSet object with persistent volume claim retention policy.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithPersistentVolumeClaimRetentionPolicy_ShouldGenerateAValidStatefulSet()
  {
    // Arrange
    var generator = new StatefulSetGenerator();
    var model = new NativeStatefulSet
    {
      Metadata = new Metadata
      {
        Name = "pvc-retention-statefulset",
        Namespace = "default"
      },
      Spec = new NativeStatefulSetSpec
      {
        Replicas = 1,
        Selector = new LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            ["app"] = "pvc-app"
          }
        },
        ServiceName = "pvc-service",
        Template = new NativePodTemplateSpec
        {
          Metadata = new TemplateMetadata
          {
            Labels = new Dictionary<string, string>
            {
              ["app"] = "pvc-app"
            }
          },
          Spec = new NativePodSpec
          {
            Containers =
            [
              new NativePodContainer
              {
                Name = "storage-container",
                Image = "nginx:latest"
              }
            ]
          }
        },
        PersistentVolumeClaimRetentionPolicy = new NativeStatefulSetPersistentVolumeClaimRetentionPolicy
        {
          WhenDeleted = NativeStatefulSetPersistentVolumeClaimRetentionPolicyType.Delete,
          WhenScaled = NativeStatefulSetPersistentVolumeClaimRetentionPolicyType.Retain
        }
      }
    };

    // Act
    string fileName = "pvc-retention-statefulset.yaml";
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
  /// Verifies that an exception is thrown when model is null.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new StatefulSetGenerator();

    // Act & Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(
      () => generator.GenerateAsync(null!, "/tmp/test.yaml"));
  }
}

