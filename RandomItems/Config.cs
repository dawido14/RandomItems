using Exiled.API.Interfaces;
using System.ComponentModel;


namespace RandomItems
{
    public sealed class Config : IConfig
    {
        [Description("You can enable/disable plugin here")]
        public bool IsEnabled { get; set; } = true;
  
    }
}
