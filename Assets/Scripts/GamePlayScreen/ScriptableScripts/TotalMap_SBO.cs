using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLevel", menuName = "ScriptableObjects/TotalMap", order = 1)]
public class TotalMap_SBO : ScriptableObject
{
    [NonReorderable]
    public List<MapLevel_SBO> AllMap;
}
