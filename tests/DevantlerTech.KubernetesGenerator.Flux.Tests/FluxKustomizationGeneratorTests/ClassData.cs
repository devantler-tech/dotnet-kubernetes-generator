using System.Collections;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models.Kustomization;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxKustomizationGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
sealed class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple Kustomization
    [new FluxKustomization()
    {
      Metadata = new NamespacedMetadata()
      {
        Name = "kustomization-simple",
      }
    }, "kustomization-simple.yaml"],

    // Complex Kustomization
    [new FluxKustomization()
    {
      Metadata = new NamespacedMetadata(new Dictionary<string, string>()
        {
          ["key"] = "value"
        }
      )
      {
        Name = "kustomization-complex",
        Namespace = "kustomization-complex"
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
        Wait = true,
        PostBuild = new FluxKustomizationSpecPostBuild(new Dictionary<string, string>()
          {
            ["key"] = "value"
          }
        )
        {
          SubstituteFrom = [
            new FluxKustomizationSpecPostBuildSubstituteFrom()
            {
              Kind = FluxConfigRefKind.ConfigMap,
              Name = "config-map",
              Optional = true
            }
          ]
        }
      }
    }, "kustomization-complex.yaml"],
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
