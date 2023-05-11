using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float speed = 0.1f;

    private Rigidbody player;
    public GameObject arrowObject;
    public Transform arrowPoint;

    void Start() {
        player = GetComponent<Rigidbody> ();
    }
    // Update is called once per frame
    void Update()
    {
        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDir, 0.0f, zDir);

        player.AddForce(moveDirection * speed);

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //ability 1 function
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //ability 2 function
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            //ability 3 function
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            //ability 4 function
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            GameObject arrow = Instantiate(arrowObject, arrowPoint.position, transform.rotation);
            arrow.GetComponent<Rigidbody>().AddForce(transform.forward *25f, ForceMode.Impulse);
        }                        
    }
    }