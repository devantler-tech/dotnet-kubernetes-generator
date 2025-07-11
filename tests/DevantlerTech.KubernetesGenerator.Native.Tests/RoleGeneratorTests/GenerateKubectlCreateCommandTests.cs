using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleGenerator.GenerateKubectlCreateCommand"/> method.
/// </summary>
public class GenerateKubectlCreateCommandTests
{
  /// <summary>
  /// Verifies that the kubectl create command is generated correctly for a role with all properties set.
  /// </summary>
  [Fact]
  public void GenerateKubectlCreateCommand_WithAllPropertiesSet_ShouldGenerateCorrectCommand()
  {
    // Arrange
    var model = new V1Role
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "Role",
      Metadata = new V1ObjectMeta
      {
        Name = "test-role",
        NamespaceProperty = "test-namespace"
      },
      Rules =
      [
        new V1PolicyRule
        {
          ApiGroups = ["apps"],
          Resources = ["deployments"],
          Verbs = ["get", "list", "create"],
          ResourceNames = ["test-deployment"]
        }
      ]
    };

    // Act
    string command = RoleGenerator.GenerateKubectlCreateCommand(model);

    // Assert
    Assert.Equal("kubectl create role test-role --namespace=test-namespace --verb=get --verb=list --verb=create --resource=deployments.apps --resource-name=test-deployment", command);
  }

  /// <summary>
  /// Verifies that the kubectl create command is generated correctly for a role without namespace.
  /// </summary>
  [Fact]
  public void GenerateKubectlCreateCommand_WithoutNamespace_ShouldGenerateCorrectCommand()
  {
    // Arrange
    var model = new V1Role
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "Role",
      Metadata = new V1ObjectMeta
      {
        Name = "test-role"
      },
      Rules =
      [
        new V1PolicyRule
        {
          Resources = ["pods"],
          Verbs = ["get", "list"]
        }
      ]
    };

    // Act
    string command = RoleGenerator.GenerateKubectlCreateCommand(model);

    // Assert
    Assert.Equal("kubectl create role test-role --verb=get --verb=list --resource=pods", command);
  }

  /// <summary>
  /// Verifies that the kubectl create command is generated correctly for a role with core API resources.
  /// </summary>
  [Fact]
  public void GenerateKubectlCreateCommand_WithCoreAPIResources_ShouldGenerateCorrectCommand()
  {
    // Arrange
    var model = new V1Role
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "Role",
      Metadata = new V1ObjectMeta
      {
        Name = "pod-reader",
        NamespaceProperty = "default"
      },
      Rules =
      [
        new V1PolicyRule
        {
          ApiGroups = [""],
          Resources = ["pods"],
          Verbs = ["get", "watch", "list"]
        }
      ]
    };

    // Act
    string command = RoleGenerator.GenerateKubectlCreateCommand(model);

    // Assert
    Assert.Equal("kubectl create role pod-reader --namespace=default --verb=get --verb=watch --verb=list --resource=pods", command);
  }

  /// <summary>
  /// Verifies that the method throws ArgumentNullException when model is null.
  /// </summary>
  [Fact]
  public void GenerateKubectlCreateCommand_WithNullModel_ShouldThrowArgumentNullException() =>
    Assert.Throws<ArgumentNullException>(() => RoleGenerator.GenerateKubectlCreateCommand(null!));

  /// <summary>
  /// Verifies that the method throws ArgumentNullException when metadata is null.
  /// </summary>
  [Fact]
  public void GenerateKubectlCreateCommand_WithNullMetadata_ShouldThrowArgumentNullException()
  {
    // Arrange
    var model = new V1Role
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "Role",
      Metadata = null
    };

    // Act & Assert
    _ = Assert.Throws<ArgumentNullException>(() => RoleGenerator.GenerateKubectlCreateCommand(model));
  }

  /// <summary>
  /// Verifies that the method throws ArgumentException when name is null or empty.
  /// </summary>
  [Fact]
  public void GenerateKubectlCreateCommand_WithNullOrEmptyName_ShouldThrowArgumentException()
  {
    // Arrange
    var model = new V1Role
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "Role",
      Metadata = new V1ObjectMeta
      {
        Name = ""
      }
    };

    // Act & Assert
    _ = Assert.Throws<ArgumentException>(() => RoleGenerator.GenerateKubectlCreateCommand(model));
  }

  /// <summary>
  /// Verifies that the method generates a basic command when there are no rules.
  /// </summary>
  [Fact]
  public void GenerateKubectlCreateCommand_WithNoRules_ShouldGenerateBasicCommand()
  {
    // Arrange
    var model = new V1Role
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "Role",
      Metadata = new V1ObjectMeta
      {
        Name = "empty-role"
      }
    };

    // Act
    string command = RoleGenerator.GenerateKubectlCreateCommand(model);

    // Assert
    Assert.Equal("kubectl create role empty-role", command);
  }
}