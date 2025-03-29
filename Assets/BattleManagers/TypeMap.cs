using System;
using System.Collections.Generic;
using UnityEngine;


public class TypeMap
{

    private List<Tuple<SpecificType, SpecificType, string>> effectList = new List<Tuple<SpecificType, SpecificType, string>>();
    private List<Tuple<SpecificType, string, string>> evolutionList = new List<Tuple<SpecificType, string, string>>();

    public TypeMap()
    {


        effectList.Add(Tuple.Create(SpecificType.THUNDER, SpecificType.WATER, "�y���d�z"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.WATER, "�y�����z"));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.WATER, "�y�����z"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.THUNDER, "�y�����ׁz"));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.THUNDER, "�y���`���z"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.FIRE, "�y���΁z"));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.FIRE, "�y�҉΁z"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.FIRE, "�y�F�F��΁z"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.ICE, "�y�n���z"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.ICE, "�y�ÉJ�z"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.ROCK, "�y�N�H�z"));

        effectList.Add(Tuple.Create(SpecificType.ROCK, SpecificType.ROCK, "�y����z"));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.WIND, "�y�z"));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.WIND, "�y�����z"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.LEAF, "�y�J�ԁz"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.LEAF, "�y�R�āz"));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.LEAF, "�y���ԁz"));

        evolutionList.Add(Tuple.Create(SpecificType.WIND, "�y�ÉJ�z", "�y���ωJ�z"));

        evolutionList.Add(Tuple.Create(SpecificType.WATER, "�y�J�ԁz", "�y�����J�ԁz"));
    }



    public Tuple<float, string> getEffect(SpecificType fromType, SpecificType toType)
    {
        Tuple<SpecificType, SpecificType> valueTuple = Tuple.Create(fromType, toType);
        
        foreach (Tuple<SpecificType, SpecificType, string> t in effectList)
        {
            if (t.Item1 == valueTuple.Item1 && t.Item2 == valueTuple.Item2)
            {
                return Tuple.Create(1.5f, t.Item3);
            }
        }

        return Tuple.Create(1.0f, "");
    }

    public Tuple<float, string> getEvolution(SpecificType fromType, string toEffect)
    {
        Tuple<SpecificType, string> valueTuple = Tuple.Create(fromType, toEffect);

        foreach (Tuple<SpecificType, string, string> t in evolutionList)
        {
            if (t.Item1 == valueTuple.Item1 && t.Item2 == valueTuple.Item2)
            {
                return Tuple.Create(2f, t.Item3);
            }
        }

        return Tuple.Create(1.0f, "");
    }

    public enum SpecificType
    {
        WATER,
        THUNDER,
        FIRE,
        ICE,
        ROCK,
        WIND,
        LEAF,
        OTHER,
    }



}
