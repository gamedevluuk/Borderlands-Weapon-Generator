using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{

    public List<GameObject> bodyParts;

    public List<GameObject> barrelParts;
    public List<GameObject> stockParts;
    public List<GameObject> scopeParts;
    public List<GameObject> magazineParts;
    public List<GameObject> gripParts;

    GameObject prevWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateWeapon();
        }
    }

    void GenerateWeapon()
    {


        GameObject randomBody = GetRandomPart(bodyParts);
        GameObject insBody = Instantiate(randomBody, Vector3.zero, Quaternion.identity);
        WeaponBody wpnBody = insBody.GetComponent<WeaponBody>();

        WeaponPart barrel = SpawnWeaponPart(barrelParts, wpnBody.barrelSocket);
        WeaponPart scope = SpawnWeaponPart(scopeParts, wpnBody.scopeSocket);
        WeaponPart magazine = SpawnWeaponPart(magazineParts, wpnBody.magazineSocket);
        WeaponPart grip = SpawnWeaponPart(gripParts, wpnBody.gripSocket);
        WeaponPart stock = SpawnWeaponPart(stockParts, wpnBody.stockSocket);

        wpnBody.Initialize((WeaponBarrelPart)barrel, scope, stock, grip, magazine);

        prevWeapon = insBody;

    }

    WeaponPart SpawnWeaponPart(List<GameObject> parts, Transform socket)
    {
        GameObject randomPart = GetRandomPart(parts);
        GameObject insPart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation);
        insPart.transform.parent = socket;

        return insPart.GetComponent<WeaponPart>();
    }

    GameObject GetRandomPart(List<GameObject> partList)
    {
        int randomNumber = Random.Range(0, partList.Count);
        return partList[randomNumber];
    }
}
