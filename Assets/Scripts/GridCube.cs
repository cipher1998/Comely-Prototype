using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCube : MonoBehaviour
{
    public bool Isoccupied;
    public GameObject OverMe;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Isoccupied = false;
        OverMe = null;
    }

 
   
}
