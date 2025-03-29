using UnityEngine;
using UnityEngine.UI;

public class DerkeUiManager : MonoBehaviour
{

    public Image myDerke;
    public Image enemyDerke;

    private PlayerData myData;
    private PlayerData enemyData;

    private bool HasLoad = false;

    public static DerkeUiManager dum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dum = this;
        Invoke("StartGetDerke", 3f);
        
    }

    // Update is called once per frame
    void StartGetDerke()
    {
        myData = Player.LocalPlayer.GetComponent<PlayerData>();
        enemyData = Player.opponent.GetComponent<PlayerData>();
        HasLoad = true;
    }

    private void Update()
    {
        if (HasLoad)
        {
            if (myData.BattleDerke != null)
            {
                myDerke.sprite = DerkeImageDictionary.derkeImageDic.GetImageDerke(myData.BattleDerke.name);
            }
            if (enemyData.BattleDerke != null)
            {
                enemyDerke.sprite = DerkeImageDictionary.derkeImageDic.GetImageDerke(enemyData.BattleDerke.name);
            }
        }
    }



}
