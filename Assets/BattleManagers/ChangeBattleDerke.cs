using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBattleDerke : NetworkBehaviour
{
    public Button BackDerke1;
    public Button BackDerke2;
    public Button BackDerke3;


    public List<string> BackDerke;

    private PlayerData playerData;

    public static ChangeBattleDerke cbd;



    private void Awake()
    {
        cbd = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BackDerke1.onClick.AddListener(() => ChangeDerke(0));
        BackDerke2.onClick.AddListener(() => ChangeDerke(1));
        BackDerke3.onClick.AddListener(() => ChangeDerke(2));

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeDerke(int number)
    {
        playerData = Player.LocalPlayer.GetComponent<PlayerData>();

        if (playerData.BackDerke.Count <= number)
        {
            return;
        }


        CommandUI.cmdUI.SetCanPress(false);
        SetCanPress(false);

        playerData.CmdSelectMove(playerData.netIdentity, "Change" + number);
        



        BackDerke1.onClick.AddListener(() => ChangeDerke(0));
        BackDerke2.onClick.AddListener(() => ChangeDerke(1));
        BackDerke3.onClick.AddListener(() => ChangeDerke(2));

    }

    public void SetCanPress(bool canPress)
    {
        BackDerke1.gameObject.SetActive(canPress);
        BackDerke2.gameObject.SetActive(canPress);
        BackDerke3.gameObject.SetActive(canPress);

    }

}
