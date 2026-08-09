/* //DEPRECIATED
#if !UNET
using Mirror;
#else
using UnityEngine.Networking;
#endif

using UnityEngine;
using System.Collections;
using TMPro;
using DX4D;

public class OverheadDamageText : OverheadText
{

    public OverheadText text;

    //DELAYED POPUPS
    //[Client] IEnumerator ShowDamagePopup(int amount, DefensiveState defensiveState, MethodOfDamage method, Element element, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //
    //    ShowDamagePopup(amount, defensiveState, method, element);
    //}

    //IMMEDIATE POPUPS
    [Client] void ShowDamagePopup(int amount, DefensiveState defensiveState, MethodOfDamage method, Element element)
    {
        //ADDED IN UMMORPG 1.166
        // spawn the damage popup (if any) and set the text
        //if (damagePopupPrefab != null)
        //{
        //    // showing it above their head looks best, and we don't have to use
        //    // a custom shader to draw world space UI in front of the entity
        //    Bounds bounds = collider.bounds;
        //    Vector3 position = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
        //
        //    GameObject popup = Instantiate(damagePopupPrefab, position, Quaternion.identity);
        //    if (damageType == DamageType.Normal)
        //        popup.GetComponentInChildren<TextMeshPro>().text = amount.ToString();
        //    else if (damageType == DamageType.Block)
        //        popup.GetComponentInChildren<TextMeshPro>().text = "<i>Block!</i>";
        //    else if (damageType == DamageType.Crit)
        //        popup.GetComponentInChildren<TextMeshPro>().text = amount + " Crit!";
        //}

        if (text.textPopupPrefab != null)
        {
            Vector3 loc = popup.orientation.AsVector;
            Bounds bounds = GetComponent<Collider>().bounds;

            Vector3 v = (bounds != null) ? (new Vector3(bounds.max.x, bounds.max.y, bounds.max.z)) : transform.position;

            ShowTextPopup(message, popup.style.color, popup.style.size, new Vector3((v.x * loc.x), (v.y * loc.y), (v.z * loc.z)), popup.style.animation.velocity);

            GameObject popup = Instantiate(text.textPopupPrefab, text.overhead, Quaternion.identity);

            string popupText = text.popup.textPrefix + ((amount < 1) ? ("0") : (amount.ToString())) + text.popup.textSuffix; // Clear and Initialize Popup Text
            Color popupTextColor = text.popup.style.color;
            int popupTextSize = text.popup.style.size;

            if (method != MethodOfDamage.Physical)
            {
                popupText += " "; // Add a Space
            }

            // A P P E N D  D A M A G E  T Y P E
            switch (method)
            {
                case MethodOfDamage.Physical:
                    {
                        //popupText += "physical";
                        break;
                    }
                case MethodOfDamage.Magic:
                    {
                        popupText += "magic";
                        break;
                    }
                case MethodOfDamage.NoDamage:
                    {
                        popupText = "";
                        break;
                    }
                case MethodOfDamage.Mana:
                    {
                        popupText += "mana";
                        break;
                    }
                case MethodOfDamage.Blood:
                    {
                        popupText += "blood";
                        break;
                    }
                case MethodOfDamage.Spirit:
                    {
                        popupText += "spirit";
                        break;
                    }
                case MethodOfDamage.Fury:
                    {
                        popupText += "fury";
                        break;
                    }
                case MethodOfDamage.Stamina:
                    {
                        popupText += "stamina";
                        break;
                    }
                case MethodOfDamage.Poison:
                    {
                        popupText += "poison";
                        break;
                    }
            }

            //popupText += " "; // Add a Space
            //if (element != DamageElement.Neutral) popupText += " "; // Add a Space

            if (element != Element.Neutral)
            {
                popupText += " "; // Add a Space
            }

            // A P P E N D  E L E M E N T A L  T Y P E
            popupTextColor = Tools.GetColorFromElement(element);

            /*switch (element)
            {
                case Element.Neutral:
                    {
                        //popupText += "neutral "; //Don't specify if damage is neutral
                        popupTextColor = Config.Text.damageTextColor;
                        break;
                    }
                case Element.Earth:
                    {
                        //popupText += "[earth]";
                        popupTextColor = Config.Text.Element.earthColor;
                        break;
                    }
                case Element.Fire:
                    {
                        //popupText += "[fire]";
                        popupTextColor = Config.Text.Element.fireColor;
                        break;
                    }
                case Element.Water:
                    {
                        //popupText += "[water]";
                        popupTextColor = Config.Text.Element.waterColor;
                        break;
                    }
                case Element.Ice:
                    {
                        //popupText += "[ice]";
                        popupTextColor = Config.Text.Element.iceColor;
                        break;
                    }
                case Element.Wind:
                    {
                        //popupText += "[wind]";
                        popupTextColor = Config.Text.Element.windColor;
                        break;
                    }
                case Element.Lightning:
                    {
                        //popupText += "[lightning]";
                        popupTextColor = Config.Text.Element.lightningColor;
                        break;
                    }
                case Element.Dark:
                    {
                        //popupText += "[dark]";
                        popupTextColor = Config.Text.Element.darkColor;
                        break;
                    }
                case Element.Holy:
                    {
                        //popupText += "[holy]";
                        popupTextColor = Config.Text.Element.holyColor;
                        break;
                    }
            }*/

            /*
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // O T H E R  P E O P L E S  D A M A G E  O V E R H E A D  D A M A G E  P O P U P
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            /* //TODO
            switch (activeOffensiveState)
            {
                    // D A M A G E
                case OffensiveState.DealingDamage:
                    {
                        popupTextSize = normalTextSize;
                        //popupText = amount.ToString() + " " + popupText;
                        break;
                    }
                case OffensiveState.DealingSpellDamage:
                    {
                        popupTextSize = normalTextSize;
                        //popupText = amount.ToString() + " " + popupText;
                        break;
                    }
                    // C R I T
                case OffensiveState.DealingCriticalDamage:
                    {
                        popupTextSize = criticalTextSize;
                        popupText = "CRITICAL!" + "\n" + popupText;
                        break;
                    }
                case OffensiveState.DealingCriticalSpellDamage:
                    {
                        popupTextSize = criticalTextSize;
                        popupText = "CRITICAL!" + "\n" + popupText;
                        break;
                    }
                // R E F L E C T  A B S O R B  I M M U N E  R E S I S T
                case OffensiveState.DamageReflected:
                    {
                        popupTextSize = reflectTextSize;
                        popupText = "REFLECTED!";
                        break;
                    }
                case OffensiveState.DamageAbsorbed:
                    {
                        popupTextSize = absorbTextSize;
                        popupTextColor = Color.green;
                        popupText = "ABSORBED!" + "\n" + popupText;
                        break;
                    }
                case OffensiveState.DamageIgnored:
                    {
                        popupTextSize = immuneTextSize;
                        popupText = "IMMUNE!";
                        break;
                    }
                case OffensiveState.DamageResisted:
                    {
                        popupTextSize = resistTextSize;
                        //popupText = "RESISTED!" + "\n" + popupText;
                        break;
                    }
                // W E A K N E S S E S
                case OffensiveState.DamagingWeakness:
                    {
                        popupTextSize = weakToTextSize;
                        popupText = "WEAK!" + "\n" + popupText;
                        break;
                    }
                case OffensiveState.DamagingAchillesHeel:
                    {
                        popupTextSize = achillesHeelTextSize;
                        popupText = "VERY WEAK!" + "\n" + popupText;
                        break;
                    }
                // B L O C K
                case OffensiveState.AttackBlocked:
                    {
                        popupTextSize = blockTextSize;
                        popupText = "BLOCKED!" + "\n" + popupText;
                        break;
                    }
                case OffensiveState.SpellBlocked:
                    {
                        popupTextSize = blockTextSize;
                        popupText = "BLOCKED!" + "\n" + popupText;
                        break;
                    }
                    // D O D G E
                case OffensiveState.AttackDodged:
                    {
                        popupTextSize = dodgeTextSize;
                        popupText = "MISSED!";
                        break;
                    }
                case OffensiveState.SpellDodged:
                    {
                        popupTextSize = dodgeTextSize;
                        popupText = "MISSED!";
                        break;
                    }
            }
            */

            /*
            // - - - - - - - - - - - - - - - - - - - - - - - - - -
            // E N E M Y  O V E R H E A D  D A M A G E  P O P U P
            // - - - - - - - - - - - - - - - - - - - - - - - - - -
            switch (defensiveState)
            {
                // N O R M A L  D A M A G E
                case DefensiveState.TakingDamage:
                    {
                        popupTextSize = Config.Text.normalTextSize;
                        break;
                    }
                case DefensiveState.TakingSpellDamage:
                    {
                        popupTextSize = Config.Text.normalTextSize;
                        break;
                    }
                // C R I T I C A L
                case DefensiveState.TakingCriticalDamage:
                    {
                        popupTextSize = Config.Text.largeTextSize;
                        popupText = "[critical]" + "\n" + popupText;
                        break;
                    }
                case DefensiveState.TakingCriticalSpellDamage:
                    {
                        popupTextSize = Config.Text.criticalTextSize;
                        popupText = "[critical]" + "\n" + popupText;
                        break;
                    }
                // B A C K S T A B
                case DefensiveState.TakingBackstabDamage:
                    {
                        popupTextSize = Config.Text.backstabTextSize;
                        popupText = "[backstab]" + "\n" + popupText;
                        break;
                    }
                case DefensiveState.TakingCriticalBackstabDamage:
                    {
                        popupTextSize = Config.Text.criticalBackstabTextSize;
                        popupText = "[critical backstab]" + "\n" + popupText;
                        break;
                    }
                // F L A N K
                case DefensiveState.TakingFlankDamage:
                    {
                        popupTextSize = Config.Text.flankTextSize;
                        popupText = "[flanked]" + "\n" + popupText;
                        break;
                    }
                case DefensiveState.TakingCriticalFlankDamage:
                    {
                        popupTextSize = Config.Text.criticalFlankTextSize;
                        popupText = "[critical flanked]" + "\n" + popupText;
                        break;
                    }
                // W E A K N E S S E S
                case DefensiveState.TakingWeakToDamage:
                    {
                        popupTextSize = Config.Text.weakToTextSize;
                        popupText = "[weak]" + "\n" + popupText;
                        break;
                    }
                case DefensiveState.TakingAchillesHeelDamage:
                    {
                        popupTextSize = Config.Text.veryWeakToTextSize;
                        popupText = "[very weak]" + "\n" + popupText;
                        break;
                    }
                // R E F L E C T  A B S O R B  I M M U N E  R E S I S T
                case DefensiveState.ReflectingDamage:
                    {
                        popupTextSize = Config.Text.reflectTextSize;
                        popupText = "[reflect]";
                        break;
                    }
                case DefensiveState.AbsorbingDamage:
                    {
                        popupTextSize = Config.Text.absorbTextSize;
                        popupTextColor = Config.Text.healTextColor;
                        popupText = "[absorb]";// + "\n" + popupText;
                        //return; //NO POPUP
                        break;
                    }
                case DefensiveState.IgnoringDamage:
                    {
                        popupTextSize = Config.Text.immuneTextSize;
                        popupText = "[immune]";
                        break;
                    }
                case DefensiveState.ResistingDamage:
                    {
                        popupTextSize = Config.Text.resistTextSize;
                        popupText = "[resist]" + "\n" + popupText;
                        break;
                    }
                // D O D G E
                case DefensiveState.DodgingAttack:
                    {
                        popupTextSize = Config.Text.dodgeTextSize;
                        popupText = "[miss]";
                        break;
                    }
                case DefensiveState.DodgingSpell:
                    {
                        popupTextSize = Config.Text.dodgeTextSize;
                        popupText = "[miss]";
                        break;
                    }
                // B L O C K
                case DefensiveState.BlockingAttack:
                    {
                        popupTextSize = Config.Text.blockTextSize;
                        popupText = "[block]" + "\n" + popupText;
                        break;
                    }
                case DefensiveState.BlockingSpell:
                    {
                        popupTextSize = Config.Text.blockTextSize;
                        popupText = "[block]" + "\n" + popupText;
                        break;
                    }
            }

            // R A N D O M  P O P U P  D I R E C T I O N
            if (Config.Popup.randomizeDamageDirection)
            {
                if (popup != null && popup.activeSelf)
                {
                    //RANDOMIZE TEXT DIRECTION
                    //TODO: The catch block here exists for legacy support...get rid of it if all of your entities are using the DX4D TextPopup
                    try
                    {
                        //popup.GetComponent<PopupBehaviour>().initialVelocity = new Vector3(
                        popup.GetComponent<DefaultVelocity>().velocity = new Vector3(
                            Random.Range(-Config.Popup.maxTextVelocity, Config.Popup.maxTextVelocity),
                            Random.Range(-Config.Popup.maxTextVelocity, Config.Popup.maxTextVelocity));
                    }
                    catch
                    {
                        popup.GetComponent<DefaultVelocity>().velocity = new Vector3(
                            Random.Range(-Config.Popup.maxTextVelocity, Config.Popup.maxTextVelocity),
                            Random.Range(-Config.Popup.maxTextVelocity, Config.Popup.maxTextVelocity),
                            Random.Range(-Config.Popup.maxTextVelocity, Config.Popup.maxTextVelocity));

                        //SET FORWARD VELOCITY
                        popup.GetComponent<DefaultVelocity>().velocity.x *= -transform.forward.x;// agent.transform.forward.x;
                        popup.GetComponent<DefaultVelocity>().velocity.y *= -transform.forward.y;// agent.transform.forward.y;
                        //popup.GetComponent<DefaultVelocity>().velocity.z *= -agent.transform.forward.z; //TODO: Removed this for 2d support
                    }

                    Rigidbody rb;
                    rb = popup.GetComponent<Rigidbody>();
                    if(rb)
                    {
                        rb.useGravity = Config.Popup.gravityEffectsPopupText;
                    }
                    else
                    {
                        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
                        if (rb2d) rb2d.gravityScale = 1.0f;
                    }
                }
            }

            // C O N F I G  P O P U P  S H A D O W
            GameObject shadow = GameObject.Find("Shadow");
            if (shadow != null)
            {
                shadow.SetActive(Config.Popup.shadowEnabled); //Enable/Disable damage text shadow
                if (shadow.activeSelf) shadow.GetComponent<TextMeshPro>().fontSize = popupTextSize; //Fixes shadow size bug
                //if (shadow.activeSelf) shadow.GetComponent<TextMesh>().fontSize = popupTextSize; //Fixes shadow size bug
            }

            // S H O W  D A M A G E  P O P U P
            if (popupTextFont != null) popup.GetComponentInChildren<TextMeshPro>().font = popupTextFont;
            popup.GetComponentInChildren<TextMeshPro>().color = popupTextColor;
            popup.GetComponentInChildren<TextMeshPro>().fontSize = popupTextSize;
            popup.GetComponentInChildren<TextMeshPro>().text = popupText;

            //DEPRECIATED IN UMMORPG 1.166
            //if (popupTextFont != null) popup.GetComponentInChildren<TextMesh>().font = popupTextFont;
            //popup.GetComponentInChildren<TextMesh>().color = popupTextColor;
            //popup.GetComponentInChildren<TextMesh>().fontSize = popupTextSize;
            //popup.GetComponentInChildren<TextMesh>().text = popupText;

            
        }
    }
}
*/
    /*
    [ClientRpc(channel = Channels.DefaultUnreliable)] // unimportant => unreliable
    void RpcOnDamageReceived(int amount, DamageType damageType)
    {
        ShowDamagePopup(amount);

        // addon system hooks
        Utils.InvokeMany(typeof(Entity), this, "OnDamageReceived_", amount, damageType);
    }
    */
    /*
    void RpcOnDamageReceived(int amount, BaseDamageType damageType, DamageElement element, DamageModifier modifier)
    {
        // show popup above receiver's head in all observers via ClientRpc
        ShowDamagePopup(amount, damageType, element, modifier);

        // addon system hooks
        Utils.InvokeMany(typeof(Entity), this, "OnDamageReceived_", amount, damageType, element, modifier);
    }
    */
