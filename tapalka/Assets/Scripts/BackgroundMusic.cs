using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
 private BackgroundMusic instance;   
    private void Awake()
    {
        if (instance == null)
        {
        DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
