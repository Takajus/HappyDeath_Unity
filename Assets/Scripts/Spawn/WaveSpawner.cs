using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [Serializable]
    public class Wave
    {
        public string name;
        public GameObject npc;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;


    public float timeBetweenWaves = 3f;
    private float waveCountdown;

    private float searchCountdown  = 1f;

    private bool isDay = false;
    private SpawnState state = SpawnState.COUNTING;
    private GameObject missy = null;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;

        DayCycleEvents.OnDayStart += IsDay;
        DayCycleEvents.OnNightStart += IsNight;

    }

    private void Update()
    {
        if (isDay)
        {
            SpawnUpdat();
        }
        else
        {
            if (missy)
            {
                missy.SetActive(false);
               
            }
        }
        
    }

    private void IsDay()
    {
        isDay = false;
    }

    private void IsNight()
    {
        isDay = true;
    }



    void SpawnUpdat()
    {
        if (state == SpawnState.WAITING)
        {
            // check if time didn't finish 
            //TODO : Faire en sorte que le ENUME state repasse en Spawning
            if (NpcIsActive() == false) //!!!
            {
                //Active next NPC 
                MssionCompleted();

                return;
            }
            else
            {
                return;
            }

        }


        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //start spawining
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void MssionCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;

        }
        else
        {
            nextWave++;
        }

    }

    bool NpcIsActive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
           
            
                if (missy != null && !missy.activeSelf)
                {
                    state = SpawnState.SPAWNING;
                }
                return false;
            
        }

        return true;
    }

    IEnumerator SpawnWave( Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {

            SpawnNpc(_wave.npc);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;

    }


    void SpawnNpc (GameObject _npc)
    {
        //Spawn NPC
        Debug.Log("Spaning NPC" + _npc.name);
        

        Transform _sp = spawnPoints[Random.Range (0, spawnPoints.Length)];

        if (missy == null)
        {
            missy = Instantiate(_npc, _sp.position, _sp.rotation);
           
        }
        else
        {
            missy.SetActive(true);
        }
        Debug.Log("is created");
        
    }

}

