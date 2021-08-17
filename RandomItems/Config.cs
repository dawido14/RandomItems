using Exiled.API.Interfaces;
using System.ComponentModel;

namespace RandomItems
{
    public sealed class Config : IConfig
    {
        [Description("You can enable/disable plugin here")]
        public bool IsEnabled { get; set; } = true;

        [Description("When the player gets an item, it will be logged in console")]
        public bool LogWhenPlayerGetItem { get; set; } = false;
    }
}
