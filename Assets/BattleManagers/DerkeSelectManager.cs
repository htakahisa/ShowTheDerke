using UnityEngine;

public class DerkeSelectManager : MonoBehaviour
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int cost;

    private PlayerData myData;
    private PlayerData enemyData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Invoke("StartGetDerke", 1f);
    }

    void StartGetDerke()
    {
        myData = Player.LocalPlayer.GetComponent<PlayerData>();
        enemyData = Player.opponent.GetComponent<PlayerData>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyDerke()
    {
        if (myData.Shadows >= cost)
        {
            myData.CmdLoadDerke(name);
            myData.Shadows -= cost;
        }
    }
}
