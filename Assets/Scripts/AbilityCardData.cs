[System.Serializable]
public class AbilityCardData
{
    public string name;
    public int powerBoost;
    public CardCategory category;

    public AbilityCardData(string name, int powerBoost, CardCategory category)
    {
        this.name = name;
        this.powerBoost = powerBoost;
        this.category = category;
    }
}
