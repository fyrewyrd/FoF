/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    public Language language = Language.English;

    #region not enough resources to cast skill
    public string notEnoughResourcesToCastSkillMessage
    {
        get {
            switch (language)
            {
                case Language.English:
                    return "You do not have the resources required to cast this skill";
                case Language.Spanish:
                    return "No tienes los recursos necesarios para lanzar esta habilidad.";
                case Language.French:
                    return "Vous n'avez pas les ressources nécessaires pour lancer cette compétence";
                case Language.Turkish:
                    return "Bu beceriyi geliştirmek için gereken kaynaklara sahip değilsin.";
                case Language.Russian:
                    return "U vas net resursov, neobkhodimykh dlya nalozheniya etogo umeniya";
                case Language.Japanese:
                    return "Kono sukiru o kyasuto suru tame ni hitsuyōna risōsu ga arimasen";
                case Language.Chinese:
                    return "Nín méiyǒu tóushè cǐ jìnéng suǒ xū de zīyuán";
                default:
                    return "You do not have the resources required to cast this skill";
            }
        }
    }
    #endregion
}
*/