using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte.Cheats
{
    public class Movement : MonoBehaviour
    {
        public static void BHop()
        {
            if (PlayerController.LFNGIIPNIDN.LIAGFOMLBEL && Input.GetKey(KeyCode.Space))
            {
                PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.Jump();
            }
        }
        public void Update()
        {
            if (Vars.Bhop && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN != null)
            {
                BHop();
            }

            if (Vars.SpeedHack && PhotonNetwork.InRoom)
            {
                Transform transform = PlayerController.LFNGIIPNIDN.transform;
                Camera camera_ = Camera.main;
                if (Input.GetKey(KeyCode.W))
                {
                    transform.localPosition += Vars.SpeedBoost * Time.deltaTime * camera_.transform.forward;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.localPosition += Vars.SpeedBoost * Time.deltaTime * -camera_.transform.right;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.localPosition += Vars.SpeedBoost * Time.deltaTime * -camera_.transform.forward;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.localPosition += Vars.SpeedBoost * Time.deltaTime * camera_.transform.right;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    transform.localPosition += Vars.SpeedBoost * Time.deltaTime * camera_.transform.up;
                }
                if (Input.GetKey(KeyCode.C))
                {
                    transform.localPosition += Vars.SpeedBoost * Time.deltaTime * -camera_.transform.up;
                }
            }
            if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpeedKeyCode", "B"))))
            {
                Vars.SpeedHack = !Vars.SpeedHack;
            }

            if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("FlyKeyCode", "F"))))
            {
                Vars.Flight = !Vars.Flight;
            }
        }
    }
}
