# SetUp
  - [SetUp] : This code will be executed before running each test within the class that has [TestFixture] attribute in order to provide a common set of functions.  

# TearDown  
  - [TearDown] : This code will be executed after executing each test within the class that has [TestFixture] attribute in order to provide a common set of functions.  

# OneTimeSetUp
  - [OneTimeSetUp] : This code will be executed only once before running the tests in a fixture. If we have n tests this will be called only once.  

# OneTimeTearDown
  - [OneTimeTearDown] : This code will be executed only once after executing the tests in a fixture. If we have n tests this will be called only once.  
  
# SetUp+TearDown+OneTimeSetUp+OneTimeTearDown  
```csharp
    [TestFixture]
    public class SetupTesingTest
    {
        [OneTimeSetUp]
        public void Init() { }

        [OneTimeTearDown]
        public void CleanUp() { }

        [SetUp]
        public void Setup() { }

        [TearDown]
        public void TearDown() { }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(true);
        }    
    }
```

# SetUpFixture
  - [SetUpFixture] : This is the attribute that marks a class that contains the one-time setup or teardown methods for all the test fixtures under a given namespace.  
    - [OneTimeSetUp] : The OneTimeSetUp method in a SetUpFixture is executed once before any of the fixtures contained in its namespace.  
    - [OneTimeTearDown] : The OneTimeTearDown method is executed once after all the fixtures have completed execution.
  ```csharp
    [SetUpFixture]
    public class MySetupTestFixture
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTest() { }

        [OneTimeTearDown]
	    public void RunAfterAnyTest() { }
    }
  ```
