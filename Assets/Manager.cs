using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Manager : MonoBehaviour
{
    static Tile[] tiles;

    //helper functions:
    static bool HasSharedVertices(Tile tileA, Tile tileB) {
        foreach (Vector3 vertexA in tileA.Vertices) {
            foreach (Vector3 vertexB in tileB.Vertices) {
                if (tileA.TileTransform.TransformPoint(vertexA) == tileB.TileTransform.TransformPoint(vertexB)) {
                    return true;
                }
            }
        }
        return false;
    }
    
    public static Tile GetTileFromCollider(Collider colliderA) {
        foreach(Tile tile in tiles) {
            Collider colliderB = tile.TileTransform.gameObject.GetComponent<Collider>();
            if (colliderA == colliderB) {
                return tile;
            }
        }
        Debug.LogWarning("null returned from GetTileFromCollider");
        return null;
    }

    //main logic:
    void Start()
    {
        
        int numberOfMines = 6;
        tiles = new Tile[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            tiles[i] = new Tile(transform.GetChild(i));
        }
        
        foreach (Tile tileA in tiles) {
            foreach (Tile tileB in tiles) {
                if (tileA == tileB) {
                    continue;
                }
                
                if (HasSharedVertices(tileA,tileB)) {
                    tileA.AddNeighbour(tileB);
                    tileB.AddNeighbour(tileA);
                }
            }
        }
        
        while (numberOfMines >= 1) {
            Tile tile = tiles[Random.Range(0,tiles.Length)];
            if (tile.SetMine() ) { //decrement if the tile was not already a mine
                numberOfMines--;
            }
        }

        foreach (Tile tile in tiles) {
             tile.onPopulate();
        }
        
        
    }
    
    

}
