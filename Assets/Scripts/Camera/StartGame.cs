using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name);
        if (gameObject.name.Contains("Start"))
        {
            SceneManager.LoadScene(1);
        }
        else if (gameObject.name.Contains("Exist"))
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                                                Application.Quit();
            #endif
        }
    }
}
