using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBody : WeaponPart
{


    public enum WeaponType
    {
        SMG,
        Rifle,
        Sniper
    }

    public WeaponType currentType;


    public Transform barrelSocket;
    public Transform scopeSocket;
    public Transform magazineSocket;
    public Transform gripSocket;
    public Transform stockSocket;

    List<WeaponPart> weaponParts = new List<WeaponPart>();
    Dictionary<WeaponStatType, float> weaponStats = new Dictionary<WeaponStatType, float>();

    public UIWeaponCard wpnCard;

    int rawRarity = 0;

    public RaritySO raritySO;

    public Weapon weapon;
    public Transform muzzle;
    public GameObject muzzleFX;

    public void Initialize(WeaponBarrelPart barrel, WeaponPart scope, WeaponPart stock, WeaponPart handle, WeaponPart magazine)
    {
        weaponParts.Add(this);
        muzzle = barrel.muzzle;
        muzzleFX = barrel.muzzleFX;

        weaponParts.Add(barrel);
        weaponParts.Add(scope);
        weaponParts.Add(stock);
        weaponParts.Add(handle);
        weaponParts.Add(magazine);

        CalculateStats();
        DetermineRarity();
        UpdateWeaponCard();

        weapon.Initialize(weaponStats, this);

    }

    void CalculateStats()
    {

        foreach (WeaponPart part in weaponParts)
        {

            rawRarity += (int)part.rarityLevel;

            foreach (KeyValuePair<WeaponStatType, float> stat in part.stats)
            {

                if (!weaponStats.ContainsKey(stat.Key))
                {
                    weaponStats.Add(stat.Key, stat.Value);
                }
                else
                {
                    weaponStats[stat.Key] += stat.Value;
                }


            }
        }

    }

    void DetermineRarity()
    {
        int averageRarity = rawRarity / weaponParts.Count;
        averageRarity = Mathf.Clamp(averageRarity, 0, 4);
        rarityLevel = (RarityLevel)averageRarity;

        foreach (WeaponPart weaponPart in weaponParts)
        {
            Outline outlineWpnPart = weaponPart.GetComponent<Outline>();
            outlineWpnPart.OutlineColor = raritySO.rarityColors[(int)rarityLevel];
        }

    }

    void UpdateWeaponCard()
    {
        wpnCard.UpdateWeaponCard(currentType, weaponStats, rarityLevel);
    }

    public void ToggleWeaponCard(bool active)
    {
        wpnCard.gameObject.SetActive(active);
    }

    public void SetWeaponCardScale(float distance)
    {
        float clampedDist = Mathf.Clamp(2/ distance, 0.7f, 1.3f);
        wpnCard.transform.GetChild(0).localScale = new Vector2(clampedDist, clampedDist);
    }
}
