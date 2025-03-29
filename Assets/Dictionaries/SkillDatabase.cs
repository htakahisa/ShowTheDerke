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
            Debug.Log($"�Z: {move.moveName}, �З�: {move.power}, ������: {move.accuracy}%");
        }
    }


    private void LoadMoves()
    {
        // Resources �t�H���_����S�Ă̋Z�f�[�^�����[�h
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
