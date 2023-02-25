using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using ExceptionCatcherMiddleware.Mappers.Dispatcher.MappersReflection;
using ExceptionCatcherMiddleware.Mappers.Exceptions;
using Moq;

namespace ExceptionCatcherMiddleware.UnitTests.Mappers.MappersReflection;

public class ReflectionBundleTests
{
    private Mock<IExceptionMapper<Exception>> _exceptionMapper = null!;
    
    [SetUp]
    public void SetUp()
    {
        _exceptionMapper = new();
    }

    [Test]
    public void Constructor_TypeWithoutMapMethod_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(object)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void Constructor_TypeWithTwoArgumentsInMapMethod_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(IReflectionBundleWithMapMethodThatHasMoreThanOneParameter)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void Constructor_TypeWithWrongResponse_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(IReflectionBundleWithWrongResponse)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void Constructor_TypeWithWrongParameterType_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(IReflectionBundleWithWrongParameterType)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void Constructor_GoodTypeWithExceptionInParameter_SuccessfullyConstruct()
    {
        new ReflectionBundle(typeof(IExceptionMapper<Exception>));
    }
    
    [Test]
    public void Constructor_GoodTypeWithArgumentExceptionInParameter_SuccessfullyConstruct()
    {
        new ReflectionBundle(typeof(IExceptionMapper<ArgumentException>));
    }
    
    [Test]
    public void Constructor_GoodTypeWithArgumentOutOfRangeExceptionInParameter_SuccessfullyConstruct()
    {
        new ReflectionBundle(typeof(IExceptionMapper<ArgumentOutOfRangeException>));
    }
}

internal interface IReflectionBundleWithMapMethodThatHasMoreThanOneParameter
{
    public BadResponse Map(Exception exception, string a);
}

internal interface IReflectionBundleWithWrongResponse
{
    public object Map(Exception exception);
}

internal interface IReflectionBundleWithWrongParameterType
{
    public object Map(object exception);
}