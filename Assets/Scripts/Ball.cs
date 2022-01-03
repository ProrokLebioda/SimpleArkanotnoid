using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10.0f;
    Rigidbody rb;

    Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        rb.velocity = Vector2.up * speed;
    }

    private void DestroyBall()
    {
        GameManager.Instance.Lives--;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!_renderer.isVisible)
        {
            DestroyBall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float x = hitFactor(transform.position,
                                collision.transform.position,
                                collision.collider.bounds.size.x);
            //calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            //set velocity with dir * speed
            rb.velocity = dir * speed;
        }
        else if (collision.gameObject.CompareTag("DangerZone"))
        {
            DestroyBall();
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
        float racketWidth)
    {
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }
}
