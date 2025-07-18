namespace DevantlerTech.KubernetesGenerator.Core;

/// <summary>
/// Interface for a Kubernetes resource generator.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IKubernetesGenerator<TModel>
{
  /// <summary>
  /// Generates Kubernetes resources based on the provided model.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  Task GenerateAsync(TModel model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default);
}
