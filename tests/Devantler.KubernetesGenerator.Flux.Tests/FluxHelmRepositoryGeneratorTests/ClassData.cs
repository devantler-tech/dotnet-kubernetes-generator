using System.Collections;
using Devantler.KubernetesGenerator.Flux.Models;
using Devantler.KubernetesGenerator.Flux.Models.HelmRepository;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxHelmRepositoryGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
sealed class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple HelmRepository
    [new FluxHelmRepository()
    {
      Metadata = new FluxNamespacedMetadata()
      {
        Name = "helm-repository-simple",
      },
      Spec = new FluxHelmRepositorySpec()
      {
        Url = new Uri("https://charts.example.com"),
      },
    }, "helm-repository-simple.yaml"],

    // Complex HelmRepository
    [new FluxHelmRepository()
    {
      Metadata = new FluxNamespacedMetadata(new Dictionary<string, string>()
        {
          ["key"] = "value"
        }
      )
      {
        Name = "helm-repository-complex",
        Namespace = "helm-repository-complex",
      },
      Spec = new FluxHelmRepositorySpec()
      {
        Interval = "1m",
        Url = new Uri("https://charts.example.com"),
        Type = FluxHelmRepositorySpecType.OCI
      },
    }, "helm-repository-complex.yaml"],
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
