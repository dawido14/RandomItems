using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace RandomItems
{
    static class ItemsConfig
    {
        static void CheckFiles()
        {
            if (!Directory.Exists(Paths.Plugins + "/RandomItems"))
                Directory.CreateDirectory(Paths.Plugins + "/RandomItems");

            if (!File.Exists(Paths.Plugins + $"/RandomItems/{Server.Port}.yml"))
            {
                Log.Warn($"Creating item config for {Server.Port}");
                CreateNewConfig();
            }         
        }
        public static void DeserializeItemsConfig()
        {
            CheckFiles();
            try
            {
                FileStream stream = new FileStream(Paths.Plugins + $"/RandomItems/{Server.Port}.yml", FileMode.OpenOrCreate, FileAccess.Read);
                IDeserializer deserializer = new DeserializerBuilder().Build();

                PermissionsConfig permissionsConfig = deserializer.Deserialize<PermissionsConfig>(new StreamReader(stream));
                AddingItems.permissionsConfig = permissionsConfig;

                stream.Close();
            }
            catch
            {
                Log.Error("Can't deserialize config");
            }   
        }
        static void CreateNewConfig()
        {
            PermissionsConfig permissionsConfig = new PermissionsConfig();
            permissionsConfig.Permissions = new Dictionary<string, PermissionsNames>();

            
            PermissionsNames permissionsNames = new PermissionsNames();
            HumanClasses humanClasses = new HumanClasses();
            ItemList items = new ItemList();

            permissionsNames.Classes = new Dictionary<string, HumanClasses>();
            humanClasses.Chances = new Dictionary<int, ItemList>();

            permissionsConfig.Permissions.Add("vip", permissionsNames);
            permissionsNames.Classes.Add("ClassD", humanClasses);
            humanClasses.Chances.Add(100, items);

            items.Items.Add("Coin");
            items.Items.Add("Medkit");            

            TextWriter writer = File.CreateText(Paths.Plugins + $"\\RandomItems\\{Server.Port}.yml");
            ISerializer serializer = new SerializerBuilder().Build();  
            
            string newString = serializer.Serialize(permissionsConfig);
            StringReader reader = new StringReader(newString);
            YamlStream yamlStream = new YamlStream();

            yamlStream.Load(reader);
            yamlStream.Save(writer);

            reader.Close();
            writer.Close();
        }
    }
    public class PermissionsConfig
    {
        public Dictionary<string, PermissionsNames> Permissions;
    }
    public class PermissionsNames
    {
        public Dictionary<string, HumanClasses> Classes;
    }
    public class HumanClasses
    {
        public int MaxNumOfItemList = 1;
        public Dictionary<int, ItemList> Chances;
    }
    public class ItemList
    {
        public List<string> Items = new List<string>();
    }
}
