using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOption : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Option", LoadSceneMode.Additive);
    }
}