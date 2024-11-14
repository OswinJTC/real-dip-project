using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionAnswer : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public Color selectedTextColor = Color.yellow;
    public Color defaultTextColor = Color.white;

    private List<Question> questions = new List<Question>();
    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;
    private List<int> selectedAnswers = new List<int>();

    private ThreeCorrectVideoManager videoManager3; // Reference to the video manager
    private TwoCorrectVideoManager videoManager2; // Reference to the video manager
    private OneCorrectVideoManager videoManager1; // Reference to the video manager
    private ZeroCorrectVideoManager videoManager0; // Reference to the video manager

    void Start()
    {
        InitializeQuestions();
        DisplayQuestion(currentQuestionIndex);

        foreach (var button in answerButtons)
        {
            button.onClick.AddListener(delegate { OnAnswerSelected(button); });
        }

        videoManager3 = FindObjectOfType<ThreeCorrectVideoManager>();
        if (videoManager3 == null)
        {
            Debug.LogError("ThreeCorrectVideoManager not found! Ensure it is added to the scene.");
        }

        videoManager2 = FindObjectOfType<TwoCorrectVideoManager>();
        if (videoManager2 == null)
        {
            Debug.LogError("TwoCorrectVideoManager not found! Ensure it is added to the scene.");
        }

        videoManager1 = FindObjectOfType<OneCorrectVideoManager>();
        if (videoManager1 == null)
        {
            Debug.LogError("OneCorrectVideoManager not found! Ensure it is added to the scene.");
        }

        videoManager0 = FindObjectOfType<ZeroCorrectVideoManager>();
        if (videoManager0 == null)
        {
            Debug.LogError("ZeroCorrectVideoManager not found! Ensure it is added to the scene.");
        }
    }

    void InitializeQuestions()
    {
        questions.Add(new Question("How did Balloon Baby die?", new List<string> { "Food Poisoning", "Sliced up", "Strangled to Death", "Drowned to death" }, new List<int> { 2 }));
        questions.Add(new Question("What were the 2 causes of the murder?", new List<string> { "Eviction of house", "Bankruptcy", "Drug abuse", "Loss of Job" }, new List<int> { 0, 3 }));
        questions.Add(new Question("What was the weapon used on Balloon Baby?", new List<string> { "Axe", "Balloon", "Knife", "Cake" }, new List<int> { 1 }));
    }

    void DisplayQuestion(int questionIndex)
    {
        if (questionIndex < 0 || questionIndex >= questions.Count)
        {
            Debug.LogError("Question index is out of range: " + questionIndex);
            return;
        }

        Question q = questions[questionIndex];
        if (questionText == null)
        {
            Debug.LogError("questionText is not assigned!");
            return;
        }

        questionText.text = q.questionText;
        selectedAnswers.Clear();

        foreach (Button button in answerButtons)
        {
            if (button != null)
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.color = defaultTextColor;
                }
                button.interactable = true;
            }
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < q.answers.Count)
            {
                if (answerButtons[i] != null)
                {
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];
                }
                else
                {
                    Debug.LogError($"Answer button at index {i} is not assigned!");
                }
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnAnswerSelected(Button selectedButton)
    {
        int selectedIndex = System.Array.IndexOf(answerButtons, selectedButton);

        if (selectedIndex < 0 || selectedIndex >= answerButtons.Length)
        {
            Debug.LogError("Selected button index is out of range: " + selectedIndex);
            return;
        }

        Question currentQuestion = questions[currentQuestionIndex];

        if (currentQuestionIndex == 1)
        {
            if (currentQuestion.correctAnswers.Count == 2)
            {
                if (selectedAnswers.Contains(selectedIndex))
                {
                    selectedAnswers.Remove(selectedIndex);
                    TextMeshProUGUI buttonText = selectedButton.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.color = defaultTextColor;
                    }
                }
                else
                {
                    if (selectedAnswers.Count < 2)
                    {
                        selectedAnswers.Add(selectedIndex);
                        TextMeshProUGUI buttonText = selectedButton.GetComponentInChildren<TextMeshProUGUI>();
                        if (buttonText != null)
                        {
                            buttonText.color = selectedTextColor;
                        }
                    }
                    else
                    {
                        questionText.text = "Please select only 2 answers.";
                        return;
                    }
                }
            }
            else
            {
                selectedAnswers.Clear();
                selectedAnswers.Add(selectedIndex);
                foreach (Button button in answerButtons)
                {
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.color = defaultTextColor;
                    }
                }
                TextMeshProUGUI selectedText = selectedButton.GetComponentInChildren<TextMeshProUGUI>();
                if (selectedText != null)
                {
                    selectedText.color = selectedTextColor;
                }
            }
        }
        else
        {
            selectedAnswers.Clear();
            selectedAnswers.Add(selectedIndex);
        }

        if (selectedAnswers.Count == currentQuestion.correctAnswers.Count &&
            selectedAnswers.TrueForAll(answer => currentQuestion.correctAnswers.Contains(answer)))
        {
            correctAnswersCount++;
        }

        if (selectedAnswers.Count == currentQuestion.correctAnswers.Count)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Count)
            {
                DisplayQuestion(currentQuestionIndex);
            }
            else
            {
                DisplayResults();  // Display results and transition logic handled by the video manager
            }
        }
    }

    void DisplayResults()
    {
        Debug.Log($"Total Correct Answers: {correctAnswersCount}");

        if (correctAnswersCount == 3)
        {
            StartCoroutine(videoManager3.PlayVideoAndChangeScene("After MCQ"));
            Debug.Log("You got all three questions correct!");
        }
        else if (correctAnswersCount == 2)
        {
            StartCoroutine(videoManager2.PlayVideoAndChangeScene("After MCQ"));
            Debug.Log("You got two questions correct.");
        }
        else if (correctAnswersCount == 1)
        {
            StartCoroutine(videoManager1.PlayVideoAndChangeScene("After MCQ"));
            Debug.Log("You got one question correct.");
        }
        else
        {
            StartCoroutine(videoManager0.PlayVideoAndChangeScene("After MCQ"));
            Debug.Log("You got all questions wrong.");
        }
    }
}

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
