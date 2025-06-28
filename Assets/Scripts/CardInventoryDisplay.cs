using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CardInventoryDisplay : MonoBehaviour
{
    public Transform contentContainer;   
    public GameObject cardPrefab;        

    private void Start()
    {
        LoadCardsFromResources();
    }

    void LoadCardsFromResources()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images");

        foreach (Sprite sprite in sprites)
        {
            GameObject go = Instantiate(cardPrefab, contentContainer);

            var image = go.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
                image.sprite = sprite;

            TMP_Text text = go.GetComponentInChildren<TMP_Text>();
            if (text != null)
                text.text = sprite.name;

        }
    }
}
