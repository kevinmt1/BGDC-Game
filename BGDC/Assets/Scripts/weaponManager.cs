using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{

    public int selectedWeapon = 0;
    public GameObject weaponHolder;
    public GameObject weaponNumToSwap;
    public GameObject weaponToSwap;

    // Start is called before the first frame update
    void Start()
    {
        switchWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        int previousWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        if (previousWeapon != selectedWeapon)
        {
            switchWeapon();
        }

        findEquippedWeapon();
    }

    void switchWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    void findEquippedWeapon()
    {
        weaponNumToSwap = weaponHolder.transform.GetChild(selectedWeapon).gameObject;
        weaponToSwap = weaponNumToSwap.transform.GetChild(0).gameObject;
    }

    public void takeNewWeapon(GameObject weaponToTake)
    {
        GameObject droppedWeapon = Instantiate(weaponToSwap, weaponToTake.transform.position, weaponToTake.transform.rotation);
        droppedWeapon.name = weaponToSwap.name;
        droppedWeapon.tag = "Weapon";
        droppedWeapon.layer = 11;
        Destroy(weaponToTake);
        Destroy(weaponToSwap);
        GameObject weaponTaken = Instantiate(weaponToTake, transform.position, transform.rotation, weaponNumToSwap.transform);
        weaponTaken.name = weaponToTake.name;
        weaponTaken.tag = "Untagged";
        weaponTaken.layer = 9;
    }
}
