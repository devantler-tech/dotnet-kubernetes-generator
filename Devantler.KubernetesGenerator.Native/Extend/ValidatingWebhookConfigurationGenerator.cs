using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Extend;

/// <summary>
/// A generator for Kubernetes ValidatingWebhookConfiguration objects.
/// </summary>
public class ValidatingWebhookConfigurationGenerator : BaseKubernetesGenerator<V1ValidatingWebhookConfiguration>
{
}
