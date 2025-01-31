namespace Devantler.KubernetesGenerator.Core.Tests;

/// <summary>
/// Tests for <see cref="KubernetesGeneratorException"/>.
/// </summary>
public class KubernetesGeneratorExceptionTests
{
  /// <summary>
  /// Tests the constructor of <see cref="KubernetesGeneratorException"/>.
  /// </summary>
  [Fact]
  public void Constructor_Default_ShouldCreateInstance()
  {
    // Act
    var exception = new KubernetesGeneratorException();

    // Assert
    Assert.NotNull(exception);
  }

  /// <summary>
  /// Tests the constructor of <see cref="KubernetesGeneratorException"/> with message.
  /// </summary>
  [Fact]
  public void Constructor_WithMessage_ShouldSetMessage()
  {
    // Arrange
    string message = "Test message";

    // Act
    var exception = new KubernetesGeneratorException(message);

    // Assert
    Assert.NotNull(exception);
    Assert.Equal(message, exception.Message);
  }

  /// <summary>
  /// Tests the constructor of <see cref="KubernetesGeneratorException"/> with message and inner exception.
  /// </summary>
  [Fact]
  public void Constructor_WithMessageAndInnerException_ShouldSetMessageAndInnerException()
  {
    // Arrange
    string message = "Test message";
    var innerException = new Exception("Inner exception");

    // Act
    var exception = new KubernetesGeneratorException(message, innerException);

    // Assert
    Assert.NotNull(exception);
    Assert.Equal(message, exception.Message);
    Assert.Equal(innerException, exception.InnerException);
  }
}
