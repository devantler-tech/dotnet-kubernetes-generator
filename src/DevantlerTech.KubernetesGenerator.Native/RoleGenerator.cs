using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models.Role;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Role objects using custom models with type-safe options.
/// Uses KubernetesGenerator since kubectl create role requires API server connectivity.
/// </summary>
public class RoleGenerator : Generator<NativeRole>
{
}
