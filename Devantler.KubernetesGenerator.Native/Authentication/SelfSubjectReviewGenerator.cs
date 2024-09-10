using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Authentication;

/// <summary>
/// A generator for Kubernetes SelfSubjectReview objects.
/// </summary>
public class SelfSubjectReviewGenerator : BaseKubernetesGenerator<V1SelfSubjectReview>
{
}
