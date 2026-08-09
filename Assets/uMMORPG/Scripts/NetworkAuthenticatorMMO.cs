using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class NetworkAuthenticatorMMO : NetworkAuthenticator
{
    [Header("Components")]
    public NetworkManagerMMO manager;

    [Header("Login")]
    public string loginAccount = "";
    public string loginPassword = "";

    [Header("Security")]
    public string passwordSalt = "at_least_16_byte";
    public int accountMaxLength = 16;

    // ===================================================================
    // CLIENT SIDE
    // ===================================================================

    public override void OnStartClient()
    {
        base.OnStartClient();

        NetworkClient.RegisterHandler<ErrorMsg>(OnClientError, false);
        NetworkClient.RegisterHandler<LoginSuccessMsg>(OnClientLoginSuccess, false);

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnStartClient_");
    }

    /// <summary>
    /// Called on the CLIENT when server sends an ErrorMsg (login failed, version mismatch, etc.)
    /// </summary>
    void OnClientError(ErrorMsg msg)
    {
        Debug.LogWarning($"Client received error: {msg.text}");

        // TODO: Uncomment when uiPopup is assigned in inspector
        // if (uiPopup != null)
        //     uiPopup.Show(msg.text);
        
        Debug.LogWarning($"Client received error: {msg.text}");
        if (msg.causesDisconnect)
            NetworkClient.Disconnect();
    }

    void OnClientLoginSuccess(LoginSuccessMsg msg)
    {
        Debug.Log("[Auth] LoginSuccessMsg received on client");
        OnClientAuthenticated.Invoke();
        // Do NOT set state here
    }

    public override void OnClientAuthenticate()
    {
        // Send login request with hashed password
        string hash = Utils.PBKDF2Hash(loginPassword, passwordSalt + loginAccount);
        LoginMsg message = new LoginMsg
        {
            account = loginAccount,
            password = hash,
            version = Application.version
        };

        NetworkClient.connection.Send(message);
        
        Debug.Log($"[Auth] Client about to send LoginMsg for account: {loginAccount}");
        Debug.Log("[Auth] LoginMsg sent");

        manager.state = NetworkState.Handshake;
    }

    // ===================================================================
    // SERVER SIDE
    // ===================================================================

    public override void OnStartServer()
    {
        // Register login handler (allowed before full authentication)
        NetworkServer.RegisterHandler<LoginMsg>(
            (conn, msg) => OnServerLogin(conn, msg), 
            false
        );

        // addon system hooks
        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnStartServer_");
    }

    public override void OnServerAuthenticate(NetworkConnectionToClient conn)
    {
        // We wait for LoginMsg from client - nothing to do here
        Debug.Log($"[Auth] OnServerAuthenticate called for connectionId: {conn.connectionId}");
    }

    void OnServerLogin(NetworkConnectionToClient conn, LoginMsg msg)
    {
        Debug.Log($"[Auth] OnServerLogin received for account: {msg.account}");
        if (msg.version != Application.version)
        {
            manager.ServerSendError(conn, "Version mismatch", true);
            return;
        }

        if (!IsAllowedAccountName(msg.account))
        {
            manager.ServerSendError(conn, "Invalid account name", true);
            return;
        }

        if (Database.singleton.TryLogin(msg.account, msg.password))
        {
            if (!AccountLoggedIn(msg.account))
            {
                manager.lobby[conn] = msg.account;

                conn.Send(manager.MakeCharactersAvailableMessage(msg.account));
                conn.Send(new LoginSuccessMsg());

                OnServerAuthenticated.Invoke(conn);

                Debug.Log($"Login successful for account: {msg.account}");
            }
            else
            {
                manager.ServerSendError(conn, "Account already logged in", true);
            }
        }
        else
        {
            manager.ServerSendError(conn, "Invalid username or password", true);
        }
    }

    // ===================================================================
    // HELPER METHODS
    // ===================================================================

    public bool IsAllowedAccountName(string account)
    {
        return account.Length <= accountMaxLength &&
               Regex.IsMatch(account, @"^[a-zA-Z0-9_]+$");
    }

    bool AccountLoggedIn(string account)
    {
        return manager.lobby.ContainsValue(account) ||
               Player.onlinePlayers.Values.Any(p => p.account == account);
    }

    private void OnValidate()
    {
        // Safely find and assign the NetworkManagerMMO if it's not set
        if (manager == null)
        {
            manager = FindObjectOfType<NetworkManagerMMO>();
        }

        if (manager == null)
        {
            Debug.LogWarning("NetworkAuthenticatorMMO: 'manager' field is not assigned. Please drag your NetworkManagerMMO into this field in the Inspector.", this);
        }
    }
}