# .NET Kubernetes Generator - Copilot Instructions

## Project Overview

This is a .NET 9.0 library that provides code generators for Kubernetes resources across multiple domains. The library enables programmatic generation of YAML manifests for Kubernetes resources through strongly-typed C# models.

## Architecture & Structure

- **Core Library**: `DevantlerTech.KubernetesGenerator.Core` - Base abstractions and common functionality
- **Native Resources**: `DevantlerTech.KubernetesGenerator.Native` - Standard Kubernetes resources (Pods, Services, Deployments, etc.)
- **Specialized Generators**:
  - `DevantlerTech.KubernetesGenerator.CertManager` - Cert Manager resources
  - `DevantlerTech.KubernetesGenerator.Flux` - Flux CD GitOps resources
  - `DevantlerTech.KubernetesGenerator.Kind` - Kind cluster configuration
  - `DevantlerTech.KubernetesGenerator.K3d` - K3d cluster configuration
  - `DevantlerTech.KubernetesGenerator.Kustomize` - Kustomize manifests

## Key Dependencies

- **KubernetesClient**: Used only for API interactions, NOT for model definitions
- **YamlDotNet.System.Text.Json**: YAML serialization with System.Text.Json integration

## Coding Standards & Conventions

### General C# Standards

- Use file-scoped namespaces (`namespace DevantlerTech.KubernetesGenerator.Core;`)
- Enable nullable reference types and implicit usings
- Treat warnings as errors with strict analysis mode
- Use expression-bodied members for simple accessors/methods when appropriate
- Prefer `var` when type is apparent or for complex types
- Private fields must use underscore prefix and camelCase (`_serializer`)
- Use PascalCase for constants

### Generator Implementation Patterns

- All generators inherit from `BaseKubernetesGenerator<T>` where T is a custom model type
- Generator classes should be simple and focused: `public class PodGenerator : BaseKubernetesGenerator<Pod> { }`
- **NEVER use KubernetesClient models** (V1ConfigMap, V1Deployment, etc.) - these are only for API interactions
- Create custom models in `Models/` subdirectories that align with CLI usage patterns (`kubectl create`, `kubectl run`, etc.)
- Custom models should reflect the CLI options but be structured to fit the generated YAML

### Documentation Standards

- All public classes and methods must have XML documentation
- Use `<summary>` tags for class and method descriptions
- Include `<param>` tags for method parameters
- Include `<returns>` tags for methods that return values
- Example: `/// <summary>A generator for Kubernetes ConfigMap objects.</summary>`

### Serialization & YAML Generation

- YAML output uses camelCase naming convention
- Disable aliases in YAML serialization
- Use custom type converters for Kubernetes-specific types (IntstrIntOrString, ResourceQuantity, ByteArray)
- Comment gathering and preservation is implemented through custom type inspectors

## Testing Patterns

### Test Organization

- Each generator has a dedicated test directory: `GeneratorNameTests/`
- Main test class: `GenerateAsyncTests.cs`
- Verification files: `*.verified.yaml` for snapshot testing

### Test Structure

- Use Verify library for snapshot testing of generated YAML
- Test method naming: `GenerateAsync_WithSpecificCondition_ShouldGenerateExpectedResult`
- Always test with comprehensive model data including all properties
- Use temporary file paths for output testing
- Clean up test files after execution

### Example Test Pattern

```csharp
[Fact]
public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidResource()
{
    // Arrange
    var generator = new PodGenerator();
    var model = new Pod
    {
        Metadata = new Metadata { Name = "test-pod", Namespace = "default" },
        Spec = new PodSpec
        {
            Containers = [new PodContainer { Name = "app", Image = "nginx:latest" }]
        }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "pod.yaml");
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName("pod.yaml");
}
```

## File and Directory Conventions

### Project Structure

- Source code: `/src/ProjectName/`
- Tests: `/tests/ProjectName.Tests/`
- Each project has its own `.csproj` file
- Solution file: `DevantlerTech.KubernetesGenerator.slnx`

### Generated Output

- Generate YAML files with `.yaml` extension
- Support overwrite parameter in `GenerateAsync` method
- Create output directories if they don't exist
- Use UTF-8 encoding for all file operations

## Error Handling & Validation

- Use `ArgumentNullException.ThrowIfNull()` for null parameter validation
- Throw `KubernetesGeneratorException` for generator-specific errors
- Validate file paths and create directories as needed
- Support cancellation tokens for async operations

## Common Implementation Tasks

### Adding a New Generator

1. Create generator class inheriting from `BaseKubernetesGenerator<T>` or a more specific base generator
2. Create custom model type T in `Models/` subdirectory that aligns with CLI usage patterns
3. Design the model to reflect CLI command options and parameters
4. Add minimal implementation (base class handles serialization)
5. Create comprehensive unit tests with verification
6. Add project reference to solution file

### Adding Custom Models

- Place in `Models/` subdirectory within each generator project
- Design models to align with CLI usage patterns (`kubectl create job`, `kubectl run`, `kubectl create ingress`, `flux create helmrelease`, etc.)
- Follow Kubernetes API conventions for property naming (camelCase in YAML output)
- Include appropriate metadata properties (use shared `Metadata` class)
- Use meaningful property names that reflect CLI parameters and options
- Support YAML serialization attributes if needed
- Models should represent the logical structure of CLI commands while generating valid Kubernetes YAML

### YAML Customization

- Custom type converters go in `Converters/` directory
- Type inspectors for special handling in `Inspectors/` directory
- Object graph visitors for processing in root of Core project

## Build and Development

- Target Framework: .NET 9.0
- Enable documentation generation for all projects
- Use EditorConfig for consistent formatting (2-space indentation, LF line endings)
- Maximum line length: 120 characters
- All projects must build without warnings in release mode

## Integration Guidelines

### Usage Patterns

```csharp
var generator = new PodGenerator();
var pod = new Pod
{
    Metadata = new Metadata { Name = "my-pod", Namespace = "default" },
    Spec = new PodSpec
    {
        Containers = [
            new PodContainer { Name = "app", Image = "nginx:latest" }
        ]
    }
};
await generator.GenerateAsync(pod, "path/to/output.yaml");
```

### NuGet Package Organization

- Each generator type should be its own NuGet package
- Core functionality shared across all packages
- Version all packages together for consistency
