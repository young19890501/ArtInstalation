using UnityEngine;
using System.Collections;

public class SaveTexture : MonoBehaviour 
{


	public void OnButtonDown(string s)
	{
		SaveToFile (s);
	}


	public void SaveToFile(string name)
	{
		RenderTexture renderTexture = GetComponent<Camera>().targetTexture;
		RenderTexture currentActiveRT = RenderTexture.active;
		RenderTexture.active = renderTexture;
		Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
		tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
		byte[] bytes = tex.EncodeToPNG();
    	System.IO.File.WriteAllBytes(OurTempSquareImageLocation(name), bytes );
		UnityEngine.Object.Destroy(tex);
		RenderTexture.active = currentActiveRT;
	}

	 private string OurTempSquareImageLocation(string name)
     {
       string r = Application.persistentDataPath +"/" + name + ".png";
	   Debug.Log(r);
       return r;
     }
}
