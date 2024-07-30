using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 _moveDir;
    public InputActionReference move;
    public bool disable = false;
    
    void Update()
    {
        _moveDir = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!disable)
        {
            rb.velocity = new Vector2(_moveDir.x * moveSpeed, _moveDir.y * moveSpeed);
        }
    }

}
