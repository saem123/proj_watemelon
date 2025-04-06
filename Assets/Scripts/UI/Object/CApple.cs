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

    public Color selectedTextColor = new Color(0.24f, 0.04f, 0.03f); // #3e0a08
    public Color normalTextColor = Color.white;

    public void SetNumber(int value)
    {
        number = value;
        numberText.text = value.ToString();
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        appleImage.sprite = selected ? selectedSprite : normalSprite;
        numberText.color = selected ? selectedTextColor : normalTextColor;
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
