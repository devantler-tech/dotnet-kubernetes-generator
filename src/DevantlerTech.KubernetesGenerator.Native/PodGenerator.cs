using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Pod objects using 'kubectl run' commands.
/// </summary>
public class PodGenerator : BaseNativeGenerator<V1Pod>
{
  static readonly string[] _defaultArgs = ["run"];

  /// <summary>
  /// Generates a Pod using kubectl run command.
  /// </summary>
  /// <param name="model">The Pod object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when Pod name is not provided or no containers are specified.</exception>
  public override async Task GenerateAsync(V1Pod model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create Pod '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a Pod from a V1Pod object.
  /// </summary>
  /// <param name="model">The V1Pod object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl run has limitations compared to full Pod specification. 
  /// It supports single container pods with basic configuration.
  /// Advanced features like multiple containers, volumes, etc. are not supported by kubectl run.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1Pod model)
  {
    var args = new List<string>();

    // Require that a Pod name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the Pod name.");
    }
    args.Add(model.Metadata.Name);

    // Require that at least one container is specified
    if (model.Spec?.Containers == null || model.Spec.Containers.Count == 0)
    {
      throw new KubernetesGeneratorException("The model.Spec.Containers must contain at least one container.");
    }

    // kubectl run only supports single container pods
    if (model.Spec.Containers.Count > 1)
    {
      throw new KubernetesGeneratorException("kubectl run only supports single container pods. Use kubectl apply for multi-container pods.");
    }

    var container = model.Spec.Containers[0];

    // Require that the container has an image
    if (string.IsNullOrEmpty(container.Image))
    {
      throw new KubernetesGeneratorException("The container image must be specified.");
    }
    args.Add($"--image={container.Image}");

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Add command if specified
    if (container.Command != null && container.Command.Count > 0)
    {
      args.Add("--command");
      args.Add("--");
      args.AddRange(container.Command);
    }
    // Add args if specified (only if command is not specified)
    else if (container.Args != null && container.Args.Count > 0)
    {
      args.Add("--");
      args.AddRange(container.Args);
    }

    // Add environment variables if specified
    if (container.Env != null && container.Env.Count > 0)
    {
      foreach (var env in container.Env)
      {
        if (!string.IsNullOrEmpty(env.Name) && !string.IsNullOrEmpty(env.Value))
        {
          args.Add($"--env={env.Name}={env.Value}");
        }
      }
    }

    // Add port if specified (only the first port)
    if (container.Ports != null && container.Ports.Count > 0)
    {
      var port = container.Ports[0];
      if (port.ContainerPort > 0)
      {
        args.Add($"--port={port.ContainerPort}");
      }
    }

    return args.AsReadOnly();
  }
}
