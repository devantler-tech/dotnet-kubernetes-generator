using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.ConfigAndStorage;

/// <summary>
/// A generator for Kubernetes PersistentVolumeClaim objects.
/// </summary>
public class PersistentVolumeClaimGenerator : BaseKubernetesGenerator<V1PersistentVolumeClaim>
{
}
