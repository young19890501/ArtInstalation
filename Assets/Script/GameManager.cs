using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameManager : NetworkManager 
{
    //private NetworkManager netManager;
    private NetworkManagerHUD hud;

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log ("OnPlayerConnected");
        hud = GetComponent<NetworkManagerHUD>();
        hud.enabled = false;

    }
}
