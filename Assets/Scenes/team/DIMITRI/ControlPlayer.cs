using UnityEngine;

public class CubeMove : MonoBehaviour
{

    public float speed = 0.1f;

    private Rigidbody player;

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
    }
}