using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_candles : MonoBehaviour
{
    private GameObject moimoi;

    public int nombreDeFeuxPossibles;

    public GameObject fire_1;
    public GameObject fire_2;
    public GameObject fire_3;
    public GameObject fire_4;

    private int nombreChoisi;

    // Start is called before the first frame update
    void Start()
    {
        nombreChoisi = Random.Range(1, nombreDeFeuxPossibles);

        switch(nombreChoisi)
        {
            case 1:
                Instantiate(fire_1, transform.gameObject);
                break;
            case 2:
                Instantiate(fire_2, moimoi.transform.position, moimoi.transform.rotation);
                break;
            case 3:
                Instantiate(fire_3, moimoi.transform.position, moimoi.transform.rotation);
                break;
            case 4:
                Instantiate(fire_4, moimoi.transform.position, moimoi.transform.rotation);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
