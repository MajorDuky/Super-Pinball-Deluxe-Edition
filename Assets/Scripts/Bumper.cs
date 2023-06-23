using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private Animator bumperAnimator;
    [SerializeField] private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.name == "LeftBumper" && gameManager.isGameActive)
        {
            bumperAnimator.SetTrigger("flipLeft");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && gameObject.name == "RightBumper" && gameManager.isGameActive)
        {
            bumperAnimator.SetTrigger("flipRight");
        }
    }
}
