using Cronos;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Provides validation for cron expressions using the Cronos library.
/// </summary>
public static class CronSchedule
{
  /// <summary>
  /// Validates a cron expression string.
  /// </summary>
  /// <param name="cronExpression">The cron expression to validate.</param>
  /// <exception cref="ArgumentException">Thrown when the cron expression format is invalid.</exception>
  public static void Validate(string cronExpression)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(cronExpression);

    try
    {
      // Validate the cron expression using Cronos
      _ = CronExpression.Parse(cronExpression);
    }
    catch (CronFormatException ex)
    {
      throw new ArgumentException($"Invalid cron expression: {ex.Message}", nameof(cronExpression), ex);
    }
  }

  /// <summary>
  /// Gets the string representation from a CronExpression.
  /// </summary>
  /// <param name="cronExpression">The CronExpression to convert.</param>
  /// <returns>The cron expression string.</returns>
  public static string FromCronExpression(CronExpression cronExpression)
  {
    ArgumentNullException.ThrowIfNull(cronExpression);
    return cronExpression.ToString();
  }
}