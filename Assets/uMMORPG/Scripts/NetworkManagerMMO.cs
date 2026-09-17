using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Mirror;
using UnityEngine.AI;


#if UNITY_EDITOR
using UnityEditor;
#endif

public enum NetworkState { Offline, Handshake, Lobby, World }

[RequireComponent(typeof(Database))]
public partial class NetworkManagerMMO : NetworkManager
{
    public NetworkState state = NetworkState.Offline;

    public Dictionary<NetworkConnectionToClient, string> lobby = new Dictionary<NetworkConnectionToClient, string>();
// Players waiting to be spawned after the world scene loads
    readonly Dictionary<NetworkConnectionToClient, GameObject> pendingPlayers =
        new Dictionary<NetworkConnectionToClient, GameObject>();
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
    public bool autoEnterLastPlayed = false;
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
                Debug.Log($"[Server] playerClasses ({playerClasses.Count}): " +
                          string.Join(", ", playerClasses.ConvertAll(p => p != null ? p.name : "null")));
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

        Debug.Log($"[Login] Connecting to {NetworkManager.singleton.networkAddress}:7777");

        NetworkClient.ReplaceHandler<ErrorMsg>(OnClientError, false);
        NetworkClient.ReplaceHandler<CharactersAvailableMsg>(OnCharactersAvailable, false);

        // Critical for Host mode
        if (NetworkClient.isConnected && !NetworkClient.ready)
        {
            Debug.Log("[Client] Marking local client as Ready");
            NetworkClient.Ready();
        }

        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnStartClient_");
    }

    void OnClientError(ErrorMsg msg)
    {
        Debug.LogWarning($"Client Error: {msg.text}");
        if (uiPopup != null) uiPopup.Show(msg.text);

        if (msg.causesDisconnect)
            NetworkClient.Disconnect();
    }

    void OnCharactersAvailable(CharactersAvailableMsg msg)
    {
        Debug.Log($"[Client] OnCharactersAvailable received {msg.characters.Length} characters");
        charactersAvailableMsg = msg;
        state = NetworkState.Lobby;

        OnClientCharactersAvailable_(msg);
        Utils.InvokeMany(typeof(NetworkManagerMMO), this, "OnClientCharactersAvailable_", msg);
    }
    
    // Camera positioning for character selection screen
    void OnClientCharactersAvailable_(CharactersAvailableMsg msg)
    
    {
        if (autoEnterLastPlayed)
            return;
        
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

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log($"[Server] OnServerConnect – connectionId: {conn.connectionId}");
        base.OnServerConnect(conn);
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

        if (pendingPlayers.ContainsKey(localConn))
            return;

        if (!lobby.ContainsKey(localConn))
        {
            Debug.Log("[Server] ForceSend skipped – not in lobby yet (waiting for login)");
            return;
        }

        string account = lobby[localConn];

        CharactersAvailableMsg msg = MakeCharactersAvailableMessage(account);
        localConn.Send(msg);
        TryAutoEnterLastPlayed(localConn, account);
        Debug.Log($"[Server] FORCED SENT CharactersAvailableMsg with {msg.characters.Length} characters");
    }

    // ===================================================================
    // CHARACTER HANDLERS (Keep UCE hooks)
    // ===================================================================
    
    public void TryAutoEnterLastPlayed(NetworkConnectionToClient conn, string account)
    {
        if (!autoEnterLastPlayed || conn == null || string.IsNullOrEmpty(account))
            return;

        if (!lobby.ContainsKey(conn))
            return;

        int index = Database.singleton.LastPlayedCharacterIndex(account);
        if (index < 0)
        {
            Debug.Log($"[AutoEnter] no last-played character for '{account}'");
            return;
        }

        Debug.Log($"[AutoEnter] account='{account}' index={index}");
        OnServerCharacterSelect(conn, new CharacterSelectMsg { index = index });
    }
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
            if (conn != null)
                ServerSendError(conn, "CharacterSelect: not in lobby", true);
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
        if (go == null)
        {
            Debug.LogError("[Select] CharacterLoad returned null!");
            ServerSendError(conn, "Character load failed", true);
            return;
        }

        var p = go.GetComponent<Player>();
        Debug.Log($"[Select] Loaded go='{go.name}' className='{p?.className}'");

        var agent = go.GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;

        DontDestroyOnLoad(go);

        pendingPlayers[conn] = go;
        Debug.Log($"[Select] Stored '{go.name}' in pendingPlayers. Count = {pendingPlayers.Count}");

        lobby.Remove(conn);
        Debug.Log($"[Server] Character selected for {account} – loading World of Faoria");
        ServerChangeScene("World of Faoria");
    }

System.Collections.IEnumerator DelayedFinalSpawn(GameObject player)
{
    yield return new WaitForSeconds(0.4f); // let agent settle on temp plane

    if (player == null) yield break;

    if (NetworkManager.startPositions != null && NetworkManager.startPositions.Count > 0)
    {
        Transform start = NetworkManager.startPositions[0];
        var agent = player.GetComponent<NavMeshAgent>();

        if (agent != null) agent.enabled = false;

        player.transform.position = start.position;
        player.transform.rotation = start.rotation;

        if (agent != null)
        {
            agent.Warp(start.position);
            agent.enabled = true;
        }

        Debug.Log($"[DirtySpawn] Final warp to {start.position}");
    }
    else
    {
        Debug.LogWarning("[DirtySpawn] No startPositions found for final warp");
    }
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
    // HELPER METHODS
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

    public CharactersAvailableMsg MakeCharactersAvailableMessage(string account)
    {
        List<string> characterNames = Database.singleton.CharactersForAccount(account);

        if (characterNames.Count == 0)
        {
            Debug.Log($"[Server] No characters for '{account}'. Creating UMA test character.");
            // your existing test character creation code here if needed
        }

        List<Player> characters = new List<Player>();

        foreach (string characterName in characterNames)
        {
            GameObject playerObj = Database.singleton.CharacterLoad(characterName, playerClasses, true);
            if (playerObj != null)
                characters.Add(playerObj.GetComponent<Player>());
        }

        CharactersAvailableMsg message = new CharactersAvailableMsg();
        message.Load(characters);

        foreach (Player p in characters)
        {
            if (p != null && p.gameObject != null)
                Destroy(p.gameObject);
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

    public void ClearPreviews()
    {
        foreach (Transform location in selectionLocations)
            if (location != null && location.childCount > 0)
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
    // FORCE CHARACTER LIST FOR HOST MODE
    // ===================================================================

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);

        Debug.Log($"[Server] OnServerReady - Conn {conn.connectionId} | LocalHost: {conn == NetworkServer.localConnection}");

        if (conn == NetworkServer.localConnection)
        {
            if (pendingPlayers.ContainsKey(conn))
                return;

            if (!lobby.ContainsKey(conn))
            {
                Debug.Log("[Server] OnServerReady skipped – not in lobby yet");
                return;
            }

            string account = lobby[conn];
            CharactersAvailableMsg msg = MakeCharactersAvailableMessage(account);
            conn.Send(msg);
            TryAutoEnterLastPlayed(conn, account);
            Debug.Log($"[Server] SENT CharactersAvailableMsg with {msg.characters.Length} characters");
        }
    }

public override void OnServerSceneChanged(string sceneName)
{
    base.OnServerSceneChanged(sceneName);

    if (sceneName != "World of Faoria")
        return;

    Debug.Log($"[Server] World of Faoria loaded – pendingPlayers.Count = {pendingPlayers.Count}");

    // Resolve spawn only after the world scene is loaded
    Transform spawn = null;
    NetworkStartPosition[] starts = FindObjectsOfType<NetworkStartPosition>();
    if (starts != null && starts.Length > 0)
        spawn = starts[0].transform;

    // Optional named fallback if you use a specific object:
    // if (spawn == null)
    // {
    //     GameObject named = GameObject.Find("first time spawn in");
    //     if (named != null) spawn = named.transform;
    // }

    foreach (var kvp in pendingPlayers)
    {
        NetworkConnectionToClient conn = kvp.Key;
        GameObject playerGO = kvp.Value;

        if (conn == null || playerGO == null)
        {
            Debug.LogWarning("[Server] Skipping null pending player entry");
            continue;
        }

        if (!conn.isAuthenticated)
        {
            conn.isAuthenticated = true;
            Debug.Log($"[Server] Re-authenticated connection {conn.connectionId}");
        }

        // Place on world spawn
        if (spawn != null)
        {
            playerGO.transform.position = spawn.position;
            playerGO.transform.rotation = spawn.rotation;
            Debug.Log($"[Server] Placed player at spawn {spawn.position}");
        }
        else
        {
            Debug.LogWarning("[Server] No NetworkStartPosition in World of Faoria – player left at current position");
        }
// TEMP – use your real world spawn numbers
        // Vector3 forcedSpawn = new Vector3(3735.785f, 0.9270434f, 3087.256f);
// or better: the actual "first time spawn in" transform values

        Vector3 pos = playerGO.transform.position; // from CharacterLoad / DB

        var agent = playerGO.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 50f, NavMesh.AllAreas))
            {
                playerGO.transform.position = hit.position;
                agent.Warp(hit.position);
                agent.enabled = true;
                Debug.Log($"[Agent] Sampled DB pos {pos} -> {hit.position}");
            }
            else
            {
                Debug.LogError($"[Agent] No NavMesh near saved pos {pos}");
                // last resort: first NetworkStartPosition, not a magic vector
            }
        }

        NetworkServer.AddPlayerForConnection(conn, playerGO);
        
        NetworkServer.AddPlayerForConnection(conn, playerGO);
        Debug.Log($"[Server] AddPlayerForConnection conn={conn.connectionId} go={playerGO.name}");

      /*  // Put agent on NavMesh
        var agent = playerGO.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;

            Vector3 pos = playerGO.transform.position;
            if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 30f, NavMesh.AllAreas))
            {
                playerGO.transform.position = hit.position;
                agent.Warp(hit.position);
                agent.enabled = true;
                Debug.Log($"[Agent] On NavMesh at {hit.position} isOnNavMesh={agent.isOnNavMesh}");
            }
            else
            {
                Debug.LogWarning($"[Agent] No NavMesh near {pos} – agent left disabled");
            }
        }*/
    }

    pendingPlayers.Clear();
    }
    System.Collections.IEnumerator ReenableAgentAfterSpawn(GameObject player)
    {
        // Wait until the scene and network are fully settled
        yield return new WaitForSeconds(0.5f);

        if (player == null) yield break;

        var agent = player.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            // In case we destroyed it earlier, add a fresh one
            agent = player.AddComponent<NavMeshAgent>();
        }

        // Make sure the agent is on the NavMesh
        agent.enabled = false;
        agent.Warp(player.transform.position);
        agent.enabled = true;

        Debug.Log($"[Agent] Re-enabled NavMeshAgent at {player.transform.position}");
    }
    
    public override void OnClientSceneChanged()
    {
        base.OnClientSceneChanged();

        Debug.Log($"[Client] OnClientSceneChanged – ready: {NetworkClient.ready}, " +
                  $"active: {NetworkClient.active}, " +
                  $"localPlayer: {(NetworkClient.localPlayer != null ? NetworkClient.localPlayer.name : "null")}");

        // Force Ready in Host mode after scene change
        if (NetworkClient.active)
        {
            if (!NetworkClient.ready)
            {
                NetworkClient.Ready();
                Debug.Log("[Client] Forced NetworkClient.Ready()");
            }
            // Put this inside OnClientSceneChanged after the Ready() call
            StartCoroutine(DelayedReadyCheck());
            
            // Extra safety for Host mode
            if (NetworkClient.localPlayer != null && !NetworkClient.localPlayer.isLocalPlayer)
            {
                Debug.LogWarning("[Client] localPlayer exists but isLocalPlayer is false – this is the problem state");
            }
        }
    }
    
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        // Clean up lobby so the same account can log in again
        if (lobby.ContainsKey(conn))
        {
            Debug.Log($"[Server] Removing account '{lobby[conn]}' from lobby on disconnect");
            lobby.Remove(conn);
        }

        // Also clean up any pending player that never got spawned
        if (pendingPlayers.ContainsKey(conn))
        {
            GameObject go = pendingPlayers[conn];
            if (go != null)
                Destroy(go);
            pendingPlayers.Remove(conn);
        }

        base.OnServerDisconnect(conn);
    }

    System.Collections.IEnumerator DelayedReadyCheck()
    {
        yield return new WaitForSeconds(0.3f);

        if (NetworkClient.active && !NetworkClient.ready)
        {
            NetworkClient.Ready();
            Debug.Log("[Client] Delayed force Ready");
        }

        if (NetworkClient.localPlayer != null)
        {
            Debug.Log($"[Client] After delay – isLocalPlayer: {NetworkClient.localPlayer.isLocalPlayer}");
        }
    }
    
    void OnClientCharacterCreation_(bool isVisible)
    {
        if (!isVisible) return;

        Debug.Log("[Client] Character Creation screen opened - Moving camera to creation view");

        if (creationCameraLocation != null && Camera.main != null)
        {
            Camera.main.transform.position = creationCameraLocation.position;
            Camera.main.transform.rotation = creationCameraLocation.rotation;
        }
    }
}