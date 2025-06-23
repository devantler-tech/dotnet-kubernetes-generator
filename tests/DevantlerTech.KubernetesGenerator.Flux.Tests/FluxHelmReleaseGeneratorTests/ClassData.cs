using System.Collections;
using DevantlerTech.KubernetesGenerator.Flux.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models.HelmRelease;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxHelmReleaseGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
sealed class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple HelmRelease - Chart
    [new FluxHelmRelease(){
      Metadata = new FluxNamespacedMetadata
      {
        Name = "helm-release-chart-simple",
      },
      Spec = new FluxHelmReleaseSpec(
        new FluxHelmReleaseSpecChart
        {
          Spec = new FluxHelmReleaseSpecChartSpec
          {
            Chart = "nginx"
          }
        }
      )
    }, "helm-release-chart-simple.yaml"],

    // Complex HelmRelease - Chart
    [new FluxHelmRelease(){
      Metadata = new FluxNamespacedMetadata(new Dictionary<string, string>
        {
          { "key", "value" },
        }
      )
      {
        Name = "helm-release-chart-complex",
        Namespace = "helm-release-chart-complex"
      },
      Spec = new FluxHelmReleaseSpec(
        new FluxHelmReleaseSpecChart
        {
          Spec = new FluxHelmReleaseSpecChartSpec
          {
            Chart = "nginx",
            Interval = "1m",
            Version = "1.0.0",
            ReconcileStrategy = FluxHelmReleaseSpecChartSpecReconcileStrategy.Revision,
            SourceRef = new FluxHelmReleaseSpecChartSpecSourceRef
            {
              Kind = FluxHelmReleaseSpecChartSpecSourceRefKind.HelmRepository,
              Name = "nginx",
              Namespace = "helm-release-chart-complex",
            }
          }
        }
      )
      {
        Interval = "1m",
        InstallUpgrade = new FluxHelmReleaseSpecInstallUpgrade
        {
          Crds = FluxHelmReleaseSpecInstallUpgradeCrds.CreateReplace
        },
        DependsOn =
        [
          new FluxDependsOn
          {
            Name = "cert-manager",
            Namespace = "helm-release-chart-complex",
          }
        ],
        Kubeconfig = new FluxKubeconfig
        {
          SecretRef = new FluxSecretRef
          {
            Name = "kubeconfig-secret"
          }
        },
        ServiceAccountName = "helm-release-chart-complex-sa",
        ValuesFrom = new FluxConfigRef
        {
          Kind = FluxConfigRefKind.ConfigMap,
          Name = "values-configmap"
        }
      }
    }, "helm-release-chart-complex.yaml"],

    // Simple HelmRelease - ChartRef
    [new FluxHelmRelease(){
      Metadata = new FluxNamespacedMetadata
      {
        Name = "helm-release-chart-ref-simple",
      },
      Spec = new FluxHelmReleaseSpec(
        new FluxHelmReleaseSpecChartRef
        {
          Kind = FluxHelmReleaseSpecChartRefKind.HelmChart,
          Name = "nginx"
        }
      )
    }, "helm-release-chart-ref-simple.yaml"],

    // Complex HelmRelease - ChartRef
    [new FluxHelmRelease(){
      Metadata = new FluxNamespacedMetadata(new Dictionary<string, string>
        {
          ["key"] = "value"
        }
      )
      {
        Name = "helm-release-chart-ref-complex",
        Namespace = "helm-release-chart-ref-complex",
      },
      Spec = new FluxHelmReleaseSpec(
        new FluxHelmReleaseSpecChartRef
        {
          Kind = FluxHelmReleaseSpecChartRefKind.HelmChart,
          Name = "nginx",
          Namespace = "helm-release-chart-ref-complex",
        }
      )
      {
        Interval = "1m",
        InstallUpgrade = new FluxHelmReleaseSpecInstallUpgrade
        {
          Crds = FluxHelmReleaseSpecInstallUpgradeCrds.CreateReplace
        },
        DependsOn =
        [
          new FluxDependsOn
          {
            Name = "cert-manager",
            Namespace = "helm-release-chart-complex",
          }
        ],
        Kubeconfig = new FluxKubeconfig
        {
          SecretRef = new FluxSecretRef
          {
            Name = "kubeconfig-secret"
          }
        },
        ServiceAccountName = "helm-release-chart-ref-complex-sa",
        ValuesFrom = new FluxConfigRef
        {
          Kind = FluxConfigRefKind.ConfigMap,
          Name = "values-configmap"
        }
      }
    }, "helm-release-chart-ref-complex.yaml"],
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
