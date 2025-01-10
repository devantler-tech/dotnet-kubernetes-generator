using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Flux.Models;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Image Update Automation objects.
/// </summary>
public class FluxImageUpdateAutomationGenerator : IKubernetesGenerator<FluxImageUpdateAutomation>
{
  /// <summary>
  /// Generates a Flux Image Update Automation object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public Task GenerateAsync(FluxImageUpdateAutomation model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
