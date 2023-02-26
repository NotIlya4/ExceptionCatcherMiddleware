using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using ExceptionCatcherMiddleware.Mappers.Exceptions;
using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.UnitTests.Mappers.Reflection.ReflectionBundleTests;

public class ConstructorTests
{
    [Test]
    public void TypeWithoutMapMethod_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(object)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void TypeWithTwoArgumentsInMapMethod_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(IReflectionBundleWithMapMethodThatHasMoreThanOneParameter)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void TypeWithWrongResponse_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(IReflectionBundleWithWrongResponse)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void TypeWithWrongParameterType_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundle(typeof(IReflectionBundleWithWrongParameterType)),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void GoodTypeWithExceptionInParameter_SuccessfullyConstruct()
    {
        new ReflectionBundle(typeof(IExceptionMapper<Exception>));
    }
    
    [Test]
    public void GoodTypeWithArgumentExceptionInParameter_SuccessfullyConstruct()
    {
        new ReflectionBundle(typeof(IExceptionMapper<ArgumentException>));
    }
    
    [Test]
    public void GoodTypeWithArgumentOutOfRangeExceptionInParameter_SuccessfullyConstruct()
    {
        new ReflectionBundle(typeof(IExceptionMapper<ArgumentOutOfRangeException>));
    }
    
    [Test]
    public void GoodTypeWithNullReferenceExceptionInParameter_SuccessfullyConstruct()
    {
        new ReflectionBundle(typeof(IExceptionMapper<NullReferenceException>));
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