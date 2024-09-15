using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Metadata;

/// <summary>
/// A generator for Kubernetes ValidatingWebhookConfiguration objects.
/// </summary>
public class ValidatingWebhookConfigurationGenerator : BaseKubernetesGenerator<V1ValidatingWebhookConfiguration>
{
}
