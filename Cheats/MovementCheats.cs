using Photon.Pun;
using System;
using UnityEngine;

namespace Recte_1v1lol.Cheats
{
    internal class MovementCheats : MonoBehaviour
    {
        public void OnGUI()
        {
            
        }
        public static void BHop()
        {
            if (PlayerController.LFNGIIPNIDN.LIAGFOMLBEL && Input.GetKey(KeyCode.Space))
            {
                PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.Jump();
            }
        }
        public void Update()
        {
            if (PlayerPrefsX.GetBool("BHop") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN != null)
            {
                BHop();
            }

            if (PlayerPrefsX.GetBool("SpeedHack") && PhotonNetwork.InRoom)
            { 
                Transform transform = PlayerController.LFNGIIPNIDN.transform;
                Camera camera_ = Camera.main;
                if (Input.GetKey(KeyCode.W))
                {
                    transform.localPosition += PlayerPrefs.GetFloat("SpeedBoost") * Time.deltaTime * camera_.transform.forward;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.localPosition += PlayerPrefs.GetFloat("SpeedBoost") * Time.deltaTime * -camera_.transform.right;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.localPosition += PlayerPrefs.GetFloat("SpeedBoost") * Time.deltaTime * -camera_.transform.forward;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.localPosition += PlayerPrefs.GetFloat("SpeedBoost") * Time.deltaTime * camera_.transform.right;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    transform.localPosition += PlayerPrefs.GetFloat("SpeedBoost") * Time.deltaTime * camera_.transform.up;
                }
                if (Input.GetKey(KeyCode.C))
                {
                    transform.localPosition += PlayerPrefs.GetFloat("SpeedBoost") * Time.deltaTime * -camera_.transform.up;
                }
            }
            if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpeedHackKeyCode", "B"))))
            {
                PlayerPrefsX.SetBool("SpeedHack", !PlayerPrefsX.GetBool("SpeedHack"));
            }

            if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("FlightKeyCode", "F"))))
            {
                PlayerPrefsX.SetBool("Flight", !PlayerPrefsX.GetBool("Flight"));
            }
        }

    }
}
