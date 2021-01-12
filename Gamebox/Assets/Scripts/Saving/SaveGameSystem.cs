﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGameSystem
{
    public static void SaveGame(MoneySystem moneySystem, Bottles bottles, ShopSystem shopSystem, Fuel fuel, ResourceSystem resourceSystem, MixingSystem mixingSystem, PotionSystem potionSystem, GuildSystem guildSystem, RecipesMenu recipesMenu)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(moneySystem, bottles, shopSystem, fuel, resourceSystem, mixingSystem, potionSystem, guildSystem, recipesMenu);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}