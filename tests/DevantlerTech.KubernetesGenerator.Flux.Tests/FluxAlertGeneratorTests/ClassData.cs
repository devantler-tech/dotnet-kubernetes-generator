using System.Collections;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models.Alert;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxAlertGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
sealed class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple alert
    [new FluxAlert()
      {
        Metadata = new NamespacedMetadata
        {
          Name = "alert-simple",
        },
        Spec = new FluxAlertSpec
        {
          ProviderRef = new FluxAlertProviderRef
          {
            Name = "provider",
          },
          EventSources =
          [
            new FluxAlertEventSource
            {
              Kind = FluxCustomResourceKind.Kustomization,
              Name = "event-source",
            }
          ],
        }
      }, "alert-simple.yaml"],

      // Complex alert
      [new FluxAlert
      {
        Metadata = new NamespacedMetadata(new Dictionary<string, string>
          {
            ["key"] = "value"
          }
        )
        {
          Name = "alert-complex",
          Namespace = "alert-complex"
        },
        Spec = new FluxAlertSpec
        {
          ProviderRef = new FluxAlertProviderRef
          {
            Name = "provider",
          },
          EventSeverity = FluxEventSeverity.Info,
          EventSources =
          [
            new FluxAlertEventSource
            {
              Kind = FluxCustomResourceKind.Kustomization,
              Name = "event-source",
            }
          ],
        }
      }, "alert-complex.yaml"]
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
