using UnityEngine;
using System.Collections;

public class SinMove : MonoBehaviour 
{
	public float speed;
	public float t;
	private float x,y,z;
	public bool par;

	// Use this for initialization
	void Start () 
	{
		if (t==0)
		t = Random.Range(-1f,1f);
		x = gameObject.transform.localPosition.x;
		y = gameObject.transform.localPosition.y;
		z = gameObject.transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		t+=Time.deltaTime;
		if(par)
		gameObject.transform.localPosition = new Vector3(x,y+Mathf.Sin((t+Time.time)*speed)/2,z);
		
		else
		gameObject.transform.localPosition = new Vector3(x,y,z+Mathf.Sin((t+Time.time)*speed)/2);
		
	
	}
}
