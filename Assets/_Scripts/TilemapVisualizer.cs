using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, corridorTilemap;
    //[SerializeField]
    //private TileBase floorTile, floorTileRoom, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
    //    wallInnerCornerDownLeft, wallInnerCornerDownRight,wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight, wallDiagonalCornerUpLeft, wallDiagonalCornerUpRight;

    [SerializeField]
    private TileBase[] floorTileArray, wallTopArray, wallSideRightArray, wallSideLeftArray, wallBottomArray, wallFullArray,
        wallInnerCornerDownLeftArray, wallInnerCornerDownRightArray, wallDiagonalCornerDownLeftArray, wallDiagonalCornerDownRightArray, wallDiagonalCornerUpLeftArray, wallDiagonalCornerUpRightArray;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTileArray);
    }

    public void PaintCorridorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, corridorTilemap, floorTileArray);
    }

    public void PaintFloorTilesList(List<HashSet<Vector2Int>> floorPositions)
    {
        foreach(var floor in floorPositions)
        {
            PaintTiles(floor, floorTilemap, floorTileArray);
        }
        
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase[] tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTopArray;
        } else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRightArray;
        } else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottomArray;
        } else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeftArray;
        } else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFullArray;
        }


        if (tile != null)
            PaintSingleTile(wallTilemap, tile[Random.Range(0, tile.Length)], position);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase[] tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile[Random.Range(0, tile.Length)], position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        corridorTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase[] tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeftArray;
        } else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRightArray;
        } else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeftArray;
        } else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRightArray;
        } else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeftArray;
        } else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRightArray;
        } else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFullArray;
        } else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottomArray;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile[Random.Range(0, tile.Length)], position);
    }
}
