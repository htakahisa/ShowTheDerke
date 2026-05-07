using Cysharp.Threading.Tasks;
using Mirror;
using Mirror.BouncyCastle.Tsp;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using UnityEngine;

public class PhaseManager : NetworkBehaviour
{
    [SyncVar] private bool player1Ready = false;
    [SyncVar] private bool player2Ready = false;

    private PlayerData player1;
    private PlayerData player2;

    public static PhaseManager pm;

    private DerkeDictionary derkeDic;

    private NetworkIdentity netIdentity;
    private PlayerData playerData;

    // Action ではなく Func を使い、最後に戻り値の型（UniTask）を指定する
    private Dictionary<string, Func<string, string, string, UniTask>> methodDictionary;


    private void Awake()
    {
        pm = this;
        Invoke("GetData", 1f);
    }

    public void GetData()
    {
        StartBattle();
        StartGetPlayerData();
    }

    private void Update()
    {
    }
    private void StartGetPlayerData()
    {
        if (Player.LocalPlayer != null)
        {
            netIdentity = Player.LocalPlayer.GetComponent<NetworkIdentity>();
            playerData = netIdentity.GetComponent<PlayerData>();
            // 関数の登録
            methodDictionary = new Dictionary<string, Func<string, string, string, UniTask>>()
            {
                { "SetBattleDerke", SetBattleDerke },
                { "SetStatus", SetStatus },
                { "RandomDamage", RandomDamage }
            };

        }
    }

    [Server]
    public void StartBattle()
    {
        PlayerData[] players = FindObjectsOfType<PlayerData>();
        if (players.Length < 2)
        {
            Debug.LogError("Not enough players to start the battle!");
            return;
        }

        InitializeBattle(players[0], players[1]);
    }

    public void InitializeBattle(PlayerData p1, PlayerData p2)
    {
        player1 = p1;
        player2 = p2;
        player1Ready = false;
        player2Ready = false;
    }

    [Server]
    public void SelectMove(NetworkIdentity player, string moveName)
    {
        PlayerData playerData = player == player1.netIdentity ? player1 : player2;
        playerData.selectedMove = moveName;

        if (player == player1.netIdentity)
            player1Ready = true;
        else
            player2Ready = true;

        if (player1Ready && player2Ready)
        {
            ResolveTurnOrder();
        }
    }




    void InvokeMethodByName(string methodName, string arg1, string arg2, string arg3)
    {
        // Action ではなく Func<..., UniTask> で受け取る
        if (methodDictionary.TryGetValue(methodName, out Func<string, string, string, UniTask> method))
        {
            method(arg1, arg2, arg3);
        }
        else
        {
            Debug.LogError($"Method '{methodName}' not found in dictionary.");
        }
    }

    [Server]
    private async UniTask ResolveTurnOrder()
    {

        PlayerData pd = player1;

        for (int t = 0; t < 2; t++)
        {
            if (pd.GetBattleDerkeStatus() == null || pd.GetBattleDerkeStatus().isAbility)
            {
                SkillDictionary method = SkillDatabase.GetMove(pd.selectedMove);
            }
        }
            
        pd = player2;

        PlayerData first, second;

        // ここでダメージ計算やアニメーションを実装
        SkillDictionary speed1 = SkillDatabase.GetMove(player1.selectedMove);
        SkillDictionary speed2 = SkillDatabase.GetMove(player2.selectedMove);

        int p1Speed = 0;
        int p2Speed = 0;

        if (player1.BattleDerke != null && speed1 != null)
        {
            p1Speed = player1.GetBattleDerkeStatus().speed + speed1.speed * 10000;
        }

        if (player2.BattleDerke != null && speed2 != null)
        {
            p2Speed = player2.GetBattleDerkeStatus().speed + speed2.speed * 10000;
        }

        if (p1Speed > p2Speed)
        {
            first = player1;
            second = player2;
        }
        else if (p1Speed < p2Speed)
        {
            first = player2;
            second = player1;
        }
        else
        {
            first = UnityEngine.Random.Range(0, 2) == 0 ? player1 : player2;
            second = first == player1 ? player2 : player1;

        }

        PlayerData attacker = first;
        PlayerData defender = second;

        for (int t = 0; t < 2; t++)
        {
            if (attacker.GetBattleDerke() == null || attacker.GetBattleDerkeStatus().canEscape || attacker.GetBattleDerkeStatus().isAbility || attacker.GetBattleDerkeStatus().hp <= 0)
            {

                await UniTask.Delay(1000);

                SkillDictionary method = SkillDatabase.GetMove(attacker.selectedMove);
                //DerkeStatus derkeStatus = identity.GetComponent<PlayerData>().GetBattleDerkeStatus();

                //SkillDictionary method = null;
                //for (int i = 0; i < derkeStatus.ability.skill.Count; i++)
                //{
                //    method = derkeStatus.ability.skill[i];

                //    if (NetworkServer.spawned.TryGetValue(uint.Parse(ID), out NetworkIdentity methodID))
                //    {
                //        PlayerData methodPlayerData = methodID.GetComponent<PlayerData>();
                //        ActiveEffect(SkillDictionary.Timing.ENTRY, method, methodPlayerData, identity == player1 ? player2 : player1);
                //    }
                //}
                //if(attacker.GetBattleDerkeStatus().ability.callTiming == )
                ActiveEffect(SkillDictionary.Timing.ENTRY, method, attacker, defender);
                ActiveEffect(SkillDictionary.Timing.CHANGEDERKE, method, attacker, defender);
                ActiveEffect(SkillDictionary.Timing.TURNORDER, method, attacker, defender);


            }
            attacker = second;
            defender = first;
        }

        StartCoroutine(ExecuteTurn(first, second));
    }

    private IEnumerator ExecuteTurn(PlayerData attacker, PlayerData defender)
    {
        StartCoroutine(ServerExecuteAttack(attacker.netIdentity, attacker.selectedMove, attacker, defender, false));

        yield return new WaitForSeconds(3f);

        if (defender.BattleDerke == null)
        {
            StartCoroutine(ServerExecuteAttack(defender.netIdentity, defender.selectedMove, defender, attacker, true));
        }
        else if (defender.GetBattleDerkeStatus().hp > 0)
        {
            StartCoroutine(ServerExecuteAttack(defender.netIdentity, defender.selectedMove, defender, attacker, true));
        }

        if (attacker.GetBattleDerkeStatus() != null)
        {
            if (attacker.GetBattleDerkeStatus().hp <= 0)
            {
                attacker.GetBattleDerkeStatus().isAlive = false;
                ServerTurnEnd();
            }
        }
        if (defender.GetBattleDerkeStatus() != null)
        {
            if (defender.GetBattleDerkeStatus().hp <= 0)
            {
                defender.GetBattleDerkeStatus().isAlive = false;
                ServerTurnEnd();
            }
        }

    }


    public IEnumerator ServerExecuteAttack(NetworkIdentity player, string move, PlayerData attacker, PlayerData defender, bool isSecond)
    {
        // ここでダメージ計算やアニメーションを実装
        SkillDictionary skill = SkillDatabase.GetMove(move);
        if (attacker != null && attacker.GetBattleDerkeStatus() != null)
        {
            if (attacker.GetBattleDerkeStatus().isAbility)
            {
                ActiveEffect(SkillDictionary.Timing.BEFOREATTACK, skill, attacker, defender);
            }
        }
        yield return new WaitForSeconds(1f);
        if (defender != null && defender.GetBattleDerkeStatus() != null)
        {
            if (defender.GetBattleDerkeStatus() != null)
            {
                if (defender.GetBattleDerkeStatus().hp <= 0)
                {
                    defender.GetBattleDerkeStatus().isAlive = false;
                    ServerTurnEnd();
                }
            }
        }

        if (move != null)
        {
            Debug.Log($"技: {skill.moveName}, 威力: {skill.power}, 命中率: {skill.accuracy}%");
        }

        bool isHit = true;

        if (attacker.GetBattleDerkeStatus() != null)
        {
            isHit = UnityEngine.Random.Range(1, 101) <= skill.accuracy * attacker.GetBattleDerkeStatus().accuracy * 0.01f;
        }

        yield return new WaitForSeconds(1f);
        RpcExecuteAttack(attacker.netIdentity, attacker.selectedMove, attacker, defender, isHit, skill, isSecond);

    }


    [ClientRpc]
    public void RpcExecuteAttack(NetworkIdentity player, string move, PlayerData attacker, PlayerData defender, bool isHit, SkillDictionary skill, bool isSecond)
    {
        TypeMap typeMap = new TypeMap();


        Tuple<float, string> effect = null;
        Tuple<float, string> evolution = null;


        if (attacker != null && attacker.GetBattleDerkeStatus() != null && attacker.GetBattleDerke() != null && isHit && skill.power != 0)
        {
            effect = typeMap.getEffect(skill.type, defender.GetBattleDerkeStatus().type);
            evolution = typeMap.getEvolution(skill.type, defender.GetBattleDerkeStatus().effection);

            float effectBonus = effect.Item1;
            float evolutionBonus = evolution.Item1;

            defender.GetBattleDerkeStatus().hp -= (int)((float)(skill.power + attacker.GetBattleDerkeStatus().attack - defender.GetBattleDerkeStatus().defensive) * 0.8f * effectBonus * evolutionBonus);
        }

 
        string text = "";



        if (isHit)
        {

            for (int i = 0; i < skill.textCode.Count; i++)
            {
                string code = "";
                if (skill.textCode[i] == "attacker" && attacker != null)
                {
                    code = attacker.GetBattleDerke().name;
                }
                else if (skill.textCode[i] == "defender" && defender != null)
                {
                    code = defender.GetBattleDerke().name;
                }
                else if (skill.textCode[i] == "attackName" && skill != null)
                {
                    code = skill.moveTextName;
                }
                else
                {
                    code = skill.textCode[i];
                }

                text += code;

            }

            


        }
        else
        {
            text = attacker.GetBattleDerke().name + "の" + skill.moveTextName + "は外れたようだ...";
        }

        if (attacker.BattleDerke != null && !attacker.GetBattleDerkeStatus().isAbility && skill.needAbility)
        {
            text = attacker.GetBattleDerke().name + "の" + skill.moveTextName + "は呪いによって効果が無効化された...";
        }

        if (defender != null && defender.GetBattleDerkeStatus() != null && isHit && effect != null && evolution != null)
        {
           
            if (effect.Item2 != "")
            {
                text += effect.Item2;
                defender.GetBattleDerkeStatus().effection = effect.Item2;
            }
            if (evolution.Item2 != "")
            {               
                text += evolution.Item2;
            }
        }
       

        BattleTextManager.btm.SetText(text);


        if (isServer && attacker.GetBattleDerkeStatus().isAbility)
        {
            ActiveEffect(SkillDictionary.Timing.AFTERATTACK, skill, attacker, defender);
        }

        if (skill.type != TypeMap.SpecificType.OTHER)
        {
            defender.GetBattleDerkeStatus().type = skill.type;
        }

        if (defender.GetBattleDerkeStatus() != null)
        {
            if (defender.GetBattleDerkeStatus().hp <= 0)
            {
                defender.GetBattleDerkeStatus().isAlive = false;
                Invoke("ServerTurnEnd", 3f);
            }
        }

        if (isSecond)
        {
            Invoke("ServerTurnEnd", 3f);

        }

    }

    [Server]
    public void ServerTurnEnd()
    {

        player1Ready = false;
        player2Ready = false;
        RpcTurnEnd();
    }

    [ClientRpc]
    public void RpcTurnEnd()
    {
        CommandUI.cmdUI.SetCanPress(true);
        ChangeBattleDerke.cbd.SetCanPress(true);
    }

    public void ActiveEffect(SkillDictionary.Timing timing , SkillDictionary skill, PlayerData attacker, PlayerData defender)
    {
        if (skill != null)
        {
            for (int i = 0; i < skill.callMethod.Count; i++)
            {
                if (skill.callTiming[i] == timing)
                {
                    string first = skill.methodValue1[i];
                    string second = skill.methodValue2[i];
                    string third = skill.methodValue3[i];

                    if (skill.methodValue2[i] == "myHp")
                    {
                        second = attacker.GetBattleDerkeStatus().hp.ToString();
                    }
                    else if (skill.methodValue2[i] == "-myHp")
                    {
                        second = (attacker.GetBattleDerkeStatus().hp * -1).ToString();
                    }
                    else if (skill.methodValue2[i] == "enemyHp")
                    {
                        second = defender.GetBattleDerkeStatus().hp.ToString();
                    }
                    else if (skill.methodValue2[i] == "-enemyHp")
                    {
                        second = (defender.GetBattleDerkeStatus().hp * -1).ToString();
                    }
                    if (skill.methodValue3[i] == "myId")
                    {
                        third = attacker.netId.ToString();
                    }
                    else if (skill.methodValue3[i] == "enemyId")
                    {
                        third = defender.netId.ToString();
                    }


                    InvokeMethodByName(skill.callMethod[i], first, second, third);
                    string text = "";

                    for (int index = 0; index < skill.abilityTextCode.Count; index++)
                    {
                        string code = "";
                        if (skill.abilityTextCode[i].list[index] == "attacker" && attacker != null)
                        {
                            code = attacker.GetBattleDerke().name;
                        }
                        else if (skill.abilityTextCode[i].list[index] == "defender" && defender != null)
                        {
                            code = defender.GetBattleDerke().name;
                        }
                        else if (skill.abilityTextCode[i].list[index] == "attackName" && skill != null)
                        {
                            code = skill.moveTextName;
                        }
                        else
                        {
                            code = skill.abilityTextCode[i].list[index];
                        }

                        text += code;

                    }

                    if (attacker.BattleDerke != null && !attacker.GetBattleDerkeStatus().isAbility && skill.needAbility)
                    {
                        text = attacker.GetBattleDerke().name + "の" + skill.moveTextName + "は呪いによって効果が無効化された...";
                    }

                    BattleTextManager.btm.SetText(text);

                }
            }
        }
    }


    public void Nothing()
    {
        return;
    }
    public async UniTask SetBattleDerke(string number, string noNeed, string ID)
    {

        if (NetworkServer.spawned.TryGetValue(uint.Parse(ID), out NetworkIdentity identity))
        {

            PlayerData pd = identity.GetComponent<PlayerData>();
            if (pd.GetBattleDerke() != null)
            {
                bool canEscape = pd.GetBattleDerkeStatus().canEscape;

                if (!canEscape && pd.GetBattleDerkeStatus().hp > 0)
                {
                    BattleTextManager.btm.SetText(pd.GetBattleDerke().name + "は交代できない！");
                    return;                
                }
               
            }
            pd.IfServerSetBattleDerke(number);
        }

    }

    

    public async UniTask SetStatus(string status, string value, string ID)
    {
        await UniTask.Delay(100);

        if (NetworkServer.spawned.TryGetValue(uint.Parse(ID), out NetworkIdentity identity))
        {
            DerkeStatus derkeStatus = identity.GetComponent<PlayerData>().GetBattleDerkeStatus();

            if (status == "maxHp")
                derkeStatus.maxHp += int.Parse(value);

            if (status == "hp")
            {
                int hpValue = int.Parse(value);

                if (derkeStatus.hp + hpValue > derkeStatus.maxHp)
                {
                    hpValue = derkeStatus.maxHp - derkeStatus.hp;
                }
                derkeStatus.hp += hpValue;
            }

            if (status == "speed")
                derkeStatus.speed += int.Parse(value);

            if (status == "attack")
                derkeStatus.attack += int.Parse(value);

            if (status == "defensive")
                derkeStatus.defensive += int.Parse(value);

            if (status == "accuracy")
                derkeStatus.accuracy += int.Parse(value);

            if (status == "isAbility")
                derkeStatus.isAbility = Convert.ToBoolean(value);

            if (status == "canEscape")
                derkeStatus.canEscape = Convert.ToBoolean(value);

            if (status == "effection")
                derkeStatus.effection = value;

        }

    }

    public async UniTask RandomDamage(string value, string maxCount, string ID)
    {
        await UniTask.Delay(100);
        StartCoroutine(RandomCoroutine(value, maxCount, ID));
    }


    public IEnumerator RandomCoroutine(string value, string maxCount, string ID)
    {
        if (NetworkServer.spawned.TryGetValue(uint.Parse(ID), out NetworkIdentity identity))
        {
            for (int count = 0; count < UnityEngine.Random.Range(1, int.Parse(maxCount) + 1); count++)
            {
                identity.GetComponent<PlayerData>().GetBattleDerkeStatus().hp += int.Parse(value);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

}





    public class BattlePlayerData : NetworkBehaviour
{
    public NetworkIdentity netIdentity;
    public string selectedMove;
    public int speed;
    public int hp;
    public bool isReady = false;
}
