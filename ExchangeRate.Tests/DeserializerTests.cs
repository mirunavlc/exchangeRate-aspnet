﻿using ExchangeRate.JSONHandlers;
using System;
using Xunit;

namespace ExchangeRate.Tests
{
    public class DeserializerTests
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

            Exception ex = null;
            var actual = Deserializer.ExtractProperty<decimal>(json, "USDEUR", out ex);

            //Assert
            Assert.True(ex == null);
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

            Exception ex = null;
            var actual = Deserializer.ExtractProperty<decimal>(json, "EUR", out ex);

            //Assert
            Assert.True(ex == null);
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

            Exception exUSDEUR = null;
            var actualUSDEUR = Deserializer.ExtractProperty<decimal>(jsonUSDEUR, "USDEUR", out exUSDEUR);

            Exception exEUR = null;
            var actualEUR = Deserializer.ExtractProperty<decimal>(jsonEUR, "EUR", out exEUR);

            var comp = decimal.Compare(actualUSDEUR, actualEUR);
            var actualSmallest = comp > 0 ? actualEUR : actualUSDEUR;

            //Assert
            Assert.True(exUSDEUR == null);
            Assert.True(exEUR == null);
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

            Exception ex = null;
            var actual = Deserializer.ExtractProperty<decimal>(json, "EUR", out ex);

            //Assert
            Assert.True(ex != null);
            Assert.Equal(expected, actual);
        }
    }
}
