namespace DevantlerTech.KubernetesGenerator.Native.Models.Autoscaling;

/// <summary>
/// Represents the valid resource kinds that can be targeted by HorizontalPodAutoscaler.
/// </summary>
public enum HorizontalPodAutoscalerTargetKind
{
  /// <summary>
  /// Deployment resource.
  /// </summary>
  Deployment,

  /// <summary>
  /// ReplicaSet resource.
  /// </summary>
  ReplicaSet,

  /// <summary>
  /// StatefulSet resource.
  /// </summary>
  StatefulSet,

  /// <summary>
  /// ReplicationController resource.
  /// </summary>
  ReplicationController
}
