namespace Skynet.DotNetClient.LockStep
{
    public class Receiver
    {
        public void Action()
        {
            SkynetLogger.Info(Channel.LockStep,"Called Receiver.Action()");
        }
    }
}