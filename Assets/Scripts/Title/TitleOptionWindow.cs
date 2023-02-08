using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleOptionWindow : OptionWindowCore
{
    protected override void Awake()
    {
        base.Awake();
        CheckData();
    }

    private void CheckData()
    {
        string filePath = Application.persistentDataPath + "/" + GameManager.fileName;
        if (!File.Exists(filePath))
        {
            optionTransforms[0].gameObject.SetActive(false);
            optionTransforms.RemoveAt(0);
            optionValue.Value = int.MaxValue;
        }
    }

    public override void DecideCurrentOption()
    {
        int val = (optionTransforms.Count == 4) ? optionValue.Value : optionValue.Value + 1;
        switch (val)
        {
            case 0:
                LoadGame();
                break;
            case 1:
                NewGame();
                break;
            case 2:
                Option();
                break;
            case 3:
                Exit();
                break;
            default:
                break;
        }
    }

    private void LoadGame()
    {
        NextScene();
        Debug.Log("LoadGame");
    }

    private void NewGame()
    {
        SaveData data = new SaveData();
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameManager.fileName;

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);

        NextScene();

        Debug.Log("NewGame");
    }

    private void NextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Option()
    {
        Debug.Log("Option");
    }

    private void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
