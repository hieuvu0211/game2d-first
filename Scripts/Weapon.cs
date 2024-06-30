using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ArrowPreFab;

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {

        Instantiate(ArrowPreFab, firePoint.position, firePoint.rotation);
    }
}
