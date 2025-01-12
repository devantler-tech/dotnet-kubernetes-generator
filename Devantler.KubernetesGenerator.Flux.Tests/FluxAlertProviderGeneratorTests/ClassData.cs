using System.Collections;
using Devantler.KubernetesGenerator.Flux.Models;
using Devantler.KubernetesGenerator.Flux.Models.AlertProvider;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxAlertProviderGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
public class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple provider
    [new FluxAlertProvider(){
      Metadata = new FluxNamespacedMetadata()
      {
        Name = "provider-simple",
      },
      Spec = new FluxAlertProviderSpec()
      {
        Type = FluxAlertProviderType.Slack,
      }
    } , "provider-simple.yaml"],

    // Complex provider
    [new FluxAlertProvider(){
      Metadata = new FluxNamespacedMetadata()
      {
        Name = "provider-complex",
        Namespace = "provider-complex",
        Labels = new Dictionary<string, string>()
        {
          ["key"] = "value"
        }
      },
      Spec = new FluxAlertProviderSpec()
      {
        Type = FluxAlertProviderType.Slack,
        Address = "https://slack.com",
        Channel = "channel",
        SecretRef = new FluxSecretRef()
        {
          Name = "secret-name"
        },
        Username = "username"
      }
    }, "provider-complex.yaml"]
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
