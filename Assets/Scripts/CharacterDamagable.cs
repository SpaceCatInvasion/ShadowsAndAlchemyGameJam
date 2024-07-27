using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamagable : Damagable
{
    public int maxHp;
    private int hp;
    public int defaultImmunityFrames;
    public GameObject characterSprite;
    
    private void Awake()
    {
        hp = maxHp;
    }
    public override void TakeDamage(float damage)
    {
        if (immunityFrames <= 0)
        {
            hp--;
            LivesManager.instance.TakeLife();
            immunityFrames = defaultImmunityFrames;
            StartCoroutine(HitFlashOff());
        }
    }
    public override void Heal(int health)
    {
        hp = Mathf.Min(hp + health, maxHp);
        LivesManager.instance.AddLives(health);
    }
    private void Update()
    {
        if (immunityFrames > 0)
        {
            immunityFrames -= Time.deltaTime;
        }
    }
    private IEnumerator HitFlashOff()
    {
        characterSprite.SetActive(false);
        yield return new WaitForSeconds(0.1F);
        if (immunityFrames > 0)
        {
            StartCoroutine(HitFlashOn());
        }
        else
        {
            characterSprite.SetActive(true);
        }
    }
    private IEnumerator HitFlashOn()
    {
        characterSprite.SetActive(true);
        yield return new WaitForSeconds(0.1F);
        if(immunityFrames > 0)
        {
            StartCoroutine(HitFlashOff());
        }
    }
}
