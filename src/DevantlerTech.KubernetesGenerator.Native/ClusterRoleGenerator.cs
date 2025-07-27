using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ClusterRole objects using custom models with type-safe options.
/// Falls back to BaseKubernetesGenerator since kubectl create clusterrole requires API server connectivity.
/// </summary>
public class ClusterRoleGenerator : BaseKubernetesGenerator<ClusterRole>
{
}
