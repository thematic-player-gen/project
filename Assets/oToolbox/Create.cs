using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Create : MonoBehaviour
{
    //using array with gameobjects
    public GameObject sDirt;
    public GameObject sCliff;
    public GameObject sPavement;
    public GameObject sFencing;
    public GameObject sWater;
    public GameObject sWaterRock;
    public GameObject sTree1;
    public GameObject sTree2;

    // Start is called before the first frame update
    void Start()
    {
        //Toolbox
        GameObject[] toolbox = new GameObject[8];
        toolbox[0] = sDirt;
        toolbox[1] = sCliff;
        toolbox[2] = sPavement;
        toolbox[3] = sFencing;
        toolbox[4] = sWater;
        toolbox[5] = sWaterRock;
        toolbox[6] = sTree1;
        toolbox[7] = sTree2;
    }
}
