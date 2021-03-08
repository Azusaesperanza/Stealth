using System;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

#pragma warning disable 618
[RequireComponent(typeof (Image))]
=======
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (GUITexture))]
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... reload the scene
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
