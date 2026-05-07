using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class DerkeStatus : NetworkBehaviour
{
    [SyncVar]
    public int maxHp = 100;
    [SyncVar]
    public int hp = 100;
    [SyncVar]
    public int speed = 100;
    [SyncVar]
    public int attack = 100;
    [SyncVar]
    public int defensive = 100;
    [SyncVar]
    public int accuracy = 100;
    [SyncVar]
    public AbilityDictionary ability;
    [SyncVar]
    public bool isAlive = true;
    [SyncVar]
    public bool isAbility = true;
    [SyncVar]
    public bool canEscape = true;
    [SyncVar]
    public TypeMap.SpecificType type = TypeMap.SpecificType.OTHER;
    [SyncVar]
    public string effection = "";
    [SyncVar]
    public bool hasEntryProcess = false;

    public List<string> skill = new List<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
