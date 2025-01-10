using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Flux.Models;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Alert Provider objects.
/// </summary>
public class FluxAlertProviderGenerator : IKubernetesGenerator<FluxAlertProvider>
{
  /// <summary>
  /// Generates a Flux Alert Provider object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public Task GenerateAsync(FluxAlertProvider model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
