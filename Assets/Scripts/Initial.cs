using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Initial : MonoBehaviour
{
    public Button dynamic;
    public Button management;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        dynamic.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Dynamic");
        });
        management.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Management");
        });
        quit.onClick.AddListener(delegate ()
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
