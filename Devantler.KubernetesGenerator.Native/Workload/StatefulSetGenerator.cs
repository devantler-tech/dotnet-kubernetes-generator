using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Workload;

/// <summary>
/// A generator for Kubernetes StatefulSet objects.
/// </summary>
public class StatefulSetGenerator : BaseKubernetesGenerator<V1StatefulSet>
{
}
