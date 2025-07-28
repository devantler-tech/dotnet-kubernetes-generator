using Cronos;

namespace DevantlerTech.KubernetesGenerator.Native.Utils;

/// <summary>
/// Utility class for creating type-safe cron schedules using the Cronos library.
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
  public static string EveryHour(int minute = 0)
  {
    ValidateMinute(minute);
    return $"{minute} * * * *";
  }

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
  public static string Daily(int hour, int minute = 0)
  {
    ValidateHour(hour);
    ValidateMinute(minute);
    return $"{minute} {hour} * * *";
  }

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
  public static string Weekly(int dayOfWeek, int hour, int minute = 0)
  {
    ValidateDayOfWeek(dayOfWeek);
    ValidateHour(hour);
    ValidateMinute(minute);
    return $"{minute} {hour} * * {dayOfWeek}";
  }

  /// <summary>
  /// Creates a cron schedule that runs monthly on the specified day and time.
  /// </summary>
  /// <param name="dayOfMonth">The day of month (1-31).</param>
  /// <param name="time">The time of day to run.</param>
  /// <returns>A cron schedule that runs monthly.</returns>
  public static string Monthly(int dayOfMonth, TimeOnly time)
  {
    ValidateDayOfMonth(dayOfMonth);
    return $"{time.Minute} {time.Hour} {dayOfMonth} * *";
  }

  /// <summary>
  /// Creates a cron schedule that runs monthly on the specified day and time.
  /// </summary>
  /// <param name="dayOfMonth">The day of month (1-31).</param>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs monthly.</returns>
  public static string Monthly(int dayOfMonth, int hour, int minute = 0)
  {
    ValidateDayOfMonth(dayOfMonth);
    ValidateHour(hour);
    ValidateMinute(minute);
    return $"{minute} {hour} {dayOfMonth} * *";
  }

  /// <summary>
  /// Creates a cron schedule that runs at intervals.
  /// </summary>
  /// <param name="intervalMinutes">The interval in minutes.</param>
  /// <returns>A cron schedule that runs at the specified interval.</returns>
  public static string EveryInterval(int intervalMinutes)
  {
    return intervalMinutes is < 1 or > 59
      ? throw new ArgumentException("Interval minutes must be between 1 and 59", nameof(intervalMinutes))
      : $"*/{intervalMinutes} * * * *";
  }

  /// <summary>
  /// Creates a cron schedule that runs at intervals.
  /// </summary>
  /// <param name="interval">The interval as a TimeSpan.</param>
  /// <returns>A cron schedule that runs at the specified interval.</returns>
  public static string EveryInterval(TimeSpan interval)
  {
    int totalMinutes = (int)interval.TotalMinutes;
    return totalMinutes is > 0 and < 60 ? $"*/{totalMinutes} * * * *" : throw new ArgumentException("Interval must be between 1 and 59 minutes", nameof(interval));
  }

  /// <summary>
  /// Validates a cron expression using the Cronos library.
  /// </summary>
  /// <param name="cronExpression">The cron expression to validate.</param>
  /// <returns>True if valid, false otherwise.</returns>
  public static bool IsValidCronExpression(string cronExpression)
  {
    if (string.IsNullOrWhiteSpace(cronExpression))
      return false;

    try
    {
      _ = CronExpression.Parse(cronExpression);
      return true;
    }
    catch (CronFormatException)
    {
      return false;
    }
  }

  /// <summary>
  /// Parses a cron expression and returns a Cronos CronExpression object.
  /// </summary>
  /// <param name="cronExpression">The cron expression string.</param>
  /// <returns>A Cronos CronExpression object.</returns>
  /// <exception cref="ArgumentException">Thrown when the cron expression format is invalid.</exception>
  public static CronExpression ParseCronExpression(string cronExpression)
  {
    ArgumentNullException.ThrowIfNull(cronExpression);

    try
    {
      return CronExpression.Parse(cronExpression);
    }
    catch (CronFormatException ex)
    {
      throw new ArgumentException($"Invalid cron expression: {ex.Message}", nameof(cronExpression), ex);
    }
  }

  static void ValidateMinute(int minute)
  {
    if (minute is < 0 or > 59)
      throw new ArgumentOutOfRangeException(nameof(minute), "Minute must be between 0 and 59");
  }

  static void ValidateHour(int hour)
  {
    if (hour is < 0 or > 23)
      throw new ArgumentOutOfRangeException(nameof(hour), "Hour must be between 0 and 23");
  }

  static void ValidateDayOfWeek(int dayOfWeek)
  {
    if (dayOfWeek is < 0 or > 7)
      throw new ArgumentOutOfRangeException(nameof(dayOfWeek), "Day of week must be between 0 and 7");
  }

  static void ValidateDayOfMonth(int dayOfMonth)
  {
    if (dayOfMonth is < 1 or > 31)
      throw new ArgumentOutOfRangeException(nameof(dayOfMonth), "Day of month must be between 1 and 31");
  }
}