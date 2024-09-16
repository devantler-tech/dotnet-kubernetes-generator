# ☸️ .NET Kubernetes Generator

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler/dotnet-kubernetes-generator/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler/dotnet-kubernetes-generator/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler/dotnet-kubernetes-generator/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler/dotnet-kubernetes-generator)

A simple code generator that can generate Kubernetes resources.

<details>
  <summary>Show/hide folder structure</summary>

<!-- readme-tree start -->
```
.
├── .github
│   └── workflows
├── Devantler.KubernetesGenerator.Core
│   ├── Converters
│   └── Inspectors
├── Devantler.KubernetesGenerator.Flux
│   └── Models
│       ├── Dependencies
│       ├── Images
│       ├── KubeConfig
│       ├── Metadata
│       ├── Patches
│       ├── SecretRef
│       └── Sources
├── Devantler.KubernetesGenerator.Flux.Tests
│   ├── FluxHelmReleaseGeneratorTests
│   ├── FluxHelmRepositoryGeneratorTests
│   └── FluxKustomizationGeneratorTests
├── Devantler.KubernetesGenerator.K3d
│   └── Models
├── Devantler.KubernetesGenerator.K3d.Tests
│   └── K3dConfigGeneratorTests
├── Devantler.KubernetesGenerator.KSail
│   └── Models
│       └── Registry
├── Devantler.KubernetesGenerator.KSail.Tests
│   └── KSailClusterGeneratorTests
├── Devantler.KubernetesGenerator.Kustomize
│   └── Models
│       ├── Generators
│       └── Patches
├── Devantler.KubernetesGenerator.Kustomize.Tests
│   ├── KustomizeComponentGeneratorTests
│   └── KustomizeKustomizationGeneratorTests
├── Devantler.KubernetesGenerator.Native
│   ├── Cluster
│   ├── ConfigAndStorage
│   ├── Metadata
│   ├── Service
│   └── Workloads
└── Devantler.KubernetesGenerator.Native.Tests
    ├── ClusterTests
    │   ├── APIServiceGeneratorTests
    │   ├── BindingGeneratorTests
    │   ├── CertificateSigningRequestGeneratorTests
    │   ├── ClusterRoleBindingGeneratorTests
    │   ├── ClusterRoleGeneratorTests
    │   ├── ComponentStatusGeneratorTests
    │   ├── FlowSchemaGeneratorTests
    │   ├── IPAddressGeneratorTests
    │   ├── LeaseCandidateGeneratorTests
    │   ├── LeaseGeneratorTests
    │   ├── LocalSubjectAccessReviewGeneratorTests
    │   ├── NamespaceGeneratorTests
    │   ├── NetworkPolicyGeneratorTests
    │   ├── NodeGeneratorTests
    │   ├── PersistentVolumeGeneratorTests
    │   ├── PriorityLevelConfigurationGeneratorTests
    │   ├── ResourceQuotaGeneratorTests
    │   ├── RoleBindingGeneratorTests
    │   ├── RoleGeneratorTests
    │   ├── RuntimeClassGeneratorTests
    │   ├── SelfSubjectAccessReviewGeneratorTests
    │   ├── SelfSubjectReviewGeneratorTests
    │   ├── SelfSubjectRulesReviewGeneratorTests
    │   ├── ServiceAccountGeneratorTests
    │   ├── ServiceCIDRGeneratorTests
    │   ├── StorageVersionGeneratorTests
    │   ├── StorageVersionMigrationGeneratorTests
    │   ├── SubjectAccessReviewGeneratorTests
    │   └── TokenReviewGeneratorTests
    ├── ConfigAndStorageTests
    │   ├── CSIDriverGeneratorTests
    │   ├── CSINodeGeneratorTests
    │   ├── CSIStorageCapacityGeneratorTests
    │   ├── ConfigMapGeneratorTests
    │   ├── PersistentVolumeClaimGeneratorTests
    │   ├── SecretGeneratorTests
    │   ├── StorageClassGeneratorTests
    │   ├── VolumeAttachmentGeneratorTests
    │   └── VolumeAttributesClassGeneratorTests
    ├── MetadataTests
    │   ├── ClusterTrustBundleGeneratorTests
    │   ├── ControllerRevisionGeneratorTests
    │   ├── CustomResourceDefinitionGeneratorTests
    │   ├── DeviceClassGeneratorTests
    │   ├── EventGeneratorTests
    │   ├── HorizontalPodAutoscalerGeneratorTests
    │   ├── LimitRangeGeneratorTests
    │   ├── MutatingWebhookConfigurationGeneratorTests
    │   ├── PodDisruptionBudgetGeneratorTests
    │   ├── PodSchedulingContextGeneratorTests
    │   ├── PodTemplateGeneratorTests
    │   ├── PriorityClassGeneratorTests
    │   ├── ResourceClaimGeneratorTests
    │   ├── ResourceClaimTemplateGeneratorTests
    │   ├── ResourceSliceGeneratorTests
    │   ├── ValidatingAdmissionPolicyBindingGeneratorTests
    │   ├── ValidatingAdmissionPolicyGeneratorTests
    │   └── ValidatingWebhookConfigurationGeneratorTests
    ├── ServiceTests
    │   ├── EndpointSliceGeneratorTests
    │   ├── EndpointsGeneratorTests
    │   ├── IngressClassGeneratorTests
    │   ├── IngressGeneratorTests
    │   └── ServiceGeneratorTests
    └── WorkloadTests
        ├── CronJobGeneratorTests
        ├── DaemonSetGeneratorTests
        ├── DeploymentGeneratorTests
        ├── JobGeneratorTests
        ├── PodGeneratorTests
        ├── ReplicaSetGeneratorTests
        ├── ReplicationControllerGeneratorTests
        └── StatefulSetGeneratorTests

115 directories
```
<!-- readme-tree end -->

</details>

## Prerequisites

- [.NET](https://dotnet.microsoft.com/en-us/)

## 🚀 Getting Started

To get started, you can install the packages from NuGet.

```bash
# For generating Flux resources
dotnet add package Devantler.KubernetesGenerator.Flux

# For generating K3d resources
dotnet add package Devantler.KubernetesGenerator.K3d

# For generating KSail resources
dotnet add package Devantler.KubernetesGenerator.KSail

# For generating Kustomize resources
dotnet add package Devantler.KubernetesGenerator.Kustomize

# For generating native resources
dotnet add package Devantler.KubernetesGenerator.Native
```

## Usage

To use the generators, all you need to do is to create and use a new instance of the generator for the specific resource you want to generate. For example, to generate a new ConfigMap resource, you can use the `ConfigMapKubernetesGenerator`.

```csharp
using Devantler.KubernetesGenerator.Native;

var generator = new ConfigMapKubernetesGenerator();

var configMap = new V1ConfigMap
{
    ApiVersion = "v1",
    Kind = "ConfigMap",
    Metadata = new V1ObjectMeta
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

### Flux

- `FluxHelmReleaseGenerator`
- `FluxHelmRepositoryGenerator`
- `FluxKustomizationGenerator`

### K3d

- `K3dConfigGenerator`

### KSail

- `KSailClusterGenerator`

### Kustomize

- `KustomizeComponentGenerator`
- `KustomizeKustomizationGenerator`

### Native

The native generators are categorized according to the groupings on [Kubernetes API Overview](https://kubernetes.io/docs/reference/generated/kubernetes-api/v1.31)

#### Cluster

- `APIServiceGenerator`
- `BindingGenerator`
- `CertificateSigningRequestGenerator`
- `ClusterRoleBindingGenerator`
- `ClusterRoleGenerator`
- `ComponentStatusGenerator`
- `FlowSchemaGenerator`
- `IPAddressGenerator`
- `LeaseCandidateGenerator`
- `LeaseGenerator`
- `LocalSubjectAccessReviewGenerator`
- `NamespaceGenerator`
- `NetworkPolicyGenerator`
- `NodeGenerator`
- `PersistentVolumeGenerator`
- `PriorityLevelConfigurationGenerator`
- `ResourceQuotaGenerator`
- `RoleBindingGenerator`
- `RoleGenerator`
- `RuntimeClassGenerator`
- `SelfSubjectAccessReviewGenerator`
- `SelfSubjectReviewGenerator`
- `SelfSubjectRulesReviewGenerator`
- `ServiceAccountGenerator`
- `ServiceCIDRGenerator`
- `StorageVersionGenerator`
- `StorageVersionMigrationGenerator`
- `SubjectAccessReviewGenerator`
- `TokenReviewGenerator`
- `TokenRequestGenerator` ❌

#### Config and Storage

- `ConfigMapGenerator`
- `CSIDriverGenerator`
- `CSINodeGenerator`
- `CSIStorageCapacityGenerator`
- `PersistentVolumeClaimGenerator`
- `SecretGenerator`
- `StorageClassGenerator`
- `VolumeAttachmentGenerator`
- `VolumeAttributesClassGenerator`

#### Metadata

- `ClusterTrustBundleGenerator`
- `ControllerRevisionGenerator`
- `CustomResourceDefinitionGenerator`
- `DeviceClassGenerator`
- `EventGenerator`
- `HorizontalPodAutoscalerGenerator`
- `LimitRangeGenerator`
- `MutatingWebhookConfigurationGenerator`
- `PodDisruptionBudgetGenerator`
- `PodSchedulingContextGenerator`
- `PodTemplateGenerator`
- `PriorityClassGenerator`
- `ResourceClaimGenerator`
- `ResourceClaimTemplateGenerator`
- `ResourceSliceGenerator`
- `ValidatingAdmissionPolicyBindingGenerator`
- `ValidatingAdmissionPolicyGenerator`
- `ValidatingWebhookConfigurationGenerator`

#### Service

- `EndpointsGenerator`
- `EndpointSliceGenerator`
- `IngressClassGenerator`
- `IngressGenerator`
- `ServiceGenerator`

#### Workloads

- `CronJobGenerator`
- `DaemonSetGenerator`
- `DeploymentGenerator`
- `JobGenerator`
- `PodGenerator`
- `ReplicaSetGenerator`
- `ReplicationControllerGenerator`
- `StatefulSetGenerator`
