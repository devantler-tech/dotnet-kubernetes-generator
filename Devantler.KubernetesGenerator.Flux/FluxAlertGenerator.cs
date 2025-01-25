using Devantler.Commons.Extensions;
using Devantler.KubernetesGenerator.Flux.Models.Alert;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Alert objects.
/// </summary>
public class FluxAlertGenerator : BaseFluxGenerator<FluxAlert>
{
  /// <summary>
  /// Generates a Flux Alert object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override async Task GenerateAsync(FluxAlert model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    var arguments = new List<string>
    {
      "create",
      "alert",
      model.Metadata.Name,
      "--provider-ref", model.Spec.ProviderRef.Name,
      "--event-source", string.Join(",", model.Spec.EventSources.Select(x => $"{x.Kind}/{x.Name}")),
      "--export"
    };
    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);
    arguments.AddIfNotNull("--event-severity={0}", model.Spec.EventSeverity);

    await RunFluxAsync(outputPath, overwrite, arguments, "Failed to generate Flux Alert object", cancellationToken);
  }
}
