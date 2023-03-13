using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.SceneView;

public class EditorStep : MonoBehaviour
{
    ControllerScript controllerScript;
    public GameObject controllerContainer;

    EditorCreate editorCreate;
    EditorScript editorScript;
    public GameObject editorContainer;

    CameraScript cameraScript;
    public GameObject cameraContainer;

    public GameObject oGameplay;
    public GameObject oEditor;
    public GameObject oPlayer;

    // Start is called before the first frame update
    void Start()
    {
        controllerScript = controllerContainer.GetComponent<ControllerScript>();
        editorCreate = editorContainer.GetComponent<EditorCreate>();
        editorScript = editorContainer.GetComponent<EditorScript>();
        cameraScript = cameraContainer.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Hide Gui
        if (controllerScript.spacebarPressed) //get input
        {
            if (roomCollision.hide) roomCollision.hide = false; else roomCollision.hide = true;
        }

        //Check if on tool bars
        if ((checkMouseCol(roomCollision) != -1) && (!toolDoingStuff))
        {
            if (roomCollision.hide)
            {
                if (assetLevelEditorHiddenIcon.targetFade == 0) assetFade(assetLevelEditorHiddenIcon, 0.9, 5);
                if (assetLevelEditorTopBar.currentFade == 0.8)
                {
                    for (var i = 0; i < 12; ++i) assetFade(assetLevelEditorIcons[i], 0, 60);
                    for (var i = 0; i < 3; ++i) assetFade(assetLevelEditorSide[i], 0, 60);
                    for (var i = 0; i < 4; ++i) assetFade(assetLevelEditorBottom[i], 0, 60);
                    assetFade(assetLevelEditorTopBar, 0, 60);
                    assetFade(assetLevelEditorBarSwitcher, 0, 60);
                    assetFade(assetLevelEditorSettings, 0, 60);
                    assetFade(assetLevelEditorSelectCorners, 0, 60);
                }
            }
            else
            {
                //Fade in gui
                if (assetLevelEditorTopBar.targetFade == 0)
                {
                    for (var i = 0; i < 12; ++i) assetFade(assetLevelEditorIcons[i], 0.9, 5);
                    for (var i = 0; i < 3; ++i) assetFade(assetLevelEditorSide[i], 0.9, 5);
                    for (var i = 0; i < 4; ++i) assetFade(assetLevelEditorBottom[i], 0.9, 5);
                    assetFade(assetLevelEditorTopBar, 0.8, 5);
                    assetFade(assetLevelEditorBarSwitcher, 0.9, 5);
                    assetFade(assetLevelEditorSettings, 0.9, 5);
                    assetFade(assetLevelEditorSelectCorners, 0.9, 5);
                    assetFade(assetLevelEditorHiddenIcon, 0, 20);
                }
                toolDisplay = 0;
                //Phase Switch
                if (phaseCool > 0) phaseCool--;
                else
                {
                    //Reset Animations
                    for (var i = 0; i < 12; ++i) assetSize(assetLevelEditorIcons[i], 0.8, 1);
                    for (var i = 0; i < 3; ++i) assetSize(assetLevelEditorSide[i], 0.75, 1);
                    for (var i = 0; i < 4; ++i) assetSize(assetLevelEditorBottom[i], 0.5, 1);
                    assetSize(assetLevelEditorBarSwitcher, 0.8, 1);
                    assetSize(assetLevelEditorSettings, 1, 1);
                    if (!valueBetween(toolSelected, 0, 41, true)) assetFade(assetLevelEditorSelectCorners, 0, 1);

                    //Mouse Hover
                    var c = checkMouseCol(guiCollision);
                    if (c != -1)
                    {
                        switch (c)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                {
                                    assetSize(assetLevelEditorIcons[c], 0.9, 1);
                                    break;
                                }
                            case 12:
                                {
                                    assetSize(assetLevelEditorBarSwitcher, 0.9, 1);
                                    break;
                                }
                            case 13:
                            case 14:
                            case 15:
                                {
                                    assetSize(assetLevelEditorSide[c - 13], 0.85, 1);
                                    break;
                                }
                            case 16:
                                {
                                    assetSize(assetLevelEditorSettings, 1.1, 1);
                                    break;
                                }
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                {
                                    assetSize(assetLevelEditorBottom[c - 17], 0.6, 1);
                                    break;
                                }
                        }
                    }

                    //Mouse Click
                    if (controllerScript.mouseButtonPressed)
                    {
                        //Animation
                        switch (c)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                {
                                    assetLevelEditorIcons[c].currentScale = 0.7;
                                    assetPause(assetLevelEditorIcons[c], 3);
                                    assetFade(assetLevelEditorSelectCorners, 1, 1);
                                    var xTra = 0;
                                    if (c < 2) xTra = 16;
                                    assetMove(assetLevelEditorSelectCorners, topBarX - topBarWidthH - xTra + 64 + (c * 128), -1, 1);
                                    break;
                                }
                            case 12:
                                {
                                    barSelected = valueLoop(barSelected + 1, 0, 3);
                                    if (valueBetween(toolSelected, 0, 39, true)) toolSelected = valueLoop(toolSelected + 10, 0, 39);
                                    assetLevelEditorBarSwitcher.currentScale = 0.7;
                                    assetPause(assetLevelEditorBarSwitcher, 3);
                                    break;
                                }
                            case 13:
                            case 14:
                            case 15:
                                {
                                    assetLevelEditorSide[c - 13].currentScale = 0.65;
                                    assetPause(assetLevelEditorSide[c - 13], 3);
                                    break;
                                }
                            case 16:
                                {
                                    assetLevelEditorSettings.currentScale = 0.9;
                                    assetPause(assetLevelEditorSettings, 3);
                                    break;
                                }
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                {
                                    assetLevelEditorBottom[c - 17].currentScale = 0.4;
                                    assetPause(assetLevelEditorBottom[c - 17], 3);
                                    break;
                                }
                        }
                        //Effects
                        switch (c)
                        {
                            case 0:
                                {
                                    //Hand
                                    toolSelected = 40;
                                    break;
                                }
                            case 1:
                                {
                                    //Eraser
                                    toolSelected = 41;
                                    break;
                                }
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                {
                                    //Tool Bar
                                    toolSelected = c - 2 + (10 * barSelected);
                                    break;
                                }
                            case 13:
                                {
                                    //Quest Manager

                                    break;
                                }
                            case 14:
                                {
                                    //Aesthetics - random environment deco slider and weather effects?

                                    break;
                                }
                            case 15:
                                {
                                    //Settings and save

                                    break;
                                }
                            case 16:
                                {
                                    //Play game
                                    instance_create_depth(0, 0, 0, oGameplay);
                                    editorClearAll();
                                    Destroy(oEditor);

                                    instance_create_depth(300, 300, 0, oPlayer);
                                    break;
                                }
                            case 17:
                                {
                                    //Clear all

                                    break;
                                }
                            case 18:
                                {
                                    //Player 

                                    break;
                                }
                            case 19:
                                {
                                    //Zoom out
                                    if (zoomedOut)
                                    {
                                        cameraZoom(1);
                                        zoomedOut = false;
                                        cameraMoveSet(zoomedOutX, zoomedOutY);
                                    }
                                    else
                                    {
                                        var zoomH = cameraScript.oInitialise.roomHeight / oCamera.defaultHeight;
                                        var zoomW = cameraScript.oInitialise.roomWidth / oCamera.defaultWidth;
                                        if (zoomH < zoomW) var zoomT = zoomH; else zoomT = zoomW;
                                        cameraZoom(zoomT);
                                        zoomedOut = true;
                                        zoomedOutX = oCamera.x;
                                        zoomedOutY = oCamera.y;
                                    }
                                    break;
                                }
                            case 20:
                                {

                                    break;
                                }
                        }
                    }
                }
            }
        }
        else
        {
            //Edit with tool
            toolDoingStuff = false;
            if (toolSelected != -1)
            {
                if (controllerScript.mouseButton) toolDoingStuff = true;
                if (toolSelected < 40)
                {
                    toolDisplay = 1;
                    switch (toolSelected)
                    {
                        case 0:
                        case 1:
                        case 2:
                            {
                                //Grounds
                                if (checkTileFree(oData.groundArray, mapToArray(controllerScript.mouseCoors)))
                                {
                                    toolDisplay = 2;
                                    if (controllerScript.mouseButton)
                                    {
                                        addTile(oData.groundArray, mapToArray(controllerScript.mouseCoors), oToolbox.toolbox[toolSelected]);
                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                //Fence
                                if ((checkTilePlaceableGround(oData.groundArray, mapToArray(controllerScript.mouseCoors))) &&
                                    (checkTileFree(oData.objectArray, mapToArray(controllerScript.mouseCoors))))
                                {
                                    toolDisplay = 2;
                                    if (controllerScript.mouseButton)
                                    {
                                        addTile(oData.objectArray, mapToArray(controllerScript.mouseCoors), oToolbox.toolbox[toolSelected]);
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                //Water
                                if (checkTileFree4(oData.groundArray, mapToArray(controllerScript.mouseCoors), oToolbox.toolbox[toolSelected]))
                                {
                                    toolDisplay = 2;
                                    if (controllerScript.mouseButton)
                                    {
                                        var coors = mapToArray(oController.mouseCoors);
                                        addTile(oData.groundArray, coors, oToolbox.toolbox[toolSelected]);
                                        addTile(oData.groundArray,[coors[0] + 1, coors[1] + 1], oToolbox.toolbox[toolSelected]);
                                        addTile(oData.groundArray,[coors[0], coors[1] + 1], oToolbox.toolbox[toolSelected]);
                                        addTile(oData.groundArray,[coors[0] + 1, coors[1]], oToolbox.toolbox[toolSelected]);
                                    }
                                }
                                break;
                            }
                        case 5:
                            {
                                //Water Objects
                                if ((checkTilePlaceableWater(oData.groundArray, mapToArray(controllerScript.mouseCoors))) &&
                                    (checkTileFree(oData.objectArray, mapToArray(controllerScript.mouseCoors))))
                                {
                                    toolDisplay = 2;
                                    if (controllerScript.mouseButton)
                                    {
                                        addTile(oData.objectArray, mapToArray(controllerScript.mouseCoors), oToolbox.toolbox[toolSelected]);
                                    }
                                }
                                break;
                            }
                        case 6:
                        case 7:
                            {
                                //Trees
                                if ((checkTilePlaceableGround3x2(oData.groundArray, mapToArray(controllerScript.mouseCoors), false)) &&
                                    (checkTileFree3x2(oData.objectArray, mapToArray(controllerScript.mouseCoors))))
                                {
                                    toolDisplay = 2;
                                    if (controllerScript.mouseButton)
                                    {
                                        var coors = mapToArray(controllerScript.mouseCoors);
                                        addObject(oToolbox.toolbox[toolSelected],[coors,[coors[0] - 1, coors[1]],[coors[0] + 1, coors[1]],
            
                                            [coors[0], coors[1] - 1],[coors[0] - 1, coors[1] - 1],[coors[0] + 1, coors[1] - 1]], toolSelected == 7 ? 2 : 0);
                                    }
                                }
                                break;
                            }
                    }
                }
                else if (toolSelected == 40)
                {
                    //Hand
                }
                else if (toolSelected == 41)
                {
                    //Eraser
                    toolDisplay = 3;
                    if (!checkTileFree(oData.objectArray, mapToArray(controllerScript.mouseCoors)))
                    {
                        toolDisplay = 4;
                        if (controllerScript.mouseButton)
                        {
                            removeTile(oData.objectArray, mapToArray(controllerScript.mouseCoors));
                        }
                    }
                    else if (!checkTileFree(oData.groundArray, mapToArray(controllerScript.mouseCoors)))
                    {
                        toolDisplay = 4;
                        if (controllerScript.mouseButton)
                        {
                            removeTile(oData.groundArray, mapToArray(controllerScript.mouseCoors));
                        }
                    }
                }
            }
            //Fade Gui if doing stuff
            if (assetLevelEditorHiddenIcon.targetFade == 0.9) assetFade(assetLevelEditorHiddenIcon, 0, 20);
            if (assetLevelEditorTopBar.currentFade == 0.8)
            {
                for (var i = 0; i < 12; ++i) assetFade(assetLevelEditorIcons[i], 0, 60);
                for (var i = 0; i < 3; ++i) assetFade(assetLevelEditorSide[i], 0, 60);
                for (var i = 0; i < 4; ++i) assetFade(assetLevelEditorBottom[i], 0, 60);
                assetFade(assetLevelEditorTopBar, 0, 60);
                assetFade(assetLevelEditorBarSwitcher, 0, 60);
                assetFade(assetLevelEditorSettings, 0, 60);
                assetFade(assetLevelEditorSelectCorners, 0, 60);
            }
        }

        //Move Camera within Game World
        var moveH = (controllerScript.keyRight - controllerScript.keyLeft) * cameraMoveS;
        var moveV = (controllerScript.keyDown - controllerScript.keyUp) * cameraMoveS;
        cameraMoveSpd(moveH, moveV);
    }
}
