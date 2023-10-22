using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TileBar : MonoBehaviour
{
    public static TileBar Instance { get; private set; }

    [SerializeField] private List<Tile> tiles;
    [SerializeField] private List<Transform> transforms;

    private Dictionary<int, int> tileValueCount= new Dictionary<int, int>();

    private int currentSlot = 0;
    private int currentPoint = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentPoint == GameManager.Instance.CurrentLevelPoint())
        {
            GameManager.Instance.GameWinning();
        }

    }

    private void CheckTiles(int tileValue)
    {
        if (!tileValueCount.ContainsKey(tileValue)) 
        {
            tileValueCount.Add(tileValue, 1);
        }
        else
        {
            tileValueCount[tileValue] = tileValueCount[tileValue] + 1;
            if (tileValueCount[tileValue] == 3) 
            {
                currentPoint = currentPoint + 3;
                // remove tile
                tileValueCount.Remove(tileValue);

                // remove in list tile
                tiles.ForEach(t =>
                {
                    if (t.GetTileValue() == tileValue)
                    {
                        t.HideGameObject();
                    }
                });

                // Reallocate in array of tilebar
                tiles = tiles.Where(x => x.GetTileValue() != tileValue).ToList();
            }
        }


        currentSlot = tiles.Count - 1 < 0 ? 0 : tiles.Count - 1;
    }

    private void ReAllocateTiles()
    {
        tiles = tiles.OrderBy(x => x.GetTileValue()).ToList();
        int count = 0;
        foreach (Tile tile in tiles)
        {
            tile.GetComponent<Tile>().SetTargetPosition(transforms[count].position);
            count++;
        }
    }

    private void CheckGameOver()
    {
        if (tiles.Count == 7)
        {
            // Fire event game over
            GameManager.Instance.GameOver();
        }
    }

    public void AddTile(Tile tile)
    {
        if (tiles == null) { return; }

        if (tile.IsSelected()) { return; }

        // Add tile into tilebar
        tiles.Add(tile);
        tile.SetIsSelected();
        tile.ActiveKinematic();
        //tile.transform.position = transforms[currentSlot].position;
        tile.GetComponent<Tile>().SetTargetPosition(transforms[currentSlot].position);
        tile.transform.rotation = Quaternion.identity;
        currentSlot++;

        CheckTiles(tile.GetTileValue());
        ReAllocateTiles();
        CheckGameOver();
    }
}
