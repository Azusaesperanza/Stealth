using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelReset :MonoBehaviour , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData data)
    {
        // reload the scene
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }
<<<<<<< HEAD
=======


    private void Update()
    {
    }
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
}
