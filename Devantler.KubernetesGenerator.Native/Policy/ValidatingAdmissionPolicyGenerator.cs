using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Policy;

/// <summary>
/// A generator for Kubernetes ValidatingAdmissionPolicy objects.
/// </summary>
public class ValidatingAdmissionPolicyGenerator : BaseKubernetesGenerator<V1ValidatingAdmissionPolicy>
{
}
