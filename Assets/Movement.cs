using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour 
{
	public GameObject cam;
	private GameObject mainCam;

   public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
	// Use this for initialization
	void Start () 
	{
		if(Camera.main)
		{
			mainCam = Camera.main.gameObject;
			mainCam.SetActive(false);
			
		}


		if(isLocalPlayer)
		return;

		if(cam)
		cam.SetActive(false);

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isLocalPlayer)
		return;
		
		transform.position += new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized* 5f*Time.deltaTime; 
	}

	void OnDisable()
	{
		if(Camera.main)
		mainCam.SetActive(true);
	}
}
