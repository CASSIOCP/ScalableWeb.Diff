using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ScalableWeb.Diff.IntegrationTest
{
    public class IntegrationTest
    {
        private const string Route = "v1/diff";
        private const int Id = 123;

        private readonly string leftUri = $"{Route}/{Id}/left";
        private readonly string rightUri = $"{Route}/{Id}/right";

        [Fact]
        public async Task Test_SetLeft_Success()
        {
            string fileContent = File.ReadAllText(@"..\..\..\..\files\SameSize1.txt");

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(leftUri, new StringContent(fileContent));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_SetLeft_InvalidContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(leftUri, new StringContent("TEST TEST TEST"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_SetLeft_EmptyContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(leftUri, new StringContent(string.Empty));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_SetLeft_NullContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(leftUri, null);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_SetRight_Success()
        {
            string fileContent = File.ReadAllText(@"..\..\..\..\files\SameSize2.txt");

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(rightUri, new StringContent(fileContent));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]        
        public async Task Test_SetRight_InvalidContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(rightUri, new StringContent("TEST TEST TEST"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_SetRight_EmptyContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(rightUri, new StringContent(string.Empty));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_SetRight_NullContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync(rightUri, null);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Get_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"{Route}/{Id}");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Get_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"{Route}/-9999");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}