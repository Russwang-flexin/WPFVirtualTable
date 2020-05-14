using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace VirtualizingPanel.Wpf
{
    public class ScrollBarManager : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ScrollBarManager()
        {
            visible = Visibility.Visible;
            ScrollValue = 0;
            _portview_size = 0;
            max_value = 100;
            real_page_count = double.NaN;
        }

        public double real_page_count;


        private double _value = 0.0;

        public double ScrollValue
        {
            get { return _value; }
            set
            {
                _value = value; 
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ScrollValue"));
                }
            }
        }

        private double _max_value;

        public double max_value
        {
            get { return _max_value; }
            set
            {
                _max_value = value; if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("max_value"));
                }
            }
        }

        private double _portview_size;

        public double portview_size
        {
            get { return _portview_size; }
            set
            {
                _portview_size = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("portview_size"));
                }
            }
        }


        private Visibility _visible;

        public Visibility visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                if (PropertyChanged != null) 
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("visible"));
                }
            }
        }
    }
}
