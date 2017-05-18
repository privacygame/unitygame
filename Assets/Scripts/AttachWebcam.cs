using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AttachWebcam : MonoBehaviour {
    WebCamTexture webCamTexture;
	// Use this for initialization
	void Start () {
        webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture=webCamTexture;
        webCamTexture.Play();
        StartCoroutine(TakePhoto());
    }
    private IEnumerator TakePhoto()
    {
    print("in numerator");
    // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame(); 
        yield return new WaitForSeconds(5);

    // it's a rare case where the Unity doco is pretty clear,
    // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
    // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        print("photo data in");
        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        print(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop));
        File.WriteAllBytes("photo.png", bytes);
        print("photo saved");
    }
}
