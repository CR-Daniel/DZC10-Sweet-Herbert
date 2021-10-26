using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntro : MonoBehaviour
{
    bool skip;

    // Update is called once per frame
    void Update()
    {
        skip = Input.GetKey(KeyCode.Space);

        if (skip)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
