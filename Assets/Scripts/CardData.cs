using UnityEngine;

[System.Serializable]
public enum CardCategory
{
    Fire,
    Ice,
    Shadow,
    Light,
    Animal
}

public class CardData
{
    public string name;
    public int power;
    public CardCategory category;
    public Sprite image;

    public CardData(string name, int power, CardCategory category, Sprite image=null)
    {
        this.name = name;
        this.power = power;
        this.category = category;
        this.image = image;
    }
}
