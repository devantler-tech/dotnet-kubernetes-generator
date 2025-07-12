using System.Collections.ObjectModel;
using DevantlerTech.KubectlCLI;
using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;


/// <summary>
/// Base class for native generators.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseNativeGenerator<T> : IKubernetesGenerator<T>
{
  /// <summary>
  /// Generates a native kubernetes object.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public abstract Task GenerateAsync(T model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default);

  /// <summary>
  /// Runs the Kubectl CLI with the provided arguments and writes the output to the specified file.
  /// </summary>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="arguments"></param>
  /// <param name="errorMessage"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  public async Task RunKubectlAsync(string outputPath, bool overwrite, ReadOnlyCollection<string> arguments, string errorMessage, CancellationToken cancellationToken)
  {
    var (exitCode, output) = await Kubectl.RunAsync([.. arguments], silent: true,
      cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
      throw new KubernetesGeneratorException($"{errorMessage}: {output}");
    await YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken).ConfigureAwait(false);
  }
}
