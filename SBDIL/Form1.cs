using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SBDIR
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Logger.OnExceptionLogged += Log_OnExceptionLogged;

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            comboMicrophone.Items.AddRange(devices.ToArray());
            comboMicrophone.SelectedIndex = 0;

            _uiTimer = new System.Windows.Forms.Timer();
            _uiTimer.Interval = (250); // .25 sec
            _uiTimer.Tick += new EventHandler(uiTimer_Tick);
            _uiTimer.Start();

            Idle();

            Logger.Info("Scott's Barking Dog Insanity Logger started!");
        }

        private void Idle()
        {
            _waveSource = new WaveIn();
            _waveSource.WaveFormat = new WaveFormat(44100, 1);
            _waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            _waveSource.StartRecording();
        }

        private void StartNewWaveFile()
        {
            _wavFileName = @"C:\Temp\" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".wav";

            _waveSource = new WaveIn();
            _waveSource.WaveFormat = new WaveFormat(44100, 1);
            _waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            _waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            _waveFile = new WaveFileWriter(_wavFileName, _waveSource.WaveFormat);
            _waveSource.StartRecording();
        }

        private void StartRecording()
        {
            // TODO: Make the ruletimer adjustable from teh UI
            if (_ruleTimer != null) _ruleTimer.Dispose();
            _ruleTimer = new System.Timers.Timer();
            _ruleTimer.Interval = (1000 * 60 * 15); // 1000 milliseconds * 60 * 15 = 15 minutes
            _ruleTimer.Elapsed += ruleTimer_Elapsed;
            _ruleTimer.Start();

            StartNewWaveFile();
        }

        /// <summary>
        /// Stop recording Wav file and dispose of resources
        /// </summary>
        /// <param name="deleteWavFile">Optionally delete the Wav file</param>
        private void StopRecording(bool deleteWavFile = false)
        {
            if (deleteWavFile)
            {
                _waveFile.Close();
                File.Delete(_wavFileName);
            }
            _waveSource.StopRecording();
            _ruleTimer.Stop();
            _ruleTimer.Dispose();
        }


        /* Event Handlers */

        /// <summary>
        /// Toggle record mode!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbToggleRecord_CheckedChanged(object sender, EventArgs e)
        {
            _isRecording = !_isRecording;

            if (_isRecording)
            {
                Logger.Info("Log started with bark threshold: " + numericThreshold.Value + "dB, listening to address: " + perpAddress.Text + ", owner: " + perpName.Text + ".");
                cbToggleRecord.Image = SBDIR.Properties.Resources.button_stop;
                numericThreshold.Enabled = false;
                StartRecording();
            }
            else
            {
                Logger.Info("Log stopped by user.");
                cbToggleRecord.Image = SBDIR.Properties.Resources.button_record;
                numericThreshold.Enabled = true;
                StopRecording();
                Idle();
            }
        }

        /// <summary>
        /// Handler outputs logs to window
        /// </summary>
        /// <param name="args"></param>
        private void Log_OnExceptionLogged(MessageLoggedEventArgs args)
        {
            textboxLog.AppendText(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff") + " - " + args.Message + Environment.NewLine);
        }

        /// <summary>
        /// When time has elapsed, save the wave file and log it! We now have proof that Scott's dogs are in violation of the statute!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ruleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StopRecording();
            Logger.Info("Wrote Wav file to disk! Scott is clearly in violation of the statute and you now have proof!");
            StartRecording();
        }

        /// <summary>
        /// Update UI doo-dads every 250 milliseconds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiTimer_Tick(object sender, EventArgs e)
        {
            if (comboMicrophone.SelectedItem != null)
            {
                var micLevel = (int)(Math.Round(((MMDevice)comboMicrophone.SelectedItem).AudioMeterInformation.MasterPeakValue * 100));
                vuMeter.Level = micLevel;
            }

            _ticksBetweenBarks++;
            // Has enough time elapsed between ticks to start a new wav?
            if (_isRecording && _ticksBetweenBarks >= _maxTicksBetweenBarks)
            {
                //TODO: Log that we ditched a recording on account of bark timeout period elapsed

                // Stop recording and throw away the current wav file.
                StopRecording(true);

                // reset tick counter
                _ticksBetweenBarks = 0;

                // Restart recording
                StartRecording();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            /* Drawing a wave form
             * The basic principle is every time the DataAvailable event fires, calculate the max peaks of the recorded 
             * audio and add that to your waveform display.
             */

            /* Start recording wave using naudio when sound input reachs a certain level
             * in waveIn_DataAvailable you can examine each sample by looking at the bytes in e.Buffer. 
             * (Assuming you recorded in 16 bit, each pair of bytes is one sample - use BitConverter.ToInt16). 
             * If any sample goes above the threshold you specified then you can write with writer.WriteData.
             * To switch off recording, you would probably want to check that a certain number of 'silent' samples had passed.
             */

            var timestamp = DateTime.Now;
            var levelDb = Convert.ToDecimal(Utilities.MeasureBufferDb(e.Buffer));

            if (_isRecording && _waveFile != null)
            {
                _waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                _waveFile.Flush();
            }

            if (levelDb != 0 && levelDb >= numericThreshold.Value)
            {
                Logger.Info("Heard bark (>-" + numericThreshold.Value + "dB)! Actual level: -" + levelDb + "dB");
                _dataConnection.InsertLog(perpName.Text);

                if (_isRecording)
                {
                    // reset the rule timer
                    _ruleTimer.Stop();
                    _ruleTimer.Start();
                }
            }
        }

        /// <summary>
        /// Handler to dispose of the wavesource and wavefile cleanly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (_waveSource != null)
            {
                _waveSource.Dispose();
                _waveSource = null;
            }
            if (_waveFile != null)
            {
                _waveFile.Dispose();
                _waveFile = null;
            }
        }

        private string _wavFileName;



        // TODO: make this configurable through the interface
        /// <summary>
        /// Threshold of UI timer ticks between bark events
        /// </summary>
        private double _maxTicksBetweenBarks = .25 * 4 * 60; // 250 milliseconds * 4 * 60 = 1 minute

        /// <summary>
        /// Store number of UI timer ticks between bark events
        /// </summary>
        private int _ticksBetweenBarks;

        private DataConnection _dataConnection;
        private bool _isRecording;
        private System.Windows.Forms.Timer _uiTimer;
        private System.Timers.Timer _ruleTimer;
        private WaveIn _waveSource;
        private WaveFileWriter _waveFile;
    }
}
