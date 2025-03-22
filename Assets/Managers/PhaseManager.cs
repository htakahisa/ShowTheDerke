using UnityEngine;
using Mirror;

public class PhaseManager : NetworkBehaviour
{
    [SyncVar] private bool player1Ready = false;
    [SyncVar] private bool player2Ready = false;

    private PlayerData player1;
    private PlayerData player2;

    public static PhaseManager pm;

    private void Awake()
    {
        pm = this;
    }

    public void InitializeBattle(PlayerData p1, PlayerData p2)
    {
        player1 = p1;
        player2 = p2;
        player1Ready = false;
        player2Ready = false;
    }

    [Command]
    public void CmdSelectMove(NetworkIdentity player, string moveName)
    {
        PlayerData playerData = player == player1.netIdentity ? player1 : player2;
        playerData.selectedMove = moveName;
        playerData.isReady = true;

        if (player == player1.netIdentity)
            player1Ready = true;
        else
            player2Ready = true;

        if (player1Ready && player2Ready)
        {
            ResolveTurnOrder();
        }
    }

    [Server]
    private void ResolveTurnOrder()
    {
        player1Ready = false;
        player2Ready = false;

        PlayerData first, second;

        if (player1.speed > player2.speed)
        {
            first = player1;
            second = player2;
        }
        else if (player1.speed < player2.speed)
        {
            first = player2;
            second = player1;
        }
        else
        {
            first = Random.Range(0, 2) == 0 ? player1 : player2;
            second = first == player1 ? player2 : player1;
        }

        ExecuteTurn(first, second);
    }

    [Server]
    private void ExecuteTurn(PlayerData attacker, PlayerData defender)
    {
        RpcExecuteAttack(attacker.netIdentity, attacker.selectedMove);

        if (defender.hp > 0)
        {
            RpcExecuteAttack(defender.netIdentity, defender.selectedMove);
        }
    }

    [ClientRpc]
    private void RpcExecuteAttack(NetworkIdentity player, string move)
    {
        Debug.Log($"{player.name} used {move}!");
        // ここでダメージ計算やアニメーションを実装
    }
}

public class PlayerData : NetworkBehaviour
{
    public NetworkIdentity netIdentity;
    public string selectedMove;
    public int speed;
    public int hp;
    public bool isReady = false;
}
