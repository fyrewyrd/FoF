using Mirror;
using UnityEngine;
using System.Collections.Generic;

public class InspectorDictionary : NetworkBehaviour {
    /*
    public ScriptableSkill[] scriptedSkills;

    [SerializeField] public Dictionary<WeaponCategory, ScriptableSkill> skills = new Dictionary<WeaponCategory, ScriptableSkill>
        (System.Enum.GetValues(typeof(WeaponCategory)).Length);

    public ScriptableSkill GetSkill(WeaponCategory weaponCategory)
    {
        return skills[weaponCategory];
    }
    */

    [System.Serializable]
    public class MyDictionaryEntry
    {
        public GameObject key;
        public float value;
    }

    [SerializeField]
    private List<MyDictionaryEntry> inspectorDictionary = new List<MyDictionaryEntry>();

    private Dictionary<GameObject, float> myDictionary;

    private void Awake()
    {
        myDictionary = new Dictionary<GameObject, float>();
        foreach (MyDictionaryEntry entry in inspectorDictionary)
        {
            myDictionary.Add(entry.key, entry.value);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
