using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{


    private float pos;
    private bool shouldSpawn = false;
    public GameObject Magnet;
    public GameObject Heart;
    public GameObject Shield;
    public GameObject Speed;
    public GameObject Time;
    public GameObject Coin;
    public static PowerUpSpawner instance;
    public float destroyTimeout;
    public float posPowerUp;
    [SerializeField]
    float posy=0;

  
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        StartSpawn();
    }

    void Update()
    {


    }

    public void StartSpawn()
    {
        shouldSpawn = true;

        StartCoroutine(SpawnObj());



    }

    public void StopSpawning()
    {

        shouldSpawn = false;




    }


    public IEnumerator SpawnObj()
    {
        while (shouldSpawn) {



            int i = Random.Range(1, 11);
            GameObject objSpawn = null;
            if (PlatformSpawner.instance.coinBool) {
                objSpawn = Coin;
            } 
            else if (PlatformSpawner.instance.powerupBool)
            {
                if (i <= 2)
                {
                    // 20% chance
                    objSpawn = Shield;


                }
                else if (i <= 4)
                {
                    // 20% chance
                    objSpawn = Speed;



                }
                else if (i <= 6)
                {
                    // 20% chance
                    objSpawn = Time;



                }
                else if (i <= 8)
                {
                    // 20% chance
                    objSpawn = Magnet;


                }
                else
                {
                    // 10% chance to health or shield(if max health)
                    if (Health.instance.health < Health.instance.numOfHearts)
                    {
                        objSpawn = Heart;
                    } else {
                        objSpawn = Shield;
                    }
                }
            }

            if(objSpawn != null)
            {
                GameObject powerObj = Instantiate(objSpawn, new Vector3(transform.position.x + PlatformSpawner.instance.posX + posPowerUp, transform.position.y + posy, 0), Quaternion.identity);
                Destroy(powerObj, destroyTimeout);
            }

            yield return new WaitForSeconds(PlatformSpawner.instance.waitTime);

        }
       
    }
}




