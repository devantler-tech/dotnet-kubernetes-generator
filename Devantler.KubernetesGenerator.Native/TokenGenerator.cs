using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ServiceAccount Token objects.
/// </summary>
public class TokenGenerator : BaseKubernetesGenerator<V1TokenReview>
{
}
