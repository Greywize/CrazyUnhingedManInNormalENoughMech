using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStats : CharStats
{
    #region PUBLIC MEMBERS
    #endregion

    #region MONONBEHAVIOURS

    private void OnEnable()
    {
        
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log($"{charName} has been killed");
            Destroy(transform.parent.gameObject);
        }
    }
    #endregion

    #region FUNCTIONS
    public void takeDMG(int dmg)
    {
        currentHealth -= dmg;
    }

    #endregion


}
