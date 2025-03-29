using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewMove", menuName = "Move/Create New Move")]
public class SkillDictionary : ScriptableObject
{
    public string moveName;     // 技名
    public List<string> callTiming;   //処理に使うタイミング集
    public List<string> callMethod;   //処理に使うメソッド名集
    public List<string> methodValue1;   //メソッドの第一引数
    public List<string> methodValue2;   //メソッドの第二引数
    public List<string> methodValue3;   //メソッドの第三引数
    public List<string> textCode; //テキストの構成
    public string moveTextName; //テキストの構成
    public int power;           // 威力
    public int accuracy;        // 命中率
    public int pp;              // 技の回数
    public int speed;
    public TypeMap.SpecificType type;         // タイプ (例: 炎, 水, 草)
    public bool needAbility = false;

}