using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HavePanelManager : MonoBehaviour
{
    private PlayerData myData;

    private bool HasLoad = false;

    public List<Button> haveDerkes = new List<Button>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Invoke("StartGetDerke", 3f);
    }

    void StartGetDerke()
    {
        myData = Player.LocalPlayer.GetComponent<PlayerData>();
        HasLoad = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasLoad)
        {
            return;
        }

        for (int i = 0; i < haveDerkes.Count; i++)
        {
            if (myData.BackDerke.Count <= i)
            {
                haveDerkes[i].image.sprite = DerkeImageDictionary.derkeImageDic.GetImageDerke("null");
            }
            else
            {
                haveDerkes[i].image.sprite = DerkeImageDictionary.derkeImageDic.GetImageDerke(myData.BackDerke[i].name);
            }
        }


    }
}
