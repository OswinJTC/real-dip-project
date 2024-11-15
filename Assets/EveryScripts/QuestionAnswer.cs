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

    private ThreeCorrectVideoManager videoManager3;
    private TwoCorrectVideoManager videoManager2;
    private OneCorrectVideoManager videoManager1;
    private ZeroCorrectVideoManager videoManager0;
    private GameObject transitionCanvas; // To reference the TransitionCanvas

    void Start()
    {
        InitializeQuestions();
        DisplayQuestion(currentQuestionIndex);

        foreach (var button in answerButtons)
        {
            button.onClick.AddListener(delegate { OnAnswerSelected(button); });
        }

        videoManager3 = FindObjectOfType<ThreeCorrectVideoManager>();
        videoManager2 = FindObjectOfType<TwoCorrectVideoManager>();
        videoManager1 = FindObjectOfType<OneCorrectVideoManager>();
        videoManager0 = FindObjectOfType<ZeroCorrectVideoManager>();

        LocateTransitionCanvas();
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
        questionText.text = q.questionText;
        selectedAnswers.Clear();

        foreach (Button button in answerButtons)
        {
            if (button != null)
            {
                var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
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
            }
            else if (answerButtons[i] != null)
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
            Debug.LogError("Selected button index out of range!");
            return;
        }

        Question currentQuestion = questions[currentQuestionIndex];

        if (!selectedAnswers.Contains(selectedIndex))
        {
            selectedAnswers.Add(selectedIndex);
            selectedButton.GetComponentInChildren<TextMeshProUGUI>().color = selectedTextColor;
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
                DisplayResults();
            }
        }
    }

    void DisplayResults()
    {
        ToggleTransitionCanvas(true);

        if (correctAnswersCount == 3 && videoManager3 != null)
        {
            StartCoroutine(videoManager3.PlayVideoAndChangeScene("After MCQ"));
        }
        else if (correctAnswersCount == 2 && videoManager2 != null)
        {
            StartCoroutine(videoManager2.PlayVideoAndChangeScene("After MCQ"));
        }
        else if (correctAnswersCount == 1 && videoManager1 != null)
        {
            StartCoroutine(videoManager1.PlayVideoAndChangeScene("After MCQ"));
        }
        else if (videoManager0 != null)
        {
            StartCoroutine(videoManager0.PlayVideoAndChangeScene("After MCQ"));
        }

        ToggleTransitionCanvas(false);
    }

    void LocateTransitionCanvas()
    {
        var allCanvases = FindObjectsOfType<Canvas>();
        foreach (var canvas in allCanvases)
        {
            if (canvas.name == "TransitionCanvas")
            {
                transitionCanvas = canvas.gameObject;
                Debug.Log("TransitionCanvas found successfully.");
                return;
            }
        }

        Debug.LogWarning("TransitionCanvas not found in the scene!");
    }

    void ToggleTransitionCanvas(bool state)
    {
        if (transitionCanvas != null)
        {
            transitionCanvas.SetActive(state);
            Debug.Log($"TransitionCanvas set to {(state ? "Active" : "Inactive")}.");
        }
        else
        {
            Debug.LogWarning("Cannot toggle TransitionCanvas because it is not assigned.");
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
