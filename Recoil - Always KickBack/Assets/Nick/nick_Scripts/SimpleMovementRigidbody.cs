using UnityEngine;
using System.Collections;

public class SimpleMovementRigidbody : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }

    }
}
