using UnityEngine;
using Mirror;

public class PlayersData : NetworkBehaviour
{
    public NetworkIdentity netIdentity;
    public string selectedMove;
    public int speed;
    public int hp;
    public bool isReady = false;
    private CommandUI cmdUI;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        cmdUI = CommandUI.cmdUI;
        cmdUI.Initialize(PhaseManager.pm, this);
    }

    [Command]
    public void CmdSelectMove(NetworkIdentity player, string moveName)
    {
        if (!isReady)
        {
            selectedMove = moveName;
            isReady = true;
            PhaseManager.pm.CmdSelectMove(player, moveName);
        }
    }
}
