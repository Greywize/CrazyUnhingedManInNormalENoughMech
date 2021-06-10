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
        if (thePlayer.currentWeaponMono[0].GetComponent<WeaponMono>() && thePlayer.currentWeaponMono[0].GetComponent<WeaponMono>().weaponPrefab == weaponMono.GetComponent<WeaponMono>().weaponPrefab)
        {
            thisImage.color = Color.red;
        }
        else
        {
            thisImage.color = Color.blue;
        }
    }
}
