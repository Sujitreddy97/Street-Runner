using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public GameObject[] obsticlePrefab;
    [SerializeField]
    private float startDelay;
    
    private float repeatRate;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        repeatRate = Random.Range(1.4f, 1.8f);
        InvokeRepeating("SpawnObsticles", startDelay, repeatRate);

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

   

    void SpawnObsticles()
    {
        
        Vector3 spawnLocation = new Vector3(30, 0, 0);
        int index = Random.Range(0, obsticlePrefab.Length);

        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obsticlePrefab[index], spawnLocation, obsticlePrefab[index].transform.rotation);
        }
       
    }
}
