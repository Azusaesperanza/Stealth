using System;
using UnityEngine;
<<<<<<< HEAD
<<<<<<< HEAD
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

#pragma warning disable 618
[RequireComponent(typeof (Image))]
=======
=======
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (GUITexture))]
<<<<<<< HEAD
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
=======
=======
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

#pragma warning disable 618
[RequireComponent(typeof (Image))]
>>>>>>> cbf288d (多分これが基なるやつ。)
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
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
