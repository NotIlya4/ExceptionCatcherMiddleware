using ExceptionCatcherMiddleware.Mappers.Dispatcher.MappersReflection;
using ExceptionCatcherMiddleware.Mappers.Exceptions;
using Moq;

namespace ExceptionCatcherMiddleware.UnitTests.Mappers.MappersReflection;

public class ReflectionBundlesManagerTests
{
    private Mock<IReflectionBundle> _exceptionReflectionBundle = null!;
    private Mock<IReflectionBundle> _argumentExceptionReflectionBundle = null!;
    
    [SetUp]
    public void SetUp()
    {
        _exceptionReflectionBundle = new();
        _exceptionReflectionBundle
            .Setup(rb => rb.ExceptionTypeThatMapperMaps)
            .Returns(typeof(Exception));

        _argumentExceptionReflectionBundle = new();
        _argumentExceptionReflectionBundle
            .Setup(rb => rb.ExceptionTypeThatMapperMaps)
            .Returns(typeof(ArgumentException));
    }
    
    [Test]
    public void Constructor_ExceptionReflectionBundle_InitWithoutThrowing()
    {
        new ReflectionBundlesManager(_exceptionReflectionBundle.Object);
    }

    [Test]
    public void Constructor_NotExceptionReflectionBundle_ThrowTypeValidationException()
    {
        Assert.That(
            () => new ReflectionBundlesManager(_argumentExceptionReflectionBundle.Object),
            Throws.Exception.TypeOf<TypeValidationException>());
    }

    [Test]
    public void Get_PassExceptionReflectionBundleToConstructor_ReturnThatBundle()
    {
        ReflectionBundlesManager manager = new(_exceptionReflectionBundle.Object);

        IReflectionBundle? result = manager.Get(typeof(Exception));
        
        Assert.That(result, Is.EqualTo(_exceptionReflectionBundle.Object));
    }

    [Test]
    public void GetByFirstAvailableParent_BundleForProvidedExceptionTypeExists_ReturnThatBundle()
    {
        ReflectionBundlesManager manager = new(_exceptionReflectionBundle.Object);
        manager.Set(_argumentExceptionReflectionBundle.Object);

        IReflectionBundle? result = manager.GetByFirstAvailableParent(typeof(ArgumentException));
        
        Assert.That(result, Is.EqualTo(_argumentExceptionReflectionBundle.Object));
    }
    
    [Test]
    public void GetByFirstAvailableParent_BundleExistsForProvidedExceptionParentType_ReturnParentBundle()
    {
        ReflectionBundlesManager manager = new(_exceptionReflectionBundle.Object);

        IReflectionBundle? result = manager.GetByFirstAvailableParent(typeof(ArgumentException));
        
        Assert.That(result, Is.EqualTo(_exceptionReflectionBundle.Object));
    }
}