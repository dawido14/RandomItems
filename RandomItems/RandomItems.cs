using Exiled.API.Features;
using Exiled.API.Enums;

using Handler = Exiled.Events.Handlers;
using System;

namespace RandomItems
{ 
    public class RandomItems: Plugin<Config>
    {
        private static readonly RandomItems randomItems = new RandomItems();
        public static RandomItems newRandomItems => randomItems;
        public override PluginPriority Priority { get; } = PluginPriority.Medium;
        public override Version RequiredExiledVersion => new Version(2, 13, 0);
        public override Version Version => new Version(1 , 2 , 0);
        private RandomItems()
        {
        }
        AddingItems addingItems;
        public override void OnEnabled()
        {
            addingItems = new AddingItems();
            Handler.Player.ChangedRole += addingItems.OnPlayerSpawn;

            ItemsConfig.DeserializeItemsConfig();

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            AddingItems.permissionsConfig = null;
            Handler.Player.ChangedRole -= addingItems.OnPlayerSpawn;
            addingItems = null;

            base.OnDisabled();
        }
    }
}
