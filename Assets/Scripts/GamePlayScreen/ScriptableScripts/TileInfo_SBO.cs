using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/TileObjects", order = 1)]
public class TileInfo_SBO : ScriptableObject
{
    public string TileName;
    public Transform TileObject;
    public Sprite TileSprite;
    public int TileValue;
}
