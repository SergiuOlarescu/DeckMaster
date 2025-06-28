using System.Collections.Generic;
using UnityEngine;

public static class CardDatabase
{
    private static Dictionary<CardCategory, List<CardData>> baseCards = new Dictionary<CardCategory, List<CardData>>()
    {
        { CardCategory.Fire, new List<CardData> {
            new CardData("Fire Fox", 1,CardCategory.Fire,Resources.Load<Sprite>("Images/FireFoxCard")),
            new CardData("Lil Flame", 2,CardCategory.Fire,Resources.Load<Sprite>("Images/Queen of flames")),
            new CardData("Flame Spirit", 3, CardCategory.Fire,Resources.Load<Sprite>("Images/BurningSpirit")),
            new CardData("Infernal Drake", 4, CardCategory.Fire,Resources.Load<Sprite>("Images/ElDRago")),
            new CardData("Queen of Flames", 5, CardCategory.Fire,Resources.Load<Sprite>("Images/Queen of flames (3)"))
        }},
        { CardCategory.Ice, new List<CardData> {
            new CardData("Galcifawn", 1, CardCategory.Ice,Resources.Load<Sprite>("Images/Glacifawn1")),
            new CardData("Snow Wolf", 2, CardCategory.Ice,Resources.Load<Sprite>("Images/SnowWolf")),
            new CardData("Ice Elf", 3, CardCategory.Ice, Resources.Load < Sprite >("Images/IceElf")),
            new CardData("Mountain Beast", 4, CardCategory.Ice, Resources.Load < Sprite >("Images/MountainBeast")),
            new CardData("Frost King", 5, CardCategory.Ice, Resources.Load < Sprite >("Images/FrostKing1")    )
        }},
        { CardCategory.Shadow, new List<CardData> {
            new CardData("Solider of the dark", 1, CardCategory.Shadow,Resources.Load<Sprite>("Images/SoliderofthedarkCard")),
            new CardData("Nightmare", 2, CardCategory.Shadow, Resources.Load < Sprite >("Images/NightmareCard")),
            new CardData("Dark Knight", 3, CardCategory.Shadow, Resources.Load < Sprite >("Images/DarkKnightCard")),
            new CardData("Shadow Reaper", 4, CardCategory.Shadow, Resources.Load < Sprite >("Images/ShadowReaperCard")),
            new CardData("Shadow Monarch", 5, CardCategory.Shadow, Resources.Load < Sprite >("Images/ShadowMonarchCard"))
        }},
        { CardCategory.Light, new List<CardData> {
            new CardData("Light Arrow", 1, CardCategory.Light, Resources.Load < Sprite >("Images/LightArrowCard")),
            new CardData("Radiant Eagle", 2, CardCategory.Light, Resources.Load < Sprite >("Images/RadiantEagleCard")),
            new CardData("Blessed Knight", 3, CardCategory.Light, Resources.Load < Sprite >("Images/BlessedKnightCard")),
            new CardData("Archangel", 4, CardCategory.Light, Resources.Load < Sprite >("Images/Archangel")),
            new CardData("Divine Empress", 5, CardCategory.Light, Resources.Load < Sprite >("Images/DivineEmpressCard"))
        }},
        { CardCategory.Animal, new List<CardData> {
            new CardData("Ethereal Crane", 1, CardCategory.Animal, Resources.Load < Sprite >("Images/EtherealCraneCard")),
            new CardData("Forest Guardian", 2, CardCategory.Animal, Resources.Load < Sprite >("Images/ForestGuardianCard")),
            new CardData("Stone Wolf", 3, CardCategory.Animal, Resources.Load < Sprite >("Images/StoneWolfCard")),
            new CardData("Heart of Woods", 4, CardCategory.Animal, Resources.Load < Sprite >("Images/HeartofwoodsCard")),
            new CardData("Miracle of Nature", 5, CardCategory.Animal, Resources.Load < Sprite >("Images/MiracleofnatureCard")   )
        }}
    };

    private static Dictionary<CardCategory, List<AbilityCardData>> abilityCards = new Dictionary<CardCategory, List<AbilityCardData>>()
    {
        { CardCategory.Fire, new List<AbilityCardData> {
            new AbilityCardData("Flame Boost", 1,CardCategory.Fire),
            new AbilityCardData("Inferno Surge", 2, CardCategory.Fire),
            new AbilityCardData("Ember Focus", 1, CardCategory.Fire),
        }},
        { CardCategory.Ice, new List<AbilityCardData> {
            new AbilityCardData("Frost Spike", 1, CardCategory.Ice),
            new AbilityCardData("Blizzard Cloak", 2, CardCategory.Ice),
            new AbilityCardData("Chill Grip", 1, CardCategory.Ice),
        }},
        { CardCategory.Shadow, new List<AbilityCardData> {
            new AbilityCardData("Dark Pulse", 1, CardCategory.Shadow),
            new AbilityCardData("Shadow Cloak", 2, CardCategory.Shadow),
            new AbilityCardData("Soul Drain", 1, CardCategory.Shadow),
        }},
        { CardCategory.Light, new List<AbilityCardData> {
            new AbilityCardData("Holy Ray", 1, CardCategory.Light),
            new AbilityCardData("Divine Blessing", 2, CardCategory.Light),
            new AbilityCardData("Purity Aura", 1, CardCategory.Light),
        }},
        { CardCategory.Animal, new List<AbilityCardData> {
            new AbilityCardData("Nature's Gift", 1, CardCategory.Animal),
            new AbilityCardData("Alpha Howl", 2, CardCategory.Animal),
            new AbilityCardData("Beast Bond", 1, CardCategory.Animal),
        }},
    };

    public static List<CardData> GetDeckByCategory(CardCategory category)
    {
        if (baseCards.ContainsKey(category))
            return new List<CardData>(baseCards[category]);

        Debug.LogWarning("Categorie necunoscuta: " + category);
        return new List<CardData>();
    }

    public static List<AbilityCardData> GetRandomAbilityForCategory(CardCategory category, int count)
    {
        if (!abilityCards.ContainsKey(category))
        {
            Debug.LogWarning("Categorie de abilitati necunoscuta: " + category);
            return new List<AbilityCardData>();
        }

        List<AbilityCardData> source = new List<AbilityCardData>(abilityCards[category]);
        List<AbilityCardData> result = new List<AbilityCardData>();

        for (int i = 0; i < count && source.Count > 0; i++)
        {
            int index = Random.Range(0, source.Count);
            result.Add(source[index]);
            source.RemoveAt(index); 
        }

        return result;
    }

    public static List<CardData> GetAllBaseCards()
    {
        List<CardData> all = new List<CardData>();
        foreach (var pair in baseCards)
        {
            all.AddRange(pair.Value);
        }
        return all;
    }

    public static List<AbilityCardData> GetAllAbilityCards()
    {
        List<AbilityCardData> all = new List<AbilityCardData>();
        foreach (var pair in abilityCards)
        {
            all.AddRange(pair.Value);
        }
        return all;
    }
}
