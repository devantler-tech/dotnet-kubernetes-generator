using System.Collections;
using Devantler.KubernetesGenerator.Flux.Models;
using Devantler.KubernetesGenerator.Flux.Models.Kustomization;

namespace Devantler.KubernetesGenerator.Flux.Tests.FluxKustomizationGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
public class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple Kustomization
    [new FluxKustomization()
    {
      Metadata = new FluxNamespacedMetadata()
      {
        Name = "kustomization-simple",
      }
    }, "kustomization-simple.yaml"],

    // Complex Kustomization
    [new FluxKustomization()
    {
      Metadata = new FluxNamespacedMetadata()
      {
        Name = "kustomization-complex",
        Namespace = "kustomization-complex",
        Labels = new Dictionary<string, string>()
        {
          ["key"] = "value"
        }
      },
      Spec = new FluxKustomizationSpec()
      {
        Interval = "1m",
        Decryption = new FluxKustomizationSpecDecryption()
        {
          Provider = FluxKustomizationSpecDecryptionProvider.SOPS,
          SecretRef = new FluxSecretRef()
          {
            Name = "sops-secret"
          }
        },
        DependsOn = [
          new FluxDependsOn()
          {
            Name = "dependency-1",
            Namespace = "dependency-1"
          },
          new FluxDependsOn()
          {
            Name = "dependency-1"
          }
        ],
        HealthChecks = [
          new FluxKustomizationSpecHealthCheck()
          {
            Kind = "Pod",
            Name = "podinfo",
            Namespace = "podinfo"
          }
        ],
        KubeConfig = new FluxKubeconfig()
        {
          SecretRef = new FluxSecretRef()
          {
            Name = "kubeconfig-secret"
          }
        },
        Path = "./root",
        Prune = true,
        RetryInterval = "10m",
        ServiceAccountName = "service-account",
        SourceRef = new FluxKustomizationSpecSourceRef()
        {
          Kind = FluxKustomizationSpecSourceRefKind.OCIRepository,
          Name = "source",
          Namespace = "flux-system"
        },
        Timeout = "5m",
        Wait = true
      },
    }, "kustomization-complex.yaml"],
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
