using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// Base class for kubectl generators.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseKubectlGenerator<T> : IKubernetesGenerator<T> where T : class
{
  /// <summary>
  /// Generates a Kubernetes object using kubectl.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public abstract Task GenerateAsync(T model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default);

  /// <summary>
  /// Runs kubectl with the provided arguments and writes the output to the specified file.
  /// </summary>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="arguments"></param>
  /// <param name="errorMessage"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  protected async Task RunKubectlAsync(string outputPath, bool overwrite, ReadOnlyCollection<string> arguments, string errorMessage, CancellationToken cancellationToken)
  {
    var (exitCode, output) = await KubectlCLI.Kubectl.RunAsync([.. arguments], silent: true,
      cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
      throw new KubernetesGeneratorException($"{errorMessage}: {output}");
    await YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken).ConfigureAwait(false);
  }
}