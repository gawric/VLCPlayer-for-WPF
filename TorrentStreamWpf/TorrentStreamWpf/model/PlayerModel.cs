using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrentStreamWpf.model
{
    public class PlayerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string infoText { get; set; }
        private string sliderValue { get; set; }
        private string sliderVolumeValue { get; set; }

        public string InfoText
        {
            get { return infoText; }
            set
            {
                if (infoText != value)
                {
                    infoText = value;
                    OnPropertyChange("InfoText");

                }
            }
        }

        public string SliderVolumeValue
        {
            get { return sliderVolumeValue; }
            set
            {
                if (sliderVolumeValue != value)
                {
                    sliderVolumeValue = value;
                    OnPropertyChange("SliderVolumeValue");

                }
            }
        }

        public string SliderValue
        {
            get { return sliderValue; }
            set
            {

                if (sliderValue != value)
                {
                    sliderValue = value;
                    OnPropertyChange("SliderValue");

                }
            }
        }

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
