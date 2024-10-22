﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public Transform weaponSocket;

    Weapon focusedWeapon;
    Weapon equippedWeapon;

    public LayerMask weaponMask;

    void Update()
    {

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 5, Color.red);
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5, weaponMask))
        {

            Weapon weapon = hit.transform.GetComponent<Weapon>();
            weapon.ToggleWeaponCard(true);
            focusedWeapon = weapon;

            float dist = Vector3.Distance(weapon.transform.position, Camera.main.transform.position);
            weapon.SetWeaponCardScale(dist);

        }

        else
        {
            if(focusedWeapon != null)
            {
                focusedWeapon.ToggleWeaponCard(false);
                focusedWeapon = null;
            }
        }



        if(focusedWeapon != null && Input.GetKeyDown(KeyCode.E))
        {
            EquipWeapon(focusedWeapon);
        }

        if(equippedWeapon!=null && Input.GetMouseButton(0))
        {
            equippedWeapon.DoFire();
        }

        if (equippedWeapon != null && Input.GetKeyDown(KeyCode.R))
        {
            equippedWeapon.DoReload();
        }


    }

    void EquipWeapon(Weapon weaponToEquip)
    {

        if(equippedWeapon != null)
        {
            DropWeapon();
        }

        equippedWeapon = weaponToEquip;
        equippedWeapon.transform.parent = weaponSocket;

        equippedWeapon.ToggleWeaponCard(false);

        equippedWeapon.GetComponent<Rigidbody>().isKinematic = true;
        equippedWeapon.GetComponent<Collider>().enabled = false;

        Collider[] childColliders = equippedWeapon.transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < childColliders.Length; i++)
        {
            childColliders[i].enabled = false;
        }


        StartCoroutine(MoveWeaponToSocket());

    }
    
    IEnumerator MoveWeaponToSocket()
    {
        float moveTimer = 0;

        Vector3 startPos = equippedWeapon.transform.localPosition;
        Vector3 endPos = Vector3.zero;

        Quaternion startRot = equippedWeapon.transform.localRotation;
        Quaternion endRot = Quaternion.identity;

        while(moveTimer < 1)
        {
            moveTimer += .1f;

            equippedWeapon.transform.localPosition = Vector3.Lerp(startPos, endPos, moveTimer);
            equippedWeapon.transform.localRotation = Quaternion.Lerp(startRot, endRot, moveTimer);


            yield return new WaitForSeconds(.01f);

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            focusedWeapon = other.GetComponent<Weapon>();
        }
    }

    void DropWeapon()
    {
        equippedWeapon.GetComponent<Collider>().enabled = true;

        Collider[] childColliders = equippedWeapon.transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < childColliders.Length; i++)
        {
            childColliders[i].enabled = true;
        }

        Rigidbody rb = equippedWeapon.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddExplosionForce(3, weaponSocket.position, 1, 1, ForceMode.Impulse);

        equippedWeapon.transform.parent = null;
        equippedWeapon = null;

    }
}
