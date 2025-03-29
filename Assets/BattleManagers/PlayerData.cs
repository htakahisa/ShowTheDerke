using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class PlayerData : NetworkBehaviour
{
    public NetworkIdentity netIdentity;
    public string selectedMove;

    [SyncVar]
    public GameObject BattleDerke;

    public SyncList<GameObject> BackDerke = new SyncList<GameObject>();

    private CommandUI cmdUI;

    private DerkeDictionary derkeDic;

    public int Shadows = 100;

    public void Start()
    {

        derkeDic = DerkeDictionary.derkeDic;
        cmdUI = CommandUI.cmdUI;

       // CmdLoadDerke("Omoko");


    }

    void Update()
    {

    }


    [Command]
    public void CmdLoadDerke(string name)
    {
        GameObject obj = Instantiate(derkeDic.GetDerke(name), Vector3.zero, Quaternion.identity);     
        NetworkServer.Spawn(obj);
        RpcLoadDerke(obj, name);
        BackDerke.Add(obj);
    }

    [ClientRpc]
    public void RpcLoadDerke(GameObject obj, string name)
    {
        obj.name = name;
        Debug.Log(isServer);
    }


    [Command]
    public void CmdSelectMove(NetworkIdentity player, string moveName)
    {
     
         selectedMove = moveName;
         PhaseManager.pm.SelectMove(player, moveName);
        
    }



    [Command]
    public void CmdSetBattleDerke(string number)
    {
        IfServerSetBattleDerke(number);
    }

    [Server]
    public void IfServerSetBattleDerke(string number)
    {
        if(BackDerke.Count <= int.Parse(number))
        {
            return;
        }
        BattleDerke = BackDerke[int.Parse(number)];
        BackDerke.RemoveAt(int.Parse(number));
        TargetChangeBattleDerke(connectionToClient, BattleDerke);
    }


    [TargetRpc]
    public void TargetChangeBattleDerke(NetworkConnectionToClient nc, GameObject battleDerke)
    {
        BattleDerke = battleDerke;
        CommandUI.cmdUI.SetSkill();
    }


    public GameObject GetBattleDerke()
    {
        return BattleDerke;
    }


    public DerkeStatus GetBattleDerkeStatus()
    {
        if (BattleDerke == null)
        {
            return null;
        }
        return BattleDerke.GetComponent<DerkeStatus>();
    }



}
