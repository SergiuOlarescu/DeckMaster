using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private CardCategory playerDeckCategory;
    private CardCategory aiDeckCategory;

    public int playerSetWins = 0;
    public int aiSetWins = 0;
    public int currentSet = 1;

    public List<CardCategory> usedCategories = new List<CardCategory>();

    public CardCategory PlayerDeckCategory => playerDeckCategory;
    public CardCategory AIDeckCategory => aiDeckCategory;

    public bool IsSet3 => currentSet == 3;
    public int UsedDecks => usedCategories.Count;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void StartNewMatch(CardCategory firstPlayerDeck)
    {
        playerSetWins = 0;
        aiSetWins = 0;
        currentSet = 1;
        usedCategories.Clear();

        StartNewSet(firstPlayerDeck);
    }

    
    public void StartNewSet(CardCategory playerChoice)
    {
        playerDeckCategory = playerChoice;
        aiDeckCategory = ChooseAIDeck(playerChoice);

        usedCategories.Add(playerDeckCategory);
        usedCategories.Add(aiDeckCategory);

        Debug.Log($"Set {currentSet} Start: Player = {playerDeckCategory}, AI = {aiDeckCategory}");
    }

    public void RegisterSetChoice(CardCategory playerChoice, CardCategory aiChoice)
    {
        usedCategories.Add(playerChoice);
        usedCategories.Add(aiChoice);
    }

    private CardCategory ChooseAIDeck(CardCategory excluded)
    {
        List<CardCategory> available = new List<CardCategory>((CardCategory[])System.Enum.GetValues(typeof(CardCategory)));
        available.Remove(excluded);
        foreach (var used in usedCategories)
            available.Remove(used);

        if (available.Count == 0)
            return excluded; // fallback

        return available[Random.Range(0, available.Count)];
    }

    public List<CardCategory> GetRemainingCategories()
    {
        List<CardCategory> all = new List<CardCategory>((CardCategory[])System.Enum.GetValues(typeof(CardCategory)));
        foreach (var used in usedCategories)
            all.Remove(used);
        return all;
    }

    public void NextSet()
    {
        currentSet++;
    }
    public void ResetGame()
    {
        playerSetWins = 0;
        aiSetWins = 0;
        currentSet = 1;
        usedCategories.Clear();

        Debug.Log("Game has been reset.");
    }

}
