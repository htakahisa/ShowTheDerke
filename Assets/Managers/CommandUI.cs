using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CommandUI : MonoBehaviour
{
    public Button attackButton1; // 技1のボタン
    public Button attackButton2; // 技2のボタン
    public Button attackButton3; // 技3のボタン
    public Button attackButton4; // 技4のボタン

    private PhaseManager battleManager;
    private PlayersData playerData;

    public static CommandUI cmdUI;

    private void Awake()
    {
        cmdUI = this;
    }

    public void Initialize(PhaseManager manager, PlayersData player)
    {
        battleManager = manager;
        playerData = player;

        attackButton1.onClick.AddListener(() => OnAttackButtonPressed("Thunderbolt"));
        attackButton2.onClick.AddListener(() => OnAttackButtonPressed("Flamethrower"));
        attackButton3.onClick.AddListener(() => OnAttackButtonPressed("Water Gun"));
        attackButton4.onClick.AddListener(() => OnAttackButtonPressed("Tackle"));
    }

    private void OnAttackButtonPressed(string move)
    {
        if (playerData.isOwned)
        {
            playerData.CmdSelectMove(playerData.netIdentity, move);
        }
    }
}
