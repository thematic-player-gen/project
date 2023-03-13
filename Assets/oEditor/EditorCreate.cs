using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class EditorCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Initialise
        gui1Layer = layer_get_id("Gui1");
        gui2Layer = layer_get_id("Gui2");
        gui3Layer = layer_get_id("Gui3");
        gui4Layer = layer_get_id("Gui4");
        gui5Layer = layer_get_id("Gui5");
        int barSelected = 0; //0-Ground,1-Interactables,2-Enemies,3-Environment
        int toolSelected = -1; //0-39: Tool Bar Top, 40-Hand, 41-Eraser
        int phaseCool = 0;
        int toolDisplay = 0;
        bool toolDoingStuff = false;
        int cameraMoveS = 20;

        bool zoomedOut = false;
        int zoomedOutX = 0;
        int zoomedOutY = 0;

        //Coordinates
        int topBarX = 960;
        int topBarY = 96;
        int topBarWidthH = 760;
        int topBarHeightH = 64;
        int sideBarX = 1840;
        int sideBarY = 584;
        int bottomBarX = 1408;
        int bottomBarY = 1016;
        int settingsX = 1824;
        int settingsY = 984;
        int hiddenIconX = 80;
        int hiddenIconY = 48;

    }

    // Update is called once per frame
    void Update()
    {
        //Assets
        assetLevelEditorTopBar = assetCreateSprite(sLevelEditorTopBar, topBarX, topBarY, gui2Layer, 0, 0.8, c_white, true);
        for (var i = 0; i < 12; ++i)
        {
            var xTra = 0;
            var ind = 1;
            var spr = sLevelEditorIcon;
            if (i < 2) xTra = 16;
            else
            {
                spr = sLevelPlaceIcons;
                ind = i - 2;
            }
            assetLevelEditorIcons[i] = assetCreateSprite(spr, topBarX - xTra - topBarWidthH + 64 + (i * 128), topBarY, gui3Layer, ind, 0.9, c_white, true);
            assetSize(assetLevelEditorIcons[i], 0.8, 1);
        }
        assetLevelEditorSelectCorners = assetCreateSprite(sLevelEditorSelect, topBarX, topBarY, gui2Layer, 0, 0, c_white, true);
        assetLevelEditorBarSwitcher = assetCreateSprite(sLevelEditorIcon, topBarX + topBarWidthH + 96, topBarY, gui2Layer, 1, 0.9, c_white, true);
        assetSize(assetLevelEditorBarSwitcher, 0.8, 1);
        for (var i = 0; i < 4; ++i)
        {
            if (i != 3)
            {
                assetLevelEditorSide[i] = assetCreateSprite(sLevelEditorIcon, sideBarX, sideBarY + (i * 128), gui2Layer, 0, 0.9, c_white, true);
                assetSize(assetLevelEditorSide[i], 0.75, 1);
            }
            assetLevelEditorBottom[i] = assetCreateSprite(sLevelEditorIcon, bottomBarX + (i * 96), bottomBarY, gui2Layer, 0, 0.9, c_white, true);
            assetSize(assetLevelEditorBottom[i], 0.5, 1);
        }
        assetLevelEditorSettings = assetCreateSprite(sLevelEditorIcon, settingsX, settingsY, gui2Layer, 0, 0.9, c_white, true);
        assetLevelEditorHiddenIcon = assetCreateSprite(sLevelEditorHiddenIcon, hiddenIconX, hiddenIconY, gui2Layer, 0, 0, c_white, true);


        //Collision
        for (var i = 0; i < 48; ++i)
        {
            var xTra = 0;
            if (i < 8) xTra = 16;
            var n = (i div 4);
        switch (i mod 4) {
		case 0: guiCol[i] = topBarX - xTra - topBarWidthH + (n * 128); break;
		case 1: guiCol[i] = topBarY - topBarHeightH; break;
		case 2: guiCol[i] = topBarX - xTra - topBarWidthH + ((n + 1) * 128); break;
		case 3: guiCol[i] = topBarY + topBarHeightH; break;
        }
    }
    guiCol[48] = topBarX+topBarWidthH+32;
guiCol[49] = topBarY-topBarHeightH;
guiCol[50] = topBarX+topBarWidthH+160;
guiCol[51] = topBarY+topBarHeightH;
for (var i = 0; i< 12; ++i) {
	var n = (i div 4);
	switch (i mod 4) {
		case 0: guiCol[i + 52] = sideBarX-48; break;
		case 1: guiCol[i + 52] = sideBarY-48+(n*128); break;
		case 2: guiCol[i + 52] = sideBarX+48; break;
		case 3: guiCol[i + 52] = sideBarY+48+(n*128); break;
	}
}
guiCol[64] = settingsX - 64;
guiCol[65] = settingsY - 64;
guiCol[66] = settingsX + 64;
guiCol[67] = settingsY + 64;
for (var i = 0; i < 16; ++i)
{
    var n = (i div 4);
switch (i mod 4) {
		case 0: guiCol[i + 68] = bottomBarX - 32 + (n * 96); break;
		case 1: guiCol[i + 68] = bottomBarY - 32; break;
		case 2: guiCol[i + 68] = bottomBarX + 32 + (n * 96); break;
		case 3: guiCol[i + 68] = bottomBarY + 32; break;
}
}
guiCollision = createMouseCol(guiCol, true);

//Room Collision
roomCollision = createMouseCol([0, 0, 1920, 192, 1728, 0, 1920, 1080, bottomBarX - 64, bottomBarY - 64, 1920, 1080],true);
    }
}
