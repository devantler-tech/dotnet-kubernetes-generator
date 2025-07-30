#pragma warning disable IDE0008 // Use explicit type instead of 'var'
#pragma warning disable IDE0058 // Expression value is never used

using System.Net;
using DevantlerTech.KubernetesGenerator.Core.Converters;
using Moq;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Core.Tests.Converters;

/// <summary>
/// Tests for <see cref="IPNetworkTypeConverter"/>.
/// </summary>
public class IPNetworkTypeConverterTests
{
  readonly IPNetworkTypeConverter _converter = new();

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.Accepts(Type)"/> method with IPNetwork type.
  /// </summary>
  [Fact]
  public void Accepts_WithIPNetworkType_ShouldReturnTrue()
  {
    // Arrange
    var type = typeof(IPNetwork);

    // Act
    var result = _converter.Accepts(type);

    // Assert
    Assert.True(result);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.Accepts(Type)"/> method with nullable IPNetwork type.
  /// </summary>
  [Fact]
  public void Accepts_WithNullableIPNetworkType_ShouldReturnTrue()
  {
    // Arrange
    var type = typeof(IPNetwork?);

    // Act
    var result = _converter.Accepts(type);

    // Assert
    Assert.True(result);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.Accepts(Type)"/> method with different type.
  /// </summary>
  [Fact]
  public void Accepts_WithDifferentType_ShouldReturnFalse()
  {
    // Arrange
    var type = typeof(string);

    // Act
    var result = _converter.Accepts(type);

    // Assert
    Assert.False(result);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.ReadYaml(IParser, Type, ObjectDeserializer)"/> method with valid CIDR.
  /// </summary>
  [Fact]
  public void ReadYaml_WithValidCidr_ShouldReturnIPNetwork()
  {
    // Arrange
    var mockParser = new Mock<IParser>();
    var scalar = new Scalar("192.168.1.0/24");
    var setup = mockParser.Setup(p => p.Current).Returns(scalar);
    var type = typeof(IPNetwork);

    // Act
    object? result = _converter.ReadYaml(mockParser.Object, type, null!);

    // Assert
    Assert.NotNull(result);
    Assert.IsType<IPNetwork>(result);
    var network = (IPNetwork)result;
    Assert.Equal("192.168.1.0/24", network.ToString());
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.ReadYaml(IParser, Type, ObjectDeserializer)"/> method with invalid CIDR.
  /// </summary>
  [Fact]
  public void ReadYaml_WithInvalidCidr_ShouldReturnNull()
  {
    // Arrange
    var mockParser = new Mock<IParser>();
    var scalar = new Scalar("invalid-cidr");
    var setup = mockParser.Setup(p => p.Current).Returns(scalar);
    var type = typeof(IPNetwork);

    // Act
    object? result = _converter.ReadYaml(mockParser.Object, type, null!);

    // Assert
    Assert.Null(result);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.ReadYaml(IParser, Type, ObjectDeserializer)"/> method with null parser.
  /// </summary>
  [Fact]
  public void ReadYaml_WithNullParser_ShouldReturnNull()
  {
    // Arrange
    IParser? parser = null;
    var type = typeof(IPNetwork);

    // Act
    object? result = _converter.ReadYaml(parser!, type, null!);

    // Assert
    Assert.Null(result);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.ReadYaml(IParser, Type, ObjectDeserializer)"/> method with non-scalar event.
  /// </summary>
  [Fact]
  public void ReadYaml_WithNonScalarEvent_ShouldReturnNull()
  {
    // Arrange
    var mockParser = new Mock<IParser>();
    var sequenceStart = new SequenceStart(null, null, false, SequenceStyle.Block);
    var setup = mockParser.Setup(p => p.Current).Returns(sequenceStart);
    var type = typeof(IPNetwork);

    // Act
    object? result = _converter.ReadYaml(mockParser.Object, type, null!);

    // Assert
    Assert.Null(result);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.WriteYaml(IEmitter, object?, Type, ObjectSerializer)"/> method with valid IPNetwork.
  /// </summary>
  [Fact]
  public void WriteYaml_WithValidIPNetwork_ShouldEmitScalar()
  {
    // Arrange
    var mockEmitter = new Mock<IEmitter>();
    var network = IPNetwork.Parse("10.0.0.0/8");
    var type = typeof(IPNetwork);

    // Act
    _converter.WriteYaml(mockEmitter.Object, network, type, null!);

    // Assert
    mockEmitter.Verify(e => e.Emit(It.Is<Scalar>(s => s.Value == "10.0.0.0/8")), Times.Once);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.WriteYaml(IEmitter, object?, Type, ObjectSerializer)"/> method with null value.
  /// </summary>
  [Fact]
  public void WriteYaml_WithNullValue_ShouldNotEmit()
  {
    // Arrange
    var mockEmitter = new Mock<IEmitter>();
    object? value = null;
    var type = typeof(IPNetwork);

    // Act
    _converter.WriteYaml(mockEmitter.Object, value, type, null!);

    // Assert
    mockEmitter.Verify(e => e.Emit(It.IsAny<ParsingEvent>()), Times.Never);
  }

  /// <summary>
  /// Tests the <see cref="IPNetworkTypeConverter.WriteYaml(IEmitter, object?, Type, ObjectSerializer)"/> method with null emitter.
  /// </summary>
  [Fact]
  public void WriteYaml_WithNullEmitter_ShouldNotThrow()
  {
    // Arrange
    IEmitter? emitter = null;
    var network = IPNetwork.Parse("172.16.0.0/16");
    var type = typeof(IPNetwork);

    // Act & Assert
    var exception = Record.Exception(() => _converter.WriteYaml(emitter!, network, type, null!));
    Assert.Null(exception);
  }
}
