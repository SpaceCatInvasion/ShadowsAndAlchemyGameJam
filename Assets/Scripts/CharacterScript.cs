using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Color _currentEffect;
    private CharacterShadow _shadow;
    // Start is called before the first frame update
    void Start()
    {
        _shadow = GetComponent<CharacterShadow>();
    }

    // Update is called once per frame
    void Update()
    {
        _shadow.ChangeColor(_currentEffect);
    }
}
