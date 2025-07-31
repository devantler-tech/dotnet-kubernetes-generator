using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

namespace DevantlerTech.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux ImagePolicy objects.
/// </summary>
public class FluxImagePolicyGenerator : BaseFluxGenerator<FluxImagePolicy>
{
  /// <summary>
  /// Generates a Flux ImagePolicy object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public override async Task GenerateAsync(FluxImagePolicy model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));
    var arguments = new List<string>
    {
      "create",
      "image",
      "policy",
      model.Metadata.Name,
      "--image-ref", model.Spec.ImageRef,
      "--export"
    };

    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);

    // Add sort-specific arguments
    switch (model.Spec.SortBy)
    {
      case FluxImagePolicySortBy.Semver:
        arguments.AddIfNotNull("--select-semver={0}", model.Spec.SemverRange ?? ">=0.0.0");
        break;
      case FluxImagePolicySortBy.Numeric:
#pragma warning disable CA1308 // Normalize strings to uppercase - flux CLI expects lowercase values
        arguments.AddIfNotNull("--select-numeric={0}", model.Spec.SortOrder?.ToString().ToLowerInvariant() ?? "asc");
#pragma warning restore CA1308
        break;
      case FluxImagePolicySortBy.Alpha:
#pragma warning disable CA1308 // Normalize strings to uppercase - flux CLI expects lowercase values
        arguments.AddIfNotNull("--select-alpha={0}", model.Spec.SortOrder?.ToString().ToLowerInvariant() ?? "asc");
#pragma warning restore CA1308
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(model), model.Spec.SortBy, "Invalid sort by option");
    }

    // Add filter arguments
    arguments.AddIfNotNull("--filter-regex={0}", model.Spec.FilterRegex);
    arguments.AddIfNotNull("--filter-extract={0}", model.Spec.FilterExtract);

    // Add other optional arguments
    arguments.AddIfNotNull("--reflect-digest={0}", model.Spec.ReflectDigest?.ToString());
    if (model.Spec.Interval.HasValue && model.Spec.ReflectDigest == FluxImagePolicyReflectDigest.Always)
    {
      arguments.Add($"--interval={model.Spec.Interval.Value.TotalSeconds}s");
    }

    await RunFluxAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate Flux ImagePolicy object", cancellationToken).ConfigureAwait(false);
  }
}