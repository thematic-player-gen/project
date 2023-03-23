using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject sDirt;
    public GameObject sCliff;
    public GameObject sPavement;
    public GameObject sFencing;
    public GameObject sWater;
    public GameObject sWaterRock;
    public GameObject sTree1;
    public GameObject sTree2;
    void Start()
    {
        GameObject[] toolbox = new GameObject[8];
        //Toolbox
        toolbox[0] = sDirt;
        toolbox[1] = sCliff;
        toolbox[2] = sPavement;
        toolbox[3] = sFencing;
        toolbox[4] = sWater;
        toolbox[5] = sWaterRock;
        toolbox[6] = sTree1;
        toolbox[7] = sTree2;
    }/*
    void getToolDraw(int toolNum)
    {
        var t = oToolbox.toolbox[toolNum];
        if ((t == sCliff) || (t == sPavement)) return (t, 18);
        if ((t == sFencing)) return (t, 12);
        if ((t == sTree2)) return (t, 2);
        else return (t, 0);
    }
    */
}

