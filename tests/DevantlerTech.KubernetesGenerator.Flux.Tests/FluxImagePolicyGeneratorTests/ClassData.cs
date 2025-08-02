using System.Collections;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxImagePolicyGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
sealed class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple image policy with semver
    [new FluxImagePolicy()
      {
        Metadata = new NamespacedMetadata
        {
          Name = "image-policy-semver",
        },
        Spec = new FluxImagePolicySpec
        {
          ImageRef = "podinfo",
          SortBy = FluxImagePolicySortBy.Semver,
          SemverRange = ">=1.0.0"
        }
      }, "image-policy-semver.yaml"],

    // Complex image policy with numeric sorting and filters
    [new FluxImagePolicy
    {
      Metadata = new NamespacedMetadata(new Dictionary<string, string>
        {
          ["app"] = "podinfo"
        }
      )
      {
        Name = "image-policy-numeric",
        Namespace = "default"
      },
      Spec = new FluxImagePolicySpec
      {
        ImageRef = "podinfo-repo",
        SortBy = FluxImagePolicySortBy.Numeric,
        SortOrder = FluxImagePolicySortOrder.Asc,
        FilterRegex = "^main-[a-f0-9]+-(?P<ts>[0-9]+)",
        FilterExtract = "$ts",
        Interval = TimeSpan.FromMinutes(5),
        ReflectDigest = FluxImagePolicyReflectDigest.Always  // Changed to Always to make interval work
      }
    }, "image-policy-numeric.yaml"],

    // Image policy with alpha sorting
    [new FluxImagePolicy
    {
      Metadata = new NamespacedMetadata
      {
        Name = "image-policy-alpha",
        Namespace = "flux-system"
      },
      Spec = new FluxImagePolicySpec
      {
        ImageRef = "nginx",
        SortBy = FluxImagePolicySortBy.Alpha,
        SortOrder = FluxImagePolicySortOrder.Desc
      }
    }, "image-policy-alpha.yaml"]
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
