using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.UnitTests.Mappers.Reflection.ReflectionBundlesManagerTests;

internal class ReflectionBundleCreator
{
    public static Mock<IReflectionBundle> CreateForType(Type type)
    {
        Mock<IReflectionBundle> reflectionBundle = new();
        reflectionBundle
            .Setup(rb => rb.ExceptionTypeThatMapperMaps)
            .Returns(type);
        return reflectionBundle;
    }
}