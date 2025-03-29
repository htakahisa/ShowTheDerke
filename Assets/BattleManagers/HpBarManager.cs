using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    public Slider myHpBar;
    public Slider enemyHpBar;

    PlayerData myPlayerData;
    PlayerData enemyPlayerData;

    private bool HasLoad = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Invoke("StartGetPlayerData", 1f);
    }

    public void StartGetPlayerData()
    {

        myPlayerData = Player.LocalPlayer.GetComponent<PlayerData>();
        enemyPlayerData = Player.opponent.GetComponent<PlayerData>();

        HasLoad = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!HasLoad)
        {
            return;
        }

        if (myPlayerData.GetBattleDerkeStatus() != null)
        {
            myHpBar.maxValue = myPlayerData.GetBattleDerkeStatus().maxHp; ;
            myHpBar.value = myPlayerData.GetBattleDerkeStatus().hp;
        }

        if (enemyPlayerData.GetBattleDerkeStatus() != null)
        {
            enemyHpBar.maxValue = enemyPlayerData.GetBattleDerkeStatus().maxHp; ;
            enemyHpBar.value = enemyPlayerData.GetBattleDerkeStatus().hp;
        }
    }
}
