using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] grassSteps;
    [SerializeField]
    private AudioClip[] stoneSteps;
    [SerializeField]
    private AudioClip[] woodSteps;

    // The minimum and maximum intervals between playing footstep sounds.
    public float MinInterval = 0.5f;
    public float MaxInterval = 1f;
    private float timer = 0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

public void PlayFootstep()
{
    Debug.Log("PlayFootstep method called"); // New debug message

    RaycastHit hit;
    if (Physics.Raycast(transform.position, -transform.up, out hit, 1f))
    {
        Debug.Log("Raycast hit: " + hit.collider.gameObject.name); // New debug message

        AudioClip[] stepSounds = null;
        switch (hit.collider.gameObject.tag)
        {
            case "grass":
                stepSounds = grassSteps;
                break;
            case "wood":
                stepSounds = woodSteps;
                break;
            case "stone":
                stepSounds = stoneSteps;
                break;
        }

        // Make sure we have sounds to play
        if (stepSounds != null && stepSounds.Length > 0)
        {
            // Pick a random clip from the chosen array
            AudioClip clip = stepSounds[Random.Range(0, stepSounds.Length)];
            audioSource.PlayOneShot(clip);

            // Debugging Information
            Debug.Log("Playing footstep sound. Current tag: " + hit.collider.gameObject.tag + ". Clip: " + clip.name);
        }
    }
}





    void Update()
    {
        timer += Time.deltaTime;
    }

    public bool CanPlaySound()
    {
        return timer >= MinInterval;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}
