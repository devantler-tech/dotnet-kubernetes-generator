namespace Devantler.KubernetesGenerator.Core;

/// <summary>
/// Exception that is thrown when an error occurs in the Kubernetes generator.
/// </summary>
public class KubernetesGeneratorException : Exception
{
  /// <summary>
  /// Initializes a new instance of the <see cref="KubernetesGeneratorException"/> class.
  /// </summary>
  public KubernetesGeneratorException()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="KubernetesGeneratorException"/> class with a specified error message.
  /// </summary>
  /// <param name="message"></param>
  public KubernetesGeneratorException(string? message) : base(message)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="KubernetesGeneratorException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
  /// </summary>
  /// <param name="message"></param>
  /// <param name="innerException"></param>
  public KubernetesGeneratorException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}
