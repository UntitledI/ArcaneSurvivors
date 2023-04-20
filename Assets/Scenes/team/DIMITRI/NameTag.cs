using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    private Camera camera;

    private void Update() {
        if(camera==null) {
            camera = Camera.main;
        }
        if(camera==null) {
            return;
        }
        transform.LookAt(camera.transform);
    }
}
