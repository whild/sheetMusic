using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    [SerializeField] Button button;

    private void Start()
    {
        SetButtonEvent();
    }

    private void SetButtonEvent()
    {
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
        });
    }
}
