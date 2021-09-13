# DeckOfCardsAPI
Testing for the Deck of Cards API at http://deckofcardsapi.com/

Requirements
------------

.NET Framework 4.8 (Though should work with previous versions of framework)
TestCentric (nunit should work, but it appears they split the nunit gui off to a new project found at http://test-centric.org/testcentric-gui/GettingStarted/installation)

Installation from Code
----------------------

1. From the command line run:

```
git clone https://github.com/john-p-meyer/DeckOfCardsAPI.git
```

2. Open DeckOfCardsAPI.sln in Visual Studio
3. Click Build -> Build Solution
4. Either run tests from Visual Studio or use testcentric or nunit with the created DeckOfCardsAPI.Tests.dll
```
testcentric DeckOfCardsAPI.Test.dll
```

Methodologies
-------------

Normally I would have used .NET Core/.NET 5.0 for this, but after some investigation, found that nunit (the gui runner) did not appear to work with .NET Core/.NET 5.0
so .NET Framework 4.8 was used. The TestCentric gui does work with .NET Core/.NET 5.0 so could be used instead.

The solution is broken down into three parts: Tests, Response Structures, and Test Data.

The Response Structures (DeckOfCardsAPI.ResponseClasses) contains all the response structures that were run into during development.

The Test Data (DeckOfCardsAPI.TestData) contains any test data that was duplicated between multiple test classes.

The actual Tests (DeckOfCardsAPI.Tests) are contained here. For testing the New Deck functionality, since it could be done through both a GET and a POST,
and since testing POST was specifically called out, they were split into two test fixtures. With this API it can be argued whether having it all in one 
test fixture (using an extra parameter to determine whether to call GET or POST) or the way I did it is better. While there is code duplication this way,
the test code itself is cleaner.

As for the test cases themselves, because of the finite number of potentials (e.g. max 20 decks are allowed to be used), it would be possible to test all
the outcomes, I chose to only do a smaller range because while these have a small number of potentials, because of run time constraints, I would not do this 
on a much larger number of potentials.

In addition, I opted to test the full response to make sure it was correctly returned rather than just the smallest part that would change (e.g. when drawing
a card, a minimal test would check that cards were returned and that the remaining number of cards is now correct), so error messages were added to make it 
easier to see what went wrong should a test fail.

There is also a config.json file so you can change where the tests are pointing to for multiple environments.
