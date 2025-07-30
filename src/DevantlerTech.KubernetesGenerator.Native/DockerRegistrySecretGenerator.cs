using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Docker registry Kubernetes Secret objects using 'kubectl create secret docker-registry' commands.
/// </summary>
public class DockerRegistrySecretGenerator : BaseSecretGenerator<DockerRegistrySecret>
{
  /// <summary>
  /// Gets the command prefix for Docker registry secrets.
  /// </summary>
  protected override ReadOnlyCollection<string> CommandPrefix => new(["create", "secret", "docker-registry"]);

  /// <summary>
  /// Builds the specific arguments for creating a Docker registry secret from a DockerRegistrySecret object.
  /// </summary>
  /// <param name="model">The DockerRegistrySecret object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The kubectl arguments.</returns>
  protected override Task<ReadOnlyCollection<string>> BuildSpecificArgumentsAsync(DockerRegistrySecret model, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = BuildCommonArguments(model).ToList();

    // Add Docker registry specific arguments
    if (!string.IsNullOrEmpty(model.DockerServer))
    {
      args.Add($"--docker-server={model.DockerServer}");
    }

    args.Add($"--docker-username={model.DockerUsername}");
    args.Add($"--docker-password={model.DockerPassword}");
    args.Add($"--docker-email={model.DockerEmail}");

    return Task.FromResult(args.AsReadOnly());
  }
}
