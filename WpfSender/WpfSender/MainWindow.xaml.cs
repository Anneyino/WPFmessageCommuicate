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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSender
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int WM_COPYDATA = 0x004A;

        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("User32.dll")]
         private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            IntPtr WINDOW_HANDLER = FindWindow(null, "接收窗口");
            if (WINDOW_HANDLER != IntPtr.Zero)
            {
                string text = this.ContentBox.Text;
                byte[] sarr = System.Text.Encoding.Default.GetBytes(text);
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = text;
                cds.cbData = len + 1;
                SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cds);
            }
            else
            {
                MessageBox.Show("未找到目标程序");
            }
        }
    }

    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

}
