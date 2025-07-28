namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a CronJob for use with kubectl create cronjob.
/// </summary>
public class CronJobSpec
{
  /// <summary>
  /// Gets or sets the schedule in cron format for the cronjob.
  /// </summary>
  public required CronSchedule Schedule { get; set; }

  /// <summary>
  /// Gets or sets the job template for the cronjob.
  /// </summary>
  public required CronJobJobTemplate JobTemplate { get; init; }

  /// <summary>
  /// Creates a CronJobSpec with the required hierarchical structure from simple parameters.
  /// </summary>
  /// <param name="schedule">The cron schedule.</param>
  /// <param name="image">The container image.</param>
  /// <param name="name">The job name.</param>
  /// <param name="command">Optional container command.</param>
  /// <param name="restartPolicy">Optional restart policy.</param>
  /// <returns>A properly structured CronJobSpec.</returns>
  public static CronJobSpec Create(CronSchedule schedule, string image, string name, IList<string>? command = null, PodRestartPolicy? restartPolicy = null)
  {
    return new CronJobSpec
    {
      Schedule = schedule,
      JobTemplate = new CronJobJobTemplate
      {
        Metadata = new Metadata { Name = name },
        Spec = new CronJobJobTemplateSpec
        {
          Template = new CronJobPodTemplate
          {
            Spec = new CronJobPodTemplateSpec
            {
              Containers = [
                new PodContainer
                {
                  Name = name,
                  Image = image,
                  Command = command
                }
              ],
              RestartPolicy = restartPolicy
            }
          }
        }
      }
    };
  }
}