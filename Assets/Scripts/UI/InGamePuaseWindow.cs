using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePuaseWindow :OptionWindowCore
{
    /// <summary>
    /// 0 = Return Game
    /// 1 = Retry
    /// 2 = Select Level
    /// 3 = Option
    /// 4 = Title
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }


    public override void DecideCurrentOption()
    {
        base.DecideCurrentOption();
        switch (optionValue.Value)
        {
            case 0:
                ReturnGame(this);
                break;
            case 1:
                Retry();
                break;
            //case 2:
            //    SelectLevel();
            //  break;
            case 2:
                Option();
                break;
            case 3:
                Title();
                break;
            default:
                break;
        }

    }

    private void Retry()
    {
        StageManager.Instance.Retry();
        ReturnGame(this);
    }

    private void SelectLevel()
    {
        Debug.Log("SelectLevel");
    }

    private void Option()
    {
        Debug.Log("Option");
        container.SetActive(false);
        var option = GameObject.FindObjectOfType<OptionWindow>();
        PlayerInputController.Instance.SetOptionWindow(option);
        option.Init();
        option.OpenOptionWindow();
    }

    private void Title()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
