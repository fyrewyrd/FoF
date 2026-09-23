using System.Collections.Generic;
using UnityEngine;

#if _MYSQL
using MySql.Data;
using MySql.Data.MySqlClient;
#elif _SQLITE
using SQLite;
#endif

public partial class Database
{
    private class uce_uma
    {
        public string character { get; set; }
        public string dna { get; set; }
    }

    private void Connect_UmaIntegration()
    {
        connection.CreateTable<uce_uma>();
#if _MYSQL
        ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS uce_uma (`character` VARCHAR(32) NOT NULL, dna TEXT NOT NULL) CHARACTER SET=utf8mb4");
#endif
    }

    private void CharacterLoad_UmaIntegration(Player player)
    {
        LoadUma(player);
    }

    private void LoadUma(Player player)
    {
        string loadedDna = connection.ExecuteScalar<string>(
            "SELECT dna FROM uce_uma WHERE character=?", player.name);

        player.umaDna = loadedDna ?? "";

#if _MYSQL
        var table = ExecuteReaderMySql(
            "SELECT dna FROM uce_uma WHERE `character`=@character",
            new MySqlParameter("@character", player.name));
        if (table.Count == 1)
        {
            player.umaDna = (string)table[0][0];
        }
#endif
    }

    private void CharacterSave_UmaIntegration(Player player)
    {
        saveUMA(player);
    }

    private void saveUMA(Player player)
    {
        player.PackUmaDna();

        connection.Execute("DELETE FROM uce_uma WHERE character=?", player.name);
        connection.InsertOrReplace(new uce_uma
        {
            character = player.name,
            dna = player.umaDna ?? ""
        });

        Debug.Log("[UMA] Saved " + player.name + " dnaLen=" + (player.umaDna ?? "").Length);

#if _MYSQL
        ExecuteNonQueryMySql("DELETE FROM uce_uma WHERE `character`=@character",
            new MySqlParameter("@character", player.name));
        ExecuteNonQueryMySql("INSERT INTO uce_uma VALUES (@character, @dna)",
            new MySqlParameter("@character", player.name),
            new MySqlParameter("@dna", player.umaDna ?? ""));
#endif
    }
}