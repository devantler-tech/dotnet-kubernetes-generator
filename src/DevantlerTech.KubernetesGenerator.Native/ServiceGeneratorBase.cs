using System.Collections.ObjectModel;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// Base class for Kubernetes Service generators that provides shared functionality.
/// </summary>
public abstract class ServiceGeneratorBase : BaseNativeGenerator<V1Service>
{
  /// <summary>
  /// Adds TCP port mappings to the arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Service object.</param>
  protected static void AddTcpPorts(Collection<string> args, V1Service model)
  {
    ArgumentNullException.ThrowIfNull(args);
    ArgumentNullException.ThrowIfNull(model);

    if (model.Spec?.Ports?.Count > 0)
    {
      foreach (var port in model.Spec.Ports)
      {
        if (port.Protocol?.ToUpperInvariant() == "TCP" || string.IsNullOrEmpty(port.Protocol))
        {
          // Format: --tcp=port:targetPort
          string tcpArg = $"--tcp={port.Port}";
          if (port.TargetPort != null)
          {
            tcpArg += $":{port.TargetPort.Value}";
          }
          args.Add(tcpArg);
        }
      }
    }
  }
}
