using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernFM
{
    static class MyControls
    {

        public static RoutedCommand SelectAddressBar = new RoutedCommand();
        public static RoutedCommand BackControl = new RoutedCommand();
        public static RoutedCommand BackControlAlternative = new RoutedCommand();
        public static RoutedCommand ForwardControl = new RoutedCommand();

        static MyControls()
        {
            SelectAddressBar.InputGestures.Add((new KeyGesture(Key.D, ModifierKeys.Alt)));

            BackControl.InputGestures.Add((new KeyGesture(Key.Left, ModifierKeys.Alt)));
            BackControlAlternative.InputGestures.Add((new KeyGesture(Key.Back, ModifierKeys.None)));
            ForwardControl.InputGestures.Add((new KeyGesture(Key.Right, ModifierKeys.Alt)));
        }
    }
}
