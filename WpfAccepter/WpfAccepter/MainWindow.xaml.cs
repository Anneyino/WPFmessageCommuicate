using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAccepter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        #region 定义常量消息值
        public const int WM_GETTEXT = 0x0D;
        public const int WM_SETTEXT = 0x0C;
        public const int WM_SIZEING = 0x0214;
        public const int WM_COPYDATA = 0x004A;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        #endregion

        #region 定义结构体
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource hWndSource;
            WindowInteropHelper wih = new WindowInteropHelper(this);
            hWndSource = HwndSource.FromHwnd(wih.Handle);
            //添加处理程序 
            hWndSource.AddHook(MainWindowProc);
        }

        private IntPtr MainWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {

                case WM_COPYDATA:
                    {

                        COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                        Type mytype = mystr.GetType();

                        COPYDATASTRUCT MyKeyboardHookStruct = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT));
                        this.ContentBox.Text = MyKeyboardHookStruct.lpData;
                        break;

                    }
                default:
                    {
                        break;
                    }


            }
            return IntPtr.Zero;
        }

    }
}
