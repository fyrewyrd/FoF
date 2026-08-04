using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX4D
{
    public enum Hostility { Neutral, Friendly, Hostile }
    public enum TimeOfDay { Day, Night, Twilight }
    public enum LightSource { Unlit, Lantern }

    public class UniversalTargetIndicator : MonoBehaviour
    {
        [SerializeField] Hostility hostility = Hostility.Neutral;
        [SerializeField] TimeOfDay timeOfDay = TimeOfDay.Day;
        [SerializeField] LightSource lighting = LightSource.Unlit;

        private void OnValidate()
        {
            Projector[] projectors = gameObject.GetComponentsInChildren<Projector>();
            string[] conditions = new string[] { hostility.ToString(), timeOfDay.ToString(), lighting.ToString() };
            UpdateProjectors(conditions, projectors);
            //UpdateProjectors(timeOfDay.ToString(), projectors);
            //UpdateProjectors(lighting.ToString(), projectors);
        }
        private void UpdateProjectors(string[] projectorConditions, Projector[] projectors)
        {
            foreach (Projector projector in projectors)
            {
                if (projector.enabled) { projector.enabled = false; }

                foreach (string projectorLabel in projectorConditions)
                {
                    if (projector.transform.parent.gameObject.name.ToLower().Contains(projectorLabel.ToLower()))
                    {
                        projector.enabled = true;
                    }
                }

                //if (projector.transform.parent.gameObject.name.ToLower().Contains(projectorLabel.ToLower())
                    //|| projector.transform.parent.gameObject.name.ToLower().Contains(timeOfDay.ToString().ToLower())
                //    )
                //{
                    //projector.transform.parent.gameObject.SetActive(true);
                //    projector.enabled = true;
                //}
                //else
                //{
                    //projector.transform.parent.gameObject.SetActive(false);
                //    projector.enabled = false;
                //}
            }
        }
        // Start is called before the first frame update
        //void Start() { }
        // Update is called once per frame
        //void Update() { }
    }
}