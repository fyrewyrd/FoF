using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Mirror;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum NetworkState { Offline, Handshake, Lobby, World }

[RequireComponent(typeof(Database))]
public partial class NetworkManagerMMO : NetworkManager
{
    public NetworkState state = NetworkState.Offline;

    public Dictionary<NetworkConnectionToClient, string> lobby = new Dictionary<NetworkConnectionToClient, string>();

    [Header("UI")]
    public UIPopup uiPopup;

    [Header("Server List")]
    public List<ServerInfo> serverList = new List<ServerInfo>()
    {
        new ServerInfo { name = "Local", ip = "localhost" }
    };

    [Header("Logout")]
    public float combatLogoutDelay = 5f;

    [Header("Character Selection")]
    public int selection = -1;
    public Transform[] selectionLocations;
    public Transform selectionCameraLocation;
    
    [Header("Camera Positions")]
    //public Transform selectionCameraLocation;        // Existing - for character list
    public Transform creationCameraLocation;         // NEW - for character creation screen

    [Header("Character Preview Settings")]
    public float creationCameraDistance = 2.5f;      // How far back the camera sits
    public float creationCameraHeight = 1.2f;        // Height offset

    [HideInInspector] public List<Player> playerClasses = new List<Player>();

    [Header("Database")]
    public int characterLimit = 4;
    public int characterNameMaxLength = 16;
    public float saveInterval = 60f;

    [HideInInspector]
    public CharactersAvailableMsg charactersAvailableMsg;

    [Serializable]
    public class ServerInfo
    {
        public string name;
        public string ip;
    }

    // ===================================================================
    // INITIALIZATION
    // ===================================================================

    public override void Awake()
    {
        base.Awake();

        if (playerClasses == null)
            playerClasses = new List<Player>();

        playerClasses.Clear();
        playerClasses.AddRange(FindPlayerClasses());
    }

    public List<Player> FindPlayerClasses()
    {
        List<Player> classes = new List<Player>();

        if (spawnPrefabs == null) return classes;

        foreach (GameObject prefab in spawnPrefabs)
        {
            if (prefab != null)
            {
                Player player = prefab.GetComponent<Player>();
                if (player != null && !classes.Contains(player))
                    classes.Add(player);
            }
        }

        return classes;
    }

    public override void Start()
    {
        base.Start();
        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "Start_");
    }

    void Update()
    {
        if (NetworkClient.localPlayer != null)
            state = NetworkState.World;
    }

    // ===================================================================
    // CLIENT SIDE
    // ===================================================================

    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log("[Client] OnStartClient fired");

        NetworkClient.ReplaceHandler<ErrorMsg>(
            (msg, channel) => OnClientError(null, msg),
            false
        );

        NetworkClient.ReplaceHandler<CharactersAvailableMsg>(
            (msg, channel) => OnCharactersAvailable(null, msg),
            false
        );

        // Critical for Host mode
        if (NetworkClient.isConnected && !NetworkClient.ready)
        {
            Debug.Log("[Client] Marking local client as Ready");
            NetworkClient.Ready();
        }

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnStartClient_");
    }

    void OnClientError(NetworkConnectionToClient conn, ErrorMsg msg)
    {
        Debug.LogWarning($"Client Error: {msg.text}");
        if (uiPopup != null) uiPopup.Show(msg.text);

        if (msg.causesDisconnect)
            NetworkClient.Disconnect();
    }

    void OnCharactersAvailable(NetworkConnectionToClient conn, CharactersAvailableMsg msg)
    {
        Debug.Log($"[Client] OnCharactersAvailable received {msg.characters.Length} characters");
        charactersAvailableMsg = msg;
        state = NetworkState.Lobby;

        // Call the camera positioning hook
        OnClientCharactersAvailable_(msg);

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnClientCharactersAvailable_", msg);
    }
    
    // Camera positioning for character selection screen
    void OnClientCharactersAvailable_(CharactersAvailableMsg msg)
    {
        Debug.Log("[Camera] OnClientCharactersAvailable_ triggered");

        if (Camera.main == null)
        {
            Debug.LogWarning("[Camera] Camera.main is null!");
            return;
        }

        if (selectionCameraLocation != null)
        {
            Camera.main.transform.position = selectionCameraLocation.position;
            Camera.main.transform.rotation = selectionCameraLocation.rotation;
            Debug.Log($"[Camera] Moved Camera.main to selection position");
        }
        else
        {
            Debug.LogWarning("[Camera] selectionCameraLocation is not assigned in Inspector!");
        }
    }
    
/*
    // ===================================================================
    // SERVER SIDE - Reliable Lobby Handling
    // ===================================================================

    public override void OnStartServer()
    {
        base.OnStartServer();

        Database.singleton.Connect();

        NetworkServer.RegisterHandler<CharacterCreateMsg>(
            (conn, msg) => OnServerCharacterCreate(conn, msg),
            false
        );

        NetworkServer.RegisterHandler<CharacterSelectMsg>(
            (conn, msg) => OnServerCharacterSelect(conn, msg),
            false
        );

        NetworkServer.RegisterHandler<CharacterDeleteMsg>(
            (conn, msg) => OnServerCharacterDelete(conn, msg),
            false
        );

        InvokeRepeating(nameof(SavePlayers), saveInterval, saveInterval);

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnStartServer_");
    }*/
    
    // ===================================================================
    // FALLBACK: Force character list after scene load (Host mode safety net)
    // ===================================================================

    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("[Server] OnStartServer called");

        Database.singleton.Connect();

        NetworkServer.RegisterHandler<CharacterCreateMsg>((conn, msg) => OnServerCharacterCreate(conn, msg), false);
        NetworkServer.RegisterHandler<CharacterSelectMsg>((conn, msg) => OnServerCharacterSelect(conn, msg), false);
        NetworkServer.RegisterHandler<CharacterDeleteMsg>((conn, msg) => OnServerCharacterDelete(conn, msg), false);

        InvokeRepeating(nameof(SavePlayers), saveInterval, saveInterval);

        // NEW: Force character list for local host after a short delay
        Invoke(nameof(ForceSendCharacterListToHost), 0.8f);

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnStartServer_");
    }

    private void ForceSendCharacterListToHost()
    {
        Debug.Log("[Server] ForceSendCharacterListToHost triggered (fallback timer)");

        NetworkConnectionToClient localConn = NetworkServer.localConnection;
        if (localConn == null)
        {
            Debug.LogWarning("[Server] localConnection was null");
            return;
        }

        if (!lobby.ContainsKey(localConn))
        {
            lobby[localConn] = "local";
            Debug.Log("[Server] Added 'local' account to lobby");
        }

        string account = lobby[localConn];

        // Force at least one character if none exist (for testing)
        if (Database.singleton.CharactersForAccount(account).Count == 0)
        {
            Debug.LogWarning($"[Server] No characters found for account '{account}'. Creating a test character for debugging.");
            // You can manually create one here, or just continue with empty list for now
        }

        CharactersAvailableMsg msg = MakeCharactersAvailableMessage(account);
        localConn.Send(msg);

        Debug.Log($"[Server] FORCED SENT CharactersAvailableMsg with {msg.characters.Length} characters");
    }

    // ===================================================================
    // CHARACTER HANDLERS (Keep UCE hooks)
    // ===================================================================

    void OnServerCharacterCreate(NetworkConnectionToClient conn, CharacterCreateMsg message)
    {
        if (conn == null || !lobby.ContainsKey(conn))
        {
            if (conn != null) ServerSendError(conn, "CharacterCreate: not in lobby", true);
            return;
        }

        string account = lobby[conn];

        // ... (your existing validation code stays the same)

        if (!IsAllowedCharacterName(message.name)) { ServerSendError(conn, "character name not allowed", false); return; }
        if (Database.singleton.CharacterExists(message.name)) { ServerSendError(conn, "name already exists", false); return; }
        if (Database.singleton.CharactersForAccount(account).Count >= characterLimit) { ServerSendError(conn, "character limit reached", false); return; }
        if (message.classIndex < 0 || message.classIndex >= playerClasses.Count) { ServerSendError(conn, "character invalid class", false); return; }

        GameObject classPrefab = playerClasses[message.classIndex].gameObject;
        Player player = CreateCharacter(classPrefab, message.name, account, message.dna, message.classIndex);

        // Let UCE addons do their thing via hooks
        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnServerCharacterCreate_", message, player);

        Database.singleton.CharacterSave(player, false);
        Destroy(player.gameObject);

        // Send updated character list back
        conn.Send(MakeCharactersAvailableMessage(account));
    }

    // Keep your existing OnServerCharacterSelect and OnServerCharacterDelete...

    void OnServerCharacterSelect(NetworkConnectionToClient conn, CharacterSelectMsg message)
    {
        if (conn == null || !lobby.ContainsKey(conn))
        {
            if (conn != null) ServerSendError(conn, "CharacterSelect: not in lobby", true);
            return;
        }

        string account = lobby[conn];
        List<string> characters = Database.singleton.CharactersForAccount(account);

        if (message.index < 0 || message.index >= characters.Count)
        {
            ServerSendError(conn, "invalid character index", false);
            return;
        }

        GameObject go = Database.singleton.CharacterLoad(characters[message.index], playerClasses, false);

        NetworkServer.AddPlayerForConnection(conn, go);

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnServerCharacterSelect_", account, go, conn, message);

        lobby.Remove(conn);

        // === LOAD THE WORLD SCENE AFTER SUCCESSFUL CHARACTER SELECTION ===
        Debug.Log($"[Server] Character selected successfully - Loading World of Faoria");
        ServerChangeScene("World of Faoria");
    }

    void OnServerCharacterDelete(NetworkConnectionToClient conn, CharacterDeleteMsg message)
    {
        if (conn == null || !lobby.ContainsKey(conn))
        {
            if (conn != null) ServerSendError(conn, "CharacterDelete: not in lobby", true);
            return;
        }

        string account = lobby[conn];
        List<string> characters = Database.singleton.CharactersForAccount(account);

        if (message.index < 0 || message.index >= characters.Count)
        {
            ServerSendError(conn, "invalid character index", false);
            return;
        }

        Database.singleton.CharacterDelete(characters[message.index]);

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnServerCharacterDelete_", message);

        conn.Send(MakeCharactersAvailableMessage(account));
    }

    // ===================================================================
    // HELPER METHODS (unchanged)
    // ===================================================================

    public bool IsAllowedCharacterName(string characterName)
    {
        return characterName.Length <= characterNameMaxLength &&
               Regex.IsMatch(characterName, @"^[a-zA-Z0-9_]+$");
    }

    public static Transform GetNearestStartPosition(Vector3 from) =>
        Utils.GetNearestTransform(startPositions, from);

    public Transform GetStartPositionFor(int classIndex)
    {
        if (selectionLocations == null || selectionLocations.Length == 0)
        {
            Debug.LogWarning("No selection locations defined!");
            return null;
        }

        int index = classIndex % selectionLocations.Length;
        return selectionLocations[index];
    }

    public static int GetClassIndex(string className)
    {
        if (string.IsNullOrEmpty(className)) return 0;

        NetworkManagerMMO mmo = singleton as NetworkManagerMMO;
        if (mmo == null || mmo.playerClasses == null)
        {
            Debug.LogWarning("NetworkManagerMMO.singleton or playerClasses is not initialized.");
            return 0;
        }

        for (int i = 0; i < mmo.playerClasses.Count; ++i)
        {
            if (mmo.playerClasses[i].name == className)
                return i;
        }

        Debug.LogWarning($"Class index not found for: {className}");
        return 0;
    }

   /* public CharactersAvailableMsg MakeCharactersAvailableMessage(string account)
    {
        List<Player> characters = new List<Player>();

        foreach (string characterName in Database.singleton.CharactersForAccount(account))
        {
            GameObject playerObj = Database.singleton.CharacterLoad(characterName, playerClasses, true);
            characters.Add(playerObj.GetComponent<Player>());
        }

        CharactersAvailableMsg message = new CharactersAvailableMsg();
        message.Load(characters);

        characters.ForEach(p => Destroy(p.gameObject));
        return message;
    }*/
/*   public CharactersAvailableMsg MakeCharactersAvailableMessage(string account)
   {
       List<string> characterNames = Database.singleton.CharactersForAccount(account);

       // If no characters exist for this account (common on first Host), create one automatically for testing
       if (characterNames.Count == 0)
       {
           Debug.Log($"[Server] No characters found for account '{account}'. Creating a default test character.");

           // Create a default character using the first player class
           if (playerClasses.Count > 0)
           {
               string defaultName = "TestCharacter_" + System.DateTime.Now.Second;
               GameObject classPrefab = playerClasses[0].gameObject;

               Player player = CreateCharacter(classPrefab, defaultName, account, "", 0); // empty dna for now

               Database.singleton.CharacterSave(player, false);
               Destroy(player.gameObject);

               Debug.Log($"[Server] Created default character: {defaultName}");
           }
       }
*/

    public CharactersAvailableMsg MakeCharactersAvailableMessage(string account)
    {
        List<string> characterNames = Database.singleton.CharactersForAccount(account);

        if (characterNames.Count == 0)
        {
            Debug.Log($"[Server] No characters for '{account}'. Creating UMA test character.");
            // ... your existing test character creation code ...
        }

        List<Player> characters = new List<Player>();

        foreach (string characterName in characterNames)
        {
            GameObject playerObj = Database.singleton.CharacterLoad(characterName, playerClasses, true); // preview = true
            if (playerObj != null)
            {
                characters.Add(playerObj.GetComponent<Player>());
            }
        }

        CharactersAvailableMsg message = new CharactersAvailableMsg();
        message.Load(characters);

        // === SAFER CLEANUP ===
        foreach (Player p in characters)
        {
            if (p != null && p.gameObject != null)
            {
                // Optional: add a small delay or just destroy safely
                Destroy(p.gameObject);
            }
        }

        return message;
    }

    void SavePlayers()
    {
        Database.singleton.CharacterSaveMany(Player.onlinePlayers.Values);
        if (Player.onlinePlayers.Count > 0)
            Debug.Log($"Saved {Player.onlinePlayers.Count} player(s)");
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.LogWarning("Use CharacterSelectMsg instead of AddPlayerMessage");
    }

   /* public void ClearPreviews()
    {
        selection = -1;
        foreach (Transform location in selectionLocations)
            if (location.childCount > 0)
                Destroy(location.GetChild(0).gameObject);
    }*/
   
   public void ClearPreviews()
   {
       // Do NOT reset selection here - let UI control it
       // selection = -1;   <--- COMMENT THIS OUT or remove

       foreach (Transform location in selectionLocations)
           if (location.childCount > 0)
               Destroy(location.GetChild(0).gameObject);
   }
    // ===================================================================
    // ERROR HANDLING
    // ===================================================================

    public void ServerSendError(NetworkConnectionToClient conn, string error, bool disconnect)
    {
        if (conn != null)
        {
            conn.Send(new ErrorMsg 
            { 
                text = error, 
                causesDisconnect = disconnect 
            });
        }
    }
    
    // ===================================================================
    // FORCE CHARACTER LIST FOR HOST MODE (Critical Fix)
    // ===================================================================
    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);

        Debug.Log($"[Server] OnServerReady - Conn {conn.connectionId} | LocalHost: {conn == NetworkServer.localConnection}");

        if (conn == NetworkServer.localConnection)
        {
            Debug.Log("[Server] Local Host detected - forcing character list");

            if (!lobby.ContainsKey(conn))
                lobby[conn] = "local";

            string account = lobby[conn];

            CharactersAvailableMsg msg = MakeCharactersAvailableMessage(account);
            conn.Send(msg);

            Debug.Log($"[Server] SENT CharactersAvailableMsg with {msg.characters.Length} characters");
        }
    }
    
    // Called when character creation screen is shown
    void OnClientCharacterCreation_(bool isVisible)
    {
        if (!isVisible) return;

        Debug.Log("[Client] Character Creation screen opened - Moving camera to creation view");

        if (creationCameraLocation != null && Camera.main != null)
        {
            Camera.main.transform.position = creationCameraLocation.position;
            Camera.main.transform.rotation = creationCameraLocation.rotation;
            Debug.Log("[Client] Camera moved to creationCameraLocation");
        }
        else if (Camera.main != null)
        {
            // Fallback: position camera in front of the creation preview
            Debug.LogWarning("[Client] creationCameraLocation not assigned - using fallback");
            // You can improve this fallback later
        }
    }
    
}