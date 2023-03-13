using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraScript : MonoBehaviour
{

    public GameObject oPlayer;
    public GameObject oCamera;
    public GameObject oInitialise;

    //for other scripts usage
    public int x;
    public int y;

    public float halfWidth;
    public float halfHeight;

    //Editing
    int currentZoom = 1;
    int zoom = 1;
    int zoomSpd = 10;
    int fixedX = 0;
    int fixedY = 0;

    //
    int defaultWidth;
    int defaultHeight;

    int roomWidth;
    int roomHeight;

    float xTowards;
    float yTowards;
    int camSpd = 25;
    int centreLeewayX = 256;
    int centreLeewayY = 96;
    float cameraZoomLeeway = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise
        var cam = view_camera[0];
        var follow = oPlayer;
        int defaultWidth = camera_get_view_width(cam);
        int defaultHeight = camera_get_view_height(cam);
        halfWidth = defaultWidth * 0.5f;
        halfHeight = defaultHeight * 0.5f;
        if (instance_exists(follow))
        {
            x = follow.x;
            y = follow.y;
        }
        else
        {
            x = camera_get_view_x(cam) + halfWidth;
            y = camera_get_view_y(cam) + halfHeight;
        }
        int xTowards = x;
        int yTowards = y;
    }

    // Update is called once per frame
    void Update()
    {
        //Destination
        if (instance_exists(follow))
        {
            if (abs(xTowards - follow.x) > centreLeewayX) xTowards = follow.x - ((sign(follow.x - xTowards) * centreLeewayX));
            if (abs(yTowards - follow.y) > centreLeewayY) yTowards = follow.y - ((sign(follow.y - yTowards) * centreLeewayY));
        }

        //Fixed Position
        if (fixedX != 0) xTowards = fixedX;
        if (fixedY != 0) yTowards = fixedY;

        //Zoom
        if (abs(zoom - currentZoom) < cameraZoomLeeway) currentZoom = zoom;
        else currentZoom += (zoom - currentZoom) / zoomSpd;
        camera_set_view_size(cam, defaultWidth * currentZoom, defaultHeight * currentZoom);

        //Reset Size
        halfWidth = camera_get_view_width(cam) * 0.5;
        halfHeight = camera_get_view_height(cam) * 0.5;

        //Move Object
        x += (xTowards - x) / camSpd;
        y += (yTowards - y) / camSpd;

        x = clamp(x, halfWidth, oInitialise.roomWidth - halfWidth);
        y = clamp(y, halfHeight, oInitialise.roomHeight - halfHeight);

        //Update Camera
        camera_set_view_pos(cam, x - halfWidth, y - halfHeight);
        
    }
    void getCameraSize()
    {
        var w = oCamera.camera_get_view_width(cam);
        var h = oCamera.camera_get_view_height(cam);
        return w, h;
    }

    void getCameraZoom()
    {
        return oCamera.currentZoom;
    }

    void cameraFix(int xCoor, int yCoor)
    {
        with(oCamera) {
            if (xCoor != -1000) fixedX = xCoor;
            if (yCoor != -1000) fixedY = yCoor;
        }
    }

    void cameraZoom(int zoomS, int zoomSp = 0)
    {
        with(oCamera) {
            zoom = zoomS;
            if (zoomSp > 0) zoomSpd = zoomSp;
        }
    }

    void cameraMoveSpd(int xSpd, int ySpd)
    {
        with(oCamera) {
            xTowards += xSpd;
            if (xTowards < halfWidth) xTowards = halfWidth;
            else if (xTowards > oInitialise.roomWidth - halfWidth) xTowards = oInitialise.roomWidth - halfWidth;
            yTowards += ySpd;
            if (yTowards < halfHeight) yTowards = halfHeight;
            else if (yTowards > oInitialise.roomHeight - halfHeight) yTowards = oInitialise.roomHeight - halfHeight;
        }
    }

    void cameraMoveSet(float xSet, float ySet)
    {
        with(oCamera) {
            xTowards = xSet;
            if (xTowards < halfWidth) xTowards = halfWidth;
            else if (xTowards > oInitialise.roomWidth - halfWidth) xTowards = oInitialise.roomWidth - halfWidth;
            yTowards = ySet;
            if (yTowards < halfHeight) yTowards = halfHeight;
            else if (yTowards > oInitialise.roomHeight - halfHeight) yTowards = oInitialise.roomHeight - halfHeight;
        }
    }
}
