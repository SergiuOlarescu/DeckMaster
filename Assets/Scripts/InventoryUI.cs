using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform cardContentContainer;      
    public GameObject cardItemPrefab;           

    public void PopulateInventory()
    {
        
        foreach (Transform child in cardContentContainer)
            Destroy(child.gameObject);

        
        List<CardData> baseCards = CardDatabase.GetAllBaseCards();
        List<AbilityCardData> abilities = CardDatabase.GetAllAbilityCards();

       
        foreach (CardData card in baseCards)
        {
            GameObject item = Instantiate(cardItemPrefab, cardContentContainer);
            item.GetComponentInChildren<TMP_Text>().text =
                $"{card.name} | Power: {card.power} | {card.category}";
        }

       
        foreach (AbilityCardData ability in abilities)
        {
            GameObject item = Instantiate(cardItemPrefab, cardContentContainer);
            item.GetComponentInChildren<TMP_Text>().text =
                $"{ability.name} | +{ability.powerBoost} | {ability.category}";
        }
    }
}
