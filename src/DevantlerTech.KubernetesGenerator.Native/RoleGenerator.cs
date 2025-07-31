using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Role objects using custom models with type-safe options.
/// Uses BaseKubernetesGenerator since kubectl create role requires API server connectivity.
/// </summary>
public class RoleGenerator : BaseKubernetesGenerator<Role>
{
}
