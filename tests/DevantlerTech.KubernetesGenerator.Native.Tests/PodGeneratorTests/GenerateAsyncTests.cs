using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PodGeneratorTests;


/// <summary>
/// Tests for the <see cref="PodGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Pod object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPod()
  {
    // Arrange
    var generator = new PodGenerator();
    var model = new Pod
    {
      Metadata = new Metadata
      {
        Name = "nginx-pod",
        Namespace = "default",
        Labels = new Dictionary<string, string> { ["app"] = "nginx" },
        Annotations = new Dictionary<string, string> { ["example.com/annotation"] = "value" }
      },
      Spec = new PodSpec
      {
        Containers =
        [
          new PodContainer
          {
            Name = "nginx",
            Image = "nginx:1.21",
            ImagePullPolicy = PodImagePullPolicy.IfNotPresent,
            Command = ["nginx"],
            Args = ["-g", "daemon off;"],
            Env =
            [
              new PodContainerEnvVar{
                Name = "ENV_VAR",
                Value = "value"
              },
            ],
            Ports =
            [
              new PodContainerPort{ Name = "http", Protocol = PodContainerPortProtocol.TCP, ContainerPort = 80 }
            ],
            SecurityContext = new PodContainerSecurityContext
            {
              Privileged = false,
              RunAsNonRoot = true,
              ReadOnlyRootFilesystem = true
            }
          }
        ],
        RestartPolicy = PodRestartPolicy.Always,
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

