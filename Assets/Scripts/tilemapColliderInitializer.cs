using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemapColliderInitializer : MonoBehaviour
{
    private Tilemap walls;
    public GameObject obstacle;
    // Use this for initialization
    void Start()
    {
        walls = GetComponent<Tilemap>();
        BoundsInt bounds = walls.cellBounds;
        TileBase[] allTiles = walls.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; ++x)
        {
            for (int y = 0; y < bounds.size.y; ++y)
            {
                Tile tile = (Tile)allTiles[x + y * bounds.size.x];
                if (tile)
                {
                    Debug.Log(new Vector2(x, y).ToString() + "\n" + tile.name);
                }
            }
        }
        //Instantiate(obstacle, walls.GetCellCenterWorld(t.transform));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
