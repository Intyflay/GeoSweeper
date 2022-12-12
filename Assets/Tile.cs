using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{

    //experimental: 
    private Material material;
   // private Material material;

    private bool isMine = false;
    private bool isFlagged = false;
    private bool isRevealed = false;
    private Transform tileTransform;
    private Mesh mesh;

    private Vector3[] vertices;
    

    private HashSet<Tile> neighbours = new();
    private int mineNeighbours = 0;

    public Tile(Transform self) {
        tileTransform = self;
        mesh = tileTransform.GetComponent<MeshFilter>().mesh;
        material = tileTransform.GetComponent<MeshRenderer>().material;
        
    }
    
    public void onPopulate() {
        if (isMine) {material.SetFloat("_Mine", 1f);}

        float heat;
        foreach (Tile neighbour in neighbours) {
            if (neighbour.isMine) {mineNeighbours++;}
        }
        heat = (float)mineNeighbours/(float)neighbours.Count;
        material.SetFloat("_Heat", heat);
    }


    public bool IsMine => isMine;
    public bool SetMine() {
        bool success = !isMine;
        isMine = true;
        return success;
    }

    public bool IsFlagged => isFlagged;
    public void ToggleFlagged() {
        if(isRevealed) {return;}
        isFlagged = !isFlagged;

        material.SetFloat("_Flagged", isFlagged?1f:0f);
    }

    public void ClearZeroTiles() {
        if (isRevealed) {return;} 
        if (mineNeighbours !=0) {foreach (Tile neighbour in neighbours) {ToggleRevealed();} return;}

        ToggleRevealed();

        foreach (Tile neighbour in neighbours) {neighbour.ClearZeroTiles();}
    }

    public bool IsRevealed => isRevealed;
    public void ToggleRevealed() {
        if (isFlagged) {return;}
        
        isRevealed = true;
        material.SetFloat("_Revealed", 1f);
    }

    public Vector3[] Vertices => mesh.vertices;
    public void AddNeighbour(Tile neighbour) => neighbours.Add(neighbour);
    public Transform TileTransform => tileTransform;
    public HashSet<Tile> Neighbours => neighbours;
    
}
