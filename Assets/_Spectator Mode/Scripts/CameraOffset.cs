using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraOffset : MonoBehaviour

{

    public GameObject cameraToOffset;
    public GameObject menu;

    private float offset;
    

    private string offsetString;
    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_LUMIN && !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }

    public void GetInput(string offsetValue)
    {
        Debug.Log("Your offset is:"+offsetValue);
        offsetString = offsetValue;

    }

    public void MoveCamera()
    {
        offset = float.Parse(offsetString);
        Debug.Log("Offsetting the camera by" + offset);
        //var position = cameraToOffset.transform.position;
        // Debug.Log(position);
        var position = new Vector3(0, 0, 0);
        position += new Vector3(0, offset/100, 0);
        cameraToOffset.transform.position = position;
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }
}
