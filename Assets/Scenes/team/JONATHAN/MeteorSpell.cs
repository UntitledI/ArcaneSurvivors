using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpell : MonoBehaviour
{

public GameObject player;
public GameObject meteorPrefab;
public GameObject ring;
private bool isCasting = false;
public float spawnHeight = 10f; // Height above targeting ring to spawn meteor

void Update() {
    // If the 2 key is pressed, activate the casting state and display the targeting ring
    if (Input.GetKeyDown(KeyCode.Alpha2)) {
        isCasting = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        ring.SetActive(true);
    }

    // While casting, move the targeting ring to the location of the cursor on a hit point
    if (isCasting) {
        Vector3 cursorPosition = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
        Ray cursorRay = Camera.main.ScreenPointToRay(cursorPosition);
        if (Physics.Raycast(cursorRay, out RaycastHit hit, Mathf.Infinity)) {
            float dist = Vector3.Distance(hit.point, cursorPosition); // calculate distance between cursor and hit point
            Vector3 spawnPosition = hit.point + Vector3.up * dist * 0.1f; // set position of ring based on distance
            ring.transform.position = spawnPosition;
        }
    }

    // If the left mouse button is pressed and casting is active, spawn a meteor at the targeting ring's position
    if (Input.GetButtonDown("Fire2") && isCasting) {
        isCasting = false;
        Cursor.lockState = CursorLockMode.Locked;
        ring.SetActive(false);
        Vector3 spawnPosition = new Vector3(ring.transform.position.x, spawnHeight, ring.transform.position.z);
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f); // rotate meteorPrefab to face downwards
        Instantiate(meteorPrefab, spawnPosition, spawnRotation);
    }

    // If the escape key is pressed, deactivate the casting state and hide the targeting ring
    if (Input.GetKeyDown(KeyCode.Escape)) {
        isCasting = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ring.SetActive(false);
    }
}

}
