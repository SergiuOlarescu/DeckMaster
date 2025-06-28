using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    public Image cardImage;
    

    public void Setup(Sprite image)
    {
        cardImage.sprite = image;
        
    }
}
