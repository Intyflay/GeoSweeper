using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Camera cam;
    // Start is called before the first frame update

    static Tile HoveredTile() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);
        RaycastHit hit;
        bool successfulHit = Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity);
        if(successfulHit) {
            
        }
        return Manager.GetTileFromCollider(hit.collider);
    }

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame

    Vector3 pixelCoordinates;
    Vector3 quadrantCoordinates;
    Vector3 resolution;
    Vector3 cameraSpeed = new Vector3(5, 5);
    void Update()
    {
        resolution = new Vector3(Screen.width, Screen.height);
        pixelCoordinates = Input.mousePosition;
        quadrantCoordinates = pixelCoordinates - (resolution/2);
        quadrantCoordinates.Normalize();
        
        if (pixelCoordinates.x > Screen.width - 200 || pixelCoordinates.x < 200 || pixelCoordinates.y > Screen.height - 100 || pixelCoordinates.y < 100) {
             transform.position += Vector3.Scale(quadrantCoordinates,cameraSpeed)  * Time.deltaTime;
        }


        
        if (Input.GetMouseButtonDown(0)) { //left mouse button
            HoveredTile().ClearZeroTiles();
            HoveredTile().ToggleRevealed();
        } else if (Input.GetMouseButtonDown(1)) { //right mouse button
            HoveredTile().ToggleFlagged();
        }
        
        
        

    }
}
