using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Permissions.Extensions;
using System;

namespace RandomItems
{
    class AddingItems
    {
        public static PermissionsConfig permissionsConfig = new PermissionsConfig();
        public void OnPlayerSpawn(ChangedRoleEventArgs e)
        {
            if (e.Player.IsHuman)
            {
                if (permissionsConfig != null)
                {
                    foreach (var singlePermission in permissionsConfig.Permissions)
                    {
                        if (e.Player.CheckPermission($"randomitems.{singlePermission.Key}"))
                        {
                            CheckClasses(singlePermission.Value, e);
                        }
                    }
                }
                else
                {
                    Log.Error("Can't read config!");
                }
            }        
        }
        void CheckClasses(PermissionsNames permissions, ChangedRoleEventArgs e)
        {
            foreach (var playerClass in permissions.Classes)
            {
                if (playerClass.Key.ToLower() == "all")
                {
                    RandomizeItems(playerClass.Value, e);
                }
                else
                {
                    try
                    {
                        if (e.Player.Role == (RoleType)Enum.Parse(typeof(RoleType), playerClass.Key, true))
                        {
                            RandomizeItems(playerClass.Value, e);
                        }
                    }
                    catch
                    {
                        Log.Error($"Can't find role \"{playerClass.Key}\"");
                        throw;
                    }
                }
            }
        }
        void RandomizeItems(HumanClasses humanClasses, ChangedRoleEventArgs e)
        {
            Random random = new Random();
            int chance = random.Next(1, 100);
            foreach (var item in humanClasses.Chances)
            {               
                if (item.Key >= chance)
                {
                    CheckItems(item.Value, e);
                }
            }
        }
        void CheckItems(ItemList itemList, ChangedRoleEventArgs e)
        {
            foreach (string item in itemList.Items)
            {
                try
                {
                    e.Player.AddItem((ItemType)Enum.Parse(typeof(ItemType), item, true));
                }
                catch 
                {
                    Log.Error($"Can't add {item} to {e.Player.Nickname}.");
                    throw;
                }            
            }
        }
    }
}
