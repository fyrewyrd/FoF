//NOTE: Changing these will most likely require you to reconfigure the weapon category on any weapons you have already created.
#define UNARMED
#define ANIMAL
#define MAGIC
#define SLASH
#define PIERCE
#define IMPACT
#define SHIELD
#define RANGED
#define SIEGE


//TODO: Body attacks should require you to stop moving to be used, except they can be used while mounted and moving
public enum WeaponCategory
{

#if UNARMED
    UnArmed,       //1h unarmed
#endif
#if ANIMAL
    Animal_Claw,       //1h animal
    Animal_Fang,       //2h animal
    Animal_Breath,     //body animal
    Animal_Tail,
    Animal_Magic,
#endif
#if MAGIC
	Wand,		//1h magic item
	Magic_Staff,  //2h staff magic or not
	Tome,
    Mace,   //book is singular, tome is multiple books and strong single element, grimore is many books different spells
	Death_Scythe,
	Spell_Sword,  //2hand summoned sword
#endif
#if SLASH
    Sword,      //1h slashing   
    Long_Sword, //body slashing
    Axe,        //1h splitting
    Battle_Axe,  //2h splitting
	Katana,
    Scythe, //2h slashing
#endif
#if PIERCE
    Dagger,     //1h piercing
    Spear,      //2h piercing
	Rapier,
	Double_Saber,
#endif
#if IMPACT
    Hammer,       //1h impact
    Warhammer,  //2h impact
	Cestus,  //hand-to-hand impact
	Bo_Staff,
#endif
#if SHIELD
	Buckler_Shield,
    Kite_Shield,
    Tower_Shield,
	Arm_Guard,
	Mana_Wall,
#endif
#if RANGED
    Crossbow,   //1h bow
    Bow,   //2h bow     //1h gun
    Throwing,    //1h+2h throwing
#endif
#if SIEGE
    Ballista,   //body bow
    Cannon,     //body gun
    Catapult    //body throwing
#endif
}

//games
//Rock,
//Paper,
//Scissors,