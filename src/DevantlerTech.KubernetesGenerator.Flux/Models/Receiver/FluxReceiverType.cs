namespace DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

/// <summary>
/// Represents the type of a Flux Receiver.
/// </summary>
public enum FluxReceiverType
{
  /// <summary>
  /// GitHub webhook receiver.
  /// </summary>
  Github,

  /// <summary>
  /// GitLab webhook receiver.
  /// </summary>
  Gitlab,

  /// <summary>
  /// Bitbucket webhook receiver.
  /// </summary>
  Bitbucket,

  /// <summary>
  /// Harbor webhook receiver.
  /// </summary>
  Harbor,

  /// <summary>
  /// DockerHub webhook receiver.
  /// </summary>
  Dockerhub,

  /// <summary>
  /// Quay webhook receiver.
  /// </summary>
  Quay,

  /// <summary>
  /// Azure Container Registry webhook receiver.
  /// </summary>
  Acr,

  /// <summary>
  /// Google Container Registry webhook receiver.
  /// </summary>
  Gcr,

  /// <summary>
  /// Generic webhook receiver.
  /// </summary>
  Generic
}