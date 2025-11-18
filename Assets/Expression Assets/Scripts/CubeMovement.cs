using TMPro;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] StackManager stackManager; // Reference to the StackManager
    GameObject audioPlayer; // Reference to the audio player GameObject

    // Start is called before the first frame update
    private void Start()
    {
        // Find the StackManager object in the scene
        stackManager = FindAnyObjectByType<StackManager>();

        // Find the audio player GameObject with the tag "AudioTag"
        audioPlayer = GameObject.FindGameObjectWithTag("AudioTag");
    }

    // Method called when the cube is clicked
    public void CubeClicked()
    {
        // Play audio if the audio player is found
        if (audioPlayer != null)
        {
            audioPlayer.GetComponent<AudioSource>().Play();
        }

        // Get the text displayed on the cube
        string name = transform.GetComponentInChildren<TextMeshProUGUI>().text;

        // Add the cube to the CubeManager in the StackManager
        stackManager.cubeManager.AddCube(gameObject);

        // Deactivate the cube
        gameObject.SetActive(false);

        // Push the cube's text to the stack in the StackManager
        stackManager.PushToStack(name);
    }
}
