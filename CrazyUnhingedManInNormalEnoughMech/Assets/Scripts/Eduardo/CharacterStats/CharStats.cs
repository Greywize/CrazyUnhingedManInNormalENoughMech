using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public abstract class CharStats : MonoBehaviour
{
    #region PUBLIC MEMBERS
    [Header("Scoreboard")]
    [SerializeField] protected string charName;
    [SerializeField] protected int score;
    [Space(10)]

    [Header("Health")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Space(10)]
    [Header("Attack")]
    [SerializeField] protected int damage;
    #endregion

    #region MONOBEHAVIOUR
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

    public virtual void takeDMG(int dmg)
    {
        currentHealth -= dmg;
    }

    public virtual void addScore(CharStats c)
    {
        c.score += score;
    }

   public virtual void charDeath(CharStats c, float deathTimer)
    {
        c.addScore(c);
        Destroy(c.GetComponent<AgentBehaviour>().Body, deathTimer);
    }

    public virtual void spawnChar(CharStats c, Vector3 location)
    {
        Instantiate(c.transform, location, Quaternion.identity);
    }

    public int getCharDamage()
    {
        return damage;
    }
    #endregion
}
