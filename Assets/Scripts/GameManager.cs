using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject [] Spawnpoints;
    [SerializeField] GameObject [] BlockPrefab;
     public List<GameObject> SpawnedBlocks;
    [SerializeField] GridCube[] Gridcubes1 = new GridCube[5];
    [SerializeField] GridCube[] Gridcubes2 = new GridCube[5];
    [SerializeField] GridCube[] Gridcubes3 = new GridCube[5];
    [SerializeField] GridCube[] Gridcubes4 = new GridCube[5];
    [SerializeField] GridCube[] Gridcubes5 = new GridCube[5];
    

    [SerializeField] public Text text;
    [SerializeField] public Image Gameover;
    [SerializeField] public int Score=0;
     int Size =5;
    private GridCube[,] Gridcubes;

    private void OnEnable()
    {
        Gridcubes = new GridCube [Size, Size];
        Create2Darray(Gridcubes1 , 0);
        Create2Darray(Gridcubes2 , 1);
        Create2Darray(Gridcubes3 , 2);
        Create2Darray(Gridcubes4 , 3);
        Create2Darray(Gridcubes5 , 4);
    }

    private void Create2Darray(GridCube[] gridcubes, int v)
    {

        for(int i = 0 ; i< Size ; i++)
        {
            Gridcubes[v,i] = gridcubes[i];
           
        }
    
    }

    private void RespawnBlocks()
    {
       foreach(GameObject spawnpoint in Spawnpoints)
       {
           GameObject prefab = BlockPrefab[UnityEngine.Random.Range(0, BlockPrefab.Length)];
           GameObject spawnedObj;
           spawnedObj= Instantiate(prefab , spawnpoint.transform.position , spawnpoint.transform.rotation);
           SpawnedBlocks.Add(spawnedObj);
       }
    }

    void Update()
    {
        CheckLine();
        if(SpawnedBlocks.Count == 0)
        {
            RespawnBlocks();
            
        }
        if(CheckGameover()) {Gameover.gameObject.SetActive(true);}
      
    }

    private bool CheckGameover()
    {
        bool status = true;
        for(int i=0; i< SpawnedBlocks.Count; i++)
        {
            GameObject spawnblock = SpawnedBlocks[i];
            status = Checkifplacable(spawnblock);
            if(status ==true )
            {
                return false;
            }
        }
       if(SpawnedBlocks.Count==0) {return false;}
        return true;
    }

    private bool Checkifplacable(GameObject spawnblock)
    {
        bool status = false;
        Vector3 firstchild = spawnblock.transform.GetChild(0).position;
        for(int i=0;i<Size; i++)
        {
            for(int j=0;j<Size; j++)
            {
                foreach(Transform childs in spawnblock.transform)
                {
                   bool trycatch= false;
                   Vector3 diff= firstchild - childs.position;
                   trycatch =TryCatchBlock(diff , i , j);
                   if(trycatch)
                   {
                       if(!Gridcubes[i+(int)diff.z, j -(int)diff.x].Isoccupied)
                       {
                           status =true;
                       }
                       else
                       {
                           status= false;
                           break;
                       }
                   } 
                   else
                   {
                       status = false;
                       break;
                   }               
                }
                if(status)
                {
                    return true;
                }
                
            }
        }
        return false;
    }

    private bool TryCatchBlock( Vector3 diff , int i, int j)
    {
        try
        {
            GameObject g = Gridcubes[i+(int)diff.z, j -(int)diff.x].gameObject;
            return true;
        }
        catch(IndexOutOfRangeException e)
        {
            print($"{i+(int)diff.z}, {j -(int)diff.x}");
            return false;
        }
        catch(NullReferenceException n)
        {
          print($"{i+(int)diff.z}, {j -(int)diff.x}");
            return false;
        }
    }

    private void CheckLine()
    {
        CheckLinecut(Gridcubes1);
        CheckLinecut(Gridcubes2);
        CheckLinecut(Gridcubes3);
        CheckLinecut(Gridcubes4);
        CheckLinecut(Gridcubes5);
    }

    private void CheckLinecut(GridCube[] gridcubes)
    {
        int count= 0;
        for(int i=0 ;i< gridcubes.Length; i++)
        {
            if(gridcubes[i].Isoccupied)
            {
                count++;
            }
        }
        if(count == gridcubes.Length)
        {
            for(int i=0 ;i< gridcubes.Length; i++)
            {
                Destroy(gridcubes[i].OverMe);
                gridcubes[i].OverMe = null;
                gridcubes[i].Isoccupied = false;
            }
        }
    }
}






