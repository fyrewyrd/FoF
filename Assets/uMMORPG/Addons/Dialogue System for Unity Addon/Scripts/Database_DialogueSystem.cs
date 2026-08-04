// Dialogue System for Unity addon.
// Saves player's Dialogue System data.
using SQLite;

public partial class Database
{

    class character_dialoguesystem
    {
        [PrimaryKey] // important for performance: O(log n) instead of O(n)
        public string character { get; set; }
        [Indexed]
        public string data { get; set; }
    }

    void Connect_DialogueSystem()
    {
        // create dialoguesystem table if it doesn't exist:
        //ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS dialoguesystem (
        //                  name TEXT NOT NULL PRIMARY KEY,
        //                  data TEXT NOT NULL)");
        connection.CreateTable<character_dialoguesystem>();
    }

    void CharacterLoad_DialogueSystem(Player player)
    {
        //List<List<object>> table = ExecuteReader("SELECT * FROM dialoguesystem WHERE name=@name", new SqliteParameter("@name", player.name));
        //if (table.Count >= 1)
        //{
        //    if (table.Count > 1)
        //    {
        //        UnityEngine.Debug.LogWarning("Dialogue System: The uMMORPG database contains more than one row for " + player.name + ". Using the first row.");
        //    }
        //    List<object> mainrow = table[0];
        //    player.dialogueSystemData = (string)mainrow[1];
        //}
        foreach (character_dialoguesystem row in connection.Query<character_dialoguesystem>("SELECT * FROM character_dialoguesystem WHERE character=?", player.name))
        {
            player.dialogueSystemData = row.data;
        }
    }

    void CharacterSave_DialogueSystem(Player player)
    {
        //ExecuteNonQuery("INSERT OR REPLACE INTO dialoguesystem VALUES (@name, @data)",
        //                new SqliteParameter("@name", player.name),
        //                new SqliteParameter("@data", player.dialogueSystemData));
        // quests: remove old entries first, then add all new ones
        connection.Execute("DELETE FROM character_dialoguesystem WHERE character=?", player.name);
        connection.InsertOrReplace(new character_dialoguesystem
        {
            character = player.name,
            data = player.dialogueSystemData
        });
    }
}