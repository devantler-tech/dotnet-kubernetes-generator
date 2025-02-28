using Devantler.Commons.Extensions;
using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux HelmRelease objects.
/// </summary>
public class FluxHelmReleaseGenerator : BaseFluxGenerator<FluxHelmRelease>
{
  /// <summary>
  /// Generates a Flux HelmRelease object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  public override async Task GenerateAsync(FluxHelmRelease model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));
    var arguments = new List<string>
    {
      "create",
      "helmrelease",
      model.Metadata.Name,
      "--export"
    };
    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);
    arguments.AddIfNotNull("--interval={0}", model.Spec.Interval);
    arguments.AddIfNotNull("--crds={0}", model.Spec.InstallUpgrade?.Crds);
    arguments.AddIfNotNull("--depends-on={0}", model.Spec.DependsOn != null ? string.Join(',', model.Spec.DependsOn.Select(d => d.Namespace == null ? d.Name : $"{d.Namespace}/{d.Name}")) : null);
    arguments.AddIfNotNull("--kubeconfig-secret-ref={0}", model.Spec.Kubeconfig?.SecretRef?.Name);
    arguments.AddIfNotNull("--service-account={0}", model.Spec.ServiceAccountName);
    arguments.AddIfNotNull("--values-from={0}/{1}", model.Spec.ValuesFrom?.Kind, model.Spec.ValuesFrom?.Name);
    arguments.AddIfNotNull($"--chart={model.Spec.Chart?.Spec.Chart}");
    arguments.AddIfNotNull("--chart-version={0}", model.Spec.Chart?.Spec.Version);
    if (model.Spec.Chart?.Spec.SourceRef?.Namespace == null)
    {
      arguments.AddIfNotNull("--source={0}/{1}", model.Spec.Chart?.Spec.SourceRef?.Kind, model.Spec.Chart?.Spec.SourceRef?.Name);
    }
    else
    {
      arguments.AddIfNotNull("--source={0}/{1}.{2}", model.Spec.Chart?.Spec.SourceRef?.Kind, model.Spec.Chart?.Spec.SourceRef?.Name, model.Spec.Chart?.Spec.SourceRef?.Namespace);
    }
    arguments.AddIfNotNull("--chart-interval={0}", model.Spec.Chart?.Spec.Interval);
    arguments.AddIfNotNull("--reconcile-strategy={0}", model.Spec.Chart?.Spec.ReconcileStrategy);
    if (model.Spec.ChartRef?.Namespace == null)
    {
      arguments.AddIfNotNull("--chart-ref={0}/{1}", model.Spec.ChartRef?.Kind, model.Spec.ChartRef?.Name);
    }
    else
    {
      arguments.AddIfNotNull("--chart-ref={0}/{1}.{2}", model.Spec.ChartRef?.Kind, model.Spec.ChartRef?.Name, model.Spec.ChartRef?.Namespace);
    }

    await RunFluxAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate Flux HelmRelease object", cancellationToken).ConfigureAwait(false);
  }
}
