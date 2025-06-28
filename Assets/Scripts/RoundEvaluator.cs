using UnityEngine;
using System.Collections.Generic;

public static class RoundEvaluator
{
    
    private static Dictionary<CardCategory, List<CardCategory>> categoryBeats = new Dictionary<CardCategory, List<CardCategory>>()
    {
        { CardCategory.Fire,   new List<CardCategory> { CardCategory.Shadow, CardCategory.Animal } },
        { CardCategory.Ice,    new List<CardCategory> { CardCategory.Fire, CardCategory.Light } },
        { CardCategory.Shadow, new List<CardCategory> { CardCategory.Ice, CardCategory.Animal } },
        { CardCategory.Light,  new List<CardCategory> { CardCategory.Shadow, CardCategory.Fire } },
        { CardCategory.Animal, new List<CardCategory> { CardCategory.Ice, CardCategory.Light } }
    };

   
    public static int Evaluate(CardData playerCard, CardData aiCard)
    {
        if (playerCard.power > aiCard.power)
            return 1;
        else if (playerCard.power < aiCard.power)
            return -1;
        else
        {
            
            CardCategory playerCategory = GameManager.Instance.PlayerDeckCategory;
            CardCategory aiCategory = GameManager.Instance.AIDeckCategory;

            if (Beats(playerCategory, aiCategory))
                return 1;
            else if (Beats(aiCategory, playerCategory))
                return -1;
            else
                return Random.Range(0, 2) == 0 ? 1 : -1; 
        }
    }

    private static bool Beats(CardCategory attacker, CardCategory defender)
    {
        return categoryBeats.ContainsKey(attacker) && categoryBeats[attacker].Contains(defender);
    }
}
