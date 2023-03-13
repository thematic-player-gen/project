using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public int mouse_x;
    public int mouse_y;

    public int mouseCoors;
    public int guiMouseCoors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //use if/else statements?

        //Get mouse coordinates
        mouseCoors = [clamp(mouse_x, TILE_START, 3904 + TILE_START - 1), clamp(mouse_y, TILE_START, 2048 + TILE_START - 1)];
        guiMouseCoors = mapToGui([mouse_x, mouse_y]);


        //Mouse input
        //if clicked
        Input.GetMouseButtonDown(0);
        //if held
        Input.GetMouseButton(0);
        //if release
        Input.GetMouseButtonUp(0);

        //Key input
        Input.GetKeyDown(KeyCode.D);
        Input.GetKeyDown(KeyCode.A);
        Input.GetKeyDown(KeyCode.W);
        Input.GetKeyDown(KeyCode.S);

        Input.GetKeyDown(KeyCode.Space);
    }
}
