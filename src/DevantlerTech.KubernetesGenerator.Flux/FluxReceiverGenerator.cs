using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

namespace DevantlerTech.KubernetesGenerator.Flux;

/// <summary>
/// Generator for generating Flux Receiver objects.
/// </summary>
public class FluxReceiverGenerator : FluxGenerator<FluxReceiver>
{
  /// <summary>
  /// Generates a Flux Receiver object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  public override async Task GenerateAsync(FluxReceiver model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));
    var arguments = new List<string>
    {
      "create",
      "receiver",
      model.Metadata.Name,
      "--export"
    };

    arguments.AddIfNotNull("--namespace={0}", model.Metadata.Namespace);
    arguments.AddIfNotNull("--label={0}", model.Metadata.Labels != null ? string.Join(",", model.Metadata.Labels.Select(x => $"{x.Key}={x.Value}")) : null);
    arguments.AddIfNotNull("--type={0}", GetReceiverTypeName(model.Spec.Type));

    if (model.Spec.Events != null && model.Spec.Events.Any())
    {
      foreach (var eventType in model.Spec.Events)
      {
        arguments.Add($"--event={GetEventName(eventType)}");
      }
    }

    arguments.AddIfNotNull("--secret-ref={0}", model.Spec.SecretRef?.Name);

    if (model.Spec.Resources != null && model.Spec.Resources.Any())
    {
      foreach (var resource in model.Spec.Resources)
      {
        string resourceRef = resource.Namespace != null
          ? $"{resource.Kind}/{resource.Name}.{resource.Namespace}"
          : $"{resource.Kind}/{resource.Name}";
        arguments.Add($"--resource={resourceRef}");
      }
    }

    await RunFluxAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate Flux Receiver object", cancellationToken).ConfigureAwait(false);
  }

  static string GetReceiverTypeName(FluxReceiverType type) => type switch
  {
    FluxReceiverType.Github => "github",
    FluxReceiverType.Gitlab => "gitlab",
    FluxReceiverType.Bitbucket => "bitbucket",
    FluxReceiverType.Harbor => "harbor",
    FluxReceiverType.Dockerhub => "dockerhub",
    FluxReceiverType.Quay => "quay",
    FluxReceiverType.Acr => "acr",
    FluxReceiverType.Gcr => "gcr",
    FluxReceiverType.Generic => "generic",
    _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported receiver type")
  };

  static string GetEventName(FluxReceiverEvent eventType) => eventType switch
  {
    FluxReceiverEvent.Ping => "ping",
    FluxReceiverEvent.Push => "push",
    FluxReceiverEvent.TagPush => "tag_push",
    FluxReceiverEvent.PullRequest => "pull_request",
    FluxReceiverEvent.Release => "release",
    _ => throw new ArgumentOutOfRangeException(nameof(eventType), eventType, "Unsupported event type")
  };
}