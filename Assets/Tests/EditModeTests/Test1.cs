using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test1
{

    [Test]
    public void Evaluate_PlayerHasStrongerCard_Returns1()
    {
        
        var playerCard = new CardData("FireCard", 5, CardCategory.Fire);
        var aiCard = new CardData("IceCard", 3, CardCategory.Ice);
        int result = RoundEvaluator.Evaluate(playerCard, aiCard);

        Assert.AreEqual(1, result);
    }

    [Test]

    public void ScoreIncrements_Correctly_WhenSetIsWon()
    {
        var gm = new GameManager();
        gm.StartNewMatch(CardCategory.Fire);

        gm.playerSetWins = 1;
        gm.aiSetWins = 0;
        gm.NextSet();

        Assert.AreEqual(2, gm.currentSet);
    }

    [Test]
    public void ResetGame_ClearsProgress()
    {
        var gm = new GameManager();
        gm.StartNewMatch(CardCategory.Fire);
        gm.playerSetWins = 2;
        gm.aiSetWins = 1;
        gm.NextSet();

        gm.ResetGame();

        Assert.AreEqual(0, gm.playerSetWins);
        Assert.AreEqual(0, gm.aiSetWins);
        Assert.AreEqual(1, gm.currentSet);
        Assert.IsEmpty(gm.usedCategories);
    }

    [Test]
    public void GetRemainingCategories_OnlyUnusedCategories()
    {
        var gm = new GameManager();

        gm.StartNewMatch(CardCategory.Shadow);

        var remaining = gm.GetRemainingCategories();

        Assert.IsFalse(remaining.Contains(gm.PlayerDeckCategory));
        Assert.IsFalse(remaining.Contains(gm.AIDeckCategory));
    }

    [Test]
    public void ChooseAIDeck_DoesNotRepeatUsedCategories()
    {
        var gm = new GameManager();
        gm.StartNewMatch(CardCategory.Fire);
        var firstAIDeck = gm.AIDeckCategory;

        gm.NextSet();
        gm.StartNewSet(CardCategory.Ice);
        var secondAIDeck = gm.AIDeckCategory;

        Assert.AreNotEqual(CardCategory.Fire, secondAIDeck);
        Assert.AreNotEqual(firstAIDeck, secondAIDeck);
    }

    [Test]
    public void Evaluate_CategoryBreaksTie()
    {
        var player = new CardData("ShadowCard", 5, CardCategory.Shadow);
        var ai = new CardData("IceCard", 5, CardCategory.Ice);

        GameManager.Instance = new GameManager();
        GameManager.Instance.StartNewMatch(CardCategory.Shadow);

        int result = RoundEvaluator.Evaluate(player, ai);

        Assert.AreEqual(1, result); // Shadow > Ice
    }

    [Test]
    public void GetDeckByCategory_ReturnsValidCards()
    {
        var deck = CardDatabase.GetDeckByCategory(CardCategory.Animal);

        Assert.IsNotNull(deck);
        Assert.IsTrue(deck.Count > 0);
        Assert.IsTrue(deck.All(card => card.category == CardCategory.Animal));
    }

    [Test]
    public void GetRemainingCategories_ReturnsLastCategory()
    {
        var gm = new GameManager();
        gm.RegisterSetChoice(CardCategory.Fire, CardCategory.Ice);
        gm.RegisterSetChoice(CardCategory.Shadow, CardCategory.Light);

        var remaining = gm.GetRemainingCategories();

        Assert.AreEqual(1, remaining.Count);
        Assert.AreEqual(CardCategory.Animal, remaining[0]);
    }

    [Test]
    public void MusicManager_PersistsAndPlaysMusic()
    {
        var musicManager = new GameObject("MusicManager").AddComponent<MusicManager>();
        var audio = musicManager.GetComponent<AudioSource>();

        Assert.IsNotNull(audio);
        Assert.IsTrue(audio.loop);
    }

}

