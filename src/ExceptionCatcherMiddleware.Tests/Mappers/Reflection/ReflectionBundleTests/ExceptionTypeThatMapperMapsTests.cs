using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.UnitTests.Mappers.Reflection.ReflectionBundleTests;

public class ExceptionTypeThatMapperMapsTests
{
    [Test]
    public void MapperForArgumentException_ReturnTypeOfArgumentException()
    {
        Mock<IExceptionMapper<ArgumentException>> argumentExceptionMapper = new();
        ReflectionBundle reflectionBundle = new(argumentExceptionMapper.Object.GetType());

        var result = reflectionBundle.ExceptionTypeThatMapperMaps;
        
        Assert.That(result, Is.EqualTo(typeof(ArgumentException)));
    }
    
    [Test]
    public void MapperForNullReferenceException_ReturnTypeOfNullReferenceException()
    {
        Mock<IExceptionMapper<NullReferenceException>> nullReferenceExceptionMapper = new();
        ReflectionBundle reflectionBundle = new(nullReferenceExceptionMapper.Object.GetType());

        var result = reflectionBundle.ExceptionTypeThatMapperMaps;
        
        Assert.That(result, Is.EqualTo(typeof(NullReferenceException)));
    }
}