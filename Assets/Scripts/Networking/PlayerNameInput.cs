using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Networking
{
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInput : MonoBehaviour
    {
        private const string PlayerNamePrefKey = "PlayerName";

        private void Start()
        {
            string defaultName = string.Empty;
            InputField inputField = GetComponent<InputField>();

            if (PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                inputField.text = defaultName;
            }

            PhotonNetwork.NickName = defaultName;
        }

        public void SetPlayerName()
        {
            print("ff");
            string playerName = GetComponent<InputField>().text;
            if (string.IsNullOrEmpty(playerName))
            {
                return;
            }

            PhotonNetwork.NickName = playerName;
            PlayerPrefs.SetString(PlayerNamePrefKey, playerName);
            
            print(PhotonNetwork.NickName);
        }
        
    }
}