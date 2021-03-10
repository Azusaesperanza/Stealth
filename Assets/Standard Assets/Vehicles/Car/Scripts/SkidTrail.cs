using System;
using System.Collections;
using UnityEngine;

<<<<<<< HEAD
<<<<<<< HEAD
#pragma warning disable 649
=======
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
=======
=======
#pragma warning disable 649
>>>>>>> cbf288d (多分これが基なるやつ。)
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
namespace UnityStandardAssets.Vehicles.Car
{
    public class SkidTrail : MonoBehaviour
    {
        [SerializeField] private float m_PersistTime;


        private IEnumerator Start()
        {
			while (true)
            {
                yield return null;

                if (transform.parent.parent == null)
                {
					Destroy(gameObject, m_PersistTime);
                }
            }
        }
    }
}
