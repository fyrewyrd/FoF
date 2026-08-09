using System.Collections.Generic;
//using Mono.Data.Sqlite;
using System;

public partial class Database
{
    /* //EXAMPLE ONLY - from someone elses script
    public partial class Player
    {

        public enum combatSkillEnum { MELEE, RANGE, MAGIC };

        [Header("Combat Stats")]
        [SyncVar] public int meleeLevel = 1;
        [SyncVar] public int meleeExp = 0;
        [SyncVar] public int rangeLevel = 1;
        [SyncVar] public int rangeExp = 0;
        [SyncVar] public int magicLevel = 1;
        [SyncVar] public int magicExp = 0;
}
    static void Initialize_ExpSystem_pierce()
    {

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_ExpSystem_pierce (
                            character TEXT NOT NULL PRIMARY KEY,
                            meleelevel INTEGER NOT NULL,
                            meleeexp INTEGER NOT NULL,
                            rangelevel INTEGER NOT NULL,
                            rangeexp INTEGER NOT NULL,
                            magiclevel INTEGER NOT NULL,
                            magicexp INTEGER NOT NULL)");
    }

    static void CharacterLoad_ExpSystem_pierce(Player player)
    {
        LoadExpSystem_pierce(player);
    }
    static void CharacterSave_ExpSystem_pierce(Player player)
    {
        SaveExpSystem_pierce(player);
    }

    static void SaveExpSystem_pierce(Player player)
    {
        // skills: remove old entries first, then add all new ones
        ExecuteNonQuery("DELETE FROM character_ExpSystem_pierce WHERE character=@character", new SqliteParameter("@character", player.name));
        ExecuteNonQuery("INSERT INTO character_ExpSystem_pierce VALUES (@character, @meleelevel, @meleeexp, @rangelevel, @rangeexp, @magiclevel, @magicexp)",
                        new SqliteParameter("@character", player.name),
                        new SqliteParameter("@meleelevel", player.meleeLevel),
                        new SqliteParameter("@meleeexp", player.meleeExp),
                        new SqliteParameter("@rangelevel", player.rangeLevel),
                        new SqliteParameter("@rangeexp", player.rangeExp),
                        new SqliteParameter("@magiclevel", player.magicLevel),
                        new SqliteParameter("@magicexp", player.magicExp));
    }

    static void LoadExpSystem_pierce(Player player)
    {
        List<List<object>> table = ExecuteReader("SELECT character, meleelevel, meleeexp, rangelevel, rangeexp, magiclevel, magicexp FROM character_ExpSystem_pierce WHERE character=@character", new SqliteParameter("@character", player.name));

        if (table.Count == 1)
        {
            List<object> mainrow = table[0];
            player.meleeLevel = Convert.ToInt32(mainrow[1]);
            player.meleeExp = Convert.ToInt32(mainrow[2]);
            player.rangeLevel = Convert.ToInt32(mainrow[3]);
            player.rangeExp = Convert.ToInt32(mainrow[4]);
            player.magicLevel = Convert.ToInt32(mainrow[5]);
            player.magicExp = Convert.ToInt32(mainrow[6]);

        }
    }
    */
}
