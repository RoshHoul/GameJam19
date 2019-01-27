using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBrowser : MonoBehaviour
{
    public List<Camera> cameras;
    public List<RawImage> imagesLeft;
    public List<RawImage> imagesRight;
    public GameObject highlighter;

    public RawImage display;
    public Text cameraName;
    
    public void OnEnable()
    {
        display.texture = cameras[0].activeTexture;
    }
    
    public void ShowCamera(int id)
    {
        display.texture = cameras[id].activeTexture;
        cameraName.text = cameras[id].name;
    }

    public void HighlightCamera(int id)
    {
        highlighter.SetActive(true);
        if(id < 4)
        {
            highlighter.transform.position = imagesLeft[id].transform.position;
        }
        else
        {
            int i = id - 4;
            highlighter.transform.position = imagesRight[i].transform.position;
        }
        
    }
}
