using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class TimedObjectDestructor : MonoBehaviour
    {
        [SerializeField] private float m_TimeOut = 1.0f;
        [SerializeField] private bool m_DetachChildren = false;


        private void Awake()
        {
            Invoke("DestroyNow", m_TimeOut);
        }


        private void DestroyNow()
        {
            if (m_DetachChildren)
            {
                transform.DetachChildren();
            }
<<<<<<< HEAD
<<<<<<< HEAD
            Destroy(gameObject);
=======
            DestroyObject(gameObject);
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
=======
            DestroyObject(gameObject);
=======
            Destroy(gameObject);
>>>>>>> cbf288d (多分これが基なるやつ。)
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
        }
    }
}
