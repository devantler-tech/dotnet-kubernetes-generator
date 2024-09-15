using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Cluster;

/// <summary>
/// A generator for Kubernetes SelfSubjectAccessReview objects.
/// </summary>
public class SelfSubjectAccessReviewGenerator : BaseKubernetesGenerator<V1SelfSubjectAccessReview>
{
}
