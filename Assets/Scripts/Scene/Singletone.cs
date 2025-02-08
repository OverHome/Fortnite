using UnityEngine;

public abstract class Singletone<T> : MonoBehaviour where T : Singletone<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
        OnAwake();
    }

    protected abstract void OnAwake();
}
