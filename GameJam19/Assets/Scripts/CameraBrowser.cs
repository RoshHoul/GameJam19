using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBrowser : MonoBehaviour
{
    public List<Camera> cameras;
    public RawImage display;
    public Text cameraName;

    int cameraIndex = 0;

    public void OnEnable()
    {
        display.texture = cameras[cameraIndex].activeTexture;
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            cameraIndex++;
            if(cameraIndex < cameras.Count)
            {
                display.texture = cameras[cameraIndex].activeTexture;
                cameraName.text = cameras[cameraIndex].name;
            }
            else
            {
                cameraIndex = 0;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            cameraIndex--;
            if (cameraIndex > 0)
            {
                display.texture = cameras[cameraIndex].activeTexture;
                cameraName.text = cameras[cameraIndex].name;
            }
            else
            {
                cameraIndex = cameras.Count - 1;
            }
        }
    }
}
