using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.UnitTests.Mappers.Reflection.ReflectionBundlesManagerTests;

internal class GetByFirstAvailableParentTests
{
    private ReflectionBundlesManager _managerWithArgumentExceptionBundle = null!;
    private Mock<IReflectionBundle> _exceptionBundle = null!;
    private Mock<IReflectionBundle> _argumentExceptionBundle = null!;

    [SetUp]
    public void SetUp()
    {
        _exceptionBundle = ReflectionBundleCreator.CreateForType(typeof(Exception));
        _argumentExceptionBundle = ReflectionBundleCreator.CreateForType(typeof(ArgumentException));
        
        _managerWithArgumentExceptionBundle = new(_exceptionBundle.Object);
        _managerWithArgumentExceptionBundle.Set(_argumentExceptionBundle.Object);
    }
    
    [Test]
    public void BundleForProvidedExceptionTypeStrictlyExists_ReturnBundleForThatType()
    {
        IReflectionBundle? result = _managerWithArgumentExceptionBundle.GetByFirstAvailableParent(typeof(ArgumentException));
        
        Assert.That(result, Is.EqualTo(_argumentExceptionBundle.Object));
    }
    
    [Test]
    public void ManagerContainsExceptionAndArgumentExceptionBundleArgumentOutOfRangeExceptionPassed_ReturnArgumentExceptionBundle()
    {
        var result = _managerWithArgumentExceptionBundle.GetByFirstAvailableParent(typeof(ArgumentOutOfRangeException));
        
        Assert.That(result, Is.EqualTo(_argumentExceptionBundle.Object));
    }
    
    [Test]
    public void ManagerContainsExceptionAndArgumentExceptionBundleNullReferenceExceptionPassed_ReturnExceptionBundle()
    {
        var result = _managerWithArgumentExceptionBundle.GetByFirstAvailableParent(typeof(NullReferenceException));
        
        Assert.That(result, Is.EqualTo(_exceptionBundle.Object));
    }
}