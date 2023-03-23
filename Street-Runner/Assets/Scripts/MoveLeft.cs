using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    public float leftBound;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.doubleSpeed && playerControllerScript.isOnGround == true)
            {
                transform.Translate(Vector3.left  * Time.deltaTime * (speed * 2));
            }
            else
            {
                transform.Translate(Vector3.left  * Time.deltaTime * speed);
            }

        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obsticle"))
        {
           
            Destroy(gameObject);
           
        }


    }


}
