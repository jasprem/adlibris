using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Catalog.WebApi.IntegrationTests
{
    public class SampleTests
    {
        private readonly TestContext _sut;

        public SampleTests()
        {
            _sut = new TestContext();
        }

        [Fact]
        public void TestSomething()
        {
            var productId = "test";
            var response = _sut.Client.GetAsync($"/api/products/{productId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
