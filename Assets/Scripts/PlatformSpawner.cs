using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public bool shouldSpawn = false;
    public static PlatformSpawner instance;
    public float destroyTimeout;
    public int maxSpikedPlatform = 3;

    public GameObject MetalplatformPrefab;
    public GameObject MetalspikedPlatformPrefab;

    public GameObject WoodplatformPrefab;
    public GameObject WoodspikedPlatformPrefab;

    public GameObject StoneplatformPrefab;
    public GameObject StonespikedPlatformPrefab;

    public GameObject SnowplatformPrefab;
    public GameObject SnowspikedPlatformPrefab;

    GameObject SpikedPlatform;
    GameObject Platform;

    public bool powerupBool = false;
    public bool coinBool = false;
    public bool ballPowered = false;
    public bool powerUpSpawned = false;
    public bool easy = true;

    public float waitTime;
 
    public float posX;
 
    
    public int spikedCounter;

    private static List<Coroutine> runningRoutines = new List<Coroutine>();


    //public float destroyTimeout;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        StartCoroutine(EasyGameRoutine());

        if (PlayerPrefs.GetInt("Arena", 1) == 1)
        {

            Platform = MetalplatformPrefab;
            SpikedPlatform = MetalspikedPlatformPrefab;
        

        }


        if (PlayerPrefs.GetInt("Arena") == 2)
        {

             
            Platform = SnowplatformPrefab;
            SpikedPlatform = SnowspikedPlatformPrefab;
          

        }


            if (PlayerPrefs.GetInt("Arena") == 3)
            {

                Platform = WoodplatformPrefab;
                SpikedPlatform = WoodspikedPlatformPrefab;
              
        }


            if (PlayerPrefs.GetInt("Arena") == 4)
            {
                

                    Platform = StoneplatformPrefab;
                    SpikedPlatform = StonespikedPlatformPrefab;
                
            }



        StartSpawn();




    }

    public void StartSpawn() {
      
            shouldSpawn = true;
            StartCoroutine(SpawnObj());
       
    }

    public void StopSpawning() {
        shouldSpawn = false;
        
    }

    public IEnumerator SpawnObj() {
        while (shouldSpawn)
        {



            posX = Random.Range(2.45f, -1.43f);

            GameObject prefab;

            float platformPerc = spikedCounter == maxSpikedPlatform ? 0.1f : Random.value;
            if (!easy && platformPerc > 0.8) {
                prefab = SpikedPlatform;
                powerupBool = false;
                coinBool = false;
                spikedCounter++;
            } else {
                // Make sure that both of them are false initially.
                coinBool = false;
                powerupBool = false;
                if (ballPowered || powerUpSpawned) {
                    coinBool = true;
                } else {
                    float coinPerc = Random.value;
                    //Debug.Log("Coin Percentage : " + coinPerc);
                    if (coinPerc < 0.7) {
                        coinBool = true;
                    } else {
                        powerupBool = true;
                        Coroutine routine = StartCoroutine(PowerupSpawnedRoutine());
                        runningRoutines.Add(routine);
                    }
                }
                prefab = Platform;
                spikedCounter = 0;
            }

            
            

            GameObject obj = Instantiate(prefab, new Vector3(transform.position.x + posX, transform.position.y, 0), Quaternion.identity);

            Destroy(obj, destroyTimeout);

            yield return new WaitForSeconds(waitTime);

        }
    }

    public void PowerupSpawned(float pauseTime = 10.0f) {
        StopAllRunningCoroutines();
        Coroutine routine = StartCoroutine(PowerupSpawnedRoutine(pauseTime));
        runningRoutines.Add(routine);
    }

    private void StopAllRunningCoroutines() {
        foreach (Coroutine routine in runningRoutines) {
            StopCoroutine(routine);
        }
        runningRoutines.Clear();
    }

    public IEnumerator PowerupSpawnedRoutine(float pauseTime = 10.0f) {
        powerUpSpawned = true;
        yield return new WaitForSeconds(pauseTime);
        powerUpSpawned = false;
    }

    IEnumerator PowerupCollectedRoutine(float pauseTime = 10.0f) {
        ballPowered = true;
        yield return new WaitForSeconds(pauseTime);
        ballPowered = false;
    }

    public void PowerupCollected(float pauseTime = 10.0f) {
        StartCoroutine(PowerupCollectedRoutine(pauseTime));
    }

    public IEnumerator EasyGameRoutine() {
        easy = true;
        yield return new WaitForSeconds(12f);
        easy = false;
    }
}