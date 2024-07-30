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
                BlueAttack();
                break;

        }
    }

    private void BlueAttack()
    {
        Vector2 dashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
        dashDir = dashDir.normalized;
        movement.disable = true;
        rb.velocity = dashDir * blueDashSpeed;
        blueDash.Play();
        capsCollider.excludeLayers += LayerMask.GetMask("Enemy");
        StartCoroutine(BlueMovementEnable());
    }

    private IEnumerator BlueMovementEnable()
    {
        yield return new WaitForSeconds(blueDashDist);
        movement.disable = false;
        blueDash.Stop();
        capsCollider.excludeLayers -= LayerMask.GetMask("Enemy");
    }
}
