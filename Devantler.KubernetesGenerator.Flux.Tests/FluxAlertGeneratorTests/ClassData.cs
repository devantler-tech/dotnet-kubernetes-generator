using System.Collections;
using Devantler.KubernetesGenerator.Flux.Models;
using Devantler.KubernetesGenerator.Flux.Models.Alert;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxAlertGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
public class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple alert
    [new FluxAlert()
      {
        Metadata = new FluxNamespacedMetadata
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
        Metadata = new FluxNamespacedMetadata
        {
          Name = "alert-complex",
          Namespace = "alert-complex",
          Labels = new Dictionary<string, string>
          {
            ["key"] = "value"
          }
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
