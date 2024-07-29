using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorStatus
{
    BLACK,
    RED,
    GREEN,
    BLUE,
    CYAN,
    MAGENTA,
    YELLOW,
    GRAYSCALE,
    WHITE
}
public class CharacterColorScript : MonoBehaviour
{
    private CharacterShadow _shadow;
    public Color _currentColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _shadow = GetComponent<CharacterShadow>();
        _currentColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        _shadow.ChangeColor(_currentColor);
    }
}
