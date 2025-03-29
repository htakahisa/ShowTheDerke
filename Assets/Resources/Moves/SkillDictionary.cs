using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewMove", menuName = "Move/Create New Move")]
public class SkillDictionary : ScriptableObject
{
    public string moveName;     // �Z��
    public List<string> callTiming;   //�����Ɏg���^�C�~���O�W
    public List<string> callMethod;   //�����Ɏg�����\�b�h���W
    public List<string> methodValue1;   //���\�b�h�̑�����
    public List<string> methodValue2;   //���\�b�h�̑�����
    public List<string> methodValue3;   //���\�b�h�̑�O����
    public List<string> textCode; //�e�L�X�g�̍\��
    public string moveTextName; //�e�L�X�g�̍\��
    public int power;           // �З�
    public int accuracy;        // ������
    public int pp;              // �Z�̉�
    public int speed;
    public TypeMap.SpecificType type;         // �^�C�v (��: ��, ��, ��)
    public bool needAbility = false;

}