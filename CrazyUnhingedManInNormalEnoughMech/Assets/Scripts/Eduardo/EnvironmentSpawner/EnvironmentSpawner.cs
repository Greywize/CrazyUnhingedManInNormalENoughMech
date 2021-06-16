using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    #region PRIVATE MEMBERS
    private Transform parent;
    #endregion
    
    #region PUBLIC MEMBERS
    [Header("Terrain")] public Transform terrain;
    
    [Space(10)] [Header("object spawn limit")]
    public int numTree;
    public int numFlower1;
    public int numflower2;

    [Space(10)] [Header("Object Prefabs")] [Space(10)]
    public GameObject Tree;
    public GameObject Flower1;
    public GameObject Flower2;

    #endregion
  
    #region MONOBEHAVIOURS
    private void OnEnable()
    {
        parent = GetComponent<Transform>();
    }

    private void Awake()
    {
       // throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    #endregion

    #region FUNCTIONS

    private void spawnTrees()
    {
        
    }
    

    #endregion
}
