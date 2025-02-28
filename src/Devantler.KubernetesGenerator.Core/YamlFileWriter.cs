namespace Devantler.KubernetesGenerator.Core;

/// <summary>
/// Writes content to a YAML file.
/// </summary>
public static class YamlFileWriter
{
  /// <summary>
  /// Writes the output to the specified YAML file.
  /// </summary>
  /// <param name="outputPath"></param>
  /// <param name="output"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public static async Task WriteToFileAsync(string outputPath, string output, bool overwrite, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(outputPath, nameof(outputPath));
    ArgumentNullException.ThrowIfNull(output, nameof(output));
    if (!outputPath.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase) && !outputPath.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
    {
      throw new KubernetesGeneratorException("Output path must be a YAML file.");
    }
    string outputDirectory = Path.GetDirectoryName(outputPath) ?? throw new InvalidOperationException("Output path is invalid.");
    if (!Directory.Exists(outputDirectory))
    {
      _ = Directory.CreateDirectory(outputDirectory);
    }

    if (!output.StartsWith("---", StringComparison.Ordinal))
    {
      output = $"---{Environment.NewLine}" + output;
    }

    if (overwrite)
    {
      await File.WriteAllTextAsync(outputPath, output, cancellationToken).ConfigureAwait(false);
    }
    else
    {
      await File.AppendAllTextAsync(outputPath, output, cancellationToken).ConfigureAwait(false);
    }
  }
}
