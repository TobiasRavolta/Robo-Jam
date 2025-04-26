using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/WeaponStats")]

public class WeaponStats : ScriptableObject
{
    public SerializableDictionary<WeaponStatTypes, float> entityStats = new SerializableDictionary<WeaponStatTypes, float>();
    public WeaponDamageTypes weaponDamageType;
    public Sprite projectileSprite;
}

public enum WeaponStatTypes
{
    damageValue,
    projectileSpeed,
    projectileSize,
};

public enum WeaponDamageTypes
{
    fire,
    physical,
    ice,
    electrical
};
