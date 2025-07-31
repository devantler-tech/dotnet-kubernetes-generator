using System.Collections;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models.AlertProvider;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxAlertProviderGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
sealed class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple provider
    [new FluxAlertProvider(){
      Metadata = new Metadata()
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
      Metadata = new Metadata(new Dictionary<string, string>()
        {
          ["key"] = "value"
        }
      )
      {
        Name = "provider-complex",
        Namespace = "provider-complex",
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
