using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTintScript : MonoBehaviour
{
    public SpriteRenderer character;
    public void ChangeColor(ColorStatus status)
    {
        switch (status)
        {
            case ColorStatus.RED:
                character.color = new Color(1F, 0.86F, 0.86F);
                break;
            case ColorStatus.GREEN:
                character.color = new Color(0.86F, 1F, 0.86F);
                break;
            case ColorStatus.BLUE:
                character.color = new Color(0.86F, 0.86F, 1F);
                break;
            case ColorStatus.YELLOW:
                character.color = new Color(1F, 1F, 0.86F);
                break;
            case ColorStatus.MAGENTA:
                character.color = new Color(1F, 0.86F, 1F);
                break;
            case ColorStatus.CYAN:
                character.color = new Color(0.86F, 1F, 1F);
                break;
            default:
                character.color = Color.white;
                break;
        }
    }
}
