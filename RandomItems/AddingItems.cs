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
                try
                {
                    foreach (var singlePermission in permissionsConfig.Permissions)
                    {
                        if (e.Player.CheckPermission($"randomitems.{singlePermission.Key}"))
                        {
                            CheckClasses(singlePermission.Value, e);
                        }
                    }
                }
                catch 
                {

                    Log.Error("Config contains errors or it's empty!");
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
                    }
                }
            }
        }
        int addedItems = 0;
        void RandomizeItems(HumanClasses humanClasses, ChangedRoleEventArgs e)
        {
            addedItems = 0;
            Random random = new Random();         
            foreach (var item in humanClasses.Chances)
            {
                int chance = random.Next(1, 100);
                if (item.Key >= chance)
                {
                    if (humanClasses.MaxNumOfItemList > addedItems)
                    {
                        CheckItems(item.Value, e);
                    }
                }
            }
        }
        void CheckItems(ItemList itemList, ChangedRoleEventArgs e)
        {
            if (itemList.Items != null)
            {
                addedItems++;
                foreach (string item in itemList.Items)
                {
                    if (item != null)
                    {
                        try
                        {
                            ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), item, true);
                            e.Player.AddItem(itemType);

                            LogSystem.WhenPlayerGetItem(item, e.Player.Nickname);
                        }
                        catch
                        {
                            Log.Error($"Can't add {item} to {e.Player.Nickname}.");
                        }
                    }
                }
            }           
        }
    }
}
