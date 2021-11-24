using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPointBase
{
    public Transform target;
    public float waitTime;
    public bool lookTowards;
    public Transform tragetToLook;


    public WayPointBase()
    {

    }
}