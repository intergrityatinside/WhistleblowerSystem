namespace WhistleblowerSystem.Client.Services
{
    public interface IOverlayService
    {
        public bool GetShowDisclaimer();
        public void SetShowDisclaimer(bool val);
    }
}