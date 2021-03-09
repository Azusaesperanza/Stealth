using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace oda
{
    public class PlayerCont : MonoBehaviour
    {
        
        PlayerData playerData;
        int data;
        // Start is called before the first frame update
        void Start()
        {
            playerData = new PlayerData();
            playerData.AllDataBack();
            Debug.Log("");
            playerData.FlashlightActive1 = true;
            playerData.AllDataBack();
        }

        // Update is called once per frame
        void Update()
        {

        }

       void Action()
        {

        }
    }

}

