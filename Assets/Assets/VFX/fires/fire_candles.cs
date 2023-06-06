using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_candles : MonoBehaviour
{
    public List<GameObject> fires;
    private GameObject currentFire;
    public Vector3 offSet;
    private GameObject goSpawner;
    private bool isDay = false;


    // Start is called before the first frame update
    void Start()
    {
        SpawnNewFire();
        DayCycleEvents.OnDayStart += IsDay;
        DayCycleEvents.OnNightStart += IsNight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SpawnNewFire()
	{
        DestroyCurrentFire();
        if (isDay)
        {
            int nombreChoisi = Random.Range(1, fires.Count);
            currentFire = Instantiate(fires[nombreChoisi], transform.position + offSet, Quaternion.identity, transform);
        }
        if (!isDay)
        {
            currentFire = Instantiate(fires[1], transform.position + offSet, Quaternion.identity, transform);
        }
        
    }    


    public void DestroyCurrentFire()
    {
        if (currentFire)
            Destroy(currentFire);
    }

    private void IsDay()
    {
        isDay = false;
        //changement de color jour 
    }

    private void IsNight()
    {
        isDay = true;
        //changement de color nuit
    }

}
