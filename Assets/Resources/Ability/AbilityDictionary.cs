using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability/Create New Ability")]
public class AbilityDictionary : ScriptableObject
{
    public string moveName;     // 技名
    public List<Timing> callTiming;   //処理に使うタイミング
    public List<SkillDictionary> skill;  //処理に使うスキル
    public List<string> textCode; //テキストの構成
    public string moveTextName; //テキストの構成
    public int power;           // 威力
    public int accuracy;        // 命中率
    public int speed;
    public TypeMap.SpecificType type;         // タイプ (例: 炎, 水, 草)
    public bool needAbility = false;

    public enum Timing
    {
        CHANGEDERKE,
        ENTRY,
        TURNORDER,
        BEFOREATTACK,
        AFTERATTACK,
    }
}
