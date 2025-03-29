using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{

    private static Dictionary<string, SkillDictionary> moveDict = new Dictionary<string, SkillDictionary>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        LoadMoves();
        //TestMove("ThunderBolt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TestMove(string name)
    {
        SkillDictionary move = GetMove(name);
        if (move != null)
        {
            Debug.Log($"技: {move.moveName}, 威力: {move.power}, 命中率: {move.accuracy}%");
        }
    }


    private void LoadMoves()
    {
        // Resources フォルダから全ての技データをロード
        SkillDictionary[] moves = Resources.LoadAll<SkillDictionary>("Moves");

        foreach (SkillDictionary move in moves)
        {
            if (!moveDict.ContainsKey(move.moveName))
            {
                moveDict.Add(move.moveName, move);
            }
        }
    }

    public static SkillDictionary GetMove(string moveName)
    {
        if (moveDict.TryGetValue(moveName, out SkillDictionary move))
        {
            return move;
        }
        else
        {
            Debug.LogWarning($"Move '{moveName}' not found!");
            return null;
        }
    }
}
