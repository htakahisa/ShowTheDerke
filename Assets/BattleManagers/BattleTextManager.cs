using TMPro;
using UnityEngine;

public class BattleTextManager : MonoBehaviour
{

    public TextMeshProUGUI Text;

    public static BattleTextManager btm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        btm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string _text)
    {
        Text.text = _text;
    }

}
