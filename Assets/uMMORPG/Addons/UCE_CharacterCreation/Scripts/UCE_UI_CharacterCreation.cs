// =======================================================================================
// Maintained by bobatea#9400 on Discord
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............:  
  
// * Leave a star on my Github Repo.....: https://github.com/breehuynh/Bree-mmorpg-tools
// * Instructions.......................: https://indie-mmo.net/knowledge-base/
// =======================================================================================
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UMA;
using UMA.CharacterSystem;

public partial class UCE_UI_CharacterCreation : MonoBehaviour
{
    [Header("-=-=-=- UCE Character Creation -=-=-=-")]
    public GameObject panel;
    public GameObject centerPanel, centerPanel2;
    
    public UCE_UI_CharacterTraits traitsPanel;


    public List<UCE_CharacterCreationClass> classList = new List<UCE_CharacterCreationClass>();
    public bool lookAtCamera;
    public GameObject SpawnPoint;
    public NetworkManagerMMO manager;

    public Transform creationCameraLocation;

    [Header("-=-=-=- Creation Panel -=-=-=-")]
    public InputField nameInput;

    public Button createButton;
    public Button cancelButton;

    protected List<Player> players;
    protected int classIndex = 0;
    protected bool bInit = false;
    
    [Header("-=-=-=- UMA -=-=-=-")]

    public List<UMATextRecipe> maleHairStyles;
    public List<UMATextRecipe> maleClothing;
    public List<UMATextRecipe> femaleHairStyles;
    public List<UMATextRecipe> femaleClothing;

    [HideInInspector]
    public DynamicCharacterAvatar dca;

    [HideInInspector]
    public int maleIndex = 0;

    [HideInInspector]
    public int femaleIndex = 0;

    [HideInInspector]
    public int maleClothingIndex = 0;

    [HideInInspector]
    public int femaleClothingIndex = 0;
    

    // -----------------------------------------------------------------------------------
    // currentPlayer
    // -----------------------------------------------------------------------------------
    public Player currentPlayer
    {
        get
        {
            return players[classIndex];
        }
    }

    // -----------------------------------------------------------------------------------
    // Show
    // -----------------------------------------------------------------------------------
    public void Show()
    {
        Debug.Log("[Creation] Show() called");
        Debug.Log($"[Creation] panel null? {panel == null} | activeSelf before: {(panel != null ? panel.activeSelf.ToString() : "null")}");
        
        centerPanel.SetActive(true);
        centerPanel2.SetActive(true);

        Camera.main.transform.position = creationCameraLocation.position;
        Camera.main.transform.rotation = creationCameraLocation.rotation;

        players = new List<Player>();
        players = manager.playerClasses;

        if (players == null || players.Count <= 0)
        {
            return;
        }

        for (int c = 0; c < players.Count; c++)
        {
            int temp = c;

            classList[temp].button.onClick.SetListener(() => SetCharacterClass(temp));
            classList[temp].label.text = players[temp].name;
            classList[temp].prefabID = temp;

#if _iMMOUNLOCKABLECLASSES
            if (manager.UCE_HasUnlockedClass(players[temp]))
            {
                classList[temp].button.gameObject.SetActive(true);
            }
            else
            {
                classList[temp].button.gameObject.SetActive(false);
            }
#else
            classList[temp].button.gameObject.SetActive(true);
#endif
        }

#if _iMMOUNLOCKABLECLASSES
        for (int c = 0; c < players.Count; c++)
        {
            int selectedClass = c;
            if (manager.UCE_HasUnlockedClass(players[selectedClass]))
            {
                SetCharacterClass(selectedClass);
                break;
            }
        }
#else
        SetCharacterClass(0);
#endif

        createButton.onClick.SetListener(() =>
        {
            CreateCharacter();
        });

        cancelButton.onClick.SetListener(() =>
        {
            Hide();
        });

        panel.SetActive(true);
        bInit = true;
    }

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        if (!bInit) return;
        if (nameInput.text.Length == 0)
            createButton.enabled = false;
        else
            createButton.enabled = true;
    }

    // -----------------------------------------------------------------------------------
    // CreateCharacter
    // -----------------------------------------------------------------------------------
    public virtual void CreateCharacter()
    {
        if (SpawnPoint.transform.childCount > 0)
            Destroy(SpawnPoint.transform.GetChild(0).gameObject);
        
        int[] iTraits = new int[traitsPanel.currentTraits.Count];

        for (int i = 0; i < traitsPanel.currentTraits.Count; i++)
        {
            iTraits[i] = traitsPanel.currentTraits[i].name.GetStableHashCode();
        }

        CharacterCreateMsg message = new CharacterCreateMsg
        {
            name = nameInput.text,
            classIndex = classIndex,
            traits = iTraits,
            dna = CompressedString()
        };

        NetworkClient.Send(message);

        Hide();
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public virtual void SetCharacterClass(int _classIndex)
    {
        classIndex = _classIndex;

        if (SpawnPoint.transform.childCount > 0)
            Destroy(SpawnPoint.transform.GetChild(0).gameObject);

        GameObject go = Instantiate(players[classIndex].gameObject, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        go.transform.parent = SpawnPoint.transform;

        if (lookAtCamera)
            go.transform.LookAt(creationCameraLocation);

        Player player = go.GetComponent<Player>();
        player.nameOverlay.enabled = false;

        for (int i = 0; i < players[classIndex].equipmentInfo.Length; ++i)
        {
            EquipmentInfo info = players[classIndex].equipmentInfo[i];
            player.equipment.Add(info.defaultItem.item != null ? new ItemSlot(new Item(info.defaultItem.item), info.defaultItem.amount) : new ItemSlot());
            player.RefreshLocation(i);
        }
        
        traitsPanel.Show();
        SetupAll();
    }

    // -----------------------------------------------------------------------------------
    // Hide
    // -----------------------------------------------------------------------------------
    public void Hide()
    {
        if (SpawnPoint.transform.childCount > 0)
            Destroy(SpawnPoint.transform.GetChild(0).gameObject);
        
        traitsPanel.Hide();
        panel.SetActive(false);
        bInit = false;

        Camera.main.transform.position = manager.selectionCameraLocation.position;
        Camera.main.transform.rotation = manager.selectionCameraLocation.rotation;
        
        dca = null;
    }

    public bool IsVisible()
    {
        return panel.activeSelf;
    }

    public void SetupAll()
    {
        dca = FindObjectOfType<DynamicCharacterAvatar>();
        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.1f);
        dca.ChangeRace("HumanMale");
        yield return new WaitForSeconds(0.1f);
        if (maleClothing.Count > 0)
            SelectClothing(0);
        yield return new WaitForSeconds(0.1f);
        if (maleHairStyles.Count > 0)
            SelectHair(0);
    }

    public void SelectClothing(int index)
    {
        if (dca == null || dca.activeRace == null || dca.activeRace.data == null)
        {
            Debug.LogWarning("[UMA] Avatar or Race not ready – skipping SelectClothing");
            return;
        }

        bool male = dca.activeRace.name == "HumanMale";
        dca.ClearSlot("Underwear");

        if (male)
            dca.SetSlot(maleClothing[index]);
        else
            dca.SetSlot(femaleClothing[index]);

        dca.BuildCharacter();
    }

    public void SelectHair(int index)
    {
        if (dca == null || dca.activeRace == null || dca.activeRace.data == null)
        {
            Debug.LogWarning("[UMA] Avatar or Race not ready – skipping SelectHair");
            return;
        }
        
        bool male = dca.activeRace.name == "HumanMale" ? true : false;
        dca.ClearSlot("Hair");

        if (male)
            dca.SetSlot(maleHairStyles[index]);
        if (!male)
            dca.SetSlot(femaleHairStyles[index]);

        dca.BuildCharacter();
    }

    public void SwitchGender(string genderName)
    {
        if (dca == null)
        {
            Debug.LogWarning("[UMA] Avatar not ready – skipping SwitchGender");
            return;
        }
        
        dca.ChangeRace(genderName);

        if (genderName == "HumanMale")
        {
            if (maleClothing.Count > 0)
                SelectClothing(maleClothingIndex);

            if (maleHairStyles.Count > 0)
                SelectHair(maleIndex);
        }
        if (genderName == "HumanFemale")
        {
            if (femaleClothing.Count > 0)
                SelectClothing(femaleClothingIndex);

            if (maleHairStyles.Count > 0)
                SelectHair(femaleIndex);
        }
    }

    public void ChangeHairColor(Color col)
    {
        dca.SetColor("Hair", col);
        dca.UpdateColors(true);
    }

    public void ChangeBaseColor(Color col)
    {
        dca.SetColor("Undies", col);
        dca.UpdateColors(true);
    }

    public void ChangeEyesColor(Color col)
    {
        dca.SetColor("Eyes", col);
        dca.UpdateColors(true);
    }

    public void ChangeSkinColor(Color col)
    {
        dca.SetColor("Skin", col);
        dca.UpdateColors(true);
    }
    

    private String CompressedString()
    {
        return CompressUMA.Compressor.CompressDna(dca.GetCurrentRecipe());
    }




    // -----------------------------------------------------------------------------------
}