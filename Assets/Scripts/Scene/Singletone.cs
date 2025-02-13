using UnityEngine;

public abstract class Singletone<T> : MonoBehaviour where T : Singletone<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this as T;
            OnAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected abstract void OnAwake();
}
