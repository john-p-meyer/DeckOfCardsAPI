using DeckOfCardsAPI.ResponseClasses;
using DeckOfCardsAPI.TestData;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RestSharp;

namespace DeckOfCardsAPI.Tests
{
    [TestFixture]
    public class NewDeckGetShould
    {
        private RestClient client;
        private RestRequest request;        

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("config.json", optional: true).Build();

            client = new RestClient(configuration["configuration:apiUrl"]);
        }

        [SetUp]
        public void SetUp()
        {
            request = new RestRequest("deck/new/");
        }

        [Test]        
        [Description("A single default deck created with 52 cards")]
        public void CreateDefaultDeckSuccessfully()
        {
            var sut = client.Get<NewDeckResponse>(request);

            Assert.That(sut.IsSuccessful, Is.True, "The server did not return OK");
            Assert.That(sut.Data.success, Is.True, "The request was not successful");
            Assert.That(sut.Data.shuffled, Is.False, "The shuffled flag was incorrect");
            Assert.That(sut.Data.error, Is.Null, "An Error Message was present");
            Assert.That(sut.Data.deck_id, Is.Not.Null, "deck_id was not returned");
            Assert.That(sut.Data.remaining, Is.EqualTo(52), "New single deck without jokers should have 52 cards remaining");
        }

        [Test]
        [TestCaseSource(typeof(NewDeckShouldData), "JokerEnabledTestCases")]
        public int HaveCorrectCardCountWhenjokers_enabledPassed(string jokers_enabled)
        {
            request.AddParameter("jokers_enabled", jokers_enabled);
            var sut = client.Get<NewDeckResponse>(request);

            return sut.Data.remaining;
        }

        [Test]
        [Description("Checking to make sure that decks are created when deck_count and jokers_enabled are passed")]
        [TestCaseSource(typeof(NewDeckShouldData), "DeckCountJokerEnabledTestCases")]
        public void HaveCorrectCardCountMultipleDecksjokers_enabledPassed(string deck_count, string jokers_enabled, int expectedResult)
        {
            request.AddParameter("deck_count", deck_count);
            request.AddParameter("jokers_enabled", jokers_enabled);
            var sut = client.Get<NewDeckResponse>(request);

            Assert.That(sut.IsSuccessful, Is.True, "The server did not return OK");
            Assert.That(sut.Data.success, Is.True, "The request was not successful");
            Assert.That(sut.Data.shuffled, Is.False, "The shuffled flag was incorrect");
            Assert.That(sut.Data.error, Is.Null, "An Error Message was present");
            Assert.That(sut.Data.deck_id, Is.Not.Null, "deck_id was not returned");
            Assert.That(sut.Data.remaining, Is.EqualTo(expectedResult), "Incorrect number of cards remaining");
        }

        [Test]
        [Description("More than 20 decks can not be used")]
        public void NotCreateWhenMoreThan20DecksUsed()
        {
            request.AddParameter("deck_count", 21);
            var sut = client.Get<NewDeckResponse>(request);

            Assert.That(sut.IsSuccessful, Is.True, "The server did not return OK");
            Assert.That(sut.Data.success, Is.False, "Error response should have success as false");            
            Assert.That(sut.Data.error, Is.Not.Null, "Error Response did not have error message");
            Assert.That(sut.Data.deck_id, Is.Null, "No deck_id should be returned when unsuccessfully created");            
        }
    }
}
