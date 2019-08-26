using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace UnitTestProject1.Steps
{
    [Binding]
    public class SpecflowPOSTTestFeatureSteps
    {

        public static RestClient restClient;
        public static RestRequest restRequest;
        public static IRestResponse restResponse;
        public static JObject actualResponse;

        [Given(@"I have URI (.*) with email (.*) and password (.*)")]
        public void GivenIHaveURIWithEmailAndPassword(string uri, string email, string password)
        {
            restClient = new RestClient(uri);
            restRequest = new RestRequest("api/register", Method.POST);
            restRequest.AddParameter("email", email);
            restRequest.AddParameter("password", password);

        }

        [When(@"I hit the uri")]
        public void WhenIHitTheUri()
        {

            Console.WriteLine($"Executing Request {restClient.BuildUri(restRequest)}");
            restResponse = restClient.Execute(restRequest);
            Console.WriteLine("Response {0}", restResponse.Content);
            Console.WriteLine(string.IsNullOrEmpty(restResponse.Content));

        }

        [Then(@"I should receive Token and Required (.*)")]
        public void ThenIShouldReceiveTokenAndRequired(int statusCode)
        {
            actualResponse = JObject.Parse(restResponse.Content);
            int actualStatusCode = (int)restResponse.StatusCode;
            Console.WriteLine($"Response Received {actualResponse}");

            // Verify the request returned expected status code
            Assert.That(actualStatusCode, Is.EqualTo(statusCode), $"Status{actualStatusCode} not as Expected {statusCode}");
            Console.WriteLine($"Expected status code: {statusCode} is same as Actual Status code: {actualStatusCode}");

            if (statusCode == 200)
            {
                //Verify token is present in response
                string token = actualResponse["token"].ToString();
                Console.WriteLine($"Token received {token}");
                Assert.IsTrue(!string.IsNullOrEmpty(token));

            }

        }

    }
}
