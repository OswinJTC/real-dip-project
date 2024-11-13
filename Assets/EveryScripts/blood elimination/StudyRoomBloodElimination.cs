using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudyRoomBloodElimination : MonoBehaviour
{
    public float detectionRadius = 2f;
    public GameObject[] bloodObjects;
    private int bloodCount = 0;
    // private StudyRoomVideoManager videoManager;  // Updated to match new name

    void Start()
    {
        if (GameManager.instance.isStudyRoomClean)
        {
            foreach (GameObject blood in bloodObjects)
            {
                Destroy(blood);
            }
            Debug.Log("Study room is already clean.");
            return;
        }

        bloodObjects = GameObject.FindGameObjectsWithTag("Blood");

        // Find the StudyRoomVideoManager to play the video
        // videoManager = FindObjectOfType<StudyRoomVideoManager>();
        // if (videoManager == null)
        // {
        //     Debug.LogError("StudyRoomVideoManager not found!");
        // }
    }

    void Update()
    {
        // Check if the cleaning kit is available instead of item activation
        if (GameManager.instance.isCleaningKit)
        {
            foreach (GameObject blood in bloodObjects)
            {
                if (blood != null)
                {
                    float distanceToBlood = Vector3.Distance(transform.position, blood.transform.position);

                    if (distanceToBlood <= detectionRadius && Input.GetKeyDown(KeyCode.Q) && GameManager.instance.isCleaningKitInUsed)
                    {
                        Destroy(blood);
                        bloodCount++;
                        Debug.Log("Blood eliminated in the study room. Total eliminated: " + bloodCount);

                        if (bloodCount == bloodObjects.Length)
                        {
                            GameManager.instance.isStudyRoomClean = true;

                            // Play the video first, then change the scene
                            // if (videoManager != null)
                            // {
                            //     Debug.Log("Playing video before changing scene...");
                            //     StartCoroutine(PlayVideoAndChangeScene("TutStudyCScene"));
                            // }
                            if (TransitionManager.instance != null)
                            {
                                TransitionManager.instance.ChangeScene("TutStudyCScene");
                                UIManager.instance.ShowPrompt("Whoa, what the hell...? Well, well... Someone’s got secrets. Guess I’m not done yet.", 14f);

                            }else{
                                Debug.LogWarning("TransitionManager instance not found, loading the scene directly.");
                                SceneManager.LoadScene("TutStudyCScene");

                            }
                        }
                    }
                }
            }
        }
    }

    // Coroutine to handle video playback, then change the scene
    // private IEnumerator PlayVideoAndChangeScene(string sceneName)
    // {
    //     Debug.Log("Calling PlayVideo...");
    //     videoManager.PlayVideo();  // Play the video
    //     Debug.Log("Waiting for video to finish...");

    //     yield return new WaitUntil(() => videoManager.videoPlayer.isPrepared);  // Wait for the video to be prepared
    //     yield return new WaitUntil(() => !videoManager.videoPlayer.isPlaying);  // Wait for video to finish

    //     Debug.Log("Video finished, changing scene...");
    //     SceneManager.LoadScene(sceneName);  // Change the scene after the video
    // }
}
