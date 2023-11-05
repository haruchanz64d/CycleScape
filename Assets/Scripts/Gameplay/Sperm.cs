using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sperm : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 2.5f;
    private bool isCollidedWithEgg = false;
    public bool IsCollidedWithEgg() { return isCollidedWithEgg; }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {   
        rb.velocity = new Vector2(speed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            isCollidedWithEgg = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
