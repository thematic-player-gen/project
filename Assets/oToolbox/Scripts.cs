using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    //create other script as a variable
    Create createScript;
    //insert the gameobject which holds the create script in the unity editor
    public GameObject createContainer;

    //needs a way to access array and replace 'var t' with something else

    // Start is called before the first frame update
    void Start()
    {
        //grabs create script from the object which holds it
        createScript = createContainer.GetComponent<Create>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if ((t == createScript.sCliff) || (t == createScript.sPavement)) return [t, 18];
        if ((t == createScript.sFencing)) return [t, 12];
        if ((t == createScript.sTree2)) return [t, 2];
        else return [t, 0];
    }
}
