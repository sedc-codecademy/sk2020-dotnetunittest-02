# Author
  - [Author] : Author attribute will add information about the author of the tests. Can be applied to Test and TestFixture.
  ```csharp
  [Test]
  [Author("Add Name")]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  
  [Test]
  [Author("Add Name", "Add email")]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  
  [Test(Author = "Add Name")]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  
  [TestFixture(Author = "Add Name")]
  
  ```

# Description
  - [Description] : Description attribute is used to apply descriptive text if needed. Can be applied to Test and TestFixture. 
   ```csharp
  [Test]
  [Description("Add description")]
  public void Test()
  {
      Assert.IsTrue(true);
  }
    
  [Test(Description = "Add description")]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  
  [TestFixture(Description = "Add description")]
  
  ```

# Category
  - [Category] - Category attribute is used to categorize. Can be applied to Test and TestFixture.  
   ```csharp
  [Test]
  [Category("Add category")]
  public void Test()
  {
      Assert.IsTrue(true);
  }
   
  [TestFixture(Category ="Add category")]
  ```

# Order
  - [Order] - The Order attribute may be placed on a test method or fixture to specify the order in which tests are run within the fixture. 
   ```csharp
  [Test]
  [Order(x)]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  
  [TestFixture]
  [Order(x)]
  ```

# Retry
  - [Retry] - Retry attribute is used on a test method to specify that it should be rerun if it fails, up to a maximum number of times.  
  ```csharp
  [Test]
  [Retry(x)]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  ```

# MaxTime
  - [MaxTime] - MaxTime attribute is used on test methods to specify a maximum time in milliseconds for a test case. If the test case takes longer than the specified time to complete, it is reported as a failure.
  ```csharp
  [Test]
  [MaxTime(x)]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  ```
	
# Timeout
  - [Timeout] - Timeout attribute is used to specify a timeout value in milliseconds for a test case. If the test case runs longer than the time specified it is immediately cancelled and reported as a failure, with a message indicating that the timeout was exceeded.
   ```csharp
  [Test]
  [Timeout(x)]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  ```

# Repeat
  - [Repeat] - Repeat attribute is used on a test method to specify that it should be executed multiple times.  
  ```csharp
  [Test]
  [Repeat(x)]
  public void Test()
  {
      Assert.IsTrue(true);
  }
  ```
 # Values  
  - [Values] - Values attribute is for providing a set of values in a parameterized test method.  
    - NUnit will create test cases for all possible combination of the provided values
  ```csharp  
  [Test]
  public void MyTest([Values(1997, 2001)] int year, [Values(false)] bool expectedResult)
  {
      /* ... */
  }
  ```
