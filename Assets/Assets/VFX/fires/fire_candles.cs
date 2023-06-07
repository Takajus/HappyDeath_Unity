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
		SpawnNewFire(false);
		DayCycleEvents.OnDayStart += IsDay;
		DayCycleEvents.OnNightStart += IsNight;
	}

	// Update is called once per frame

	public void SpawnNewFire(bool isDay)
	{
		DestroyCurrentFire();

		if (isDay)
		{
		currentFire = Instantiate(fires[0], transform.position + offSet, Quaternion.identity, transform);
			return;
		}
		int nombreChoisi = Random.Range(0, fires.Count);
		currentFire = Instantiate(fires[nombreChoisi], transform.position + offSet, Quaternion.identity, transform);

	}


	public void DestroyCurrentFire()
	{
		if (currentFire)
			Destroy(currentFire);
	}

	private void IsDay()
	{
		SpawnNewFire(false);
		//changement de color jour 
	}

	private void IsNight()
	{
		SpawnNewFire(true);
		//changement de color nuit
	}

}
