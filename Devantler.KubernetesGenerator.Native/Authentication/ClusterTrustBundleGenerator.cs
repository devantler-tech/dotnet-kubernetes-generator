using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Authentication;

/// <summary>
/// A generator for Kubernetes ClusterTrustBundle objects.
/// </summary>
public class ClusterTrustBundleGenerator : BaseKubernetesGenerator<V1alpha1ClusterTrustBundle>
{
}
