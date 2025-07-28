using System.Globalization;

namespace DevantlerTech.KubernetesGenerator.Utils;

/// <summary>
/// Utility class for creating type-safe cron schedules that can be converted to cron expression strings.
/// </summary>
public static class CronScheduleUtils
{
  /// <summary>
  /// Creates a cron schedule that runs every minute.
  /// </summary>
  /// <returns>A cron schedule that runs every minute.</returns>
  public static string EveryMinute() => "* * * * *";

  /// <summary>
  /// Creates a cron schedule that runs every hour at the specified minute.
  /// </summary>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs every hour.</returns>
  public static string EveryHour(int minute = 0) => $"{minute} * * * *";

  /// <summary>
  /// Creates a cron schedule that runs daily at the specified time.
  /// </summary>
  /// <param name="time">The time of day to run.</param>
  /// <returns>A cron schedule that runs daily.</returns>
  public static string Daily(TimeOnly time) => $"{time.Minute} {time.Hour} * * *";

  /// <summary>
  /// Creates a cron schedule that runs daily at the specified time.
  /// </summary>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs daily.</returns>
  public static string Daily(int hour, int minute = 0) => $"{minute} {hour} * * *";

  /// <summary>
  /// Creates a cron schedule that runs weekly on the specified day and time.
  /// </summary>
  /// <param name="dayOfWeek">The day of week.</param>
  /// <param name="time">The time of day to run.</param>
  /// <returns>A cron schedule that runs weekly.</returns>
  public static string Weekly(DayOfWeek dayOfWeek, TimeOnly time) => $"{time.Minute} {time.Hour} * * {(int)dayOfWeek}";

  /// <summary>
  /// Creates a cron schedule that runs weekly on the specified day and time.
  /// </summary>
  /// <param name="dayOfWeek">The day of week (0-7, where 0 and 7 are Sunday).</param>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs weekly.</returns>
  public static string Weekly(int dayOfWeek, int hour, int minute = 0) => $"{minute} {hour} * * {dayOfWeek}";

  /// <summary>
  /// Creates a cron schedule that runs monthly on the specified day and time.
  /// </summary>
  /// <param name="dayOfMonth">The day of month (1-31).</param>
  /// <param name="time">The time of day to run.</param>
  /// <returns>A cron schedule that runs monthly.</returns>
  public static string Monthly(int dayOfMonth, TimeOnly time) => $"{time.Minute} {time.Hour} {dayOfMonth} * *";

  /// <summary>
  /// Creates a cron schedule that runs monthly on the specified day and time.
  /// </summary>
  /// <param name="dayOfMonth">The day of month (1-31).</param>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs monthly.</returns>
  public static string Monthly(int dayOfMonth, int hour, int minute = 0) => $"{minute} {hour} {dayOfMonth} * *";

  /// <summary>
  /// Creates a cron schedule that runs at intervals.
  /// </summary>
  /// <param name="intervalMinutes">The interval in minutes.</param>
  /// <returns>A cron schedule that runs at the specified interval.</returns>
  public static string EveryInterval(int intervalMinutes) => $"*/{intervalMinutes} * * * *";

  /// <summary>
  /// Creates a cron schedule that runs at intervals.
  /// </summary>
  /// <param name="interval">The interval as a TimeSpan.</param>
  /// <returns>A cron schedule that runs at the specified interval.</returns>
  public static string EveryInterval(TimeSpan interval)
  {
    int totalMinutes = (int)interval.TotalMinutes;
    return totalMinutes > 0 ? $"*/{totalMinutes} * * * *" : throw new ArgumentException("Interval must be at least 1 minute", nameof(interval));
  }

  /// <summary>
  /// Validates a cron expression format.
  /// </summary>
  /// <param name="cronExpression">The cron expression to validate.</param>
  /// <returns>True if valid, false otherwise.</returns>
  public static bool IsValidCronExpression(string cronExpression)
  {
    if (string.IsNullOrWhiteSpace(cronExpression))
      return false;

    string[] parts = cronExpression.Split(' ');
    return parts.Length == 5;
  }

  /// <summary>
  /// Parses a cron expression into its component parts.
  /// </summary>
  /// <param name="cronExpression">The cron expression string.</param>
  /// <returns>A tuple containing the minute, hour, day of month, month, and day of week parts.</returns>
  /// <exception cref="ArgumentException">Thrown when the cron expression format is invalid.</exception>
  public static (string Minute, string Hour, string DayOfMonth, string Month, string DayOfWeek) ParseCronExpression(string cronExpression)
  {
    ArgumentNullException.ThrowIfNull(cronExpression);
    string[] parts = cronExpression.Split(' ');
    return parts.Length != 5
      ? throw new ArgumentException("Invalid cron expression. Expected format: 'minute hour dayOfMonth month dayOfWeek'", nameof(cronExpression))
      : (parts[0], parts[1], parts[2], parts[3], parts[4]);
  }
}