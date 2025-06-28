using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class BattleUI : MonoBehaviour
{
    [Header("Main UI")]
    public Transform playerHandPanel;
    public GameObject cardButtonPrefab;

    public TMP_Text playerScoreText;
    public TMP_Text aiScoreText;
    public TMP_Text setText;

    [Header("Played Cards")]
    public Transform playerCardSlot;
    public Transform aiCardSlot;
    public GameObject cardVisualPrefab;

    [Header("Abilities")]
    public Transform abilityPanel;
    public GameObject abilityButtonPrefab;
    private GameObject selectedAbilityButton;

    public GameObject endMatchButton;
    public GameObject resultPanel;
    public TMP_Text resultText;
    public Image resultBackground;
    public Sprite victoryBackgroundSprite;
    public Sprite defeatBackgroundSprite;

    private Button abilityBtn;

    public void ShowPlayerHand(List<CardData> cards, BattleManager manager)
    {
        foreach (Transform child in playerHandPanel)
            Destroy(child.gameObject);

        foreach (CardData card in cards)
        {
            GameObject go = Instantiate(cardButtonPrefab, playerHandPanel);
            CardButton button = go.GetComponent<CardButton>();
            button.Setup(card, manager);
        }
    }

    public void ShowAbilities(List<AbilityCardData> abilities, BattleManager manager)
    {
        foreach (Transform child in abilityPanel)
            Destroy(child.gameObject);

        foreach (var ability in abilities)
        {
            GameObject go = Instantiate(abilityButtonPrefab, abilityPanel);
            TMP_Text text = go.GetComponentInChildren<TMP_Text>();
            text.text = $"+{ability.powerBoost} {ability.category}";

            Button btn = go.GetComponent<Button>();
            abilityBtn = btn; 

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                manager.SelectAbility(ability);

                if (selectedAbilityButton != null)
                    ResetButtonColor(selectedAbilityButton);

                selectedAbilityButton = go;
                HighlightButton(go);
            });
        }
    }

    public void DisableAbilityButton()
    {
        if (abilityBtn != null)
        {
            abilityBtn.interactable = false;

            TMP_Text txt = abilityBtn.GetComponentInChildren<TMP_Text>();
            if (txt != null)
                txt.color = Color.gray;

            var colors = abilityBtn.colors;
            colors.normalColor = Color.gray;
            abilityBtn.colors = colors;
        }
    }

    public void HideAbilities()
    {
        foreach (Transform child in abilityPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void RefreshAbilities(List<AbilityCardData> abilities)
    {
        ShowAbilities(abilities, null);
    }

    public void ShowPlayedCards(CardData playerCard, AbilityCardData playerAbility, CardData aiCard, AbilityCardData aiAbility, int playerTotal, int aiTotal)
    {
        foreach (Transform child in playerCardSlot)
            Destroy(child.gameObject);
        foreach (Transform child in aiCardSlot)
            Destroy(child.gameObject);

        GameObject pCard = Instantiate(cardVisualPrefab, playerCardSlot);
        TMP_Text pText = pCard.GetComponentInChildren<TMP_Text>();
        if (pText != null)
        {
            if (playerAbility != null)
                pText.text = $"{playerCard.name} ({playerCard.power}) + {playerAbility.powerBoost} = {playerTotal}";
            else
                pText.text = $"{playerCard.name} = {playerTotal}";
        }

        GameObject aCard = Instantiate(cardVisualPrefab, aiCardSlot);
        TMP_Text aText = aCard.GetComponentInChildren<TMP_Text>();
        if (aText != null)
        {
            if (aiAbility != null)
                aText.text = $"{aiCard.name} ({aiCard.power}) + {aiAbility.powerBoost} = {aiTotal}";
            else
                aText.text = $"{aiCard.name} = {aiTotal}";
        }
    }

    public void UpdateScore(int playerPoints, int aiPoints, int setNumber)
    {
        if (playerScoreText != null) playerScoreText.text = $"Player: {playerPoints}";
        if (aiScoreText != null) aiScoreText.text = $"AI: {aiPoints}";
        if (setText != null) setText.text = $"Set {setNumber}";
    }

    public void ShowMatchResult(string message)
    {
        resultText.text = message;

        if (message.Contains("Player Wins"))
            resultBackground.sprite = victoryBackgroundSprite;
        else
            resultBackground.sprite = defeatBackgroundSprite;

        resultPanel.SetActive(true);
        endMatchButton.SetActive(true);
    }

    public void ResetUI()
    {
        endMatchButton.SetActive(false);
        resultPanel.SetActive(false);
        resultText.text = "";
    }

    private void HighlightButton(GameObject button)
    {
        var btn = button.GetComponent<Button>();
        var colors = btn.colors;
        colors.normalColor = Color.black;
        btn.colors = colors;

        TMP_Text txt = button.GetComponentInChildren<TMP_Text>();
        if (txt != null)
            txt.color = Color.black;
    }

    private void ResetButtonColor(GameObject button)
    {
        var btn = button.GetComponent<Button>();
        var colors = btn.colors;
        colors.normalColor = Color.white;
        btn.colors = colors;

        TMP_Text txt = button.GetComponentInChildren<TMP_Text>();
        if (txt != null)
            txt.color = Color.white;
    }
}
