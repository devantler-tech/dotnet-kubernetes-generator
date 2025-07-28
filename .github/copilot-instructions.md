# .NET Kubernetes Generator - Copilot Instructions

## Project Overview

This library provides code generators for Kubernetes resources across multiple domains. It enables programmatic generation of YAML manifests through strongly-typed models, supporting both native Kubernetes resources and specialized tooling ecosystems.

## Architecture Principles

- **Modular Design**: Core abstractions with domain-specific extensions
- **Separation of Concerns**: API interactions separate from model definitions and YAML generation
- **Extensibility**: Support for new resource types and generation strategies

## Core Development Principles

### Code Quality Standards
- Follow established language conventions and project patterns
- Enable strict error checking and nullable reference types
- Maintain consistent naming conventions throughout the codebase
- Use appropriate abstraction levels for different concerns

### Generator Design Patterns
- Inherit from appropriate base generator classes
- Create custom models that align with user-facing command patterns
- Avoid direct dependency on external client library models for core functionality
- Design models to reflect logical user workflows while generating valid output

### Documentation & Maintainability
- Document all public interfaces with clear descriptions
- Include parameter documentation for complex methods
- Maintain code that is self-documenting through clear naming
- Keep documentation current with code changes

## Testing Philosophy

### Test Organization & Structure
- Organize tests by generator with clear naming conventions
- Use snapshot testing for YAML output verification
- Structure tests to clearly demonstrate expected behavior
- Maintain test files that align with verification strategies

### Test Design Principles
- **Avoid redundant tests**: Analyze whether tests validate meaningfully different scenarios
- **Understand underlying capabilities**: Research command parameters and options to determine valid test scenarios
- **Focus on distinct functionality**: One test per unique behavior or parameter combination
- **Test meaningful variations**: Focus on different parameter combinations, error conditions, and boundary cases
- **Validate test necessity**: Each test should verify a unique aspect of functionality

### Quality Assurance Through Testing
- Tests should reflect real-world usage patterns
- Each test should have a clear, unique purpose
- Consolidate tests that differ only in trivial ways
- Focus on boundary conditions and error scenarios over superficial variations

## Development Workflow

### Implementation Approach
1. **Research thoroughly**: Understand the full scope of underlying tools, commands, or libraries
2. **Follow established patterns**: Use existing base classes and architectural patterns
3. **Create appropriate models**: Design models that align with user workflows and generate valid output
4. **Implement minimal functionality**: Let base classes handle common concerns like serialization
5. **Analyze test scenarios**: Identify genuinely different use cases before writing tests
6. **Create focused tests**: Write tests that cover unique functionality without redundancy
7. **Validate quality**: Review for duplicate functionality and adherence to established patterns
8. Add new project references to solution file if applicable
9. **Self-review for quality**: Check for duplicate functionality, redundant tests, and adherence to established patterns

### Model Design Guidelines
- Place models in appropriate subdirectories within projects
- Design models to align with user command patterns and workflows
- Follow consistent naming conventions for properties
- Include appropriate metadata properties using shared classes
- Support serialization requirements for output format
- Models should represent logical user workflows while generating valid output

### Output Generation Standards
- Generate files with appropriate extensions
- Support overwrite functionality in generation methods
- Create output directories as needed
- Use consistent encoding for file operations
- Handle errors appropriately with meaningful exception types

## Quality Assurance & Self-Review

### Pre-Implementation Analysis
- **Research dependencies thoroughly**: Understand the full scope of underlying tools, commands, or libraries
- **Analyze existing patterns**: Review similar implementations to understand established patterns and avoid inconsistencies
- **Identify distinct scenarios**: Map out genuinely different use cases and input scenarios before implementation

### Pre-Submission Checklist
- **Review test redundancy**: Ensure each test validates meaningfully different scenarios or functionality
- **Verify alignment with underlying tools**: Confirm that models and tests reflect actual capabilities and limitations
- **Check for duplicate functionality**: Scan for multiple implementations that validate the same behavior with cosmetic differences
- **Validate pattern consistency**: Ensure implementation follows established architectural patterns
- **Focus on meaningful edge cases**: Prioritize boundary conditions, error scenarios, and parameter combinations over superficial variations

### Code Review Self-Assessment
- **Evaluate test purpose**: Each test should have a clear, unique objective
- **Consider consolidation opportunities**: If tests differ only in trivial ways, combine them
- **Assess real-world relevance**: Tests should mirror actual usage patterns
- **Identify redundancies**: Look for patterns like identical structures with minimal differences

## Documentation Evolution

### Continuous Improvement
- Keep documentation current with code changes and new learnings
- Add new patterns and conventions as they emerge during development
- Update architectural guidance based on practical experience
- Include new testing strategies as they're developed
- Capture effective design principles and decisions
- Document quality lessons learned to prevent recurring issues
- Enhance validation practices based on common mistakes identified

## Integration Guidelines

### Usage Patterns
- Create generator instances with appropriate models
- Use strongly-typed model configuration
- Generate output to specified file paths
- Handle errors appropriately in consuming applications

### Extensibility Principles
- Each domain should be modular and independently packageable
- Core functionality should be shared across implementations
- Version packages consistently to maintain compatibility

## Commit, PR, and Branch Naming

- Use semantic commit conventions for all commit messages, PR titles, and branch names.
- Format:
  - `<semantic-name>/branch` for branch names (e.g., `feat/add-generator`)
  - `<semantic-name>: commit message` for commit messages (e.g., `fix: correct YAML serialization`)
  - `<semantic-name>: PR Title` for pull request titles (e.g., `chore: update documentation`)
