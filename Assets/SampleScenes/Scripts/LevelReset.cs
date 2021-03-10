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
<<<<<<< HEAD
=======
=======
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1


    private void Update()
    {
    }
<<<<<<< HEAD
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
=======
=======
>>>>>>> cbf288d (多分これが基なるやつ。)
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
}
