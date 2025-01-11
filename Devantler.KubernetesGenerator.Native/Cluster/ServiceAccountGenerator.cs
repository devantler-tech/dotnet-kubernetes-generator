using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Cluster;

/// <summary>
/// A generator for Kubernetes ServiceAccount objects.
/// </summary>
public class ServiceAccountGenerator : BaseKubernetesGenerator<V1ServiceAccount>
{
}
