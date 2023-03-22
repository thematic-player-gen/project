using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    public GameObject oPlayer;


    public GameObject oCamera;

    float zStay;
    // public GameObject oInitialise;

    Vector3 follow;
    Vector3 curPos;

    string curRoom;
    //for other scripts usage
    float x;
    float y;

    float halfWidth;
    float halfHeight;

    //Editing
    int currentZoom = 1;
    int zoom = 1;
    int zoomSpd = 10;
    float fixedX = 0f;
    float fixedY = 0f;

    //
    int defaultWidth;
    int defaultHeight;


    float xTowards;
    float yTowards;
    int camSpd = 25;
    int centreLeewayX = 256;
    int centreLeewayY = 96;
    float cameraZoomLeeway = 0.001f;

    //Tiles
    //# macro TILE_SIZE 64
    //# macro TILE_START 192

    //Layers
    // LayerMask managersLayer;
    //LayerMask gameplayLayer;

    //Room
    float roomWidth;
    float roomHeight;

    //Phase States
    enum phases
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        ELEVEN
    }

    // Start is called before the first frame update
    void Start()
    {
        zStay = transform.position.z;
        roomWidth = Screen.width;
        roomHeight = Screen.height;

        // managersLayer = "Managers";
        // gameplayLayer = "Gameplay";

        curRoom = SceneManager.GetActiveScene().name;

        curPos = transform.position;
        //Initialise
        //  var oCamera = oCamera; // view_camera[0];
        var follow = oPlayer.transform;
        float defaultWidth = oCamera.transform.localScale.x;
        float defaultHeight = oCamera.transform.localScale.y;
        halfWidth = defaultWidth * 0.5f;
        halfHeight = defaultHeight * 0.5f;
        if (follow)
        {
            x = follow.transform.position.x;
            y = follow.transform.position.y;
        }
        else
        {
            x = oCamera.transform.position.x + halfWidth;
            y = oCamera.transform.position.y + halfHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Destination

        follow = oPlayer.transform.position;
        if (Math.Abs(xTowards - follow.x) > centreLeewayX)
            xTowards = follow.x - (Math.Sign(follow.x - xTowards) * centreLeewayX);

        if (Math.Abs(yTowards - follow.y) > centreLeewayY)
            yTowards = follow.y - (Math.Sign(follow.y - yTowards) * centreLeewayY);


        //Fixed Position
        if (fixedX != 0) xTowards = fixedX;
        if (fixedY != 0) yTowards = fixedY;

        //Zoom
        if (Math.Abs(zoom - currentZoom) < cameraZoomLeeway) currentZoom = zoom;
        else currentZoom += (zoom - currentZoom) / zoomSpd;

        var newScale = transform.position;
        newScale.x = defaultWidth * currentZoom;

        oCamera.transform.localScale = new Vector3(defaultWidth * currentZoom, defaultHeight * currentZoom);

        //Reset Size
        halfWidth = oCamera.transform.localScale.x * 0.5f;
        halfHeight = oCamera.transform.localScale.y * 0.5f;

        //Move Object
        x += (xTowards - x) / camSpd;
        y += (yTowards - y) / camSpd;

        x = Math.Clamp(x, halfWidth, roomWidth - halfWidth);
        y = Math.Clamp(y, halfHeight, roomHeight - halfHeight);

        //Update Camera
        curPos = new Vector3(x - halfWidth, y - halfHeight, zStay);

        getCameraSize();
        getCameraZoom();
        cameraFix(x,y);
        cameraZoom(zoom,zoomSpd);
        cameraMoveSpd(camSpd, camSpd);
        cameraMoveSet(fixedX, fixedY);
    }
    void getCameraSize()
    {
        var w = oCamera.transform.localScale.x;
        var h = oCamera.transform.localScale.y;

        // return w, h;
    }

    void getCameraZoom()
    {
        // return oCamera.transform.localScale;
    }

    void cameraFix(float xCoor, float yCoor)
    {
        if (xCoor != -1000f) fixedX = xCoor;
        if (yCoor != -1000f) fixedY = yCoor;
    }

    void cameraZoom(int zoomS, int zoomSp = 0)
    {
        zoom = zoomS;
        if (zoomSp > 0) zoomSpd = zoomSp;
    }

    void cameraMoveSpd(int xSpd, int ySpd)
    {
        xTowards += xSpd;
        if (xTowards < halfWidth) xTowards = halfWidth;
        else if (xTowards > roomWidth - halfWidth) xTowards = roomWidth - halfWidth;
        yTowards += ySpd;
        if (yTowards < halfHeight) yTowards = halfHeight;
        else if (yTowards > roomHeight - halfHeight) yTowards = roomHeight - halfHeight;
    }

    void cameraMoveSet(float xSet, float ySet)
    {
        xTowards = xSet;
        if (xTowards < halfWidth) xTowards = halfWidth;
        else if (xTowards > roomWidth - halfWidth) xTowards = roomWidth - halfWidth;
        yTowards = ySet;
        if (yTowards < halfHeight) yTowards = halfHeight;
        else if (yTowards > roomHeight - halfHeight) yTowards = roomHeight - halfHeight;
    }
}

