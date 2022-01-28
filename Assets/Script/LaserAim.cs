using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [Header("Aim")]
    public Transform center; //center of baby object
    [SerializeField] private GameObject laser;
    private Vector3 aimDirection;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Aim at
        AimInput();
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Young
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            laser.SetActive(true);
            //TODO method
        }
        else
            laser.SetActive(false);
    }

    public void AimInput()
    {
        // Vector between mouse and center of the player, normalized
        Vector2 tempDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - center.position;
        aimDirection = new Vector3(tempDir.x, tempDir.y, 0);
        aimDirection.Normalize();
    }
}
