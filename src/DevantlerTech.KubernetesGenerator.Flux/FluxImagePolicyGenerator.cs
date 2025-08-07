using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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

    // Generate base YAML using flux CLI
    var (exitCode, output) = await FluxCLI.Flux.RunAsync([.. arguments], silent: true, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
      throw new KubernetesGeneratorException($"Failed to generate Flux ImagePolicy object: {output}");

    // Post-process YAML if ImageRepositoryRef has a namespace
    if (!string.IsNullOrEmpty(model.Spec.ImageRepositoryRef.Namespace))
    {
      output = PostProcessYamlWithNamespace(output, model.Spec.ImageRepositoryRef.Namespace);
    }

    await YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken).ConfigureAwait(false);
  }

  static string PostProcessYamlWithNamespace(string yaml, string imageRepoNamespace)
  {
    var deserializer = new DeserializerBuilder()
      .WithNamingConvention(CamelCaseNamingConvention.Instance)
      .Build();

    var serializer = new SerializerBuilder()
      .WithNamingConvention(CamelCaseNamingConvention.Instance)
      .Build();

    // Parse the YAML document
    var yamlObject = deserializer.Deserialize<Dictionary<object, object>>(yaml);

    // Navigate to spec.imageRepositoryRef and add namespace
    if (yamlObject.TryGetValue("spec", out object? specValue) && specValue is Dictionary<object, object> spec)
    {
      if (spec.TryGetValue("imageRepositoryRef", out object? imageRepoRefValue) && imageRepoRefValue is Dictionary<object, object> imageRepoRef)
      {
        imageRepoRef["namespace"] = imageRepoNamespace;
      }
    }

    return serializer.Serialize(yamlObject);
  }
}