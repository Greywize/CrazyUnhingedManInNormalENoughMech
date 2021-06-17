using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    #region PUBLIC MEMBERS
    [Header("required Objects")]
    public GameObject Player;
    public GameObject SpiderPrefab;

    [Header("Spawn radius")]
    public float spawnRadius;
    #endregion

    #region MONOBEHAVIOURS
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Player.transform.position, spawnRadius);
    }

    private void Awake()
    {
        if (Player == null)
            Debug.Log($"Player not found: null object");

        if(SpiderPrefab == null )
            Debug.Log($"Spider not found: null object");
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
    public void spawnEnergy()
    {
     
    }
    #endregion

}
