using UnityEngine;

public class KeepAlive : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Destroy(this);
    }
}
