using System.Collections;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models;
using DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

namespace DevantlerTech.KubernetesGenerator.Flux.Tests.FluxReceiverGeneratorTests;

/// <summary>
/// Class data for the tests.
/// </summary>
sealed class ClassData : IEnumerable<object[]>
{
  readonly List<object[]> _data =
  [
    // Simple GitHub Receiver
    [new FluxReceiver
    {
      Metadata = new Metadata
      {
        Name = "github-receiver-simple"
      },
      Spec = new FluxReceiverSpec
      {
        Type = FluxReceiverType.Github,
        Events = [FluxReceiverEvent.Push],
        SecretRef = new FluxSecretRef
        {
          Name = "github-token"
        },
        Resources =
        [
          new FluxReceiverResource
          {
            Kind = FluxCustomResourceKind.GitRepository,
            Name = "webapp"
          }
        ]
      }
    }, "github-receiver-simple.yaml"],

    // Complex GitHub Receiver
    [new FluxReceiver
    {
      Metadata = new Metadata
      {
        Name = "github-receiver-complex",
        Namespace = "flux-system",
        Labels = new Dictionary<string, string>
        {
          ["app"] = "flux-receiver",
          ["env"] = "production"
        }
      },
      Spec = new FluxReceiverSpec
      {
        Type = FluxReceiverType.Github,
        Events = [FluxReceiverEvent.Push, FluxReceiverEvent.PullRequest],
        SecretRef = new FluxSecretRef
        {
          Name = "github-webhook-token"
        },
        Resources =
        [
          new FluxReceiverResource
          {
            Kind = FluxCustomResourceKind.GitRepository,
            Name = "webapp"
          },
          new FluxReceiverResource
          {
            Kind = FluxCustomResourceKind.HelmRepository,
            Name = "charts",
            Namespace = "flux-system"
          }
        ]
      }
    }, "github-receiver-complex.yaml"],

    // GitLab Receiver with Events
    [new FluxReceiver
    {
      Metadata = new Metadata
      {
        Name = "gitlab-receiver",
        Namespace = "gitlab-system"
      },
      Spec = new FluxReceiverSpec
      {
        Type = FluxReceiverType.Gitlab,
        Events = [FluxReceiverEvent.Push, FluxReceiverEvent.TagPush, FluxReceiverEvent.Release],
        SecretRef = new FluxSecretRef
        {
          Name = "gitlab-webhook-secret"
        },
        Resources =
        [
          new FluxReceiverResource
          {
            Kind = FluxCustomResourceKind.GitRepository,
            Name = "api-service"
          }
        ]
      }
    }, "gitlab-receiver.yaml"],

    // Generic Receiver 
    [new FluxReceiver
    {
      Metadata = new Metadata
      {
        Name = "generic-receiver"
      },
      Spec = new FluxReceiverSpec
      {
        Type = FluxReceiverType.Generic,
        Events = [FluxReceiverEvent.Ping],
        SecretRef = new FluxSecretRef
        {
          Name = "webhook-token"
        },
        Resources =
        [
          new FluxReceiverResource
          {
            Kind = FluxCustomResourceKind.Kustomization,
            Name = "apps"
          }
        ]
      }
    }, "generic-receiver.yaml"],

    // Harbor Receiver
    [new FluxReceiver
    {
      Metadata = new Metadata
      {
        Name = "harbor-receiver",
        Namespace = "harbor-system"
      },
      Spec = new FluxReceiverSpec
      {
        Type = FluxReceiverType.Harbor,
        SecretRef = new FluxSecretRef
        {
          Name = "harbor-webhook-token"
        },
        Resources =
        [
          new FluxReceiverResource
          {
            Kind = FluxCustomResourceKind.OCIRepository,
            Name = "app-images",
            Namespace = "harbor-system"
          }
        ]
      }
    }, "harbor-receiver.yaml"]
  ];

  /// <inheritdoc/>
  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}