using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
using System;

public class QuestionManager : MonoBehaviour
{
    // TextMeshPro fields for displaying question text, question number, and score
    public TextMeshProUGUI questionTextField, questionNumber, ScoreText;

    // Prefab for cube GameObject, parent Transform for instantiated cubes, and next button
    public GameObject cubePrefab;
    public Transform cubesParent;
    public Button nextButton;

    // GameObjects for congratulations and oops screens
    public GameObject congratulationsScreen, OopsScreen;

    // Audio player for win and lose sounds
    [Header("Audio")]
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip Win, Lose;

    // Other variables
    private JSONArray mediumQuestions;
    private int currentQuestionIndex = 0;
    private StackManager stackManager;
    private string difficulty, root, currentAnswer;
    private int score = 0;

    void Start()
    {
        // Get the difficulty from player preferences
        difficulty = PlayerPrefs.GetString("Difficulty");

        // Determine the root based on the difficulty
        switch (difficulty)
        {
            case "easy":
                root = "easy_questions";
                break;
            case "medium":
                root = "medium_questions";
                break;
        }

        // Find the StackManager object in the scene
        stackManager = FindAnyObjectByType<StackManager>();

        // Load the JSON file
        TextAsset jsonFile = Resources.Load<TextAsset>(difficulty);

        // Parse the JSON data
        JSONNode jsonData = JSON.Parse(jsonFile.text);

        // Get the medium_questions array
        mediumQuestions = jsonData[root].AsArray;

        // Load the first question
        LoadQuestion(currentQuestionIndex);

        // Attach next button click event listener
        nextButton.onClick.AddListener(NextQuestion);
    }

    void LoadQuestion(int index)
    {
        int totalQuestions = 18;

        // Set the question number
        questionNumber.text = String.Format("{0}/{1}", index + 1, totalQuestions);

        // Check if the index is within the range of mediumQuestions array
        if (index >= 0 && index < mediumQuestions.Count)
        {
            // Clear previous cubes
            ClearCubes();
            // Get question data
            JSONNode questionData = mediumQuestions[index];
            string initialExpression = questionData["initial_expression"];
            string questionType = questionData["type"];
            currentAnswer = questionData["answer"];

            // Display the question text
            questionTextField.text = questionType + "\n \n" + initialExpression;

            // Instantiate cubes based on the characters in the initial expression
            InstantiateCubes(initialExpression);
        }
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < mediumQuestions.Count)
        {
            // Check the answer before loading the next question
            checkAnswer();

            // Load the next question
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            // Check the answer before showing the final score
            checkAnswer();
            ShowScore();
        }
    }

    public void ShowScore()
    {
        // Clear cubes before showing the final score
        ClearCubes();

        // Set the score text
        ScoreText.text = score.ToString();

        // Play audio based on the score and show congratulations or oops screen
        if (score == 0)
        {
            audioPlayer.PlayOneShot(Lose);
            OopsScreen.SetActive(true);
        }
        else
        {
            congratulationsScreen.SetActive(true);
            audioPlayer.PlayOneShot(Win);
        }
    }

    void ClearCubes()
    {
        // Find all CubeMovement components in the scene and destroy their parent GameObjects
        CubeMovement[] cubeMovements = FindObjectsOfType<CubeMovement>(true);
        foreach (CubeMovement child in cubeMovements)
        {
            Destroy(child.gameObject);
        }

        // Clear the stack in the StackManager
        stackManager.ClearStack();
    }

    void InstantiateCubes(string initialExpression)
    {
        Vector3 startingPos = cubesParent.position;

        // Iterate through each character in the initial expression
        for (int i = 0; i < initialExpression.Length; i++)
        {
            // Ignore characters that are brackets or white spaces
            if (initialExpression[i] == '(' || initialExpression[i] == ')' || initialExpression[i] == '[' || initialExpression[i] == ']' || initialExpression[i] == ' ')
            {
                continue;
            }

            // Instantiate cube prefab
            GameObject cube = Instantiate(cubePrefab, cubesParent);

            // Position the cube based on its index in the initial expression
            cube.transform.position = startingPos;

            // Set the text field of the cube to the character
            cube.GetComponentInChildren<TextMeshProUGUI>().text = initialExpression[i].ToString();

            // Update starting position for the next cube
            startingPos = new Vector3(startingPos.x - 0.15f, startingPos.y, startingPos.z);
        }
    }

    void checkAnswer()
    {
        // Get the answer from the StackManager and compare it with the current answer
        string answer = stackManager.getAnswer();
        bool result = CompareStrings(answer, currentAnswer);

        // Increment score if the answer is correct
        if (result)
        {
            score++;
        }
    }

    bool CompareStrings(string str1, string str2)
    {
        // Remove blank spaces and brackets from both strings
        string cleanStr1 = RemoveSpacesAndBrackets(str1);
        string cleanStr2 = RemoveSpacesAndBrackets(str2);

        // Compare the cleaned strings
        return cleanStr1.Equals(cleanStr2);
    }

    string RemoveSpacesAndBrackets(string str)
    {
        // Replace blank spaces and brackets with an empty string
        return str.Replace(" ", "").Replace("(", "").Replace(")", "");
    }

}
