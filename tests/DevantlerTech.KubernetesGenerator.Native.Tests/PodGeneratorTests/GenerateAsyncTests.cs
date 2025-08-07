
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.Pod;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PodGeneratorTests;


/// <summary>
/// Tests for the <see cref="PodGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativePod object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPod()
  {
    // Arrange
    var generator = new PodGenerator();
    var model = new NativePod
    {
      Metadata = new Metadata
      {
        Name = "nginx-pod",
        Namespace = "default",
        Labels = new Dictionary<string, string> { ["app"] = "nginx" },
        Annotations = new Dictionary<string, string> { ["example.com/annotation"] = "value" }
      },
      Spec = new NativePodSpec
      {
        Containers =
        [
          new NativePodContainer
          {
            Name = "nginx",
            Image = "nginx:1.21",
            ImagePullPolicy = NativePodImagePullPolicy.IfNotPresent,
            Command = ["nginx"],
            Args = ["-g", "daemon off;"],
            Env =
            [
              new NativePodContainerEnvVar{
                Name = "ENV_VAR",
                Value = "value"
              },
            ],
            Ports =
            [
              new NativePodContainerPort{ Name = "http", Protocol = NativePodContainerPortProtocol.TCP, ContainerPort = 80 }
            ],
            SecurityContext = new NativePodContainerSecurityContext
            {
              Privileged = false,
              RunAsNonRoot = true,
              ReadOnlyRootFilesystem = true
            }
          }
        ],
        RestartPolicy = NativePodRestartPolicy.Always,
        Tty = false,
        Stdin = false
      }
    };

    // Act
    string fileName = "pod.yaml";
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

