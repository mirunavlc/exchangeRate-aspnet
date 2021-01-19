using Xunit;

namespace ExchangeRate.Tests
{
    public class LoaderTests
    {
        [Fact]
        public void ExtractProperty_USDEURPropertyShouldBeSeparated()
        {
            //Arrange
            double expected = 0.847802;

            //Act
            string json =
            @"{
                ""success"": true,
                ""terms"": ""https:\/\/currencylayer.com\/terms"",
                ""privacy"": ""https:\/\/currencylayer.com\/privacy"",
                ""timestamp"": 1507639147,
                ""source"": ""USD"",
                ""quotes"":
                    {
                    ""USDETB"": 23.410168,
                    ""USDEUR"": 0.847802,
                    ""USDFJD"": 2.052013,
                    ""USDFKP"": 0.757298
                    }
              }";

            double actual = Loader.ExtractProperty<double>(json, "USDEUR");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExtractProperty_EURPropertyShouldBeSeparated()
        {
            //Arrange
            var expected = 0.848383;

            //Act
            string json = 
            @"{
                ""disclaimer"": ""Usage subject to terms: https://openexchangerates.org/terms"",
                ""license"": ""https://openexchangerates.org/license"",
                ""timestamp"": 1507640400,
                ""base"": ""USD"",
                ""rates"": 
                {
                    ""ETB"": 23.66,
                    ""EUR"": 0.848383,
                    ""FJD"": 2.056501,
                    ""FKP"": 0.758728,
                    ""GBP"": 0.758728,
                    ""GEL"": 2.475408,
                    ""GGP"": 0.758728
                }
               }";

            double actual = Loader.ExtractProperty<double>(json, "EUR");

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
