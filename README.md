# ☸️ .NET Kubernetes Generator

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler-tech/dotnet-kubernetes-generator/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler-tech/dotnet-kubernetes-generator/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler-tech/dotnet-kubernetes-generator/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler-tech/dotnet-kubernetes-generator)

A simple code generator that can generate Kubernetes resources.

<details>
  <summary>Show/hide folder structure</summary>

<!-- readme-tree start -->
```
.
├── .github
│   └── workflows
├── TEMP
│   ├── src
│   │   └── Models
│   │       ├── ImagePolicy
│   │       ├── ImageRepository
│   │       ├── ImageUpdateAutomation
│   │       └── Receiver
│   └── tests
│       ├── FluxHelmRepositoryGeneratorTests
│       └── FluxKustomizationGeneratorTests
├── src
│   ├── Devantler.KubernetesGenerator.CertManager
│   │   └── Models
│   │       └── IssuerRef
│   ├── Devantler.KubernetesGenerator.Core
│   │   ├── Converters
│   │   └── Inspectors
│   ├── Devantler.KubernetesGenerator.Flux
│   │   └── Models
│   │       ├── Alert
│   │       ├── AlertProvider
│   │       ├── HelmRelease
│   │       ├── HelmRepository
│   │       └── Kustomization
│   ├── Devantler.KubernetesGenerator.K3d
│   │   └── Models
│   │       ├── Options
│   │       │   ├── K3d
│   │       │   ├── K3s
│   │       │   └── Runtime
│   │       └── Registries
│   ├── Devantler.KubernetesGenerator.Kind
│   │   └── Models
│   │       ├── Networking
│   │       └── Nodes
│   ├── Devantler.KubernetesGenerator.Kustomize
│   │   └── Models
│   │       ├── Generators
│   │       └── Patches
│   └── Devantler.KubernetesGenerator.Native
│       └── Models
└── tests
    ├── Devantler.KubernetesGenerator.CertManager.Tests
    │   ├── CertManagerCertificateGeneratorTests
    │   └── CertManagerClusterIssuerGeneratorTests
    ├── Devantler.KubernetesGenerator.Core.Tests
    ├── Devantler.KubernetesGenerator.Flux.Tests
    │   ├── FluxAlertGeneratorTests
    │   ├── FluxAlertProviderGeneratorTests
    │   ├── FluxHelmReleaseGeneratorTests
    │   ├── FluxHelmRepositoryGeneratorTests
    │   └── FluxKustomizationGeneratorTests
    ├── Devantler.KubernetesGenerator.K3d.Tests
    │   └── K3dConfigGeneratorTests
    ├── Devantler.KubernetesGenerator.Kind.Tests
    │   └── KindConfigGeneratorTests
    ├── Devantler.KubernetesGenerator.Kustomize.Tests
    │   ├── KustomizeComponentGeneratorTests
    │   └── KustomizeKustomizationGeneratorTests
    └── Devantler.KubernetesGenerator.Native.Tests
        ├── ClusterRoleBindingGeneratorTests
        ├── ClusterRoleGeneratorTests
        ├── ConfigMapGeneratorTests
        ├── CronJobGeneratorTests
        ├── DaemonSetGeneratorTests
        ├── DeploymentGeneratorTests
        ├── HorizontalPodAutoscalerGeneratorTests
        ├── IngressGeneratorTests
        ├── JobGeneratorTests
        ├── NamespaceGeneratorTests
        ├── NetworkPolicyGeneratorTests
        ├── PersistentVolumeClaimGeneratorTests
        ├── PersistentVolumeGeneratorTests
        ├── PodDisruptionBudgetGeneratorTests
        ├── PodGeneratorTests
        ├── PriorityClassGeneratorTests
        ├── ResourceQuotaGeneratorTests
        ├── RoleBindingGeneratorTests
        ├── RoleGeneratorTests
        ├── SecretGeneratorTests
        ├── ServiceAccountGeneratorTests
        ├── ServiceGeneratorTests
        └── StatefulSetGeneratorTests

86 directories
```
<!-- readme-tree end -->

</details>

## Prerequisites

- [.NET](https://dotnet.microsoft.com/en-us/)

## 🚀 Getting Started

To get started, you can install the packages from NuGet.

```bash
# For generating Cert Manager resources
dotnet add package Devantler.KubernetesGenerator.CertManager

# For generating Flux resources
dotnet add package Devantler.KubernetesGenerator.Flux

# For generating Kind resources
dotnet add package Devantler.KubernetesGenerator.Kind

# For generating K3d resources
dotnet add package Devantler.KubernetesGenerator.K3d

# For generating Kustomize resources
dotnet add package Devantler.KubernetesGenerator.Kustomize

# For generating native resources
dotnet add package Devantler.KubernetesGenerator.Native
```

## 📝 Usage

To use the generators, all you need to do is to create and use a new instance of the generator for the specific resource you want to generate. For example, to generate a new ConfigMap resource, you can use the `ConfigMapKubernetesGenerator`.

```csharp
using Devantler.KubernetesGenerator.Native;

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
