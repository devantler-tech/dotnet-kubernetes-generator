using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

namespace DevantlerTech.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux ImagePolicy objects.
/// </summary>
public class FluxImagePolicyGenerator : FluxGenerator<FluxImagePolicy>
{
  /// <summary>
  /// Generates a Flux ImagePolicy object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  public override async Task GenerateAsync(FluxImagePolicy model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));
    var arguments = new List<string>
    {
      "create",
      "image",
      "policy",
      model.Metadata.Name,
      "--export"
    };

    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);
    arguments.AddIfNotNull("--image-ref={0}", model.Spec.ImageRepositoryRef.Name);

    // Add selection policy
    if (model.Spec.Policy.SemVer != null)
    {
      arguments.AddIfNotNull("--select-semver={0}", model.Spec.Policy.SemVer.Range);
    }
    else if (model.Spec.Policy.Numerical != null)
    {
#pragma warning disable CA1308 // Flux CLI requires lowercase values
      arguments.AddIfNotNull("--select-numeric={0}", model.Spec.Policy.Numerical.Order.ToString().ToLower(System.Globalization.CultureInfo.InvariantCulture));
#pragma warning restore CA1308
    }
    else if (model.Spec.Policy.Alphabetical != null)
    {
#pragma warning disable CA1308 // Flux CLI requires lowercase values
      arguments.AddIfNotNull("--select-alpha={0}", model.Spec.Policy.Alphabetical.Order.ToString().ToLower(System.Globalization.CultureInfo.InvariantCulture));
#pragma warning restore CA1308
    }

    // Add filter tags
    arguments.AddIfNotNull("--filter-regex={0}", model.Spec.FilterTags?.Pattern);
    arguments.AddIfNotNull("--filter-extract={0}", model.Spec.FilterTags?.Extract);

    await RunFluxAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate Flux ImagePolicy object", cancellationToken).ConfigureAwait(false);
  }
}