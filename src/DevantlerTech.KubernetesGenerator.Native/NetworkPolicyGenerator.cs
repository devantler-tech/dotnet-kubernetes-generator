using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes NetworkPolicy objects using kubectl commands.
/// </summary>
/// <remarks>
/// Since kubectl does not have a native "create networkpolicy" command, this generator
/// creates NetworkPolicy resources by generating the YAML representation directly.
/// </remarks>
public class NetworkPolicyGenerator : BaseNativeGenerator<NetworkPolicy>
{
  /// <summary>
  /// Generates a NetworkPolicy using kubectl with dry-run to create the YAML output.
  /// </summary>
  /// <param name="model">The NetworkPolicy object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when NetworkPolicy name is not provided.</exception>
  public override async Task GenerateAsync(NetworkPolicy model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    // Since kubectl doesn't have a native create command for NetworkPolicy,
    // we'll use the traditional approach of generating the YAML directly
    // This ensures compatibility with the BaseNativeGenerator pattern while
    // handling the NetworkPolicy resource appropriately

    // Validate that the model has the required fields
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the NetworkPolicy name.");
    }

    // Convert the custom model to V1NetworkPolicy for YAML generation
    var v1NetworkPolicy = new V1NetworkPolicy
    {
      ApiVersion = "networking.k8s.io/v1",
      Kind = "NetworkPolicy",
      Metadata = model.Metadata,
      Spec = new V1NetworkPolicySpec
      {
        PodSelector = model.PodSelector,
        Ingress = model.Ingress,
        Egress = model.Egress,
        PolicyTypes = model.PolicyTypes
      }
    };

    // Generate the YAML representation directly using the BaseKubernetesGenerator approach
    // but wrapped in our BaseNativeGenerator pattern
    var baseGenerator = new BaseKubernetesGenerator<V1NetworkPolicy>();
    await baseGenerator.GenerateAsync(v1NetworkPolicy, outputPath, overwrite, cancellationToken).ConfigureAwait(false);
  }
}
