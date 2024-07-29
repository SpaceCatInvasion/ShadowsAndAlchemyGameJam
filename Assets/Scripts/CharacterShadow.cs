using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShadow : MonoBehaviour
{
    public SpriteRenderer shadow;
    public void ChangeColor(Color color)
    {
        shadow.color = Darken(color);
    }
    private Color Darken(Color color)
    {
        return new Color(color.r/3, color.g/3, color.b/3);
    }
}
