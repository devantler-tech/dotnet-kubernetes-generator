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
    // Simple ImagePolicy - SemVer
    [new FluxImagePolicy
    {
      Metadata = new Metadata
      {
        Name = "image-policy-semver-simple",
      },
      Spec = new FluxImagePolicySpec
      {
        ImageRepositoryRef = new FluxImagePolicySpecImageRepositoryRef
        {
          Name = "nginx"
        },
        Policy = new FluxImagePolicySpecPolicy
        {
          SemVer = new FluxImagePolicySpecPolicySemVer
          {
            Range = ">=1.0.0"
          }
        }
      }
    }, "image-policy-semver-simple.yaml"],

    // Complex ImagePolicy - SemVer with filters
    [new FluxImagePolicy
    {
      Metadata = new Metadata
      {
        Name = "image-policy-semver-complex",
        Namespace = "test-namespace",
        Labels = new Dictionary<string, string>
        {
          { "key", "value" },
        }
      },
      Spec = new FluxImagePolicySpec
      {
        ImageRepositoryRef = new FluxImagePolicySpecImageRepositoryRef
        {
          Name = "nginx",
          Namespace = "image-repo-namespace"
        },
        Policy = new FluxImagePolicySpecPolicy
        {
          SemVer = new FluxImagePolicySpecPolicySemVer
          {
            Range = ">=1.0.0 <2.0.0"
          }
        },
        FilterTags = new FluxImagePolicySpecFilterTags
        {
          Pattern = "^v[0-9]+\\.[0-9]+\\.[0-9]+$",
          Extract = "$1"
        }
      }
    }, "image-policy-semver-complex.yaml"],

    // ImagePolicy - Numerical
    [new FluxImagePolicy
    {
      Metadata = new Metadata
      {
        Name = "image-policy-numerical",
      },
      Spec = new FluxImagePolicySpec
      {
        ImageRepositoryRef = new FluxImagePolicySpecImageRepositoryRef
        {
          Name = "app"
        },
        Policy = new FluxImagePolicySpecPolicy
        {
          Numerical = new FluxImagePolicySpecPolicyNumerical
          {
            Order = FluxImagePolicySpecPolicySortOrder.Asc
          }
        }
      }
    }, "image-policy-numerical.yaml"],

    // ImagePolicy - Alphabetical
    [new FluxImagePolicy
    {
      Metadata = new Metadata
      {
        Name = "image-policy-alphabetical",
      },
      Spec = new FluxImagePolicySpec
      {
        ImageRepositoryRef = new FluxImagePolicySpecImageRepositoryRef
        {
          Name = "app"
        },
        Policy = new FluxImagePolicySpecPolicy
        {
          Alphabetical = new FluxImagePolicySpecPolicyAlphabetical
          {
            Order = FluxImagePolicySpecPolicySortOrder.Desc
          }
        }
      }
    }, "image-policy-alphabetical.yaml"],
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}