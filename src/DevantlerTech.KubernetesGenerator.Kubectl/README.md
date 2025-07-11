# kubectl-based SecretGenerator

This implementation provides a `SecretGenerator` that uses `kubectl create secret` commands to generate Kubernetes secret YAML files.

## Features

- **Generic Secrets**: Create secrets from literal values, files, or environment files
- **Docker Registry Secrets**: Create secrets for Docker registry authentication
- **TLS Secrets**: Create secrets for TLS certificates and keys
- **Dry-run mode**: All commands use `--dry-run=client` to generate YAML without creating actual resources
- **Namespace support**: Optional namespace specification
- **Append hash**: Optional hash appending to secret names

## Usage

### Generic Secret

```csharp
using DevantlerTech.KubernetesGenerator.Kubectl;
using DevantlerTech.KubernetesGenerator.Kubectl.Models;

var generator = new SecretGenerator();
var secret = new KubectlGenericSecret
{
    Name = "my-secret",
    Namespace = "default",
    FromLiteral = new Dictionary<string, string>
    {
        ["username"] = "admin",
        ["password"] = "secret123"
    }
};

await generator.GenerateAsync(secret, "secret.yaml");
```

### Docker Registry Secret

```csharp
var dockerSecret = new KubectlDockerRegistrySecret
{
    Name = "my-docker-secret",
    Namespace = "default",
    DockerServer = "registry.example.com",
    DockerUsername = "user",
    DockerPassword = "pass",
    DockerEmail = "user@example.com"
};

await generator.GenerateAsync(dockerSecret, "docker-secret.yaml");
```

### TLS Secret

```csharp
var tlsSecret = new KubectlTlsSecret
{
    Name = "my-tls-secret",
    Namespace = "default",
    CertPath = "/path/to/cert.crt",
    KeyPath = "/path/to/key.key"
};

await generator.GenerateAsync(tlsSecret, "tls-secret.yaml");
```

## Output

The generated YAML files are identical to what `kubectl create secret` would produce:

```yaml
apiVersion: v1
data:
  username: YWRtaW4=
  password: c2VjcmV0MTIz
kind: Secret
metadata:
  creationTimestamp: null
  name: my-secret
  namespace: default
```

## Requirements

- kubectl must be installed and available in the PATH
- This implementation uses `kubectl create secret` with `--dry-run=client --output=yaml` to generate the YAML files