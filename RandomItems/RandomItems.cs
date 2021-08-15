using Exiled.API.Features;
using Exiled.API.Enums;

using Player = Exiled.Events.Handlers.Player;

namespace RandomItems
{ 
    public class RandomItems: Plugin<Config>
    {
        private static readonly RandomItems randomItems = new RandomItems();
        public static RandomItems newRandomItems => randomItems;
        public override PluginPriority Priority { get; } = PluginPriority.Medium;
        private RandomItems()
        {
        }
        AddingItems addingItems;
        public override void OnEnabled()
        {           
            addingItems = new AddingItems();
            Player.ChangedRole += addingItems.OnPlayerSpawn;

            ItemsConfig.DeserializeItemsConfig();

            base.OnEnabled();
        }
        public override void OnReloaded()
        {
            AddingItems.permissionsConfig = null;
            ItemsConfig.DeserializeItemsConfig();
            base.OnReloaded();  
        }
        public override void OnDisabled()
        {
            AddingItems.permissionsConfig = null;
            Player.ChangedRole -= addingItems.OnPlayerSpawn;
            addingItems = null;

            base.OnDisabled();           
        }    
    }
}
