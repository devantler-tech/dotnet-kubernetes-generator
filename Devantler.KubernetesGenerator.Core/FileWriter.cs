namespace Devantler.KubernetesGenerator.Core;

/// <summary>
/// Writes content to a file.
/// </summary>
public static class FileWriter
{
  /// <summary>
  /// Writes the output to the file at the given path.
  /// </summary>
  /// <param name="outputPath"></param>
  /// <param name="output"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public static async Task WriteToFileAsync(string outputPath, string output, bool overwrite, CancellationToken cancellationToken = default)
  {
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
