using System;
using System.Collections.Generic;
using UnityEngine;


public class TypeMap
{

    private List<Tuple<SpecificType, SpecificType, string, float>> effectList = new List<Tuple<SpecificType, SpecificType, string, float>>();
    private List<Tuple<SpecificType, string, string, float>> evolutionList = new List<Tuple<SpecificType, string, string, float>>();

    public TypeMap()
    {


        effectList.Add(Tuple.Create(SpecificType.THUNDER, SpecificType.WATER, "yŠ´“dz", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.WATER, "yö”­z", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.WATER, "y“€Œ‹z", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.WATER, "y‰Í“¶z", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.THUNDER, "y‚•‰‰×z", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.THUNDER, "y’´“`“±z", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.FIRE, "y’Á‰Îz", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.FIRE, "y–Ò‰Îz", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.FIRE, "y—ó‰Îz", 1.3f));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.FIRE, "y‹S‰Îz", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.ICE, "y—n‰ğz", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.ICE, "yŠÃ‰Jz", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.ICE, "yá—z", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.ROCK, "yNHz", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.ROCK, SpecificType.ROCK, "y•ö‰óz", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.WIND, "yèÂz", 1.5f));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.WIND, "y—³Šªz", 1.3f));

        effectList.Add(Tuple.Create(SpecificType.LEAF, SpecificType.WIND, "y•——Ñz", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.WIND, "yŠ™êŒz", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.LEAF, "yŠJ‰Ôz", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.LEAF, "y”RÄz", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.LEAF, "y•—‰Ôz", 1.7f));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.PAIMON, "y‰ƒz", 1.5f));

        evolutionList.Add(Tuple.Create(SpecificType.WIND, "yŠÃ‰Jz", "y–P™€ŠÃ‰Jz", 2.5f));

        evolutionList.Add(Tuple.Create(SpecificType.FIRE, "yŠÃ‰Jz", "yˆ¨àÕ—ØŒçz", 3f));

        evolutionList.Add(Tuple.Create(SpecificType.WATER, "yŠJ‰Ôz", "yŒ…ŠJ‰Ôz", 2.3f));

        evolutionList.Add(Tuple.Create(SpecificType.FIRE, "y—ó‰Îz", "yŒFŒF‰Š‰Šz", 2f));

        evolutionList.Add(Tuple.Create(SpecificType.FIRE, "y•——Ñz", "y•——é‰Øœğz", 2.5f));

        evolutionList.Add(Tuple.Create(SpecificType.WIND, "y—³Šªz", "y•V”ò•T—´z", 2f));

        evolutionList.Add(Tuple.Create(SpecificType.PAIMON, "y‰ƒz", "yÌƒm—d‰ö‘åW‡z", 2f));

    }




    public Tuple<float, string> getEffect(SpecificType fromType, SpecificType toType)
    {
        Tuple<SpecificType, SpecificType> valueTuple = Tuple.Create(fromType, toType);
        
        foreach (Tuple<SpecificType, SpecificType, string, float> t in effectList)
        {
            if (t.Item1 == valueTuple.Item1 && t.Item2 == valueTuple.Item2)
            {
                return Tuple.Create(t.Item4, t.Item3);
            }
        }

        return Tuple.Create(1.0f, "");
    }

    public Tuple<float, string> getEvolution(SpecificType fromType, string toEffect)
    {
        Tuple<SpecificType, string> valueTuple = Tuple.Create(fromType, toEffect);

        foreach (Tuple<SpecificType, string, string, float> t in evolutionList)
        {
            if (t.Item1 == valueTuple.Item1 && t.Item2 == valueTuple.Item2)
            {
                return Tuple.Create(t.Item4, t.Item3);
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
        PAIMON,
        OTHER,
    }



}
