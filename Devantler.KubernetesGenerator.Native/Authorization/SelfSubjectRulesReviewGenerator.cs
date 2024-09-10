using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Authorization;

/// <summary>
/// A generator for Kubernetes SelfSubjectRulesReview objects.
/// </summary>
public class SelfSubjectRulesReviewGenerator : BaseKubernetesGenerator<V1SelfSubjectRulesReview>
{
}
