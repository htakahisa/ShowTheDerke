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
        // ���݂̃V�[���ɂ��邷�ׂĂ� NetworkIdentity ���擾
        var players = FindObjectsOfType<NetworkIdentity>()
                      .Where(p => p.CompareTag("Player")) // Player �^�O�����Ă���I�u�W�F�N�g�̂ݑΏ�
                      .ToList();

        if (players.Count != 2)
        {
            Debug.LogWarning("�v���C���[�̐���2�l�ł͂���܂���");
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
