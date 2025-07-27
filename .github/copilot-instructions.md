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

### Test Design Principles

- **Avoid redundant tests**: Before creating multiple test methods, analyze if they test meaningfully different scenarios
- **Understand CLI command capabilities**: Research the underlying kubectl command parameters and options to determine valid test scenarios
- **One test per distinct functionality**: If a CLI command only accepts a name parameter (like `kubectl create namespace`), one comprehensive test is usually sufficient
- **Test edge cases and variations**: Focus on testing different parameter combinations, error conditions, and boundary cases rather than cosmetic differences
- **Validate test necessity**: Each test should verify a unique aspect of the generator's functionality or handle a distinct input scenario

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

1. **Research the CLI command thoroughly**: Understand the full parameter set, options, and limitations of the underlying kubectl/CLI command
2. Create generator class inheriting from `BaseKubernetesGenerator<T>` or a more specific base generator
3. Create custom model type T in `Models/` subdirectory that aligns with CLI usage patterns
4. Design the model to reflect CLI command options and parameters accurately
5. Add minimal implementation (base class handles serialization)
6. **Analyze test scenarios**: Identify genuinely different use cases and parameter combinations before writing tests
7. Create comprehensive unit tests with verification, avoiding redundant test methods
8. **Self-review for quality**: Check for duplicate functionality, redundant tests, and adherence to established patterns
9. Add project reference to solution file

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

## Quality Assurance & Self-Review

### Pre-Implementation Analysis
- **Research CLI commands thoroughly**: Before implementing generators, understand the full scope of the underlying kubectl/CLI command parameters, options, and limitations
- **Analyze existing patterns**: Review similar generators in the codebase to understand established patterns and avoid inconsistencies
- **Identify distinct scenarios**: Map out genuinely different use cases and input scenarios before writing tests or implementation

### Pre-Submission Checklist
- **Review test redundancy**: Ensure each test method validates a meaningfully different scenario or functionality
- **Verify CLI command alignment**: Confirm that models and tests reflect the actual capabilities and limitations of the underlying CLI commands
- **Check for duplicate functionality**: Scan for multiple tests that essentially validate the same behavior with cosmetic differences
- **Validate implementation consistency**: Ensure the generator follows established patterns (BaseNativeGenerator vs BaseKubernetesGenerator, custom models vs KubernetesClient models)
- **Test edge cases appropriately**: Focus on boundary conditions, error scenarios, and parameter combinations rather than superficial variations

### Code Review Self-Assessment
- **Ask "What does this test actually validate?"**: Each test should have a clear, unique purpose
- **Consider "Could these tests be combined?"**: If tests differ only in names or simple values without testing different functionality, consolidate them
- **Evaluate "Does this reflect real-world usage?"**: Tests should mirror how users would actually use the generator and CLI commands
- **Confirm "Are there any obvious redundancies?"**: Look for patterns like identical test structures with trivial differences

## Build and Development

- Target Framework: .NET 9.0
- Enable documentation generation for all projects
- Use EditorConfig for consistent formatting (2-space indentation, LF line endings)
- Maximum line length: 120 characters
- All projects must build without warnings in release mode

## Documentation Maintenance

### README.md Updates
- **Must be kept up-to-date** with new features, generators, and capabilities
- Update usage examples when adding new generator types
- Include new NuGet package information when packages are added
- Document breaking changes and migration guidance temporarily via GitHub admonitions at the top of the README
- Update feature lists and supported Kubernetes resources

### Copilot Instructions Evolution
- **copilot-instructions.md must be kept current** with new learnings and project evolution
- Add new patterns and conventions as they emerge during development
- Document generator-specific patterns when new generator types are added
- Update coding standards based on practical experience and code reviews
- Include new testing patterns and verification strategies as they're developed
- Capture architectural decisions and design principles that prove effective
- **Document quality assurance lessons**: Add new self-review practices and quality checkpoints based on discovered issues
- **Update pre-submission guidelines**: Enhance checklists and validation steps as new common mistakes are identified

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
