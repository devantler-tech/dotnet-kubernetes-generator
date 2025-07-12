using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes DaemonSet objects.
/// </summary>
/// <remarks>
/// This generator uses BaseKubernetesGenerator instead of BaseNativeGenerator because kubectl does not support 
/// 'kubectl create daemonset' command. The generator serializes V1DaemonSet objects to YAML format.
/// </remarks>
public class DaemonSetGenerator : BaseKubernetesGenerator<V1DaemonSet>
{
}
