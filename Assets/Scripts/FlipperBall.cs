using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBall : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 65);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Peg"))
        {
            
            PegBehavior peg = collision.gameObject.GetComponent<PegBehavior>();
            if (peg)
            {
                gameManager.increaseMultiplier();
                gameManager.increaseTempScore(peg.scoreAmount);
            }
        }

        if (collision.gameObject.CompareTag("Bumper"))
        {
            gameManager.AddToScore();
        }
    }
}
