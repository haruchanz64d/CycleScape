using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sperm : MonoBehaviour
{
    public delegate void OnSpermCollidedWithEggEventHandler();
    private Rigidbody2D rb;
    private float speed = -5.0f;
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
            SceneManager.LoadScene("CS_Scene_5_Phase_Bad");
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
