using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Workload;

/// <summary>
/// A generator for Kubernetes CronJob objects.
/// </summary>
public class CronJobGenerator : BaseKubernetesGenerator<V1CronJob>
{
}
