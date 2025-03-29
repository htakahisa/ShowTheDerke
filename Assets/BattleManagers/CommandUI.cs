using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections.Generic;
using TMPro;

public class CommandUI : NetworkBehaviour
{
    public Button attackButton1; // ‹Z1‚Ìƒ{ƒ^ƒ“
    public Button attackButton2; // ‹Z2‚Ìƒ{ƒ^ƒ“
    public Button attackButton3; // ‹Z3‚Ìƒ{ƒ^ƒ“
    public Button attackButton4; // ‹Z4‚Ìƒ{ƒ^ƒ“

    private PlayerData playerData;

    public static CommandUI cmdUI;


    private void Awake()
    {
        cmdUI = this;
        Invoke("StartGetPlayerData", 0.1f);
    }

    void StartGetPlayerData()
    {
        playerData = Player.LocalPlayer.GetComponent<PlayerData>();
    }


    public void SetSkill()
    {
        List<string> skills = playerData.GetBattleDerkeStatus().skill;

        attackButton1.onClick.RemoveAllListeners();
        attackButton2.onClick.RemoveAllListeners();
        attackButton3.onClick.RemoveAllListeners();
        attackButton4.onClick.RemoveAllListeners();

        attackButton1.onClick.AddListener(() => OnAttackButtonPressed(skills[0]));
        attackButton2.onClick.AddListener(() => OnAttackButtonPressed(skills[1]));
        attackButton3.onClick.AddListener(() => OnAttackButtonPressed(skills[2]));
        attackButton4.onClick.AddListener(() => OnAttackButtonPressed(skills[3]));

        attackButton1.GetComponentInChildren<TextMeshProUGUI>().text = skills[0];
        attackButton2.GetComponentInChildren<TextMeshProUGUI>().text = skills[1];
        attackButton3.GetComponentInChildren<TextMeshProUGUI>().text = skills[2];
        attackButton4.GetComponentInChildren<TextMeshProUGUI>().text = skills[3];
    }

    private void OnAttackButtonPressed(string move)
    {
        if (playerData.isOwned && playerData.GetBattleDerkeStatus().isAlive)
        {
            SetCanPress(false);
            ChangeBattleDerke.cbd.SetCanPress(false);
            playerData.CmdSelectMove(playerData.netIdentity, move);
        }
    }

    public void SetCanPress(bool canPress)
    {
        attackButton1.gameObject.SetActive(canPress);
        attackButton2.gameObject.SetActive(canPress);
        attackButton3.gameObject.SetActive(canPress);
        attackButton4.gameObject.SetActive(canPress);
    }

}
