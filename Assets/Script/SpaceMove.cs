using UnityEngine;
using System.Collections;

public class SpaceMove : MonoBehaviour 
{
	private float t,z,tempz,i,y,tempy;
	private bool move;

	// Use this for initialization
	void Start () 
	{
		tempy = transform.position.y;
		tempz = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log(i);
	
		if(Input.GetKeyDown(KeyCode.Space))
		{
			t = 0;
			i =0;
			y = Random.Range(-0.5f,0.5f);
			y = transform.position.y +y;
			z = Random.Range(-0.5f,0.5f);
			z = transform.position.z +z;
			move = true;
		}
		else if(Input.GetKeyUp(KeyCode.Space))
		{
			i = 0;
			move = false;
		}
		if(move)
		{
			
		t += Time.deltaTime;
		transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y,y,t),transform.position.z);
		transform.position = new Vector3(transform.position.x,transform.position.y, Mathf.Lerp(transform.position.z,z,t));
			
		}
		else 
		{
			
		i += Time.deltaTime;
		transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y,tempy,i),transform.position.z);
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z,tempz,i));
			
		}
		
	}
	
	
}
