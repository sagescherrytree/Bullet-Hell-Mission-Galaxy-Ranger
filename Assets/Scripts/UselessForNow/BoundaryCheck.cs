using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    public Camera MainCamera; //be sure to assign this in the inspector to your main camera
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width * 1.5f - 1.5f, Screen.height * 2.3f - 2.3f, MainCamera.transform.position.z));
        objectWidth = transform.localScale.x; 
        objectHeight = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
        Debug.Log("object width: " + objectWidth);
        Debug.Log("object height: " + objectHeight);
        Debug.Log("screen bounds: (" + screenBounds.x + ", " + screenBounds.y + ")");
    }
}
