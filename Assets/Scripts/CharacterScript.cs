using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private CharacterShadow _shadow;
    private Color _currentColor;
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
