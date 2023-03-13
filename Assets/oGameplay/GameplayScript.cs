using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GameplayScript : MonoBehaviour
{
    public GameObject sCliff;
    public GameObject sFencing;
    public GameObject sWater;
    public GameObject oCollision;

    //create other script as a variable
    CameraScript cameraScript;
    //insert the gameobject which holds the create script in the unity editor
    public GameObject cameraContainer;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        //grabs create script from the object which holds it
        cameraScript = cameraContainer.GetComponent<CameraScript>();
        //unsure on arrays
        int[] array_length = new int[8];
    }

    // Update is called once per frame
    void Update()
    {
        for(var i = 0; i < array_length(arr); ++i) {
            for (var n = 0; n < array_length(arr[i]); ++n)
            {
                var t = arr[i, n].tile;
                if ((t == sCliff) || (t == sWater)) collisionArray[count] = instance_create_layer(coorToTile(n), coorToTile(i), cameraScript.oInitialise.gameplayLayer, oCollision);
                count++;
            }
        }

        for (var i = 0; i < array_length(arr); ++i)
        {
            for (var n = 0; n < array_length(arr[i]); ++n)
            {
                if (arr[i, n].tile == sFencing) collisionArray[count] = instance_create_layer(coorToTile(n), coorToTile(i), cameraScript.oInitialise.gameplayLayer, oCollision);
                count++;
            }
        }
    }
}
