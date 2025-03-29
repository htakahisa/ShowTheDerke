using Mirror;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class CustomNetworkManager : NetworkManager
{
    private int playersInLobby = 0;
    public int requiredPlayers = 2; // �K�v�ȃv���C���[��
    public string battleSceneName = "BattleScene"; // �o�g���V�[����

    // �N���C�A���g�̐ڑ���ۑ��i�V�[���J�ڌ�Ƀv���C���[���X�|�[�����邽�߁j
    private List<NetworkConnectionToClient> pendingConnections = new List<NetworkConnectionToClient>();

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name == battleSceneName)
        {
            // �o�g���V�[���Ȃ炷���X�|�[��
            SpawnPlayer(conn);
        }
        else
        {
            // ���r�[�ł̓X�|�[�������A���X�g�ɒǉ�
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
        yield return new WaitForSeconds(0f); // �����ҋ@�i���o�p�j

        // �V�[���ڍs�O�ɐڑ����Ă���N���C�A���g��ۑ�
        pendingConnections.Clear();
        foreach (var conn in NetworkServer.connections.Values)
        {
            pendingConnections.Add(conn);
        }

        // �o�g���V�[���ֈڍs
        ServerChangeScene(battleSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == battleSceneName)
        {
            Debug.Log("Battle scene loaded. Spawning players...");

            // �ۗ����Ă����v���C���[���X�|�[��
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
