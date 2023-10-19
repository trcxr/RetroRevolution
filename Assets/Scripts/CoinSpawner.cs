using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    int i;
    int rand;
    public GameObject coin;
    public float pos = 0;
    bool shouldSpawn;
    public float wait1;
    public float wait2;
    public Collider2D[] colliders;
    public float radius;

    public GameObject Coin1;
    public GameObject Coin2;
    public GameObject Coin3;
    public GameObject Coin4;

    // Use this for initialization
    void Start()
    {


        StartSpawn();


    }



    public void StartSpawn()
    {

        shouldSpawn = true;

        StartCoroutine(SpawnCoin());



    }

    public void StopSpawning()
    {

        shouldSpawn = false;




    }

    public IEnumerator SpawnCoin()
    {
        while (shouldSpawn)
        {
            GameObject coinObj;

            Vector3 SpawnPos = new Vector3(0, 0, 0);

            bool canSpawnHere = PlatformSpawner.instance.coinBool;

            float SpawnPosX = Random.Range(2f, -2f);

            int safetyNet = 0;

            i = Random.Range(1, 5);

            while (canSpawnHere) {
                Debug.Log("Spawning Coin");

                SpawnPos = new Vector3(SpawnPosX, transform.position.y, 0);


                if (!canSpawnHere) {

                    break;
                }

                if (safetyNet > 50) {

                    break;

                }
                safetyNet++;
            }


            if (i == 1)
            {

                coinObj = Coin1;
            }
            else if (i == 2)
            {

                coinObj = Coin2;
            }
            else if (i == 3)
            {

                coinObj = Coin3;
            }

            else {
                coinObj = Coin4;

            }



            GameObject newCoin = Instantiate(coinObj, SpawnPos, Quaternion.identity) as GameObject;
        
            yield return new WaitForSeconds(Random.Range(wait1, wait2));

        }
    }

   bool PreventSpawnOverLap(Vector3 SpawnPos) {

        colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int j = 0; j < colliders.Length; j++)

        {
            Vector3 centerPoint = colliders[j].bounds.center;
            float width = colliders[j].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.y + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (SpawnPos.x >= leftExtent && SpawnPos.x <= rightExtent) {
                if (SpawnPos.y >= lowerExtent && SpawnPos.y <= upperExtent) {

                    return false;
                }

            }


            
        }
        return true;

    }


}
