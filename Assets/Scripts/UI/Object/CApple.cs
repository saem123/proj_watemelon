using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CApple : MonoBehaviour
{
    public int number;
    public TextMeshProUGUI numberText;
    public Image appleImage;
    public Sprite normalSprite;
    public Sprite selectedSprite;
    private bool isSelected = false;

    public void SetNumber(int value)
    {
        number = value;
        numberText.text = value.ToString();
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        appleImage.sprite = selected ? selectedSprite : normalSprite;
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void DestroyApple()
    {
        Destroy(gameObject);
    }
}
