using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private List<GameObject> stack = new List<GameObject>();
    [SerializeField]  public CubeManager cubeManager;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Add the child GameObject to the list
            stack.Add(transform.GetChild(i).gameObject);
        }
    }


    public void PushToStack(string name)
    {
        foreach (GameObject go in stack)
        {
            if (!go.activeInHierarchy)
            {
                go.GetComponentInChildren<TextMeshProUGUI>().text = name;
                go.SetActive(true);
                return;
            }
        }
    }

    public void Pop()
    {
        for (int i = stack.Count-1; i >= 0; i--)
        {
            if (stack[i].activeInHierarchy)
            {
                stack[i].gameObject.SetActive(false);
                cubeManager.RemoveCube();
                return;
            }
        }
    }

    public void ClearStack()
    {
        foreach (GameObject go in stack)
        {
            go.SetActive(false);
        }
    }

    public string getAnswer()
    {
        StringBuilder concatenatedText = new StringBuilder(); // StringBuilder to efficiently concatenate strings

        // Iterate through the stack list
        foreach (GameObject cube in stack)
        {
            // Check if the cube is active in the hierarchy
            if (cube.activeInHierarchy)
            {
                // Get the TextMeshPro component attached to the cube
                TextMeshProUGUI textMesh = cube.GetComponentInChildren<TextMeshProUGUI>();

                // If TextMeshPro component is found, append its text to the StringBuilder
                if (textMesh != null)
                {
                    concatenatedText.Append(textMesh.text);
                }
            }
        }

        return concatenatedText.ToString();
    }
}
