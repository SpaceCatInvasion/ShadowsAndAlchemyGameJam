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
    private void Start()
    {
        _manaCosts.Clear();
        _manaCosts.Add(ColorStatus.BLUE, 20);
        _manaCosts.Add(ColorStatus.RED, 40);
    }
    private void OnEnable()
    {
        skill.action.started += UseSkill;
        blueDash.Stop();
        rb = GetComponent<Rigidbody2D>();
        capsCollider = GetComponent<CapsuleCollider2D>();
    }
    private void OnDisable()
    {
        skill.action.started -= UseSkill;
    }
    public void UseSkill(InputAction.CallbackContext context)
    {
        switch (CharacterColorScript.instance.GetColorStatus())
        {
            case ColorStatus.BLUE:
                UseBlueAttack();
                break;
            case ColorStatus.RED:
                UseRedAttack();
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
            GameObject rattack = Instantiate(RedPrefab, (attackDir * 0.75F), Quaternion.Euler(new Vector3(0, 0, angle)));
            rattack.transform.SetParent(transform, false);
            ManaManager.instance.UseMana(_manaCosts[ColorStatus.RED]);
        }
    }
}
