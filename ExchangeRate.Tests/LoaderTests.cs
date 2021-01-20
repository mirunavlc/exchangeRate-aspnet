using Xunit;

namespace ExchangeRate.Tests
{
    public class LoaderTests
    {
        [Fact]
        public void ExtractProperty_USDEURPropertyShouldBeSeparated()
        {
            //Arrange
            var expected = 0.847802M;

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

            var actual = Loader.ExtractProperty<decimal>(json, "USDEUR");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExtractProperty_EURPropertyShouldBeSeparated()
        {
            //Arrange
            var expected = 0.848383M;

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

            var actual = Loader.ExtractProperty<decimal>(json, "EUR");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExtractProperty_EURShouldBeSmallest()
        {
            //Arrange
            var expectedSmallest = 0.847802M;

            //Act

            string jsonUSDEUR =
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

            string jsonEUR =
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

            var actualUSDEUR = Loader.ExtractProperty<decimal>(jsonUSDEUR, "USDEUR");
            var actualEUR = Loader.ExtractProperty<decimal>(jsonEUR, "EUR");

            var comp = decimal.Compare(actualUSDEUR, actualEUR);
            var actualSmallest = comp > 0 ? actualEUR : actualUSDEUR;

            //Assert
            Assert.Equal(expectedSmallest, actualSmallest);
        }

        [Fact]
        public void ExtractProperty_ShouldHandleBadInput()
        {
            //Arrange
            var expected = default(decimal);

            //Act
            string json =
            @"salut}";

            var actual = Loader.ExtractProperty<decimal>(json, "EUR");

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
