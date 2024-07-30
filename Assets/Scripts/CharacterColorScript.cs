using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorStatus
{
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
    private CharacterTintScript _tint;
    public Color _currentColor;
    public float colorBaseline;
    public float whiteColorBaseLine;
    private ColorStatus _colorStatus;
    public static CharacterColorScript instance;
    
    // Start is called before the first frame update
    void Start()
    {
        _shadow = GetComponent<CharacterShadow>();
        _tint = GetComponent<CharacterTintScript>();
        _colorStatus = ColorStatus.GRAYSCALE;
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColorStatus();
        _shadow.ChangeColor(_currentColor);
        _tint.ChangeColor(_colorStatus);
    }

    private void UpdateColorStatus()
    {
        float r = _currentColor.r;
        float g = _currentColor.g;
        float b = _currentColor.b;
        if (AmtOver() == 0)
        {
            //black
            _colorStatus = ColorStatus.GRAYSCALE;
        }
        else if (r > whiteColorBaseLine && g > whiteColorBaseLine && b > whiteColorBaseLine)
        {
            //white
            _colorStatus = ColorStatus.WHITE;
        }
        else if (IsFar(r, g, b, 2) && AmtOver() >= 1)
        {
            //red
            _colorStatus = ColorStatus.RED;
        }
        else if (IsFar(g, r, b, 2) && AmtOver() >= 1)
        {
            //green
            _colorStatus = ColorStatus.GREEN;
        }
        else if (IsFar(b, r, g, 2) && AmtOver() >= 1)
        {
            //blue
            _colorStatus = ColorStatus.BLUE;
        }
        else if (IsClose(r, g, 0.6F) && IsFar(r, b, 2F) && AmtOver() >= 2)
        {
            //yellow
            _colorStatus = ColorStatus.YELLOW;
        }
        else if (IsClose(b, g, 0.6F) && IsFar(b, r, 2F) && AmtOver() >= 2)
        {
            //cyan
            _colorStatus = ColorStatus.CYAN;
        }
        else if (IsClose(r, b, 0.6F) && IsFar(r, g, 2F) && AmtOver() >= 2)
        {
            //magenta
            _colorStatus = ColorStatus.MAGENTA;
        }
        else
        {
            //gray
            _colorStatus = ColorStatus.GRAYSCALE;
        }
    }

    private bool IsClose(float val1, float val2, float ratio)
    {
        return (val1/val2 > ratio) && (val2/val1 > ratio);
    }
    private bool IsFar(float large, float small, float ratio)
    {
        return (large / small > ratio);
    }
    private bool IsFar(float large,float small1,float small2, float ratio)
    {
        return (large / small1 > ratio) && (large / small2 > ratio);
    }
    private int AmtOver()
    {
        int amt = 0;
        if (_currentColor.r > colorBaseline) amt++;
        if (_currentColor.g > colorBaseline) amt++;
        if (_currentColor.b > colorBaseline) amt++;
        return amt;
    }
    public ColorStatus GetColorStatus()
    {
        return _colorStatus;
    }
}
