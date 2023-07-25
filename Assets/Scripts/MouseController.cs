using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private RectTransform mouse;
    
    void Start()
    {
        mouse = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 mousePos = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        mouse.position = worldPos;
    }
}
