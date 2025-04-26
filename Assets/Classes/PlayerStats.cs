using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PlayerStats")]

public class PlayerStats : ScriptableObject
{
    public SerializableDictionary<PlayerStatTypes, float> stats = new SerializableDictionary<PlayerStatTypes, float>();

}

public enum PlayerStatTypes
{
    walkSpeed,
    sprintSpeed,
    jumpForce,
    armor,
    size,
    maxHP,
};
