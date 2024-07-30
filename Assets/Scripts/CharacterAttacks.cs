using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttacks : MonoBehaviour
{
    public InputActionReference shoot;
    public GameObject attackPrefab;
    public float projspeed;
    public float projfreq;
    private float projtimer = 0;
    private void Update()
    {
        if (projtimer > 0)
        {
            projtimer -= Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        shoot.action.started += Fire;
    }
    private void OnDisable()
    {
        shoot.action.started -= Fire;
    }
    private void Fire(InputAction.CallbackContext context)
    {
        if (projtimer <= 0)
        {
            Vector2 shootDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            GameObject bullet = Instantiate(attackPrefab, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan(shootDir.y / shootDir.x))));
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir.normalized * projspeed;
            projtimer = projfreq;
        }
    }
}
