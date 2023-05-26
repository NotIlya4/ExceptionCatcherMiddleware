using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.UnitTests.Core.ReflectionBundlesManager;

public class ReflectionBundlesManagerTests
{
    private readonly ExceptionCatcherMiddleware.Core.MainClasses.ReflectionBundlesManager _manager = new();
    
    [Fact]
    public void Get_ManagerContainsExceptionTryToGetForInvalidCastException_Exception()
    {
        _manager.Set<ExceptionMapper>();

        ReflectionBundle result = _manager.Get<InvalidCastException>();
        
        Assert.Equal(typeof(Exception), result.ExceptionType);
    }
    
    [Fact]
    public void Get_ManagerContainsSystemExceptionTryToGetForInvalidCastException_SystemException()
    {
        _manager.Set<ExceptionMapper>();
        _manager.Set<SystemExceptionMapper>();

        ReflectionBundle result = _manager.Get<InvalidCastException>();
        
        Assert.Equal(typeof(SystemException), result.ExceptionType);
    }
    
    [Fact]
    public void Get_ManagerContainsInvalidCastExceptionTryToGetForInvalidCastException_InvalidCastException()
    {
        _manager.Set<ExceptionMapper>();
        _manager.Set<SystemExceptionMapper>();
        _manager.Set<InvalidCastExceptionMapper>();

        ReflectionBundle result = _manager.Get<InvalidCastException>();
        
        Assert.Equal(typeof(InvalidCastException), result.ExceptionType);
    }
    
    [Fact]
    public void FindTypeInDictKeyThatMostCloseToProvided_DictionaryContainsExceptionProvideInvalidCastException_Exception()
    {
        var dict = new Dictionary<Type, object>();
        dict[typeof(Exception)] = new object();

        Type result = ExceptionCatcherMiddleware.Core.MainClasses.ReflectionBundlesManager.FindTypeInDictKeyThatMostCloseToProvided(dict, typeof(InvalidCastException));
        
        Assert.Equal(typeof(Exception), result);
    }
    
    [Fact]
    public void FindTypeInDictKeyThatMostCloseToProvided_DictionaryContainsSystemExceptionProvideInvalidCastException_SystemException()
    {
        var dict = new Dictionary<Type, object>();
        dict[typeof(Exception)] = new object();
        dict[typeof(SystemException)] = new object();

        Type result = ExceptionCatcherMiddleware.Core.MainClasses.ReflectionBundlesManager.FindTypeInDictKeyThatMostCloseToProvided(dict, typeof(InvalidCastException));
        
        Assert.Equal(typeof(SystemException), result);
    }
    
    [Fact]
    public void FindTypeInDictKeyThatMostCloseToProvided_DictionaryContainsInvalidCastExceptionProvideInvalidCastException_InvalidCastException()
    {
        var dict = new Dictionary<Type, object>();
        dict[typeof(Exception)] = new object();
        dict[typeof(SystemException)] = new object();
        dict[typeof(InvalidCastException)] = new object();

        Type result = ExceptionCatcherMiddleware.Core.MainClasses.ReflectionBundlesManager.FindTypeInDictKeyThatMostCloseToProvided(dict, typeof(InvalidCastException));
        
        Assert.Equal(typeof(InvalidCastException), result);
    }
}