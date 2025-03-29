using System.Linq;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public static GameObject LocalPlayer;
    public static GameObject opponent;

    public override void OnStartAuthority ()
    {
        FindPlayers();
    }

    void FindPlayers()
    {
        // 現在のシーンにあるすべての NetworkIdentity を取得
        var players = FindObjectsOfType<NetworkIdentity>()
                      .Where(p => p.CompareTag("Player")) // Player タグがついているオブジェクトのみ対象
                      .ToList();

        if (players.Count != 2)
        {
            Debug.LogWarning("プレイヤーの数が2人ではありません");
            return;
        }

        foreach (var player in players)
        {
            if (player.isLocalPlayer)
                LocalPlayer = player.gameObject;
            else
                opponent = player.gameObject;
        }

        Debug.Log(LocalPlayer, LocalPlayer.gameObject);
        Debug.Log(opponent, opponent.gameObject);
    }
}
