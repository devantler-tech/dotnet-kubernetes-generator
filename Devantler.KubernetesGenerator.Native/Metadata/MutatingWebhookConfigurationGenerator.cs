using Devantler.KubernetesGenerator.Core;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Metadata;

/// <summary>
/// A generator for Kubernetes MutatingWebhookConfiguration objects.
/// </summary>
public class MutatingWebhookConfigurationGenerator : BaseKubernetesGenerator<V1MutatingWebhookConfiguration>
{
}
