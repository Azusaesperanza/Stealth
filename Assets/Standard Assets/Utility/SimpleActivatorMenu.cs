using System;
using UnityEngine;
<<<<<<< HEAD
<<<<<<< HEAD
using UnityEngine.UI;

#pragma warning disable 618
=======

>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
=======

=======
using UnityEngine.UI;

#pragma warning disable 618
>>>>>>> cbf288d (多分これが基なるやつ。)
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // An incredibly simple menu which, when given references
        // to gameobjects in the scene
<<<<<<< HEAD
<<<<<<< HEAD
        public Text camSwitchButton;
=======
        public GUIText camSwitchButton;
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
=======
        public GUIText camSwitchButton;
=======
        public Text camSwitchButton;
>>>>>>> cbf288d (多分これが基なるやつ。)
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
        public GameObject[] objects;


        private int m_CurrentActiveObject;


        private void OnEnable()
        {
            // active object starts from first in array
            m_CurrentActiveObject = 0;
            camSwitchButton.text = objects[m_CurrentActiveObject].name;
        }


        public void NextCamera()
        {
            int nextactiveobject = m_CurrentActiveObject + 1 >= objects.Length ? 0 : m_CurrentActiveObject + 1;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextactiveobject);
            }

            m_CurrentActiveObject = nextactiveobject;
            camSwitchButton.text = objects[m_CurrentActiveObject].name;
        }
    }
}
