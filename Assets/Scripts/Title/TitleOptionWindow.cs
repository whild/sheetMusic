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

        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
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
