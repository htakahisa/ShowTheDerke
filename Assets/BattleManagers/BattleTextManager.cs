using Mirror;
using TMPro;
using UnityEngine;

public class BattleTextManager :  NetworkBehaviour
{

    public TextMeshProUGUI Text;

    public static BattleTextManager btm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        btm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Server]
    public void ServerSetText(string _text)
    {
        RpcSetText(_text);
    }

    [ClientRpc]
    public void RpcSetText(string _text)
    {
        Text.text = _text;
    }

}
