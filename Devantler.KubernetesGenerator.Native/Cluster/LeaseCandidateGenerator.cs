using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Cluster;

/// <summary>
/// A generator for Kubernetes LeaseCandidate objects.
/// </summary>
public class LeaseCandidateGenerator : BaseKubernetesGenerator<V1alpha1LeaseCandidate>
{
}
