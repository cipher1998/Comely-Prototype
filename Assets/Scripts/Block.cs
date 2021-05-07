using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    
    [SerializeField] public int ExpectedCount;
    [SerializeField] public int Currcount;
    [SerializeField]  Vector3 Mystartpos;
    GameManager gameManager;
    void Start()
    {
        Mystartpos = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
         
    }
    public void UpdateHighscore()
    {
        gameManager.Score += 1;
       
        gameManager.text.text= "Score: " + gameManager.Score.ToString();
       
    }
    public void CheckisPlaceable()
    {
        if(Currcount == ExpectedCount)
        {
           
            gameManager.SpawnedBlocks.Remove(gameObject);
            foreach(Transform child in transform)
            {
                Destroy(child.GetComponent<Inputs>());
                child.parent = null;
            }
            Destroy(gameObject);
        }
        else
        {
            Currcount=0;
            transform.position = Mystartpos;
        }
    }
}
