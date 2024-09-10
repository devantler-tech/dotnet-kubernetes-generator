using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Service;

/// <summary>
/// A generator for Kubernetes EndpointSlice objects.
/// </summary>
public class EndpointSliceGenerator : BaseKubernetesGenerator<V1EndpointSlice>
{
}
