using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Take2DCamera : MonoBehaviour
{
    public Camera _camera;
    public MeshRenderer wall;
    public RawImage rawImage;
    private void Awake()
    {
        TryGetComponent(out _camera);
    }


    [ContextMenu("Test")]
    public void TakeCamera_ToRender()
    {
        StartCoroutine(TakeRender());
    }

    IEnumerator TakeRender()
    {
        yield return new WaitForEndOfFrame();
       // RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        Texture2D _main2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, _main2D.width, _main2D.height);
        //RenderTexture.active = rt;
        _main2D.ReadPixels(rect, 0, 0);
        _main2D.Apply();
        GameManager._walls.material.mainTexture = _main2D;
        rawImage.texture = _main2D;
    }

}
