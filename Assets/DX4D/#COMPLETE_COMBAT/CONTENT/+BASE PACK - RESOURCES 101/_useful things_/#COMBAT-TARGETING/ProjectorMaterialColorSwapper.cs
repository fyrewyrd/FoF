/* //DEPRECIATED - This turned out to be pretty damgerous...
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class ProjectorMaterialColorSwapper : MonoBehaviour
{
    [SerializeField] Color color = Color.white;
    [SerializeField] Projector _projector;
    Projector projector
    {
        get
        {
            //NOTE: Unity may complain here if this happens at runtime.
            //      It's best to have everything predeclared in the inspector instead.
            if (!_projector) _projector = gameObject.GetComponent<Projector>();

            return _projector;
        }
    }

    private void OnValidate()
    {
        projector.material.color = color;
    }

    // Start is called before the first frame update
    //void Start() { }
    // Update is called once per frame
    //void Update() { }
}
*/