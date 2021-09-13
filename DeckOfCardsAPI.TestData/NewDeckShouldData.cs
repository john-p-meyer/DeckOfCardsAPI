using NUnit.Framework;
using System;
using System.Collections;

namespace DeckOfCardsAPI.TestData
{
    public class NewDeckShouldData
    {
        public static IEnumerable JokerEnabledTestCases
        {
            get
            {
                yield return new TestCaseData("false").Returns(52).SetDescription("A Deck without jokers has 52 cards");
                yield return new TestCaseData("true").Returns(54).SetDescription("A Deck with jokers has 54 cards");
                yield return new TestCaseData("").Returns(52).SetDescription("Blank passed for jokers_enabled is treated as default deck");
                yield return new TestCaseData("yes").Returns(52).SetDescription("Other values passed for jokers_enabled is treated as default deck");
            }
        }

        public static IEnumerable DeckCountJokerEnabledTestCases
        {
            get
            {
                yield return new TestCaseData("1", "false", 52).SetDescription("A single deck without jokers");
                yield return new TestCaseData("1", "true", 54).SetDescription("A single deck with jokers");
                yield return new TestCaseData("2", "false", 104).SetDescription("Two decks without jokers");
                yield return new TestCaseData("2", "true", 108).SetDescription("Two decks with jokers");
                yield return new TestCaseData("5", "false", 260).SetDescription("Five decks without jokers");
                yield return new TestCaseData("5", "true", 270).SetDescription("Five decks with jokers");
                yield return new TestCaseData("10", "false", 520).SetDescription("Ten decks without jokers");
                yield return new TestCaseData("10", "true", 540).SetDescription("Ten decks with jokers");
                yield return new TestCaseData("15", "false", 780).SetDescription("Fifteen decks without jokers");
                yield return new TestCaseData("15", "true", 810).SetDescription("Fifteen decks with jokers");
                yield return new TestCaseData("20", "false", 1040).SetDescription("Twenty decks with jokers");
                yield return new TestCaseData("20", "true", 1080).SetDescription("Twenty decks with jokers");
            }
        }
    }
}
