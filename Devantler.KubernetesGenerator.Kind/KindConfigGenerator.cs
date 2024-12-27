using Devantler.KubernetesGenerator.Core;
using Devantler.KubernetesGenerator.Kind.Models;

namespace Devantler.KubernetesGenerator.Kind;

/// <summary>
/// Generator for generating K3d config files.
/// </summary>
public class KindConfigGenerator() : BaseKubernetesGenerator<KindConfig>(omitDefaults: true)
{
}
