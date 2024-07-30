using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public static ManaManager instance;
    public Image ManaBar;
    public float regenSpeed;
    public int regenAmt;
    public float regenPause;
    private bool regen = false;
    private int waiting = 0;
    private int maxmana = 100;
    private int _mana = 100;
    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void UpdateManaBar()
    {
        ManaBar.fillAmount = _mana / 100f;
    }
    public float GetMana()
    {
        return _mana;
    }
    public void UseMana(int mana)
    {
        _mana -= mana;
        if (_mana < 0) _mana = 0;
        UpdateManaBar();
        StartCoroutine(StartRegen(regenPause));
    }
    public void GainMana(int mana)
    {
        _mana += mana;
        if (_mana > maxmana) _mana = maxmana;
        UpdateManaBar();
    }
    private IEnumerator StartRegen(float time)
    {
        waiting++;
        regen = false;
        yield return new WaitForSeconds(time);
        if (--waiting == 0)
            regen = true;
        StartCoroutine(RegenMana());
    }
    private IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(regenSpeed);
        
        if (regen && _mana < maxmana)
        {
            GainMana(regenAmt);
            StartCoroutine(RegenMana());
        }
    }
}
