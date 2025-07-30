using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for generic Kubernetes Secret objects using 'kubectl create secret generic' commands.
/// </summary>
public class GenericSecretGenerator : BaseSecretGenerator<GenericSecret>
{
  /// <summary>
  /// Gets the command prefix for generic secrets.
  /// </summary>
  protected override ReadOnlyCollection<string> CommandPrefix => new(["create", "secret", "generic"]);

  /// <summary>
  /// Builds the specific arguments for creating a generic secret from a GenericSecret object.
  /// </summary>
  /// <param name="model">The GenericSecret object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The kubectl arguments.</returns>
  protected override Task<ReadOnlyCollection<string>> BuildSpecificArgumentsAsync(GenericSecret model, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = BuildCommonArguments(model).ToList();

    // Add type if specified (but don't require it)
    if (!string.IsNullOrEmpty(model.Type))
    {
      args.Add($"--type={model.Type}");
    }

    // Add all data as literals
    foreach (var kvp in model.Data)
    {
      args.Add($"--from-literal={kvp.Key}={kvp.Value}");
    }

    return Task.FromResult(args.AsReadOnly());
  }
}
