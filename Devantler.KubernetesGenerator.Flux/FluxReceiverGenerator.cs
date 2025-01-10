using Devantler.KubernetesGenerator.Core;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Receiver objects.
/// </summary>
public class FluxReceiverGenerator : IKubernetesGenerator<FluxReceiver>
{
  /// <summary>
  /// Generates a Flux Receiver object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public Task GenerateAsync(FluxReceiver model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
