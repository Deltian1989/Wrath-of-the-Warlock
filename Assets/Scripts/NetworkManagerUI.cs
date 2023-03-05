using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostServerButton;
    [SerializeField] private Button joinServerButton;

    private void Awake()
    {
        hostServerButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            
        });

        joinServerButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) => {
            NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        };
    }
}
