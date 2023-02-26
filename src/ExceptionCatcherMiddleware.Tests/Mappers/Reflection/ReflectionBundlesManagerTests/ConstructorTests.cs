using ExceptionCatcherMiddleware.Mappers.Exceptions;
using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.UnitTests.Mappers.Reflection.ReflectionBundlesManagerTests;

internal class ConstructorTests
{
    [Test]
    public void ReflectionBundleForException_SuccessfullyInitAndContainReflectionBundleForException()
    {
        Mock<IReflectionBundle> reflectionBundle = ReflectionBundleCreator.CreateForType(typeof(Exception));
        ReflectionBundlesManager reflectionBundlesManager = new(reflectionBundle.Object);

        IReflectionBundle? result = reflectionBundlesManager.Get(typeof(Exception));

        Assert.That(result, Is.EqualTo(reflectionBundle.Object));
    }

    [Test]
    [TestCase(typeof(ArgumentException))]
    [TestCase(typeof(ArgumentOutOfRangeException))]
    [TestCase(typeof(object))]
    [TestCase(typeof(int))]
    public void ReflectionBundleNotForException_ThrowTypeValidationException(Type type)
    {
        Mock<IReflectionBundle> reflectionBundle = ReflectionBundleCreator.CreateForType(type);

        Assert.That(
            () => new ReflectionBundlesManager(
                reflectionBundle.Object),
            Throws.Exception.TypeOf<TypeValidationException>());
    }
}