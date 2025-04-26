using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PlayerStats")]

public class PlayerStats : ScriptableObject
{
    public SerializableDictionary<PlayerStatTypes, float> entityStats = new SerializableDictionary<PlayerStatTypes, float>();

}

public enum PlayerStatTypes
{
    speed,
    jumpHeight,
    armor,
    size,
    maxHP,
};
