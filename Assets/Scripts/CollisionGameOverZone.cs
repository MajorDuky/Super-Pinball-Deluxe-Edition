using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGameOverZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            gameManager.LoseBall();
        }
    }
}
