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
    public InputActionReference fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveDir = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveDir.x*moveSpeed, _moveDir.y*moveSpeed);
    }

    private void OnEnable()
    {
        fire.action.started += Fire;
    }
    private void OnDisable()
    {
        fire.action.started -= Fire;
    }
    private void Fire(InputAction.CallbackContext obj)
    {
        Debug.Log("Firing");
    }
}
