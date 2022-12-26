using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
    Vector3 attackPos = Vector3.zero;
    public List<GameObject> krakenSpots;
    int count = 0;
    int highest = 0;
    int spotList = 0;

    public void FindPlayerSpot()
    {
        int i = 0;
        foreach(var spot in krakenSpots)
        {
            Collider[] area = Physics.OverlapSphere(spot.transform.position, 10.0f);
            foreach(Collider c in area)
                if(c.GetComponent<MyPlayer>() != null)
                    count++;
            if (count > highest)
            {
                highest = count;
                spotList = i;
            }
            i++;
        }

        foreach (var c in krakenSpots)
            c.GetComponent<KrakenOriginSwing>().SetActive(true);
    }
}
 