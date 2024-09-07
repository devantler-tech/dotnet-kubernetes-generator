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
├── Devantler.KubernetesGenerator.Configs.K3d
│   └── Models
├── Devantler.KubernetesGenerator.Configs.K3d.Tests
│   └── K3dConfigResourceGeneratorTests
├── Devantler.KubernetesGenerator.Core
├── Devantler.KubernetesGenerator.CustomResources.Flux
├── Devantler.KubernetesGenerator.CustomResources.Flux.Tests
├── Devantler.KubernetesGenerator.Native
└── Devantler.KubernetesGenerator.Native.Tests

11 directories
```
<!-- readme-tree end -->

</details>

## Prerequisites

- [.NET](https://dotnet.microsoft.com/en-us/)

## 🚀 Getting Started

To get started, you can install the packages from NuGet.

```bash
# For the Native Kubernetes Generator
dotnet add package Devantler.KubernetesGenerator.Native

# For the Flux Custom Resource Generator
dotnet add package Devantler.KubernetesGenerator.CustomResources.Flux

# For the K3d Config Generator
dotnet add package Devantler.KubernetesGenerator.Configs.K3d
```

## Usage

To use the generators, all you need to do is to create and use a new instance of the generator for the specific resource you want to generate. For example, to generate a new ConfigMap resource, you can use the `ConfigMapKubernetesGenerator`.

```csharp
using Devantler.KubernetesGenerator.Native;

var generator = new ConfigMapKubernetesGenerator();

var configMap = new ConfigMap
{
    Metadata = new ObjectMeta
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

### Native Kubernetes Generators

- ConfigMapKubernetesGenerator (To be implemented)
- SecretKubernetesGenerator (To be implemented)
- DeploymentKubernetesGenerator (To be implemented)
- ServiceKubernetesGenerator (To be implemented)
- And more...

### Custom Resource Generators

- FluxKustomizationKubernetesGenerator (To be implemented)
- FluxHelmReleaseKubernetesGenerator (To be implemented)
- FluxHelmRepositoryKubernetesGenerator (To be implemented)
- And more...

### Config Generators

- K3dConfigKubernetesGenerator
