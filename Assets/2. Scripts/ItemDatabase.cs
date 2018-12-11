using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemDatabase : MonoBehaviour {

    private static ItemDatabase _instance = null;
    public static ItemDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(ItemDatabase)) as ItemDatabase;

                if (_instance == null)
                {
                    Debug.LogError("There's no active ItemDatabase Object");
                }
            }
            return _instance;
        }


    }


    void Enable()
    {

        
    }




   




}
