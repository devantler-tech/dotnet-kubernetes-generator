using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Flux.Models;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux HelmRelease objects.
/// </summary>
public class FluxHelmReleaseGenerator : IKubernetesGenerator<FluxHelmRelease>
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
  public async Task GenerateAsync(FluxHelmRelease model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    if (model.Spec.Chart == null && model.Spec.ChartRef == null)
    {
      throw new KubernetesGeneratorException("One of Chart or ChartRef must be set.");
    }
    if (model.Spec.Chart != null && model.Spec.ChartRef != null)
    {
      throw new KubernetesGeneratorException("Only one of Chart and ChartRef can be set.");
    }
    // TODO:     --crds crds                      upgrade CRDs policy, available options are: (Skip, Create, CreateReplace)
    // TODO:     --depends-on strings             HelmReleases that must be ready before this release can be installed, supported formats '<name>' and '<namespace>/<name>'
    // TODO:     --kubeconfig-secret-ref string   the name of the Kubernetes Secret that contains a key with the kubeconfig file for connecting to a remote cluster
    // TODO:     --reconcile-strategy string      the reconcile strategy for helm chart created by the helm release(accepted values: Revision and ChartRevision) (default "ChartVersion")
    // TODO:     --service-account string         the name of the service account to impersonate when reconciling this HelmRelease
    var arguments = new List<string>
    {
      "create",
      "helmrelease",
      model.Metadata.Name,
      { "--namespace", model.Metadata.Namespace },
      { "--interval", model.Spec.Interval },
      { "--chart", model.Spec.Chart?.Spec.Chart },
      { "--chart-version", model.Spec.Chart?.Spec.Version },
      {
        "--source={0}/{1}.{2}",
        model.Spec.Chart?.Spec.SourceRef?.Kind,
        model.Spec.Chart?.Spec.SourceRef?.Name,
        model.Spec.Chart?.Spec.SourceRef?.Namespace
      },
      { "--chart-interval", model.Spec.Chart?.Spec.Interval },
      {
        "--chart-ref={0}/{1}.{2}",
        model.Spec.ChartRef?.Kind,
        model.Spec.ChartRef?.Name,
        model.Spec.ChartRef?.Namespace
      },
       "--export"
    };

    var (exitCode, output) = await FluxCLI.Flux.RunAsync([.. arguments],
      cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new KubernetesGeneratorException($"Failed to generate Flux HelmRelease object. {output}");
    }
    await FileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken);
  }
}
