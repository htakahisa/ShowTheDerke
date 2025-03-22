using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : NetworkBehaviour
{
    [SyncVar]
    private string battleDerke = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBattleDerke(string name)
    {
        battleDerke = name;
    }
    public string GetBattleDerke()
    {
        return battleDerke;
    }
}
