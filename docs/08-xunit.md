# Theory
  - [Theory] attribute denotes a parameterised test that is true for a subset of data  
    - InlineData
    ```csharp  
    [Theory]
    [InlineData("2020-01-15", "2020-01-25", 100, 110)]
    [InlineData("2020-03-25", "2020-04-05", 100, 112)]
    [InlineData("2020-12-25", "2021-01-05", 100, 113)]
    public void CalculatePrice_HasValidPeriod_ResultShouldBeCorrect(string checkIn, string checkOut, decimal price, decimal expectedResult)
    {
        //Arrange
        //Act
        //Assert
    }
    ```
      
    - ClassData  
    ```csharp  
    [Theory]
    [ClassData(typeof(PricingTestData))]
    public void CalculatePrice_TestCasesAreDefinedInClassData_ResultShouldBeCorrect(DateTime checkIn, DateTime checkOut, decimal price, decimal expectedResult)
    {
        //Arrange
        //Act
        //Assert
    }
    ```
      Create separate class "PricingTestData"
      ```csharp  
      public class PricingTestData : IEnumerable<object[]>
      {
          public IEnumerator<object[]> GetEnumerator()
          {
              yield return new object[] { new DateTime(2020, 01, 10), new DateTime(2020, 01, 15), 100, 110 };
              yield return new object[] { new DateTime(2020, 03, 27), new DateTime(2020, 04, 15), 100, 112 };
              yield return new object[] { new DateTime(2020, 12, 10), new DateTime(2021, 01, 15), 100, 113 };
          }
          IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
      }
      ```
            
    - MemberData  
    ```csharp  
    [Theory]
    [MemberData(nameof(PricingMemberTestCases))]
    public void CalculatePrice_TestCasesAreDefinedAsMemberData_ResultShouldBeCorrect(DateTime checkIn, DateTime checkOut, decimal price, decimal expectedResult)
    {
        //Arrange
        //Act
        //Assert
    }
    ```
      Create static property within the Test class
      ```csharp
      public static IEnumerable<object[]> PricingMemberTestCases => new List<object[]>
      {
          new object[] { new DateTime(2020, 01, 10), new DateTime(2020, 01, 15), 100, 110 },
          new object[] { new DateTime(2020, 03, 27), new DateTime(2020, 04, 15), 100, 112 },
          new object[] { new DateTime(2020, 12, 10), new DateTime(2021, 01, 15), 100, 113 }
      };
      ```
