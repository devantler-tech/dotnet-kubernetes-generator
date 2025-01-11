// TODO: Implement `FluxImagePolicyGenerator` with `flux create` command.
using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Flux.Models;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Image Policy objects.
/// </summary>
public class FluxImagePolicyGenerator : IKubernetesGenerator<FluxImagePolicy>
{
  /// <summary>
  /// Generates a Flux Image Policy object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public Task GenerateAsync(FluxImagePolicy model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
