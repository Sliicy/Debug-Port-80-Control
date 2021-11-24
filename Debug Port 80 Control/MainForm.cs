using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using OpenLibSys;

namespace Debug_Port_80_Control {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        // OpenLibSystem object used for read/writing to debug port:
        Ols ols;

        private int cachedVolumeLevel = 0;

        private bool IsAdmin()
        {
            OperatingSystem osInfo = Environment.OSVersion;
            if (osInfo.Platform == PlatformID.Win32Windows)
            {
                return true;
            }
            else
            {
                WindowsIdentity usrId = WindowsIdentity.GetCurrent();
                WindowsPrincipal p = new WindowsPrincipal(usrId);
                return p.IsInRole(@"BUILTIN\Administrators");
            }
        }

        private bool RunAsRestart()
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string s in args)
            {
                if (s.Equals("runas"))
                {
                    return false;
                }
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            startInfo.Arguments = "runas";

            try
            {
                Process.Start(startInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            if (!IsAdmin())
            {
                RunAsRestart();
                Environment.Exit(0);
            }
            Init();
        }

        private void Init()
        {
            try
            {
                ols = new Ols();

                // Check support library statuses:
                switch (ols.GetStatus())
                {
                    case (uint)Ols.Status.NO_ERROR:
                        break;
                    case (uint)Ols.Status.DLL_NOT_FOUND:
                        MessageBox.Show("Status Error!! DLL_NOT_FOUND");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                        MessageBox.Show("Status Error!! DLL_INCORRECT_VERSION");
                        break;
                    case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                        MessageBox.Show("Status Error!! DLL_INITIALIZE_ERROR");
                        break;
                }

                // Check WinRing0 status:
                switch (ols.GetDllStatus())
                {
                    case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                        MessageBox.Show("DLL Status Error!! OLS_DRIVER_NOT_LOADED");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                        MessageBox.Show("DLL Status Error!! OLS_UNSUPPORTED_PLATFORM");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                        MessageBox.Show("DLL Status Error!! OLS_DLL_DRIVER_NOT_FOUND");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                        MessageBox.Show("DLL Status Error!! OLS_DLL_DRIVER_UNLOADED");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                        MessageBox.Show("DLL Status Error!! DRIVER_NOT_LOADED_ON_NETWORK");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                        MessageBox.Show("DLL Status Error!! OLS_DLL_UNKNOWN_ERROR");
                        Environment.Exit(0);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Environment.Exit(0);
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            ols.WriteIoPortWord(0x80, ushort.Parse(txtInput.Text));
        }

        private void tmrNumberLoop_Tick(object sender, EventArgs e)
        {
            ols.WriteIoPortWord(0x80, 0x01);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x12);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x23);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x34);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x45);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x56);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x67);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x78);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x89);
            System.Threading.Thread.Sleep(700);
            ols.WriteIoPortWord(0x80, 0x90);
            System.Threading.Thread.Sleep(700);
        }

        // https://stackoverflow.com/questions/2534595/get-master-sound-volume-in-c-sharp
        private int GetCurrentSpeakerVolume()
        {
            int volume = 0;
            var enumerator = new MMDeviceEnumerator();
            IEnumerable<MMDevice> speakDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToArray();
            if (speakDevices.Count() > 0)
            {
                MMDevice mMDevice = speakDevices.ToList()[0];
                volume = Convert.ToInt16(mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            }
            return volume;
        }

        private void TmrVolume_Tick(object sender, EventArgs e)
        {
            if (cachedVolumeLevel == GetCurrentSpeakerVolume())
            {
                return;
            }
            int tens = GetCurrentSpeakerVolume() / 10;
            int ones = GetCurrentSpeakerVolume() % 10;

            switch (tens)
            {
                case 0:
                    ols.WriteIoPortWord(0x80, (ushort)(ones));
                    break;
                case 1:
                    ols.WriteIoPortWord(0x80, (ushort)(0x10 + ones));
                    break;
                case 2:
                    ols.WriteIoPortWord(0x80, (ushort)(0x20 + ones));
                    break;
                case 3:
                    ols.WriteIoPortWord(0x80, (ushort)(0x30 + ones));
                    break;
                case 4:
                    ols.WriteIoPortWord(0x80, (ushort)(0x40 + ones));
                    break;
                case 5:
                    ols.WriteIoPortWord(0x80, (ushort)(0x50 + ones));
                    break;
                case 6:
                    ols.WriteIoPortWord(0x80, (ushort)(0x60 + ones));
                    break;
                case 7:
                    ols.WriteIoPortWord(0x80, (ushort)(0x70 + ones));
                    break;
                case 8:
                    ols.WriteIoPortWord(0x80, (ushort)(0x80 + ones));
                    break;
                case 9:
                    ols.WriteIoPortWord(0x80, (ushort)(0x90 + ones));
                    break;
                default:
                    ols.WriteIoPortWord(0x80, 0xFF);
                    break;
            }
            cachedVolumeLevel = GetCurrentSpeakerVolume();
        }

        private void CboOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOption.Text == "Volume")
            {
                tmrVolume.Start();
            } else
            {
                tmrVolume.Stop();
            }
        }
    }
}