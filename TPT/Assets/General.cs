using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class GenScripts : MonoBehaviour
{
    //create other script as a variable
    CameraScript cameraScript;
    //insert the gameobject which holds the create script in the unity editor
    public GameObject cameraContainer;
    public GameObject oCamera;

    // Start is called before the first frame update
    void Start()
    {
        //grabs create script from the object which holds it
        cameraScript = cameraContainer.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    int valueLoop(int value, int lower, int upper)
    {
        //not sure what sign is for c#
        if ((value >= lower) && (value <= upper)) return value; else return valueLoop(value + (/*sign*/(lower - value) * ((upper - lower) + 1)), lower, upper);
    }

    bool valueBetween(int value, int lower, int upper, bool include = false)
    {
        if (include)
        {
            if ((value >= lower) && (value <= upper)) return true; else return false;
        }
        else if ((value > lower) && (value < upper)) return true; else return false;
    }
    /*
    void numToPhase(int num)
    {
        switch (num)
        {
            case 1: return phases.ONE;
            case 2: return phases.TWO;
            case 3: return phases.THREE;
            case 4: return phases.FOUR;
            case 5: return phases.FIVE;
            case 6: return phases.SIX;
            case 7: return phases.SEVEN;
            case 8: return phases.EIGHT;
            case 9: return phases.NINE;
            case 10: return phases.TEN;
            case 11: return phases.ELEVEN;
        }
    }

    int phaseToNum(int phase)
    {
        switch (phase)
        {
            case phases.ONE: return 1;
            case phases.TWO: return 2;
            case phases.THREE: return 3;
            case phases.FOUR: return 4;
            case phases.FIVE: return 5;
            case phases.SIX: return 6;
            case phases.SEVEN: return 7;
            case phases.EIGHT: return 8;
            case phases.NINE: return 9;
            case phases.TEN: return 10;
            case phases.ELEVEN: return 11;
        }
        return 0;
    }
    */
    float xToGui(int xCoor)
    {
        return xCoor - cameraScript.x + cameraScript.halfWidth;
    }

    float yToGui(int yCoor)
    {
        return yCoor - cameraScript.y + cameraScript.halfHeight;
    }
}