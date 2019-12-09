using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLogic : MonoBehaviour
{
    private ScreenTransition st;

    public bool pickRandomScene;
    private string sceneToGoTo;
    private int randomSceneToGoTo;

    public AudioSource exitSound;

    void Start()
    {
        sceneToGoTo = "TitleScreen";
        st = GameObject.Find("Player").GetComponent<ScreenTransition>();
        //print("num scenes: " + SceneManager.sceneCountInBuildSettings);
    }

    IEnumerator StartTransition()
    {

        if (pickRandomScene)
        {
            exitSound.Play();
            st.FadeOut();
            yield return new WaitForSeconds(st.fadeLength);
            int randomSceneToGoTo = Random.Range(0, SceneManager.sceneCountInBuildSettings);
            SceneManager.LoadScene(randomSceneToGoTo);
        }
        else
        {
            SceneManager.LoadScene(sceneToGoTo);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player1")
        {
            st.FadeOut();
           // StartCoroutine(StartTransition());
        }
    }
}
