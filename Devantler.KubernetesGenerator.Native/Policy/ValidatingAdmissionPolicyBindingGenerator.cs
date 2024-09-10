using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Policy;

/// <summary>
/// A generator for Kubernetes ValidatingAdmissionPolicyBinding objects.
/// </summary>
public class ValidatingAdmissionPolicyBindingGenerator : BaseKubernetesGenerator<V1ValidatingAdmissionPolicyBinding>
{
}
