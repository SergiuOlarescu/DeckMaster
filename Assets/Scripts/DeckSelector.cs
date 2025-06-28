using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckSelector : MonoBehaviour
{
    public CameraManager cameraManager;
    public BattleManager battleManager;

    public Button fireButton;
    public Button iceButton;
    public Button shadowButton;
    public Button lightButton;
    public Button animalButton;

    private Dictionary<CardCategory, Button> categoryButtons;

    private void Start()
    {
        categoryButtons = new Dictionary<CardCategory, Button>()
        {
            { CardCategory.Fire, fireButton },
            { CardCategory.Ice, iceButton },
            { CardCategory.Shadow, shadowButton },
            { CardCategory.Light, lightButton },
            { CardCategory.Animal, animalButton }
        };

        UpdateDeckButtons();
    }

    public void SelectDeck(int categoryIndex)
    {
        CardCategory selected = (CardCategory)categoryIndex;

        if (GameManager.Instance.currentSet == 1)
        {
            GameManager.Instance.StartNewMatch(selected);
        }
        else
        {
            GameManager.Instance.StartNewSet(selected);
        }

        UpdateDeckButtons();
        battleManager.StartBattle();
        cameraManager.GoToBattle();
    }

    public void SelectFinalForcedDeck()
    {
        List<CardCategory> remaining = GameManager.Instance.GetRemainingCategories();
        if (remaining.Count == 1)
        {
            CardCategory forcedCategory = remaining[0];

            Debug.Log("Set 3 - Player forced to use last deck: " + forcedCategory);
            GameManager.Instance.StartNewSet(forcedCategory);

            battleManager.StartBattle();
            cameraManager.GoToBattle();
        }
        else
        {
            Debug.LogError("Final forced deck failed: invalid remaining deck count.");
        }
    }

    public void UpdateDeckButtons()
    {
        List<CardCategory> used = GameManager.Instance.usedCategories;

        foreach (var pair in categoryButtons)
        {
            bool alreadyUsed = used.Contains(pair.Key);
            pair.Value.interactable = !alreadyUsed;
        }
    }
}
