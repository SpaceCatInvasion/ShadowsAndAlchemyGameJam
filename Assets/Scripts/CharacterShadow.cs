using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShadow : MonoBehaviour
{
    public SpriteRenderer shadow;
    public void ChangeColor(Color color)
    {
        shadow.color = color;
    }
}
