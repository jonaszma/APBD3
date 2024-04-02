using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    private readonly UserService _userService;

    public UserServiceTest()
    {
        _userService = new UserService();
    }
    
    

    [Fact]
    public void AddUser_should_return_false_when_firstname_is_incorrect()
    {
 
        var addResult = _userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_should_return_false_when_email_is_incorrect()
    {
 //            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);

        var addResult = _userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_should_return_false_when_age_is_incorrect()
    {
        //            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);

        var addResult = _userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2020-03-21"), 1);
        
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_should_return_false_when_credit_is_incorrect()
    {
        
        
        var addResult = _userService.AddUser("John", "Kowalski", "johndoe@gmail.com", DateTime.Parse("2020-03-21"), 1);
        
        Assert.False(addResult);
    }
    
}













