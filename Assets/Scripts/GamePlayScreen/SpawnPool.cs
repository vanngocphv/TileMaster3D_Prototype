using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    private List<ObjectChance> objectChances= new List<ObjectChance>();
    private MapLevel_SBO currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        // Get current Level setting data from gamemanager
        currentLevel = GameManager.Instance.totalMaps.AllMap[GameManager.Instance.currentLevel];
        objectChances = currentLevel.AppearObjects;

        foreach(ObjectChance item in objectChances)
        {
            int chance = item.Chance;
            for (int count = 0; count < chance * 3; count++) {
                Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-1.7f, 1.7f), this.transform.position.y + UnityEngine.Random.Range(-0.4f, 3f), UnityEngine.Random.Range(-1.5f, 2.5f));
                Quaternion randomRotation = new Quaternion(0, UnityEngine.Random.Range(-1f, 1f), 0, 1);
                Transform gameObject = Instantiate(item.Tile.TileObject, randomPosition, randomRotation);
                gameObject.gameObject.GetComponent<Tile>().SetTileValue(item.Tile.TileValue);
                gameObject.gameObject.GetComponent<Tile>().SetTileImage(item.Tile.TileSprite);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
