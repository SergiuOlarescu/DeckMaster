using TMPro;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    private AbilityCardData data;
    private BattleManager manager;

    public TMP_Text label;

    public void Setup(AbilityCardData ability, BattleManager mng)
    {
        data = ability;
        manager = mng;
        label.text = $"{data.name} +{data.powerBoost}";
    }

    public void OnClick()
    {
        manager.SelectAbility(data);
    }
}
