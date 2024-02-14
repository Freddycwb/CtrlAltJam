using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float delay;
    private bool alreadyChanging;

    public void ChangeScene(string scene)
    {
        if (alreadyChanging)
        {
            return;
        }
        alreadyChanging = true;
        transition.Play("GoingToScene");
        StartCoroutine(ChangeSceneRoutine(scene));
    }

    private IEnumerator ChangeSceneRoutine(string scene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}
