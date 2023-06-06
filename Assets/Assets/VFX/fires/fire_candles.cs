using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_candles : MonoBehaviour
{
    public List<GameObject> fires;
    private GameObject currentFire;
    public Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewFire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void SpawnNewFire()
	{
        DestroyCurrentFire();

        int nombreChoisi = Random.Range(0, fires.Count);
        currentFire = Instantiate(fires[nombreChoisi], transform.position + offSet, Quaternion.identity, transform);
    }    


    public void DestroyCurrentFire()
    {
        if (currentFire)
            Destroy(currentFire);
    }
}
