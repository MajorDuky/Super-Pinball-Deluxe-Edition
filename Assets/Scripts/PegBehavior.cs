using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBehavior : MonoBehaviour
{
    [SerializeField] private Animator pegAnimator;
    [SerializeField] public int scoreAmount;
    [SerializeField] private AudioClip ping;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ball"))
        {
            pegAnimator.SetTrigger("bumpBall");
            gameManager.audioSource.PlayOneShot(ping);
        }
    }
}
