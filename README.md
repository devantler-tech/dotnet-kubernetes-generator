# ☸️ .NET Kubernetes Generator

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler-tech/dotnet-kubernetes-generator/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler-tech/dotnet-kubernetes-generator/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler-tech/dotnet-kubernetes-generator/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler-tech/dotnet-kubernetes-generator)

A simple code generator that can generate Kubernetes resources.

## Prerequisites

- [.NET](https://dotnet.microsoft.com/en-us/)

## 🚀 Getting Started

To get started, you can install the packages from NuGet.

```bash
# For generating Cert Manager resources
dotnet add package DevantlerTech.KubernetesGenerator.CertManager

# For generating Flux resources
dotnet add package DevantlerTech.KubernetesGenerator.Flux

# For generating Kind resources
dotnet add package DevantlerTech.KubernetesGenerator.Kind

# For generating K3d resources
dotnet add package DevantlerTech.KubernetesGenerator.K3d

# For generating Kustomize resources
dotnet add package DevantlerTech.KubernetesGenerator.Kustomize

# For generating native resources
dotnet add package DevantlerTech.KubernetesGenerator.Native
```

## 📝 Usage

To use the generators, all you need to do is to create and use a new instance of the generator for the specific resource you want to generate. For example, to generate a new ConfigMap resource, you can use the `ConfigMapKubernetesGenerator`.

```csharp
using DevantlerTech.KubernetesGenerator.Native;

var generator = new ConfigMapKubernetesGenerator();

var configMap = new ConfigMap
{
    Metadata = new NamespacedMetadata
    {
        Name = "my-config-map",
        Namespace = "default"
    },
    Data = new Dictionary<string, string>
    {
        { "key1", "value1" },
        { "key2", "value2" }
    }
};

await generator.GenerateAsync(configMap, "path/to/output/config-map.yaml");
```

This will generate a new ConfigMap resource at the specified path.

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: my-config-map
  namespace: default
data:
  key1: value1
  key2: value2
```

## Supported Generators

### Cert Manager

- `CertManagerCertificateGenerator`
- `CertManagerClusterIssuerGenerator`

### Flux

- `FluxAlertGenerator`
- `FluxAlertProviderGenerator`
- `FluxHelmReleaseGenerator`
- `FluxHelmRepositoryGenerator`
- `FluxKustomizationGenerator`

### Kind

- `KindConfigGenerator`

### K3d

- `K3dConfigGenerator`

### Kustomize

- `KustomizeComponentGenerator`
- `KustomizeKustomizationGenerator`

### Native

- `ClusterRoleBindingGenerator`
- `ClusterRoleGenerator`
- `ConfigMapGenerator`
- `CronJobGenerator`
- `DaemonSetGenerator`
- `DeploymentGenerator`
- `HorizontalPodAutoscalerGenerator`
- `IngressGenerator`
- `JobGenerator`
- `NamespaceGenerator`
- `NetworkPolicyGenerator`
- `PersistentVolumeClaimGenerator`
- `PersistentVolumeGenerator`
- `PodDisruptionBudgetGenerator`
- `PodGenerator`
- `PriorityClassGenerator`
- `ResourceQuotaGenerator`
- `RoleBindingGenerator`
- `RoleGenerator`
- `SecretGenerator`
- `ServiceAccountGenerator`
- `ServiceGenerator`
- `StatefulSetGenerator`
