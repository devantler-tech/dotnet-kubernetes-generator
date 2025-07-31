using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ClusterRole objects using custom models with type-safe options.
/// Falls back to KubernetesGenerator since kubectl create clusterrole requires API server connectivity.
/// </summary>
public class ClusterRoleGenerator : Generator<NativeClusterRole>
{
}
