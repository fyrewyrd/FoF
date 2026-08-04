using System;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public partial class UICharacterSelection : MonoBehaviour
{
    public UCE_UI_CharacterCreation uiCharacterCreation;
    public UIConfirmation uiConfirmation;
    public NetworkManagerMMO manager;
    public GameObject panel;
    public Button startButton;
    public Button deleteButton;
    public Button createButton;
    public Button quitButton;

    private GameObject[] currentPreviews;
    private int lastSelection = -1;

    void Awake()
    {
        currentPreviews = new GameObject[10];
        SetupButtons();
    }

    private void SetupButtons()
    {
        Debug.Log("[UI] Setting up buttons once in Awake");
        // Start, Delete, Create, Quit listeners...
        // Start Button
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() =>
        {
            NetworkClient.Ready();
            if (NetworkClient.connection != null)
                NetworkClient.connection.Send(new CharacterSelectMsg { index = manager.selection });
            manager.ClearPreviews();
            panel.SetActive(false);
        });

        // Delete Button
        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() =>
        {
            CharactersAvailableMsg.CharacterPreview[] chars = manager.charactersAvailableMsg.characters;
            if (manager.selection >= 0 && manager.selection < chars.Length)
            {
                uiConfirmation.Show(
                    "Do you really want to delete <b>" + chars[manager.selection].name + "</b>?",
                    () => { NetworkClient.Send(new CharacterDeleteMsg { index = manager.selection }); }
                );
            }
        });

        // Create Button
        createButton.onClick.RemoveAllListeners();
        createButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            uiCharacterCreation.Show();
        });

        // Quit Button
        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(NetworkManagerMMO.Quit);
    }

    void Update()
    {
        if (manager.state != NetworkState.Lobby)
        {
            if (panel.activeSelf) Debug.Log("[UI] Hiding panel - not in Lobby state");
            panel.SetActive(false);
            return;
        }

        if (manager.charactersAvailableMsg.characters == null)
        {
            Debug.Log("[UI] Waiting for character data...");
            panel.SetActive(false);
            return;
        }

        CharactersAvailableMsg.CharacterPreview[] characters = manager.charactersAvailableMsg.characters;
        panel.SetActive(true);
        
        if (!panel.activeSelf) Debug.Log("[UI] Showing panel");
        panel.SetActive(true);

        bool needsRefresh = manager.selection != lastSelection || 
                           (currentPreviews[0] == null && characters.Length > 0);

        if (needsRefresh)
        {
            Debug.Log($"[Preview-old] Refreshing previews for {characters.Length} characters");
            Debug.Log($"[Preview] === REFRESHING PREVIEWS === selection={manager.selection}, last={lastSelection}, count={characters.Length}");

            manager.ClearPreviews();

            for (int i = 0; i < characters.Length && i < manager.selectionLocations.Length; i++)
            {
                if (manager.selectionLocations[i] == null) continue;

                Debug.Log($"[Preview] Creating preview for {characters[i].name} at slot {i}");

                GameObject prefab = manager.playerClasses.Count > 0 ? manager.playerClasses[0].gameObject : null;

                if (prefab != null)
                {
                    ((NetworkManagerMMO)NetworkManager.singleton).LoadPreview(
                        prefab,
                        manager.selectionLocations[i],
                        i,
                        characters[i]
                    );

                    Transform loc = manager.selectionLocations[i];
                    if (loc.childCount > 0)
                    {
                        GameObject previewObj = loc.GetChild(loc.childCount - 1).gameObject;
                        currentPreviews[i] = previewObj;
                        ApplyNavMeshFix(previewObj);
                        MakePreviewClickable(previewObj, i);
                        
                        Debug.Log($"[Preview] Preview {i} created and made clickable");
                    }
                }
            }

            lastSelection = manager.selection;
            Debug.Log($"[Preview] Refresh complete. Current selection = {manager.selection}");
        }

// At the end of Update(), replace the button section with:
        startButton.gameObject.SetActive(manager.selection != -1 && characters.Length > 0);
        deleteButton.gameObject.SetActive(manager.selection != -1 && characters.Length > 0);
        createButton.interactable = characters.Length < manager.characterLimit;
    }

    /*private void MakePreviewClickable(GameObject previewObj, int index)
    {
        if (previewObj == null) return;
        Debug.Log($"[Click] Making preview {index} clickable");

        Collider col = previewObj.GetComponent<Collider>();
        if (col == null) col = previewObj.AddComponent<BoxCollider>();

        ClickHandler handler = previewObj.GetComponent<ClickHandler>();
        if (handler == null) handler = previewObj.AddComponent<ClickHandler>();
        Debug.Log($"[Click] Click handler assigned for index {index}");
        
        handler.onClick = () => SelectCharacter(index);
    }*/
    
    private void MakePreviewClickable(GameObject previewObj, int index)
    {
        if (previewObj == null) return;

        Debug.Log($"[Click] Making preview {index} clickable");

        Collider col = previewObj.GetComponent<Collider>();
        if (col == null) col = previewObj.AddComponent<BoxCollider>();

        ClickHandler handler = previewObj.GetComponent<ClickHandler>();
        if (handler == null) handler = previewObj.AddComponent<ClickHandler>();

        handler.onClick = () => SelectCharacter(index);

        // TEMP TEST: Make it very obvious
        Debug.Log($"[Click] Click handler assigned for index {index} on {previewObj.name}");
    
        // Force a large collider for testing
        BoxCollider box = col as BoxCollider;
        if (box != null) box.size = new Vector3(2, 3, 2);
    }

    private void ApplyNavMeshFix(GameObject previewObj)
    {
        if (previewObj == null) return;
        var agent = previewObj.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            var pos = previewObj.transform.position;
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(pos, out hit, 10f, UnityEngine.AI.NavMesh.AllAreas))
            {
                previewObj.transform.position = hit.position;
                agent.Warp(hit.position);
                agent.enabled = true;
            }
        }
    }

    /*public void SelectCharacter(int index)
    {
        Debug.Log($"[Click] === SelectCharacter({index}) CALLED ===");
        if (index >= 0 && index < manager.charactersAvailableMsg.characters.Length)
        {
            manager.selection = index;
            Debug.Log($"[UI-old] Character selected: {manager.charactersAvailableMsg.characters[index].name}");
            Debug.Log($"[UI] SUCCESS - Character selected: {manager.charactersAvailableMsg.characters[index].name} | selection now = {manager.selection}");
        }
        else
        {
            Debug.LogWarning($"[UI] Invalid selection index: {index}");
        }
    }*/
    
    public void SelectCharacter(int index)
    {
        Debug.Log($"[Click] === SelectCharacter({index}) CALLED ===");
        if (index >= 0 && index < manager.charactersAvailableMsg.characters.Length)
        {
            manager.selection = index;
            lastSelection = index;   // Prevent immediate refresh
            Debug.Log($"[UI] SUCCESS - Selected {manager.charactersAvailableMsg.characters[index].name}");

            startButton.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);
        }
    }

    public void MarkPreviewsDirty()
    {
        lastSelection = -1;
    }
}