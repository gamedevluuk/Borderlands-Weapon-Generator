using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponCard : MonoBehaviour
{
    public Text nameLbl;
    public Text rarityLbl;
    public Image weaponTypeImg;

    public List<UIWeaponStat> wpnUIStats;

    public List<Sprite> weaponTypeSprites;

    public List<Graphic> rarityColorGraphics = new List<Graphic>();
    public List<Graphic> rarityColorHLGraphics = new List<Graphic>();

    public RaritySO raritySO;

    public void UpdateWeaponCard(WeaponBody.WeaponType _type, Dictionary<WeaponPart.WeaponStatType, float> _weaponStats, WeaponPart.RarityLevel _rarity)
    {
        SetWeaponImageAndLabels(_type, _rarity);

        SetStats(_weaponStats);

        SetRarityColor(_rarity);

    }

    void SetWeaponImageAndLabels(WeaponBody.WeaponType _type, WeaponPart.RarityLevel _rarity)
    {
        nameLbl.text = _rarity.ToString() + " " + _type.ToString();
        rarityLbl.text = _rarity.ToString();

        weaponTypeImg.sprite = weaponTypeSprites[(int)_type];
    }

    void SetStats(Dictionary<WeaponPart.WeaponStatType, float> _weaponStats)
    {

        foreach (KeyValuePair<WeaponPart.WeaponStatType, float> weaponStatEntry in _weaponStats)
        {
            foreach (UIWeaponStat UIStat in wpnUIStats)
            {
                if(UIStat.stat == weaponStatEntry.Key)
                {
                    UIStat.SetStatValue(weaponStatEntry.Value);
                }
            }
        }
    }

    void SetRarityColor(WeaponPart.RarityLevel _rarity)
    {
        foreach (Graphic graphic in rarityColorGraphics)
        {
            graphic.color = raritySO.rarityColors[(int)_rarity];
        }

        foreach (Graphic graphic in rarityColorHLGraphics)
        {
            graphic.color = raritySO.rarityColors[(int)_rarity] + new Color(.2f, .2f, .2f);
        }

    }

}
