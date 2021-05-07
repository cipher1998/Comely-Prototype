using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    Camera camera;
    Vector3  offset;
    float mzcoord;
    bool Isplaceable;
    [SerializeField] Block parentblock;
  

    private void OnEnable()
    {
        parentblock = transform.parent.GetComponent<Block>();
    }
    private void Start()
    {
        camera = Camera.main;
        Isplaceable = false;

    }

    
    private void OnMouseDown()
    {
        mzcoord = camera.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetmouseWorldPos();
    }

    private Vector3 GetmouseWorldPos()
    {
        Vector3 mousepoint = Input.mousePosition;
        mousepoint.z = mzcoord;
        return camera.ScreenToWorldPoint(mousepoint);

    }

    private void OnMouseDrag()
    {
        transform.parent.position = GetmouseWorldPos() + offset;
        float x = (transform.parent.position.x * 10) % 10;
        float z = (transform.parent.position.z * 10) % 10;
        Vector3 snapvec = new Vector3(x/10 ,transform.position.y, z/10);
        transform.parent.position -= snapvec;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "OFFSET")
        {
            return;
        }
        if(other.gameObject.tag == "GRIDCUBE")
        {
            
            if(Mouse.current.leftButton.isPressed && !other.gameObject.GetComponent<GridCube>().Isoccupied)
            {
              
                parentblock.Currcount++;
            }
           
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
         if(!Mouse.current.leftButton.isPressed && other.gameObject.tag == "GRIDCUBE")
            {
                 
                if(parentblock.ExpectedCount == parentblock.Currcount)
                {
                     
                    other.gameObject.GetComponent<GridCube>().OverMe = this.gameObject;
                    other.gameObject.GetComponent<GridCube>().Isoccupied= true;
                    parentblock.UpdateHighscore();
                }
                parentblock.CheckisPlaceable();
            }
    }
    private void OnTriggerExit(Collider other)
    {
         if(other.gameObject.tag == "GRIDCUBE" && Mouse.current.leftButton.isPressed && !other.gameObject.GetComponent<GridCube>().Isoccupied)
         {
              parentblock.Currcount--;
         }
    }
  
}


