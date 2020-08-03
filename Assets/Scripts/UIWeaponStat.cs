using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIWeaponStat : MonoBehaviour
{
    public WeaponPart.WeaponStatType stat;
    public Text statValue;

    float prevAmount;
    float newAmount;
    float curAmount;
    public void SetStatValue(float amount)
    {
        // newAmount = amount;
        curAmount = amount;
        SetStats();
        gameObject.SetActive(true);

       // StartCoroutine(UpdateText());
    }

   
    IEnumerator UpdateText()
    {
        curAmount = 0;
        float timer = 0;


        statValue.transform.localScale *=1.2f;

        while (timer < 1)
        {
            curAmount = Mathf.Lerp(prevAmount, newAmount, timer);
            timer += .1f;
            SetStats();
            statValue.color = Color.white;

            yield return new WaitForSeconds(.05f);
        }

        statValue.transform.localScale = Vector3.one;
        statValue.color = GameObject.Find("StatIcon").GetComponent<Image>().color;

        prevAmount = curAmount;

    }

    void SetStats()
    {

        if (stat == WeaponPart.WeaponStatType.Accuracy)
        {
            curAmount /= 100;
            if (curAmount > 1f)
                curAmount = 1f;
            statValue.text = curAmount.ToString("P1");

        }
        else if (stat == WeaponPart.WeaponStatType.AmmoPerClip)
            statValue.text = curAmount.ToString("N0");
        else
            statValue.text = curAmount.ToString("N1");
    }
}
