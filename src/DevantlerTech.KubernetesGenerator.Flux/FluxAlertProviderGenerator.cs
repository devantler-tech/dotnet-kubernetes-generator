using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Flux.Models.AlertProvider;

namespace DevantlerTech.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Alert Provider objects.
/// </summary>
public class FluxAlertProviderGenerator : BaseFluxGenerator<FluxAlertProvider>
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
  public override async Task GenerateAsync(FluxAlertProvider model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));
    var arguments = new List<string>
    {
      "create",
      "alert-provider",
      model.Metadata.Name,
      "--type", model.Spec.Type.GetDescriptionOrDefault(),
      "--export"
    };
    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);
    arguments.AddIfNotNull("--address={0}", model.Spec.Address);
    arguments.AddIfNotNull("--channel={0}", model.Spec.Channel);
    arguments.AddIfNotNull("--secret-ref={0}", model.Spec.SecretRef?.Name);
    arguments.AddIfNotNull("--username={0}", model.Spec.Username);

    await RunFluxAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate Flux Alert Provider object", cancellationToken).ConfigureAwait(false);
  }
}
