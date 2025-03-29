using Mirror;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class CustomNetworkManager : NetworkManager
{
    private int playersInLobby = 0;
    public int requiredPlayers = 2; // 必要なプレイヤー数
    public string battleSceneName = "BattleScene"; // バトルシーン名

    // クライアントの接続を保存（シーン遷移後にプレイヤーをスポーンするため）
    private List<NetworkConnectionToClient> pendingConnections = new List<NetworkConnectionToClient>();

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name == battleSceneName)
        {
            // バトルシーンならすぐスポーン
            SpawnPlayer(conn);
        }
        else
        {
            // ロビーではスポーンせず、リストに追加
            pendingConnections.Add(conn);
            playersInLobby++;

            Debug.Log($"Player joined. Total players: {playersInLobby}");

            if (playersInLobby >= requiredPlayers)
            {
                StartCoroutine(StartBattle());
            }
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        playersInLobby--;
        pendingConnections.Remove(conn);

        Debug.Log($"Player left. Total players: {playersInLobby}");
    }

    private IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(0f); // 少し待機（演出用）

        // シーン移行前に接続しているクライアントを保存
        pendingConnections.Clear();
        foreach (var conn in NetworkServer.connections.Values)
        {
            pendingConnections.Add(conn);
        }

        // バトルシーンへ移行
        ServerChangeScene(battleSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == battleSceneName)
        {
            Debug.Log("Battle scene loaded. Spawning players...");

            // 保留していたプレイヤーをスポーン
            foreach (var conn in pendingConnections)
            {
                SpawnPlayer(conn);
            }
            pendingConnections.Clear();
        }
    }

    private void SpawnPlayer(NetworkConnectionToClient conn)
    {
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
        Debug.Log($"Spawned player for connection {conn.connectionId}");
    }
}
