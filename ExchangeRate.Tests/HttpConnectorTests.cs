
using ExchangeRate.Connectors;
using System;
using Xunit;

namespace ExchangeRate.Tests
{
    public class HttpConnectorTests
    {
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        [Fact]
        public async void Get_LinkShouldReturn200()
        {
            int desc;
            if(!InternetGetConnectedState(out desc, 0))
            {
                return;
            }

            //Arrange
            var expectedStatus = System.Net.HttpStatusCode.OK;

            //Act
            string link = "https://www.google.com";
            uint maxAttempts = 5;

            var httpConnector = new HttpConnector(maxAttempts);
            (var actualStatus, var actualResponse) = await httpConnector.Get(link);

            //Assert
            Assert.Equal(expectedStatus, actualStatus);
        }

        [Fact]
        public async void Get_LinkShouldReturn404()
        {
            //Arrange
            var expectedStatus = System.Net.HttpStatusCode.NotFound;

            //Act
            string link = "www.google.com";
            uint maxAttempts = 5;

            var httpConnector = new HttpConnector(maxAttempts);
            (var actualStatus, var actualResponse) = await httpConnector.Get(link);

            //Assert
            Assert.Equal(expectedStatus, actualStatus);
        }

        [Fact]
        public async void Get_LinkShouldReturn503()
        {
            //Arrange
            var expectedStatus = System.Net.HttpStatusCode.ServiceUnavailable;

            //Act
            string link = "https://www.myexchageratecustomsiteforthisparticularproject.com";
            uint maxAttempts = 5;

            var httpConnector = new HttpConnector(maxAttempts);
            (var actualStatus, var actualResponse) = await httpConnector.Get(link);

            //Assert
            Assert.Equal(expectedStatus, actualStatus);
        }
    }
}
