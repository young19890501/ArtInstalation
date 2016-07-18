using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ArtInstallationPlayer : NetworkBehaviour 
{

    [SerializeField] private Button sendBtn,loopBtn;
    private GameObject drawer;

	// Use this for initialization
	void Start () 
    {
        if(!isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
        drawer = GameObject.FindGameObjectWithTag("Drawer");
        sendBtn.onClick.AddListener(() =>
        {
            drawer.GetComponent<Draw>().SendButton();
        });

        loopBtn.onClick.AddListener(() =>
        {
            drawer.GetComponent<Draw>().LoopButton();
        });

	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
