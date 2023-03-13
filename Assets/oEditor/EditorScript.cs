using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EditorScript : MonoBehaviour
{
    int toolDisplay;

    ControllerScript controllerScript;
    public GameObject controllerContainer;

    CameraScript cameraScript;
    public GameObject cameraContainer;

    // Start is called before the first frame update
    void Start()
    {
        controllerScript = controllerContainer.GetComponent<ControllerScript>();
        cameraScript = cameraContainer.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Tool Display
        var guiCoors = [coorToTile(mapToArray(controllerScript.mouseCoors)[0]), coorToTile(mapToArray(controllerScript.mouseCoors)[1])];
        var guiX = mapToGui(guiCoors)[0];
        var guiY = mapToGui(guiCoors)[1];

        var mouseCoors = mapToGui(controllerScript.mouseCoors);
        var mouseX = mouseCoors[0];
        var mouseY = mouseCoors[1];

        var scale = 1 / getCameraZoom();

        if (toolDisplay == 1)
        {
            var tool = getToolDraw(toolSelected);
            draw_sprite_ext(tool[0], tool[1], mouseX, mouseY, scale, scale, 0, c_white, 0.7);
        }
        else if (toolDisplay == 2)
        {
            var tool = getToolDraw(toolSelected);
            draw_sprite_ext(tool[0], tool[1], guiX, guiY, scale, scale, 0, c_white, 1);
        }
        else if (toolDisplay == 3)
        {
            draw_sprite_ext(sLevelEditorEraser, 0, mouseX, mouseY, scale, scale, 0, c_white, 1);
        }
        else if (toolDisplay == 4)
        {
            draw_sprite_ext(sLevelEditorEraser, 1, guiX, guiY, scale, scale, 0, c_white, 0.5);
        }
    }

    void guiToMap(int coor)
    {
        var xCoor = coor[0];
        var yCoor = coor[1];

        xCoor *= getCameraZoom();
        yCoor *= getCameraZoom();
        xCoor += cameraScript.x - (getCameraSize()[0] / 2);
        yCoor += cameraScript.y - (getCameraSize()[1] / 2);

        return [xCoor, yCoor];
    }

    void mapToGui(int coor)
    {
        var xCoor = coor[0];
        var yCoor = coor[1];

        xCoor += (getCameraSize()[0] / 2) - cameraScript.x;
        yCoor += (getCameraSize()[1] / 2) - cameraScript.y;
        xCoor /= getCameraZoom();
        yCoor /= getCameraZoom();

        return [xCoor, yCoor];
    }

    void mapToArray(int coor)
    {
        return [(coor[0] - TILE_START) div TILE_SIZE, (coor[1] - TILE_START) div TILE_SIZE];
    }

    void coorToTile(int coor)
    {
        return (coor * TILE_SIZE) + (TILE_SIZE / 2) + TILE_START;
    }

    void editorClearAll()
    {
        with(oEditor) {
            assetFade(assetLevelEditorTopBar, 0, 15, true);
            for (var i = 0; i < 12; ++i) assetFade(assetLevelEditorIcons[i], 0, 15, true);
            for (var i = 0; i < 3; ++i) assetFade(assetLevelEditorSide[i], 0, 15, true);
            assetFade(assetLevelEditorSelectCorners, 0, 15, true);
            assetFade(assetLevelEditorBarSwitcher, 0, 15, true);
            assetFade(assetLevelEditorSettings, 0, 15, true);
            assetFade(assetLevelEditorHiddenIcon, 0, 15, true);
            instance_destroy(guiCollision);
            instance_destroy(roomCollision);
        }
    }
}
