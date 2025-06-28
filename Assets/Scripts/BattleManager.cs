using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public BattleUI battleUI;
    public CameraManager cameraManager;
    public DeckSelector deckSelector;

    private List<CardData> playerDeck;
    private List<CardData> aiDeck;

    private AbilityCardData selectedAbility;
    private bool abilityUsed = false;
    private bool abilityArmed = false;

    private AbilityCardData aiAbility;
    private bool aiAbilityUsed = false;

    private int playerPoints = 0;
    private int aiPoints = 0;

    public void StartBattle()
    {
        playerDeck = CardDatabase.GetDeckByCategory(GameManager.Instance.PlayerDeckCategory);
        aiDeck = CardDatabase.GetDeckByCategory(GameManager.Instance.AIDeckCategory);

        selectedAbility = CardDatabase.GetRandomAbilityForCategory(GameManager.Instance.PlayerDeckCategory, 1)[0];
        abilityUsed = false;
        abilityArmed = false;

        aiAbility = CardDatabase.GetRandomAbilityForCategory(GameManager.Instance.AIDeckCategory, 1)[0];
        aiAbilityUsed = false;

        playerPoints = 0;
        aiPoints = 0;

        battleUI.ShowPlayerHand(playerDeck, this);
        battleUI.ShowAbilities(new List<AbilityCardData> { selectedAbility }, this);
        battleUI.UpdateScore(playerPoints, aiPoints, GameManager.Instance.currentSet);
    }

    public void SelectAbility(AbilityCardData ability)
    {
        if (abilityUsed || abilityArmed)
        {
            Debug.Log("Abilitatea a fost deja folosita.");
            return;
        }

        abilityArmed = true;
        Debug.Log("Abilitate folosita: " + ability.name);
        battleUI.DisableAbilityButton(); 
    }

    public void PlayCard(CardData playerCard)
    {
        CardData aiCard = GetRandomCardFromDeck(aiDeck);
        aiDeck.Remove(aiCard);

        int playerTotal = playerCard.power;
        AbilityCardData usedPlayerAbility = null;

        if (abilityArmed && !abilityUsed)
        {
            playerTotal += selectedAbility.powerBoost;
            usedPlayerAbility = selectedAbility;
            abilityUsed = true;
            abilityArmed = false;
            battleUI.HideAbilities();
        }

        int aiTotal = aiCard.power;
        AbilityCardData usedAIAbility = null;

        if (!aiAbilityUsed && Random.value < 0.5f)
        {
            aiTotal += aiAbility.powerBoost;
            usedAIAbility = aiAbility;
            aiAbilityUsed = true;
        }

        battleUI.ShowPlayedCards(playerCard, usedPlayerAbility, aiCard, usedAIAbility, playerTotal, aiTotal);

        int result = RoundEvaluator.Evaluate(playerCard, aiCard);

        if (result == 1) playerPoints++;
        else aiPoints++;

        battleUI.UpdateScore(playerPoints, aiPoints, GameManager.Instance.currentSet);

        playerDeck.Remove(playerCard);
        battleUI.ShowPlayerHand(playerDeck, this);

        CheckSetEnd();
    }

    private void CheckSetEnd()
    {
        if (playerPoints >= 3 || aiPoints >= 3)
        {
            if (playerPoints > aiPoints) GameManager.Instance.playerSetWins++;
            else GameManager.Instance.aiSetWins++;

            if (GameManager.Instance.playerSetWins == 2 || GameManager.Instance.aiSetWins == 2 || GameManager.Instance.IsSet3)
            {
                battleUI.ShowMatchResult(
                    GameManager.Instance.playerSetWins > GameManager.Instance.aiSetWins
                        ? "Player Wins the Match!"
                        : "AI Wins the Match!");
            }
            else
            {
                GameManager.Instance.NextSet();
                GoToNextSet();
            }
        }
    }

    private void GoToNextSet()
    {
        playerPoints = 0;
        aiPoints = 0;

        if (GameManager.Instance.UsedDecks == 4)
        {
            deckSelector.SelectFinalForcedDeck();
        }
        else
        {
            cameraManager.GoToDeckSelect();
        }
    }

    private CardData GetRandomCardFromDeck(List<CardData> deck)
    {
        return deck[Random.Range(0, deck.Count)];
    }
}
