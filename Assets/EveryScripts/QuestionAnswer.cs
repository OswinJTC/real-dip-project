using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro
using UnityEngine.SceneManagement;  // Import SceneManager

public class QuestionAnswer : MonoBehaviour
{
    public TextMeshProUGUI questionText;  // TextMeshPro for question text
    public Button[] answerButtons;  // Array of buttons for the answers
    public Color selectedTextColor = Color.yellow;  // Color for the selected text
    public Color defaultTextColor = Color.white;  // Default color for button text

    private List<Question> questions = new List<Question>();  // List of questions
    private int currentQuestionIndex = 0;  // Track the current question
    private int correctAnswersCount = 0;  // Counter for correct answers
    private List<int> selectedAnswers = new List<int>();  // List to track selected answers

    void Start()
    {
        // Initialize questions
        InitializeQuestions();

        // Display the first question
        DisplayQuestion(currentQuestionIndex);

        // Attach OnClick listener to each button
        foreach (var button in answerButtons)
        {
            button.onClick.AddListener(delegate { OnAnswerSelected(button); });
        }
    }

    // Initialize a set of questions and correct answers
    void InitializeQuestions()
    {
        questions.Add(new Question("How did Balloon Baby die?", new List<string> { "Food Poisoning", "Sliced up", "Strangled to Death", "Drowned to death" }, new List<int> { 2 }));
        questions.Add(new Question("What were the 2 causes of the murder?", new List<string> { "Eviction of house", "Bankruptcy", "Drug abuse", "Loss of Job" }, new List<int> { 0, 3 }));
        questions.Add(new Question("What was the weapon used on Balloon Baby?", new List<string> { "Axe", "Balloon", "Knife", "Cake" }, new List<int> { 1 }));
    }

    // Display the question and reset the buttons
    void DisplayQuestion(int questionIndex)
    {
        // Safety check for the question index
        if (questionIndex < 0 || questionIndex >= questions.Count)
        {
            Debug.LogError("Question index is out of range: " + questionIndex);
            return;
        }

        Question q = questions[questionIndex];

        // Safety check for questionText
        if (questionText == null)
        {
            Debug.LogError("questionText is not assigned!");
            return;
        }

        questionText.text = q.questionText;

        // Clear the selected answers for the new question
        selectedAnswers.Clear();

        // Reset button text colors
        foreach (Button button in answerButtons)
        {
            if (button != null)
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.color = defaultTextColor;  // Reset button text color
                }
                button.interactable = true;  // Enable buttons
            }
        }

        // Set text for each answer button using TextMeshProUGUI
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < q.answers.Count) // Check if there are enough answers
            {
                if (answerButtons[i] != null)
                {
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];  // Update answer text
                }
                else
                {
                    Debug.LogError($"Answer button at index {i} is not assigned!");
                }
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false); // Hide buttons if there are no answers
            }
        }
    }

    // Called when any answer button is clicked
    void OnAnswerSelected(Button selectedButton)
    {
        // Get the index of the selected button
        int selectedIndex = System.Array.IndexOf(answerButtons, selectedButton);

        // Safety check for selected index
        if (selectedIndex < 0 || selectedIndex >= answerButtons.Length)
        {
            Debug.LogError("Selected button index is out of range: " + selectedIndex);
            return;
        }

        // Check if the current question allows multiple answers or single answer
        Question currentQuestion = questions[currentQuestionIndex];

        if (currentQuestionIndex == 1)  // Only change text color for question 2
        {
            if (currentQuestion.correctAnswers.Count == 2)
            {
                // Toggle the selection of the answer for multiple-choice questions
                if (selectedAnswers.Contains(selectedIndex))
                {
                    selectedAnswers.Remove(selectedIndex);  // Deselect if already selected
                    // Reset the button text color if deselected
                    TextMeshProUGUI buttonText = selectedButton.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.color = defaultTextColor;  // Reset text color
                    }
                }
                else
                {
                    if (selectedAnswers.Count < 2)
                    {
                        selectedAnswers.Add(selectedIndex);  // Select the answer
                        // Change the button text color to indicate selection
                        TextMeshProUGUI buttonText = selectedButton.GetComponentInChildren<TextMeshProUGUI>();
                        if (buttonText != null)
                        {
                            buttonText.color = selectedTextColor;  // Change text color
                        }
                    }
                    else
                    {
                        // Provide feedback for selecting more than 2 answers
                        questionText.text = "Please select only 2 answers.";
                        return; // Exit if more than 2 answers are selected
                    }
                }
            }
            else
            {
                // For single answer questions, replace the selection
                selectedAnswers.Clear();  // Clear previous selections
                selectedAnswers.Add(selectedIndex);  // Add the new selection
                // Reset color for all buttons and change text color of the selected button
                foreach (Button button in answerButtons)
                {
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.color = defaultTextColor;  // Reset button text color
                    }
                }
                TextMeshProUGUI selectedText = selectedButton.GetComponentInChildren<TextMeshProUGUI>();
                if (selectedText != null)
                {
                    selectedText.color = selectedTextColor;  // Change text color of selected button
                }
            }
        }
        else
        {
            // For other questions, just clear previous selections without changing text color
            selectedAnswers.Clear();
            selectedAnswers.Add(selectedIndex);
        }

        // Check if the correct answers are selected
        if (selectedAnswers.Count == currentQuestion.correctAnswers.Count &&
            selectedAnswers.TrueForAll(answer => currentQuestion.correctAnswers.Contains(answer)))
        {
            correctAnswersCount++;  // Increment counter for correct answers
        }

        // Move to the next question if the correct number of answers is selected
        if (selectedAnswers.Count == currentQuestion.correctAnswers.Count)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Count)
            {
                DisplayQuestion(currentQuestionIndex);
            }
            else
            {
                // Handle scene transition after the third question
                SceneManager.LoadScene("After MCQ");  // Replace "After MCQ" with the actual name of your scene
            }
        }
    }
}

// Define a Question class to store the question text and answers
[System.Serializable]
public class Question
{
    public string questionText;
    public List<string> answers;
    public List<int> correctAnswers;

    public Question(string questionText, List<string> answers, List<int> correctAnswers)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswers = correctAnswers;
    }
}
