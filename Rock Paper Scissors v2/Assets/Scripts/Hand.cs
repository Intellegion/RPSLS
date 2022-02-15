using System;
using UnityEngine;

public enum Type
{
    Rock,
    Paper,
    Scissors,
    Lizard,
    Spock
}

[CreateAssetMenu(fileName = "Hand", menuName = "Hands/Hand")]
public class Hand : ScriptableObject
{
    public Type HandType;

    public Type[] VictoryTypes;
}
