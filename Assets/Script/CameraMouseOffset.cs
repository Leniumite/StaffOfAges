using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMouseOffset : MonoBehaviour
{
    public Transform player;
    private Camera camera;

    public float maxOffset = 5;
    
    private void Start()
    {
        if(!player){Debug.LogError("no player " + name);}

        TryGetComponent(out camera);
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        Vector3 center = mousePos - player.transform.position;

        Vector3 cameraOffsetFromMouse = new Vector3(player.transform.position.x + center.x, player.transform.position.y + center.y, -10);
        if (cameraOffsetFromMouse.magnitude > maxOffset) cameraOffsetFromMouse = cameraOffsetFromMouse.normalized * maxOffset;

        transform.position = transform.parent.position  + cameraOffsetFromMouse;
    }
}
