//#define RPG2D //NOTE: Enable this define for 2D support...or import the 2D_MODE unity package included with this asset
#define TEXTMESHPRO
using Mirror;
using UnityEngine;
//using System.Collections;

//TEXT
#if TEXTMESHPRO && !RPG2D
using TMPro;
#else
using System.Text;
#endif

public partial class CharacterSheet : NetworkBehaviour
{
    // T E X T  P O P U P
    ///[CLIENT]
    [Client] void HandleTextPopup(Transform target, string message, ScriptedTextStyle text)
    {
        if (!target) { return; }
        if (!text || !text.popupPrefab) { return; }

        //Vector3 offset = textConfig.orientation.AsVector;


        //  ________________________________________________________________
        //  |                > > >  NOTE TO 2D USERS  < < <                 |
        //  |   If you are using 2D just scroll to the top of this script   |
        //  |   and remove the // from in front of #define RPG2D            |
        //  |   Alternatively you can just import the 2D_MODE.unitypackage  |
#if RPG2D
        Collider2D col = target.GetComponent<Collider2D>();
#else
        Collider col = target.GetComponentInChildren<Collider>();
#endif


        Bounds bounds = col.bounds;

        Vector3 v = (bounds != null) ? (bounds.center) : (transform.position);// (bounds.center + offset) : (transform.position + offset);
        if (bounds != null) v.y = bounds.max.y;

        //CREATE THE TEXT POPUP
        GameObject myPopup = Instantiate(text.popupPrefab, v, Quaternion.identity);
        if (myPopup == null || !myPopup.activeSelf) return;

        //PARENT THE POPUP TO THE TARGET
        myPopup.transform.SetParent(target, true);

        // C O N F I G  D A M A G E  S H A D O W
        GameObject shadow = GameObject.Find("Shadow");
        if (shadow != null)
        {
            shadow.SetActive(text.textShadowEnabled); //Enable/Disable damage text shadow

            if (shadow.activeSelf)
            {
#if TEXTMESHPRO && !RPG2D
                TextMeshPro textMesh = shadow.GetComponent<TextMeshPro>();
#else
                TextMesh textMesh = shadow.GetComponent<TextMesh>();
#endif
                if(textMesh != null) textMesh.fontSize = text.size; //Fixes shadow size bug
            }
            //if (shadow.activeSelf)
            //{
            //    if(textMesh != null) textMesh.fontSize = text.size; //Fixes shadow size bug
            //}
            //if (shadow.activeSelf) shadow.GetComponent<TextMesh>().fontSize = text.size; //Fixes shadow size bug
        }

        // S H O W  T E X T  P O P U P
#if TEXTMESHPRO && !RPG2D
        myPopup.GetComponentInChildren<TextMeshPro>().text = message; // m e s s a g e
        if (text.popupTextFont != null) myPopup.GetComponentInChildren<TextMeshPro>().font = text.popupTextFont; // f o n t
        myPopup.GetComponentInChildren<TextMeshPro>().color = text.color; // c o l o r
        myPopup.GetComponentInChildren<TextMeshPro>().fontSize = text.size; // t e x t s i z e
        myPopup.GetComponent<FadeTextMeshPro>().duration = text.duration; //FADE
        myPopup.GetComponent<Velocity3D>().velocity = text.velocity; //MOTION
#else
        TextMesh popupText = myPopup.GetComponent<TextMesh>();
        if (!popupText) popupText = myPopup.GetComponentInChildren<TextMesh>();

        if (text.popupTextFont != null) popupText.font = text.popupTextFont;
        popupText.color = text.color; // Debug.Log(message+text.color.ToString() + popupText.color.ToString());
        popupText.fontSize = text.size + 50; //NOTE: we add 50 here to scale the text size up for 2d
        popupText.text = message; //popupText;
        myPopup.GetComponent<FadeTextMesh>().duration = text.duration; //FADE
        myPopup.GetComponent<Velocity2D>().velocity = text.velocity; //MOTION
#endif

        //FADE AWAY AND DESTROY
        myPopup.GetComponent<DestroyAfter>().time = text.duration;

        //MOTION
        //myPopup.GetComponent<DefaultVelocity>().velocity = text.velocity;
    }
}
