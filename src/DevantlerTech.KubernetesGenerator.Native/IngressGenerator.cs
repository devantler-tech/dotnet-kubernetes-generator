using System.Collections.ObjectModel;
using System.Globalization;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Ingress objects using 'kubectl create ingress' commands.
/// </summary>
public class IngressGenerator : BaseNativeGenerator<V1Ingress>
{
  static readonly string[] _defaultArgs = ["create", "ingress"];

  /// <summary>
  /// Generates an ingress using kubectl create ingress command.
  /// </summary>
  /// <param name="model">The ingress object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when ingress name is not provided.</exception>
  public override async Task GenerateAsync(V1Ingress model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create ingress '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating an ingress from a V1Ingress object.
  /// </summary>
  /// <param name="model">The V1Ingress object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create ingress supports basic ingress creation with name, class, rules, default backend, and annotations.
  /// Advanced properties like TLS configuration beyond basic rule-based TLS are limited to what kubectl create supports.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1Ingress model)
  {
    var args = new List<string> { };

    // Require that an ingress name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the ingress name.");
    }
    args.Add(model.Metadata.Name);

    // Add ingress class if specified
    if (!string.IsNullOrEmpty(model.Spec?.IngressClassName))
    {
      args.Add($"--class={model.Spec.IngressClassName}");
    }

    // Add default backend if specified
    if (model.Spec?.DefaultBackend != null)
    {
      var defaultBackend = model.Spec.DefaultBackend;
      if (defaultBackend.Service != null)
      {
        string serviceName = defaultBackend.Service.Name;
        string servicePort = defaultBackend.Service.Port?.Number?.ToString(CultureInfo.InvariantCulture) ??
                           defaultBackend.Service.Port?.Name ?? "80";
        args.Add($"--default-backend={serviceName}:{servicePort}");
      }
    }

    // Add rules if specified
    if (model.Spec?.Rules != null)
    {
      foreach (var rule in model.Spec.Rules)
      {
        if (rule.Http?.Paths != null)
        {
          foreach (var path in rule.Http.Paths)
          {
            if (path.Backend?.Service != null)
            {
              string host = rule.Host ?? "";
              string pathValue = path.Path ?? "/";
              string serviceName = path.Backend.Service.Name;
              string servicePort = path.Backend.Service.Port?.Number?.ToString(CultureInfo.InvariantCulture) ??
                                 path.Backend.Service.Port?.Name ?? "80";

              // Build the rule string: host/path=service:port[,tls=secret]
              string ruleString = $"{host}{pathValue}={serviceName}:{servicePort}";

              // Check if TLS is configured for this rule
              var tlsConfig = model.Spec.Tls?.FirstOrDefault(t => t.Hosts?.Contains(rule.Host) == true);
              if (tlsConfig != null)
              {
                if (!string.IsNullOrEmpty(tlsConfig.SecretName))
                {
                  ruleString += $",tls={tlsConfig.SecretName}";
                }
                else
                {
                  ruleString += ",tls";
                }
              }

              args.Add($"--rule={ruleString}");
            }
          }
        }
      }
    }

    // Add annotations if specified
    if (model.Metadata?.Annotations != null)
    {
      foreach (var annotation in model.Metadata.Annotations)
      {
        args.Add($"--annotation={annotation.Key}={annotation.Value}");
      }
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
