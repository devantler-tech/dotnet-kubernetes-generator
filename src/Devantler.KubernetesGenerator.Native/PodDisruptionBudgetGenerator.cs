using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PodDisruptionBudget objects.
/// </summary>
public class PodDisruptionBudgetGenerator : BaseKubernetesGenerator<V1PodDisruptionBudget>
{
}
