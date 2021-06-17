using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public GameObject weapon;
    Image thisImage;
    Player thePlayer;

    void Start()
    {
        thisImage = GetComponent<Image>();
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (thePlayer.currentWeapon[0].GetComponent<WeaponMono>() && thePlayer.currentWeapon[0].GetComponent<WeaponMono>().weaponPrefab == weapon.GetComponent<WeaponMono>().weaponPrefab)
        {
            thisImage.color = Color.red;
        }
        else
        {
            thisImage.color = Color.blue;
        }
    }
}
