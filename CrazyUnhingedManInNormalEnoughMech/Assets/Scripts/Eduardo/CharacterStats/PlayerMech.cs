using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMech : MonoBehaviour
{

    #region PUBLIC MEMBERS
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    [Space(10)]

    [Header("Weapons")]
    public Weapon[] weapons;
    public Weapon currentWeapon;
    public int ammunition;
    [Space(15)]

    [Header("Score")]
    public int score;

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
        
      //  if()
    }

    private void LateUpdate()
    {
        
    }
    #endregion

    #region FUNCTIONS



    #endregion
}
