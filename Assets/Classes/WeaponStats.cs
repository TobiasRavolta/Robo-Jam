using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/WeaponStats")]

public class WeaponStats : ScriptableObject
{
    public SerializableDictionary<WeaponStatTypes, float> stats = new SerializableDictionary<WeaponStatTypes, float>();
    public WeaponDamageTypes weaponDamageType;
    public Sprite projectileSprite;
}

public enum WeaponStatTypes
{
    damageValue,
    projectileSpeed,
    projectileSize,
    travelTime,
};

public enum WeaponDamageTypes
{
    physical,
    fire,
    ice,
    electrical
};
