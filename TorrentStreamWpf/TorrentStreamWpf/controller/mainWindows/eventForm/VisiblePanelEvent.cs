using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.mainWindows.eventForm
{
    public class VisiblePanelEvent
    {
        private MainObjModel _mom;

        public VisiblePanelEvent(MainObjModel mom)
        {
            _mom = mom;
        }

        public void VisiblePanel()
        {
            VisiblePanelControl();
        }

        public void HidePanel()
        {
            HidePanelControl();
        }
        private void VisiblePanelControl()
        {
            _mom.main.BorderSlider.Opacity = 100;
            _mom.main.SliderVlc.Opacity = 100;
            _mom.main.BorderButton.Opacity = 100;
            _mom.main.ButtonPlay.Opacity = 100;
            _mom.main.ButtonPause.Opacity = 100;
            _mom.main.ButtonStop.Opacity = 100;
            _mom.main.ButtonFullScreen.Opacity = 100;
            _mom.main.SlederVolume.Opacity = 100;
            _mom.main.Menu.Opacity = 100;
        }
        private void HidePanelControl()
        {
            _mom.main.BorderSlider.Opacity = 0;
            _mom.main.SliderVlc.Opacity = 0;
            _mom.main.BorderButton.Opacity = 0;
            _mom.main.ButtonPlay.Opacity = 0;
            _mom.main.ButtonPause.Opacity = 0;
            _mom.main.ButtonStop.Opacity = 0;
            _mom.main.ButtonFullScreen.Opacity = 0;
            _mom.main.SlederVolume.Opacity = 0;
            _mom.main.Menu.Opacity = 0;
        }
    }
}
