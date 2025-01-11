using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Core.Extensions;
using Devantler.KubernetesGenerator.Flux.Models.AlertProvider;

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
  public async Task GenerateAsync(FluxAlertProvider model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    var arguments = new List<string>
    {
      "create",
      "alert-provider",
      model.Metadata.Name,
      "--type", model.Spec.Type.GetDescription(),
      "--export"
    };
    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);
    arguments.AddIfNotNull("--address={0}", model.Spec.Address);
    arguments.AddIfNotNull("--channel={0}", model.Spec.Channel);
    arguments.AddIfNotNull("--secret-ref={0}", model.Spec.SecretRef?.Name);
    arguments.AddIfNotNull("--username={0}", model.Spec.Username);

    var (exitCode, output) = await FluxCLI.Flux.RunAsync([.. arguments],
      cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new KubernetesGeneratorException($"Failed to generate Flux Alert Provider object. {output}");
    }
    await FileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken);
  }
}
