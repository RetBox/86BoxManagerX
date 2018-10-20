﻿using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace _86boxManager
{
    static class Program
    {
        public const bool PRERELEASE = false; //Is this a pre-release version?
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Check if 86box.exe is already running
            Process[] pname = Process.GetProcessesByName("86manager");
            if (pname.Length > 1)
            {
                MessageBox.Show("86Box Manager is already running. You can only run one instance at a time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pname = Process.GetProcessesByName("86box");
                if (pname.Length > 0)
                {
                    DialogResult result = MessageBox.Show("At least one instance of 86box is already running. It's not recommended that you run 86Box.exe directly outside of Manager. Do you want to continue at your own risk?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
        }
    }
}
