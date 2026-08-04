using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DX4D
{
    /// <summary>
    /// The Quickstart is responsible for managing scene setups. It can also be extended to generate a world population list.
    /// </summary>
    public static partial class Quickstart
    {
        //EFFECT MOUNTS
        public const string rightHandEffectMount = "EquipmentLocation Sword";
        public const string leftHandEffectMount = "EquipmentLocation Shield";

        //EQUIPMENT
        public const string headEquipLocation = "EquipmentLocation Chest";//"EquipmentLocation Head"; //TODO
        public const string shoulderEquipLocation = "EquipmentLocation Shoulders";
        public const string handEquipLocation = "EquipmentLocation Hands";

        public const string chestEquipLocation = "EquipmentLocation Chest";
        public const string legEquipLocation = "EquipmentLocation Legs";
        public const string feetEquipLocation = "EquipmentLocation Feet";

        public const string shieldEquipLocation = "EquipmentLocation Shield";
        public const string accessoryEquipLocation = "EquipmentLocation Chest";

        public const string mainWeaponEquipLocation = "EquipmentLocation Sword";
        public const string offhandWeaponEquipLocation = "EquipmentLocation Shield";

        public const string siegeWeaponEquipLocation = "EquipmentLocation Chest";
        public const string unarmedWeaponEquipLocation = "EquipmentLocation Hands";

        /// <summary>Adds necessary components to the spawnable prefabs in your project.</summary>
        public static bool modifyPrefabs = true;
        /// <summary>Adds necessary components to the objects in your scene.</summary>
        public static bool modifyScene = true;

        //SETUP SCENE
        public static void SetupScene()
        {
            GameObject[] sceneObjects = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject sceneItem in sceneObjects)
            {
                //UPDATE SCENE ITEM
                UpdateComponents(sceneItem);
            }
        }

        //UPDATE COMPONENTS
        public static void UpdateComponents(GameObject go)
        {
            UpdatePrefabComponents(go);
            UpdateSceneComponents(go);
        }

        //ADD COMPONENT TO SCENE OBJECT
        static void AddComponentToSceneObject<T>(GameObject go) where T : Component
        {
            if (!go.GetComponent<T>())
            {
                go.AddComponent<T>();
                LinkEffectMountLocations(go);
                LinkEquipmentLocations(go);
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "\nAdded " + typeof(T).ToString() + " to <b>scene object</b> ");
#endif
                #endregion
            }
        }

        //ADD COMPONENT TO PREFAB
        static void AddComponentToPrefab<T>(GameObject go) where T : Component
        {
            GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(go);
            if (!prefab) prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource<GameObject>(go);
            if (prefab != null)
            {
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(" <b>[QUICKSTART]</b> " + prefab.name + "|" + go.name + "\nUpdating Prefab Components..."); //DEBUG
#endif
                #endregion
                if (!prefab.GetComponent<T>())
                {
                    //PrefabUtility.RecordPrefabInstancePropertyModifications(prefab);

                    prefab.AddComponent<T>();

                    #region DEBUG
#if UNITY_EDITOR
                    Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "\nAdded " + typeof(T).ToString() + " to <b>prefab</b> ");
#endif
                    #endregion
                }
                else
                {
                    #region DEBUG
#if UNITY_EDITOR
                    Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "\nSkipped " + typeof(T).ToString() + " <b>prefab</b> ");
#endif
                    #endregion
                }

                LinkEffectMountLocations(prefab);
                LinkEquipmentLocations(prefab);
                PrefabUtility.SavePrefabAsset(prefab);
            }
        }

        //LINK EFFECT MOUNT LOCATIONS
        static void LinkEffectMountLocations(GameObject go)
        {
            if (!go) return; //NO GAME OBJECT

            CharacterSheet character = go.GetComponent<CharacterSheet>();
            if (character == null) return; //NO CHARACTER COMPONENT

            character.rightHandEffectMount = go.transform.FindChildByName(rightHandEffectMount);
            character.leftHandEffectMount = go.transform.FindChildByName(leftHandEffectMount);
        }

        //UPDATE EQUIPMENT LOCATION
        public static void UpdateEquipmentLocation(Gear gear, int index, string location)
        {
            EquipmentInfo info = new EquipmentInfo
            {
                requiredCategory = gear.slots[index].requiredCategory,
#if !RPG2D
                location = gear.gameObject.transform.FindChildByName(location),
#endif
                defaultItem = gear.slots[index].defaultItem
            };
            equipSlots.Add(info);
        }

        //LINK EQUIPMENT LOCATIONS
        static bool mainWeaponSet = false;
        static List<EquipmentInfo> equipSlots = new List<EquipmentInfo>(); //EQUIP SLOTS LIST
        static void LinkEquipmentLocations(GameObject go)
        {
            if (!go) return; //NO GAME OBJECT

            Gear gear = go.GetComponent<Gear>();
            if (gear == null) return; //NO GEAR COMPONENT
            
            mainWeaponSet = false; //RESET MAIN WEAPON CHECK
            equipSlots.Clear();

            //ITERATE THROUGH EQUIPMENT INFO
            for (int i = 0; i < gear.slots.Length; i++)
            {
                Debug.Log("CHECKING " + gear.slots[i]);

                if (gear.slots[i].location == null)
                {
                    switch (gear.slots[i].requiredCategory.ToUpper())
                    {
                        case "HEAD":
                            {
                                UpdateEquipmentLocation(gear, i, headEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(headEquipLocation);
                                break;
                            }
                        case "SHOULDERS":
                            {
                                UpdateEquipmentLocation(gear, i, shoulderEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(shoulderEquipLocation);
                                break;
                            }
                        case "HANDS":
                            {
                                UpdateEquipmentLocation(gear, i, handEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(handEquipLocation);
                                break;
                            }
                        case "CHEST":
                            {
                                UpdateEquipmentLocation(gear, i, chestEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(chestEquipLocation);
                                break;
                            }
                        case "LEGS":
                            {
                                UpdateEquipmentLocation(gear, i, legEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(legEquipLocation);
                                break;
                            }
                        case "FEET":
                            {
                                UpdateEquipmentLocation(gear, i, feetEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(feetEquipLocation);
                                break;
                            }
                        case "WEAPONSIEGE":
                            {
                                UpdateEquipmentLocation(gear, i, siegeWeaponEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(siegeWeaponEquipLocation);
                                break;
                            }
                        case "WEAPONUNARMED":
                            {
                                UpdateEquipmentLocation(gear, i, unarmedWeaponEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(unarmedWeaponEquipLocation);
                                break;
                            }
                        case "WEAPON":
                            {
                                if (!mainWeaponSet)
                                {
                                UpdateEquipmentLocation(gear, i, mainWeaponEquipLocation);
                                    //gear.slots[i].location = gear.gameObject.transform.FindChildByName(mainWeaponEquipLocation);
                                    mainWeaponSet = true;
                                }
                                else
                                {
                                UpdateEquipmentLocation(gear, i, offhandWeaponEquipLocation);
                                    //gear.slots[i].location = gear.gameObject.transform.FindChildByName(offhandWeaponEquipLocation);
                                }
                                break;
                            }
                        case "SHIELD":
                            {
                                UpdateEquipmentLocation(gear, i, shieldEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(shieldEquipLocation);
                                break;
                            }
                        case "ACCESSORY":
                            {
                                UpdateEquipmentLocation(gear, i, accessoryEquipLocation);
                                //gear.slots[i].location = gear.gameObject.transform.FindChildByName(accessoryEquipLocation);
                                break;
                            }
                        default:
                            {
                                //gear.slots[i].location = gear.gameObject.transform;
                                break;
                            }
                    }

#region DEBUG
#if UNITY_EDITOR
                    if (gear.slots[i].location != null) Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "\nBinding equipment location " + gear.slots[i].requiredCategory + " @" + gear.slots[i].location.ToString());
#endif
#endregion

                }
            }

            gear.slots = equipSlots.ToArray();
        }

        //UPDATE SPAWNABLE PREFABS
        public static void UpdatePrefabComponents(GameObject targetObject)
        {
            if (modifyPrefabs == false) return; //DOES NOT MODIFY PREFABS
            /*
            */
            NetworkManagerMMO network = targetObject.GetComponent<NetworkManagerMMO>();
            if (network != null)
            {
                //VALIDATE SPAWNABLE PREFABS
                if (network.spawnPrefabs != null && network.spawnPrefabs.Count > 0)
                {
                    //ITERATE SPAWNABLE PREFABS
                    foreach (GameObject go in network.spawnPrefabs)
                    {
                        if (go != null)
                        {
                            //PLAYER
                            if (go.GetComponent<Player>())
                            {
                                AddComponentToPrefab<PlayerCharacter>(go);
                                /*
                                GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(go);
                                if (prefab != null && !prefab.gameObject.GetComponent<PlayerCharacter>())
                                    prefab.gameObject.AddComponent<PlayerCharacter>();
                                //else go.gameObject.AddComponent<PlayerCharacter>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[PLAYER]" + "\nAdded PlayerCharacter to <b>prefab</b> ");
#endif
#endregion
                                */
                            }
                            //MONSTER
                            else if (go.GetComponent<Monster>())// && !go.GetComponent<CharacterSheet>())
                            {
                                AddComponentToPrefab<CharacterSheet>(go);
                                /*//if (!prefab) prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource<GameObject>(go);
                                GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(go);
                                if (prefab != null && !prefab.gameObject.GetComponent<CharacterSheet>())
                                    prefab.gameObject.AddComponent<CharacterSheet>();
                                //else go.gameObject.AddComponent<CharacterSheet>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[MONSTER]" + "\nAdded CharacterSheet to <b>prefab</b> ");
#endif
#endregion
                                */
                            }
                            //NPC
                            else if (go.GetComponent<Npc>())// && !go.GetComponent<CharacterSheet>())
                            {
                                AddComponentToPrefab<CharacterSheet>(go);
                                /*
                                GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(go);
                                if (prefab != null && !prefab.gameObject.GetComponent<CharacterSheet>())
                                    prefab.gameObject.AddComponent<CharacterSheet>();
                                //else go.gameObject.AddComponent<CharacterSheet>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[NPC]" + "\nAdded CharacterSheet to <b>prefab</b> ");
#endif
#endregion
                                */
                            }
                            //PET
                            else if (go.GetComponent<Pet>())
                            {
                                AddComponentToPrefab<CharacterSheet>(go);
                                /*
                                GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(go);
                                if (prefab != null && !prefab.gameObject.GetComponent<CharacterSheet>())
                                    prefab.gameObject.AddComponent<CharacterSheet>();
                                //else go.gameObject.AddComponent<CharacterSheet>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[PET]" + "\nAdded CharacterSheet to <b>prefab</b> ");
#endif
#endregion
                                */
                            }
                            //MOUNT
                            else if (go.GetComponent<Mount>())// && !go.GetComponent<CharacterSheet>())
                            {
                                AddComponentToPrefab<CharacterSheet>(go);
                                /*
                                GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(go);
                                if (prefab != null && !prefab.gameObject.GetComponent<CharacterSheet>())
                                    prefab.gameObject.AddComponent<CharacterSheet>();
                                //else go.gameObject.AddComponent<CharacterSheet>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[MOUNT]" + "\nAdded CharacterSheet to <b>prefab</b> ");
#endif
#endregion
                                */
                            }
                        }
                    }
                }
            }
        }

        //UPDATE SCENE OBJECTS
        public static void UpdateSceneComponents(GameObject go)
        {
            if (modifyScene == false) return; //DOES NOT MODIFY SCENE
            /*
#region DEBUG
#if UNITY_EDITOR
            //Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "\nUpdating Scene Components..."); //DEBUG
#endif
#endregion
            */
            //PLAYER
            if (go.GetComponent<Player>())
            {
                AddComponentToSceneObject<PlayerCharacter>(go);
                /*
                if (!go.gameObject.GetComponent<PlayerCharacter>()) go.gameObject.AddComponent<PlayerCharacter>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[PLAYER]" + "\nAdded PlayerCharacter to <b>scene object</b> ");
#endif
#endregion
                */
            }
            //MONSTER
            if (go.GetComponent<Monster>())
            {
                AddComponentToSceneObject<CharacterSheet>(go);
                /*
                if (!go.gameObject.GetComponent<CharacterSheet>()) go.gameObject.AddComponent<CharacterSheet>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[MONSTER]" + "\nAdded CharacterSheet to <b>scene object</b> ");
#endif
#endregion
                */
            }
            //NPC
            if (go.GetComponent<Npc>())
            {
                AddComponentToSceneObject<CharacterSheet>(go);
                /*
                if (!go.gameObject.GetComponent<CharacterSheet>()) go.gameObject.AddComponent<CharacterSheet>(); //NO PREFAB FOUND - ADD IT DIRECTLY
#region DEBUG
#if UNITY_EDITOR
                Debug.Log(" <b>[QUICKSTART]</b> " + go.name + "[NPC]" + "\nAdded CharacterSheet to <b>scene object</b> ");
#endif
#endregion
                */
            }
        }
    }
}

                //GameObject prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource<GameObject>(go); //Might not find unlinked prefabs this way
                //GameObject prefab = null;
                //if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                //{
                //prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(go);
                //if (prefab != null && !prefab.GetComponent<PlayerCharacter>()) prefab.gameObject.AddComponent<PlayerCharacter>(); //ADD TO PREFAB
                //}
                //else
                //{
                //}
                //if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                //{
                //GameObject prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource<GameObject>(go);
                //if (prefab != null && !prefab.GetComponent<CharacterSheet>()) prefab.gameObject.AddComponent<CharacterSheet>(); //ADD TO PREFAB
                //}
                //else
                //{
                //}