// Note: this script has to be on an always-active UI parent, so that we can
// always find it from other code. (GameObject.Find doesn't find inactive ones)
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public partial class UILogin : MonoBehaviour
{
    public UIPopup uiPopup;
    public NetworkManagerMMO manager; // singleton=null in Start/Awake
    public NetworkAuthenticatorMMO auth;
    public GameObject panel;
    public Text statusText;
    public InputField accountInput;
    public InputField passwordInput;
    public Dropdown serverDropdown;
    public Button loginButton;
    public Button registerButton;
    [TextArea(1, 30)] public string registerMessage = "First time? Just log in and we will\ncreate an account automatically.";
    public Button hostButton;
    public Button dedicatedButton;
    public Button cancelButton;
    public Button quitButton;

    void Start()
    {
        // load last server by name in case order changes some day.
        if (PlayerPrefs.HasKey("LastServer"))
        {
            string last = PlayerPrefs.GetString("LastServer", "");
            serverDropdown.value = manager.serverList.FindIndex(s => s.name == last);
        }
    }

    void OnDestroy()
    {
        // save last server by name in case order changes some dayf
        PlayerPrefs.SetString("LastServer", serverDropdown.captionText.text);
    }

void Update()
{
    // Show login panel ONLY when truly offline or in handshake
    // Hide it once we reach Lobby (character selection) or World
    bool shouldShowLogin = (manager.state == NetworkState.Offline || 
                            manager.state == NetworkState.Handshake);

    panel.SetActive(shouldShowLogin);

    if (shouldShowLogin)
    {
        // status
        if (manager.isNetworkActive)
        {
            if (NetworkServer.active)           // ← Correct replacement for isServer
                statusText.text = "Hosting...";
            else
                statusText.text = "Connecting...";
        }
        else if (manager.state == NetworkState.Handshake)
            statusText.text = "Handshake...";
        else
            statusText.text = "";

        // buttons
        registerButton.interactable = !manager.isNetworkActive;
        registerButton.onClick.SetListener(() => { uiPopup.Show(registerMessage); });

        loginButton.interactable = !manager.isNetworkActive && auth.IsAllowedAccountName(accountInput.text);
        loginButton.onClick.SetListener(() => { manager.StartClient(); });

        hostButton.interactable = Application.platform != RuntimePlatform.WebGLPlayer && 
                                  !manager.isNetworkActive && 
                                  auth.IsAllowedAccountName(accountInput.text);
        hostButton.onClick.SetListener(() => { manager.StartHost(); });

        cancelButton.gameObject.SetActive(manager.isNetworkActive);
        cancelButton.onClick.SetListener(() => { manager.StopClient(); });

        dedicatedButton.interactable = Application.platform != RuntimePlatform.WebGLPlayer && !manager.isNetworkActive;
        dedicatedButton.onClick.SetListener(() => { manager.StartServer(); });

        quitButton.onClick.SetListener(() => { NetworkManagerMMO.Quit(); });

        // inputs
        auth.loginAccount = accountInput.text;
        auth.loginPassword = passwordInput.text;

        // server dropdown
        serverDropdown.interactable = !manager.isNetworkActive;
        serverDropdown.options = manager.serverList.Select(sv => new Dropdown.OptionData(sv.name)).ToList();
        manager.networkAddress = manager.serverList[serverDropdown.value].ip;
    }
}
}
