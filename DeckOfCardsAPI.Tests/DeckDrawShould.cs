using DeckOfCardsAPI.ResponseClasses;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RestSharp;

namespace DeckOfCardsAPI.Tests
{
    [TestFixture]
    public class DeckDrawShould
    {
        private RestClient client;
        private RestRequest request;
        private NewDeckResponse currentDeck;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("config.json", optional: true).Build();

            client = new RestClient(configuration["configuration:apiUrl"]);
        }

        [SetUp]
        public void SetUp()
        {
            var newDeckRequest = new RestRequest("deck/new/");

            var currentDeckResponse = client.Get<NewDeckResponse>(newDeckRequest);

            currentDeck = currentDeckResponse.Data;

            request = new RestRequest("deck/{deck_id}/draw/");
            request.AddUrlSegment("deck_id", currentDeck.deck_id);
        }

        [Test]
        [Description("When called with no parameters, should return 1 card.")]
        public void ShouldReturnOneCardDefault()
        {
            var sut = client.Get<DrawResponse>(request);

            Assert.That(sut.IsSuccessful, Is.True, "The server did not return OK");
            Assert.That(sut.Data.success, Is.True, "The request was not successful");
            Assert.That(sut.Data.cards, Has.Count.EqualTo(1), "1 card was not returned");
            Assert.That(sut.Data.error, Is.Null, "An Error Message was present");
            Assert.That(sut.Data.deck_id, Is.EqualTo(currentDeck.deck_id), "Response deck_id does not match original deck_id");
            Assert.That(sut.Data.remaining, Is.EqualTo(51), "Deck should have 51 cards remaining");
        }

        [Test]
        [Description("Should return count number of cards and have 52 - count number of cards remaining.")]
        public void ShouldReturnCountCards([Range(0, 52, 13)] int count)
        {
            request.AddParameter("count", count);

            var sut = client.Get<DrawResponse>(request);

            Assert.That(sut.IsSuccessful, Is.True, "The server did not return OK");
            Assert.That(sut.Data.success, Is.True, "The request was not successful");
            Assert.That(sut.Data.cards, Has.Count.EqualTo(count), "Number of cards drawn was incorrect");
            Assert.That(sut.Data.error, Is.Null, "An Error Message was present");
            Assert.That(sut.Data.deck_id, Is.EqualTo(currentDeck.deck_id), "Response deck_id does not match original deck_id");
            Assert.That(sut.Data.remaining, Is.EqualTo(52 - count), "Number of cards remaining is incorrect");
        }

        [Test]
        [Description("When more cards than are in deck is requested, should return remaining deck in cards, set successful to false, and give an error message.")]
        public void ShouldNotAllowMoreCardsThanInDeckDrawn()
        {
            request.AddParameter("count", 53);

            var sut = client.Get<DrawResponse>(request);

            Assert.That(sut.IsSuccessful, Is.True, "The server did not return OK");
            Assert.That(sut.Data.success, Is.False, "Error response should have success as false");
            Assert.That(sut.Data.cards, Has.Count.EqualTo(52), "Did not return remaining cards in deck");
            Assert.That(sut.Data.error, Is.Not.Null, "Error Response did not have error message");
            Assert.That(sut.Data.deck_id, Is.EqualTo(currentDeck.deck_id), "Response deck_id does not match original deck_id");
            Assert.That(sut.Data.remaining, Is.EqualTo(0), "No cards should be left in the deck");
        }

        [TearDown]
        public void TearDown()
        {
            currentDeck = null;
        }
    }
}
