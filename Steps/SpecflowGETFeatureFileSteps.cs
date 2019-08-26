using NUnit.Framework;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace UnitTestProject1.Steps
{
    [Binding]
    public class SpecflowGETFeatureFileSteps
    {
        public static RestClient restClient;
        public static IRestResponse restResponse;
        public static RestRequest restRequest;

        [Given(@"I have the (.*) and (.*)")]
        public void GivenIHaveTheURI(string uri, string param)
        {
            restClient = new RestClient(uri);
            restRequest = new RestRequest("api/{param}", Method.GET);
            restRequest.AddUrlSegment("param", param);
            //restRequest.AddQueryParameter("users", "2");
        }

        [When(@"I Execute Request")]
        public void WhenIExecuteRequest()
        {
            restRequest.RequestFormat=DataFormat.Json;
            Console.WriteLine($"Executing Request {restClient.BuildUri(restRequest)}");

            restResponse = restClient.Execute(restRequest);
        }


        [Then(@"I should get (.*)")]
        public void ThenIShouldGet(int statusCode)
        {
            string response = restResponse.Content;
            int a = (int)restResponse.StatusCode;
            Console.WriteLine($"Response Received {response}");

            Assert.That(a, Is.EqualTo(statusCode), $"Status{a} not as Expected {statusCode}");
            if (statusCode == 200)
            {
                Assert.That(response.Contains("page"), "Paging information not available");
                Assert.That(response.Contains("id"), "list of user not available");
            }
        }
       
    }
}
