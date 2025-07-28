using Cronos;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a cron schedule that can be created from a string or CronExpression and implicitly converts to string.
/// </summary>
public readonly struct CronSchedule : IEquatable<CronSchedule>
{
  readonly string _value;

  /// <summary>
  /// Initializes a new instance of the <see cref="CronSchedule"/> struct from a cron expression string.
  /// </summary>
  /// <param name="cronExpression">The cron expression string.</param>
  /// <exception cref="ArgumentException">Thrown when the cron expression format is invalid.</exception>
  public CronSchedule(string cronExpression)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(cronExpression);

    try
    {
      // Validate the cron expression using Cronos
      _ = CronExpression.Parse(cronExpression);
      _value = cronExpression;
    }
    catch (CronFormatException ex)
    {
      throw new ArgumentException($"Invalid cron expression: {ex.Message}", nameof(cronExpression), ex);
    }
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="CronSchedule"/> struct from a Cronos CronExpression.
  /// </summary>
  /// <param name="cronExpression">The Cronos CronExpression.</param>
  public CronSchedule(CronExpression cronExpression)
  {
    ArgumentNullException.ThrowIfNull(cronExpression);
    _value = cronExpression.ToString();
  }

  /// <summary>
  /// Creates a cron schedule that runs daily at the specified time.
  /// </summary>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs daily.</returns>
  public static CronSchedule Daily(int hour, int minute = 0) => new($"{minute} {hour} * * *");

  /// <summary>
  /// Creates a cron schedule that runs weekly on the specified day and time.
  /// </summary>
  /// <param name="dayOfWeek">The day of week.</param>
  /// <param name="hour">The hour to run (0-23).</param>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs weekly.</returns>
  public static CronSchedule Weekly(DayOfWeek dayOfWeek, int hour, int minute = 0) => new($"{minute} {hour} * * {(int)dayOfWeek}");

  /// <summary>
  /// Creates a cron schedule that runs every minute.
  /// </summary>
  /// <returns>A cron schedule that runs every minute.</returns>
  public static CronSchedule EveryMinute() => new("* * * * *");

  /// <summary>
  /// Creates a cron schedule that runs every hour at the specified minute.
  /// </summary>
  /// <param name="minute">The minute to run (0-59).</param>
  /// <returns>A cron schedule that runs every hour.</returns>
  public static CronSchedule EveryHour(int minute = 0) => new($"{minute} * * * *");

  /// <summary>
  /// Creates a CronSchedule from a cron expression string.
  /// </summary>
  /// <param name="cronExpression">The cron expression string.</param>
  /// <returns>A CronSchedule.</returns>
  public static CronSchedule FromString(string cronExpression) => new(cronExpression);

  /// <summary>
  /// Creates a CronSchedule from a Cronos CronExpression.
  /// </summary>
  /// <param name="cronExpression">The Cronos CronExpression.</param>
  /// <returns>A CronSchedule.</returns>
  public static CronSchedule FromCronExpression(CronExpression cronExpression) => new(cronExpression);

  /// <summary>
  /// Implicitly converts a string to a CronSchedule.
  /// </summary>
  /// <param name="cronExpression">The cron expression string.</param>
  public static implicit operator CronSchedule(string cronExpression) => new(cronExpression);

  /// <summary>
  /// Implicitly converts a CronSchedule to a string.
  /// </summary>
  /// <param name="schedule">The CronSchedule.</param>
  public static implicit operator string(CronSchedule schedule) => schedule._value ?? string.Empty;

  /// <summary>
  /// Implicitly converts a Cronos CronExpression to a CronSchedule.
  /// </summary>
  /// <param name="cronExpression">The Cronos CronExpression.</param>
  public static implicit operator CronSchedule(CronExpression cronExpression) => new(cronExpression);

  /// <summary>
  /// Determines whether two CronSchedule instances are equal.
  /// </summary>
  /// <param name="left">The first CronSchedule to compare.</param>
  /// <param name="right">The second CronSchedule to compare.</param>
  /// <returns>true if the CronSchedule instances are equal; otherwise, false.</returns>
  public static bool operator ==(CronSchedule left, CronSchedule right) => left.Equals(right);

  /// <summary>
  /// Determines whether two CronSchedule instances are not equal.
  /// </summary>
  /// <param name="left">The first CronSchedule to compare.</param>
  /// <param name="right">The second CronSchedule to compare.</param>
  /// <returns>true if the CronSchedule instances are not equal; otherwise, false.</returns>
  public static bool operator !=(CronSchedule left, CronSchedule right) => !left.Equals(right);

  /// <summary>
  /// Determines whether this instance and another specified CronSchedule object have the same value.
  /// </summary>
  /// <param name="other">The CronSchedule to compare to this instance.</param>
  /// <returns>true if the value of the other parameter is the same as this instance; otherwise, false.</returns>
  public bool Equals(CronSchedule other) => string.Equals(_value, other._value, StringComparison.Ordinal);

  /// <summary>
  /// Determines whether this instance and a specified object have the same value.
  /// </summary>
  /// <param name="obj">The object to compare to this instance.</param>
  /// <returns>true if obj is a CronSchedule and its value is the same as this instance; otherwise, false.</returns>
  public override bool Equals(object? obj) => obj is CronSchedule other && Equals(other);

  /// <summary>
  /// Returns the hash code for this CronSchedule.
  /// </summary>
  /// <returns>A 32-bit signed integer hash code.</returns>
  public override int GetHashCode() => _value?.GetHashCode(StringComparison.Ordinal) ?? 0;

  /// <summary>
  /// Returns the string representation of the cron schedule.
  /// </summary>
  /// <returns>The cron expression string.</returns>
  public override string ToString() => _value ?? string.Empty;
}