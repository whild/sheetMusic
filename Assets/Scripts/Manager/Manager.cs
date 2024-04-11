using UnityEngine;

/// <summary>
/// Singleton Pattern
/// 「.instance」で呼び出す。
/// </summary>
/// <typeparam name="T"></typeparam>
public class Manager<T> : MonoBehaviour
    where T : Manager<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        Instance = (T)this;
    }
}