using System.ComponentModel;

namespace DevantlerTech.KubernetesGenerator.Flux.Models.AlertProvider;

/// <summary>
/// The type of a Flux Alert Provider.
/// </summary>
public enum FluxAlertProviderType
{
  /// <summary>
  /// Generic webhook alert provider.
  /// </summary>
  [Description("generic")]
  generic,

  /// <summary>
  /// Generic webhook with HMAC alert provider.
  /// </summary>
  [Description("generic-hmac")]
  GenericHmac,

  /// <summary>
  /// Azure event hub alert provider.
  /// </summary>
  [Description("azureeventhub")]
  AzureEventHub,

  /// <summary>
  /// DataDog alert provider.
  /// </summary>
  [Description("datadog")]
  DataDog,

  /// <summary>
  /// Discord alert provider.
  /// </summary>
  [Description("discord")]
  Discord,

  /// <summary>
  /// GitHub dispatch alert provider.
  /// </summary>
  [Description("githubdispatch")]
  GitHubDispatch,

  /// <summary>
  /// Google chat alert provider.
  /// </summary>
  [Description("googlechat")]
  GoogleChat,

  /// <summary>
  /// Google Pub/Sub alert provider.
  /// </summary>
  [Description("googlepubsub")]
  GooglePubSub,

  /// <summary>
  /// Grafana alert provider.
  /// </summary>
  [Description("grafana")]
  Grafana,

  /// <summary>
  /// Lara alert provider.
  /// </summary>
  [Description("lark")]
  Lark,

  /// <summary>
  /// Matrix alert provider.
  /// </summary>
  [Description("matrix")]
  Matrix,

  /// <summary>
  /// Microsoft Teams alert provider.
  /// </summary>
  [Description("msteams")]
  MSTeams,

  /// <summary>
  /// OpsGenie alert provider.
  /// </summary>
  [Description("opsgenie")]
  OpsGenie,

  /// <summary>
  /// PagerDuty alert provider.
  /// </summary>
  [Description("pagerduty")]
  PagerDuty,

  /// <summary>
  /// Prometheus alertmanager alert provider.
  /// </summary>
  [Description("alertmanager")]
  AlertManager,

  /// <summary>
  /// Rocket alert provider.
  /// </summary>
  [Description("rocket")]
  Rocket,

  /// <summary>
  /// Sentry alert provider.
  /// </summary>
  [Description("sentry")]
  Sentry,

  /// <summary>
  /// Slack alert provider.
  /// </summary>
  [Description("slack")]
  Slack,

  /// <summary>
  /// Telegram alert provider.
  /// </summary>
  [Description("telegram")]
  Telegram,

  /// <summary>
  /// WebEx alert provider.
  /// </summary>
  [Description("webex")]
  Webex,

  /// <summary>
  /// NATS alert provider.
  /// </summary>
  [Description("nats")]
  NATS,

  /// <summary>
  /// Azure DevOps alert provider (for git commit status updates).
  /// </summary>
  [Description("azuredevops")]
  AzureDevOps,

  /// <summary>
  /// Bitbucket alert provider (for git commit status updates).
  /// </summary>
  [Description("bitbucket")]
  Bitbucket,

  /// <summary>
  /// Bitbucket Server alert provider (for git commit status updates).
  /// </summary>
  [Description("bitbucketserver")]
  BitbucketServer,

  /// <summary>
  /// GitHub alert provider (for git commit status updates).
  /// </summary>
  [Description("github")]
  GitHub,

  /// <summary>
  /// GitLab alert provider (for git commit status updates).
  /// </summary>
  [Description("gitlab")]
  GitLab,

  /// <summary>
  /// Gitea alert provider (for git commit status updates).
  /// </summary>
  [Description("gitea")]
  Gitea
}
