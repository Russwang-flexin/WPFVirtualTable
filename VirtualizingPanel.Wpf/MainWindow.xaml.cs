using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VirtualizingPanel.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScrollBar sb = new ScrollBar();
        public ScrollBarManager manager = new ScrollBarManager();
        DockPanel dock = new DockPanel();
        private WrapPanel _breif_list_box = new WrapPanel()
        {
            Orientation = Orientation.Vertical,
        };
        double minHeight = 20;
        List<int> list = new List<int>();
        List<Button> buttons = new List<Button>();
        private void StartUIFeed()
        {
            for (int i = 0; i < 200; i++)
            {
                list.Add(i);
            }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }



        public MainWindow()
        {
            sb.SmallChange = 1;
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            StartUIFeed();
            manager.max_value = list.Count;
            dock.Children.Add(sb);
            dock.Children.Add(_breif_list_box);
            DockPanel.SetDock(sb, Dock.Right);
            DockPanel.SetDock(_breif_list_box, Dock.Left);
            init_bar();
            Content = dock;
            _breif_list_box.SizeChanged += (sender, e) =>
              {
                  manager.portview_size = (sender as WrapPanel).ActualHeight / minHeight - 1;
                  for (int i = 0; i < manager.portview_size; i++)
                  {
                      var data = list[i];
                      Button btn = new Button() { Width = 30, Height = minHeight, Content = i.ToString() };
                      _breif_list_box.Children.Add(btn);
                      buttons.Add(btn);
                  }
              };
        }

        private int firstOrderIndex;

        public int FirstOrderIndex
        {
            get { return firstOrderIndex; }
            set 
            {
                firstOrderIndex = Math.Max(0, Math.Min(value, (int)manager.max_value));
                UpdateUI(firstOrderIndex);
            }
        }

        private void UpdateUI(int firstOrderIndex)
        {
            for (int index = 0; index < manager.portview_size; index++)
            {
                if (index >= buttons.Count) return;
                if (index + firstOrderIndex < list.Count) 
                {
                    buttons[index].Content = list[index + firstOrderIndex].ToString();
                }
            }
        }

        private void init_bar()
        {
            sb.DataContext = manager;
            sb.Orientation = Orientation.Vertical;
            sb.SmallChange = 1;
            var bi = new Binding("visible");
            sb.SetBinding(VisibilityProperty, bi);
            bi = new Binding("ScrollValue");
            sb.SetBinding(VisibilityProperty, bi);
            bi = new Binding("portview_size");
            sb.SetBinding(ScrollBar.ViewportSizeProperty, bi);
            bi = new Binding("max_value");
            sb.SetBinding(RangeBase.MaximumProperty, bi);
            sb.ValueChanged += (sender, e) =>
            {
                FirstOrderIndex = (int)e.NewValue;
            };

            PreviewMouseWheel += (sender, e) => 
            {
                on_preview_mouse_wheel(sender, e);
            };
            set_scrollbar_maxvalue(list.Count);
        }

        private void set_scrollbar_maxvalue(int count)
        {
        }

        private void on_preview_mouse_wheel(object sender, MouseWheelEventArgs e)
        {
        }
    }
}
