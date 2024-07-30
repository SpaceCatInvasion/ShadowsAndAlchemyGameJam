using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSkills : MonoBehaviour
{
    public InputActionReference skill;
    public CharacterMovement movement;
    public float blueDashSpeed;
    public float blueDashDist;
    public ParticleSystem blueDash;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsCollider;
    private Dictionary<ColorStatus, int> _manaCosts = new Dictionary<ColorStatus, int>();
    public GameObject RedPrefab;
    public GameObject GreenPrefab;
    public GameObject YellowPrefab;
    private bool continueAttack = true;
    public float yellowAttackRate;
    public float yellowSpeed;
    private void Start()
    {
        _manaCosts.Clear();
        _manaCosts.Add(ColorStatus.BLUE, 20);
        _manaCosts.Add(ColorStatus.RED, 40);
        _manaCosts.Add(ColorStatus.GREEN, 80);
        _manaCosts.Add(ColorStatus.YELLOW, 2);
    }
    private void OnEnable()
    {
        skill.action.started += UseSkill;
        skill.action.canceled += CancelAttack;
        blueDash.Stop();
        rb = GetComponent<Rigidbody2D>();
        capsCollider = GetComponent<CapsuleCollider2D>();
    }
    private void OnDisable()
    {
        skill.action.started -= UseSkill;
    }
    public void CancelAttack(InputAction.CallbackContext context)
    {
        continueAttack = false;
    }
    public void UseSkill(InputAction.CallbackContext context)
    {
        continueAttack = true;
        switch (CharacterColorScript.instance.GetColorStatus())
        {
            case ColorStatus.BLUE:
                UseBlueAttack();
                break;
            case ColorStatus.RED:
                UseRedAttack();
                break;
            case ColorStatus.GREEN:
                UseGreenAttack();
                break;
            case ColorStatus.YELLOW:
                UseYellowAttack();
                break;

        }
    }
    private bool EnoughMana(int manaCost)
    {
        return ManaManager.instance != null && ManaManager.instance.GetMana() >= manaCost;
    }
    private void UseBlueAttack()
    {
        if (EnoughMana(_manaCosts[ColorStatus.BLUE]))
        {
            Vector2 dashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            dashDir = dashDir.normalized;
            movement.disable = true;
            rb.velocity = dashDir * blueDashSpeed;
            blueDash.Play();
            capsCollider.excludeLayers += LayerMask.GetMask("Enemy");
            StartCoroutine(BlueMovementEnable());
            ManaManager.instance.UseMana(_manaCosts[ColorStatus.BLUE]);
        }
    }

    private IEnumerator BlueMovementEnable()
    {
        yield return new WaitForSeconds(blueDashDist);
        movement.disable = false;
        blueDash.Stop();
        capsCollider.excludeLayers -= LayerMask.GetMask("Enemy");
    }
    private void UseRedAttack()
    {
        if (EnoughMana(_manaCosts[ColorStatus.RED]))
        {
            Vector2 attackDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            attackDir = attackDir.normalized;
            float angle = Mathf.Rad2Deg * Mathf.Atan(attackDir.y / attackDir.x) + 90;
            if (attackDir.x < 0) angle += 180;
            angle %= 360;
            Instantiate(RedPrefab, (attackDir * 0.75F), Quaternion.Euler(new Vector3(0, 0, angle))).transform.SetParent(transform, false);
            ManaManager.instance.UseMana(_manaCosts[ColorStatus.RED]);
        }
    }
    private void UseGreenAttack()
    {
        if (EnoughMana(_manaCosts[ColorStatus.GREEN]))
        {
            Vector2 attackDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            attackDir = attackDir.normalized;
            Instantiate(GreenPrefab, (attackDir * 2.5F), Quaternion.Euler(new Vector3(0, 0, 0))).transform.SetParent(transform, false);
            ManaManager.instance.UseMana(_manaCosts[ColorStatus.GREEN]);
        }
    }
    private void UseYellowAttack()
    {
        if (EnoughMana(_manaCosts[ColorStatus.YELLOW]))
        {
            StartCoroutine(ContinuousYellow());
        }
    }
    private IEnumerator ContinuousYellow()
    {
        Vector2 attackDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
        attackDir = attackDir.normalized;
        float angle = Mathf.Rad2Deg * Mathf.Atan(attackDir.y / attackDir.x) + 90;
        if (attackDir.x < 0) angle += 180;
        angle %= 360;
        Rigidbody2D rb = Instantiate(YellowPrefab, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<Rigidbody2D>();
        rb.velocity = attackDir * yellowSpeed;
        ManaManager.instance.UseMana(_manaCosts[ColorStatus.YELLOW]);
        yield return new WaitForSeconds(yellowAttackRate);
        if(continueAttack && EnoughMana(_manaCosts[ColorStatus.YELLOW])) StartCoroutine(ContinuousYellow());
    }
}
