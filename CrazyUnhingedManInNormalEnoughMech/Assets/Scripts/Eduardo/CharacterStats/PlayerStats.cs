using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharStats
{

    #region PUBLIC MEMBERS
    #endregion

    #region MONOBEHAVIOUR
    private void OnEnable()
    {
        
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }

    private void LateUpdate()
    {
        
    }
    #endregion

    #region FUNCTIONS
    public void takeDMG(int dmg)
    {
        currentHealth -= dmg;
    }

    public override void addScore(CharStats c)
    {
        
    }


    #endregion
}
