namespace WhistleblowerSystem.Client.Services
{
    public class OverlayService: IOverlayService
    {
        private bool _showDisclaimer = true;

        public bool GetShowDisclaimer()
        {
            return _showDisclaimer;

        }

        public void SetShowDisclaimer(bool val)
        {
            _showDisclaimer = val;
        }
    }
}