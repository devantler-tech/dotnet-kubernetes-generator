using System.Globalization;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a type-safe cron schedule that can be converted to a cron expression string.
/// </summary>
public class CronSchedule
{
  /// <summary>
  /// Gets or sets the minute field (0-59, *, */n).
  /// </summary>
  public string Minute { get; set; } = "*";

  /// <summary>
  /// Gets or sets the hour field (0-23, *, */n).
  /// </summary>
  public string Hour { get; set; } = "*";

  /// <summary>
  /// Gets or sets the day of month field (1-31, *, */n).
  /// </summary>
  public string DayOfMonth { get; set; } = "*";

  /// <summary>
  /// Gets or sets the month field (1-12, *, */n).
  /// </summary>
  public string Month { get; set; } = "*";

  /// <summary>
  /// Gets or sets the day of week field (0-7, *, */n, where 0 and 7 are Sunday).
  /// </summary>
  public string DayOfWeek { get; set; } = "*";

  /// <summary>
  /// Converts the cron schedule to a standard cron expression string.
  /// </summary>
  /// <returns>A valid cron expression string.</returns>
  public override string ToString() => $"{Minute} {Hour} {DayOfMonth} {Month} {DayOfWeek}";

  /// <summary>
  /// Creates a cron schedule that runs every minute.
  /// </summary>
  /// <returns>A cron schedule that runs every minute.</returns>
  public static CronSchedule EveryMinute() => new() { Minute = "*" };

  /// <summary>
  /// Creates a cron schedule that runs every hour at the specified minute.
  /// </summary>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs every hour.</returns>
  public static CronSchedule EveryHour(int minute = 0) => new() { Minute = minute.ToString(CultureInfo.InvariantCulture) };

  /// <summary>
  /// Creates a cron schedule that runs daily at the specified time.
  /// </summary>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs daily.</returns>
  public static CronSchedule Daily(int hour, int minute = 0) => new() { Minute = minute.ToString(CultureInfo.InvariantCulture), Hour = hour.ToString(CultureInfo.InvariantCulture) };

  /// <summary>
  /// Creates a cron schedule that runs weekly on the specified day and time.
  /// </summary>
  /// <param name="dayOfWeek">The day of week (0-7, where 0 and 7 are Sunday).</param>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs weekly.</returns>
  public static CronSchedule Weekly(int dayOfWeek, int hour, int minute = 0) => new()
  {
    Minute = minute.ToString(CultureInfo.InvariantCulture),
    Hour = hour.ToString(CultureInfo.InvariantCulture),
    DayOfWeek = dayOfWeek.ToString(CultureInfo.InvariantCulture)
  };

  /// <summary>
  /// Creates a cron schedule that runs monthly on the specified day and time.
  /// </summary>
  /// <param name="dayOfMonth">The day of month (1-31).</param>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs monthly.</returns>
  public static CronSchedule Monthly(int dayOfMonth, int hour, int minute = 0) => new()
  {
    Minute = minute.ToString(CultureInfo.InvariantCulture),
    Hour = hour.ToString(CultureInfo.InvariantCulture),
    DayOfMonth = dayOfMonth.ToString(CultureInfo.InvariantCulture)
  };

  /// <summary>
  /// Creates a cron schedule that runs at intervals.
  /// </summary>
  /// <param name="intervalMinutes">The interval in minutes.</param>
  /// <returns>A cron schedule that runs at the specified interval.</returns>
  public static CronSchedule EveryInterval(int intervalMinutes) => new() { Minute = $"*/{intervalMinutes}" };

  /// <summary>
  /// Implicitly converts a cron schedule to a string.
  /// </summary>
  /// <param name="schedule">The cron schedule.</param>
  public static implicit operator string(CronSchedule schedule)
  {
    ArgumentNullException.ThrowIfNull(schedule);
    return schedule.ToString();
  }

  /// <summary>
  /// Converts a string to a cron schedule.
  /// </summary>
  /// <param name="cronExpression">The cron expression string.</param>
  /// <returns>A cron schedule object.</returns>
  public static CronSchedule FromString(string cronExpression)
  {
    ArgumentNullException.ThrowIfNull(cronExpression);
    string[] parts = cronExpression.Split(' ');
    return parts.Length != 5
      ? throw new ArgumentException("Invalid cron expression. Expected format: 'minute hour dayOfMonth month dayOfWeek'", nameof(cronExpression))
      : new CronSchedule
      {
        Minute = parts[0],
        Hour = parts[1],
        DayOfMonth = parts[2],
        Month = parts[3],
        DayOfWeek = parts[4]
      };
  }

  /// <summary>
  /// Implicitly converts a string to a cron schedule.
  /// </summary>
  /// <param name="cronExpression">The cron expression string.</param>
  public static implicit operator CronSchedule(string cronExpression) => FromString(cronExpression);
}