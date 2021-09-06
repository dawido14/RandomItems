using Exiled.API.Features;

namespace RandomItems
{
    public static class LogSystem
    {
        public static void OnPlayerGetItem(string item, string playerNickname)
        {
            if (RandomItems.newRandomItems.Config.LogWhenPlayerGetItem)
            {
                Log.Info($"{playerNickname} get {item}!");
            }
        }
    }
}
