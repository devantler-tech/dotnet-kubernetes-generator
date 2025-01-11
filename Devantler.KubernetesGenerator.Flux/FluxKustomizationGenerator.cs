using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Core.Extensions;
using Devantler.KubernetesGenerator.Flux.Models.Kustomization;

namespace Devantler.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Kustomization objects.
/// </summary>
public class FluxKustomizationGenerator : IKubernetesGenerator<FluxKustomization>
{
  /// <summary>
  /// Generates a Flux Kustomization object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  public async Task GenerateAsync(FluxKustomization model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    var arguments = new List<string>
    {
      "create",
      "kustomization",
      model.Metadata.Name,
      "--export"
    };
    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--interval={0}", model.Spec?.Interval);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);
    arguments.AddIfNotNull("--decryption-provider={0}", model.Spec?.Decryption?.Provider.GetDescription());
    arguments.AddIfNotNull("--decryption-secret={0}", model.Spec?.Decryption?.SecretRef?.Name);
    arguments.AddIfNotNull("--depends-on={0}", model.Spec?.DependsOn != null ? string.Join(",", model.Spec.DependsOn.Select(x => $"{x.Name}/{x.Namespace}")) : null);
    arguments.AddIfNotNull("--health-check={0}", model.Spec?.HealthChecks?.Select(x => $"{x.Kind}/{x.Name}.{x.Namespace}"));
    arguments.AddIfNotNull("--health-check-timeout={0}", model.Spec?.Timeout);
    arguments.AddIfNotNull("--kubeconfig-secret-ref={0}", model.Spec?.KubeConfig?.SecretRef?.Name);
    arguments.AddIfNotNull("--path={0}", model.Spec?.Path);
    arguments.AddIfNotNull("--prune", model.Spec?.Prune);
    arguments.AddIfNotNull("--retry-interval={0}", model.Spec?.RetryInterval);
    arguments.AddIfNotNull("--service-account={0}", model.Spec?.ServiceAccountName);
    arguments.AddIfNotNull("--source={0}/{1}.{2}", model.Spec?.SourceRef?.Kind, model.Spec?.SourceRef?.Name, model.Spec?.SourceRef?.Namespace);
    arguments.AddIfNotNull("--wait", model.Spec?.Wait);


    var (exitCode, output) = await FluxCLI.Flux.RunAsync([.. arguments],
      cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new KubernetesGeneratorException($"Failed to generate Flux HelmRelease object. {output}");
    }
    await FileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken);
  }
}


