using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ObjectChance
{
    public TileInfo_SBO Tile;
    public int Chance;
}

[CreateAssetMenu(fileName = "MapLevel", menuName = "ScriptableObjects/MapLevel", order = 2)]
public class MapLevel_SBO : ScriptableObject
{
    public int Level;
    public string LevelName;
    public int TotalTiles;
    public int TotalTime;
    [NonReorderable]
    public List<ObjectChance> AppearObjects = new List<ObjectChance>();
}
