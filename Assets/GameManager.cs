using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    
  
    

    void Start()
    {
       Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        Debug.Log(Display.displays[0]);

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }

    void Update()
    {
      
    }

}
