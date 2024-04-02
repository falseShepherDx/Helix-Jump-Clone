using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public static Destructible Instance;
    public GameObject shatteredVersion;
    
    private void Awake()
    {
        Instance = this;
        
    }

    public void ExplodePlatform(GameObject ringParent)
    {
        int childCount=ringParent.transform.childCount;
        List<GameObject> instantiatedObjects = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            Transform child = ringParent.transform.GetChild(i);
            Quaternion newRotation = Quaternion.Euler(child.rotation.eulerAngles.x, child.rotation.eulerAngles.y + 90, child.rotation.eulerAngles.z);
            GameObject newShatteredObj = Instantiate(shatteredVersion, child.position, newRotation);
            instantiatedObjects.Add(newShatteredObj);
            
            Destroy(newShatteredObj,1.75f);
        }
       
    }
  
}
