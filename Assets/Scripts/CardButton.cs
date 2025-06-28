using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButton : MonoBehaviour
{
    private CardData data;
    private BattleManager manager;

    public TMP_Text label;
    public Image cardImage;

    public void Setup(CardData card, BattleManager mng)
    {
        data = card;
        manager = mng;
        label.text = card.name + " (" + card.power + ")";

        if (cardImage != null && card.image != null)
        {
            cardImage.sprite = card.image;
        }

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        manager.PlayCard(data);
    }
}
