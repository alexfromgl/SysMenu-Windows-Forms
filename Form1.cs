using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SysMenu_Windows_Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 350);
            this.Text = "Right click on the Border";
        }

        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        // ID for the About item on the system menu
        private int SYSMENU1 = 0x1;
        private int SYSMENU2 = 0x2;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);
            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);
            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU1, "&First");
            AppendMenu(hSysMenu, MF_STRING, SYSMENU2, "&Second");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU1))
            {
                MessageBox.Show("First Command", "First Command", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU2))
            {
                MessageBox.Show("Second Command", "Second Command", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
