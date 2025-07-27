using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ConfigMapGeneratorTests;


/// <summary>
/// Tests for the <see cref="ConfigMapGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ConfigMap object using kubectl create configmap with literal data.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLiteralData_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = "test-config",
        Namespace = "default"
      },
      FromLiteral = new Dictionary<string, string>
      {
        ["key1"] = "value1",
        ["key2"] = "value2"
      }
    };

    // Act
    string fileName = "config-map-literal.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated ConfigMap object using kubectl create configmap with file data.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithFileData_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();

    // Create temporary files for the test
    string tempDir = Path.GetTempPath();
    string configFile = Path.Combine(tempDir, "config.yaml");
    string dataFile = Path.Combine(tempDir, "data.txt");

    await File.WriteAllTextAsync(configFile, "key: value");
    await File.WriteAllTextAsync(dataFile, "sample data");

    try
    {
      var model = new ConfigMap
      {
        Metadata = new Metadata
        {
          Name = "test-config-file",
          Namespace = "default"
        },
        FromFile = [
          new() { Key = "config", FilePath = configFile },
          new() { FilePath = dataFile }
        ]
      };

      // Act
      string fileName = "config-map-file.yaml";
      string outputPath = Path.Combine(Path.GetTempPath(), fileName);
      if (File.Exists(outputPath))
        File.Delete(outputPath);
      await generator.GenerateAsync(model, outputPath);
      string fileContent = await File.ReadAllTextAsync(outputPath);

      // Assert
      _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

      // Cleanup
      File.Delete(outputPath);
    }
    finally
    {
      // Clean up temporary files
      if (File.Exists(configFile)) File.Delete(configFile);
      if (File.Exists(dataFile)) File.Delete(dataFile);
    }
  }

  /// <summary>
  /// Verifies the generated ConfigMap object using kubectl create configmap with environment file.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEnvFile_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();

    // Create temporary env files for the test
    string tempDir = Path.GetTempPath();
    string envFile1 = Path.Combine(tempDir, ".env");
    string envFile2 = Path.Combine(tempDir, "production.env");

    await File.WriteAllTextAsync(envFile1, "DATABASE_URL=postgres://localhost\nPORT=3000");
    await File.WriteAllTextAsync(envFile2, "NODE_ENV=production\nLOG_LEVEL=info");

    try
    {
      var model = new ConfigMap
      {
        Metadata = new Metadata
        {
          Name = "test-config-env",
          Namespace = "production"
        },
        FromEnvFile = [
          envFile1,
          envFile2
        ],
        AppendHash = true
      };

      // Act
      string fileName = "config-map-env.yaml";
      string outputPath = Path.Combine(Path.GetTempPath(), fileName);
      if (File.Exists(outputPath))
        File.Delete(outputPath);
      await generator.GenerateAsync(model, outputPath);
      string fileContent = await File.ReadAllTextAsync(outputPath);

      // Assert
      _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

      // Cleanup
      File.Delete(outputPath);
    }
    finally
    {
      // Clean up temporary files
      if (File.Exists(envFile1)) File.Delete(envFile1);
      if (File.Exists(envFile2)) File.Delete(envFile2);
    }
  }

  /// <summary>
  /// Verifies the generated ConfigMap object using kubectl create configmap with mixed literal and file data.
  /// Note: kubectl doesn't support mixing --from-env-file with other sources.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMixedDataSources_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();

    // Create temporary files for the test
    string tempDir = Path.GetTempPath();
    string nginxFile = Path.Combine(tempDir, "nginx.conf");

    await File.WriteAllTextAsync(nginxFile, "server { listen 80; }");

    try
    {
      var model = new ConfigMap
      {
        Metadata = new Metadata
        {
          Name = "test-config-mixed"
        },
        FromLiteral = new Dictionary<string, string>
        {
          ["database_url"] = "postgresql://localhost:5432/mydb"
        },
        FromFile = [
          new() { Key = "nginx", FilePath = nginxFile }
        ]
      };

      // Act
      string fileName = "config-map-mixed.yaml";
      string outputPath = Path.Combine(Path.GetTempPath(), fileName);
      if (File.Exists(outputPath))
        File.Delete(outputPath);
      await generator.GenerateAsync(model, outputPath);
      string fileContent = await File.ReadAllTextAsync(outputPath);

      // Assert
      _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

      // Cleanup
      File.Delete(outputPath);
    }
    finally
    {
      // Clean up temporary files
      if (File.Exists(nginxFile)) File.Delete(nginxFile);
    }
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the ConfigMap name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyConfigMapName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = ""
      },
      FromLiteral = new Dictionary<string, string>
      {
        ["key"] = "value"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when no data sources are specified.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNoDataSources_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = "test-config"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
