using System;
using System.Collections.Generic;
using UnityEngine;


public class TypeMap
{

    private List<Tuple<SpecificType, SpecificType, string>> effectList = new List<Tuple<SpecificType, SpecificType, string>>();
    private List<Tuple<SpecificType, string, string>> evolutionList = new List<Tuple<SpecificType, string, string>>();

    public TypeMap()
    {


        effectList.Add(Tuple.Create(SpecificType.THUNDER, SpecificType.WATER, "yŠ´“dz"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.WATER, "yö”­z"));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.WATER, "y“€Œ‹z"));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.WATER, "y‰Í“¶z"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.THUNDER, "y‚•‰‰×z"));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.THUNDER, "y’´“`“±z"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.FIRE, "y’Á‰Îz"));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.FIRE, "y–Ò‰Îz"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.FIRE, "y—ó‰Îz"));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.FIRE, "y‹S‰Îz"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.ICE, "y—n‰ğz"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.ICE, "yŠÃ‰Jz"));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.ICE, "yá—z"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.ROCK, "yNHz"));

        effectList.Add(Tuple.Create(SpecificType.ROCK, SpecificType.ROCK, "y•ö‰óz"));

        effectList.Add(Tuple.Create(SpecificType.ICE, SpecificType.WIND, "yèÂz"));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.WIND, "y—³Šªz"));

        effectList.Add(Tuple.Create(SpecificType.LEAF, SpecificType.WIND, "y•——Ñz"));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.WIND, "yŠ™êŒz"));

        effectList.Add(Tuple.Create(SpecificType.WATER, SpecificType.LEAF, "yŠJ‰Ôz"));

        effectList.Add(Tuple.Create(SpecificType.FIRE, SpecificType.LEAF, "y”RÄz"));

        effectList.Add(Tuple.Create(SpecificType.WIND, SpecificType.LEAF, "y•—‰Ôz"));

        effectList.Add(Tuple.Create(SpecificType.PAIMON, SpecificType.PAIMON, "y‰ƒz"));

        evolutionList.Add(Tuple.Create(SpecificType.WIND, "yŠÃ‰Jz", "y–P™€ŠÃ‰Jz"));

        evolutionList.Add(Tuple.Create(SpecificType.FIRE, "yŠÃ‰Jz", "yˆ¨àÕ—ØŒçz"));

        evolutionList.Add(Tuple.Create(SpecificType.WATER, "yŠJ‰Ôz", "yŒ…ŠJ‰Ôz"));

        evolutionList.Add(Tuple.Create(SpecificType.FIRE, "y—ó‰Îz", "yŒFŒF‰Š‰Šz"));

        evolutionList.Add(Tuple.Create(SpecificType.FIRE, "y•——Ñz", "y•——é‰Øœğz"));

        evolutionList.Add(Tuple.Create(SpecificType.WIND, "y—³Šªz", "y•V”ò•T—´z"));

        evolutionList.Add(Tuple.Create(SpecificType.PAIMON, "y‰ƒz", "yÌƒm—d‰ö‘åW‡z"));

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
        PAIMON,
        OTHER,
    }



}
