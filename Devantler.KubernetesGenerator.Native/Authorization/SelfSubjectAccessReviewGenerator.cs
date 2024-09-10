using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Authorization;

/// <summary>
/// A generator for Kubernetes SelfSubjectAccessReview objects.
/// </summary>
public class SelfSubjectAccessReviewGenerator : BaseKubernetesGenerator<V1SelfSubjectAccessReview>
{
}
