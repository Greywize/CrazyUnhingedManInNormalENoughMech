using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    //public Weapon weapon;
    public GameObject weaponMono;
    Image thisImage;
    Player thePlayer;

    void Start()
    {
        thisImage = GetComponent<Image>();
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        //if (thePlayer.currentWeapon.projectile == weapon.projectile)
        if (thePlayer.currentWeaponMono[0] == weaponMono)
        {
            thisImage.color = Color.red;
        }
        else
        {
            thisImage.color = Color.blue;
        }
    }
}
