using System;
using System.Collections;
using UnityEngine;

<<<<<<< HEAD
#pragma warning disable 649
=======
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
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
