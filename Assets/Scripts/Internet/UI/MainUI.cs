using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Internet
{
 	public class MainUI : MonoBehaviour
	{
        [SerializeField] Button startSeverBtn;
        [SerializeField] Button startClientBtn;
        [SerializeField] Button startHostBtn;
        [SerializeField] Button stopInternetBtn;

        private void Awake()
        {
            startSeverBtn.onClick.AddListener(OnStartSever);
            startClientBtn.onClick.AddListener(OnStartClient);
            startHostBtn.onClick.AddListener(OnStartHost);
            stopInternetBtn.onClick.AddListener(OnStopInternet);

            //NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnect;
            //NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
            //NetworkManager.Singleton.OnServerStarted += OnSeverStart;
        }
        void OnStartSever()
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("NetworkManager.Singleton.StartServer");
            }
            else
            {
                Debug.Log("Fail:NetworkManager.Singleton.StartServer");
            }
        }
        void OnStartClient()
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("NetworkManager.Singleton.StartClient");
            }
            else
            {
                Debug.Log("Fail:NetworkManager.Singleton.StartClient");
            }
        }
        void OnStartHost()
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("NetworkManager.Singleton.StartHost");
            }
            else
            {
                Debug.Log("Fail:NetworkManager.Singleton.StartHost");
            }
        }
        void OnStopInternet()
        {
            NetworkManager.Singleton.Shutdown();
            Debug.Log("InternetShutDown");
        }
        void OnClientConnect(ulong id)
        {
            Debug.Log(string.Format("A new Client Enter, id={0}",id));
        }
        void OnClientDisconnect(ulong id)
        {
            Debug.Log(string.Format("A Client Leave, id={0}", id));
        }
        void OnSeverStart()
        {
            Debug.Log("SeverStart");
        }
    }
}
