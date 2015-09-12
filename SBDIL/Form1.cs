using NAudio.CoreAudioApi;
using NAudio.Wave;
using SBDIL.Models;
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

namespace SBDIL
{
    /// <summary>
    /// Scott Martin 6106 S. Arbor Ln.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
            var fileName = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "wav"), DateTime.Now.ToString("yyyyMMddTHHmmss") + ".wav");
            _currentRecording = new Recording(fileName);

            _waveSource = new WaveIn();
            _waveSource.WaveFormat = new WaveFormat(44100, 1);
            _waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            _waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            _waveFile = new WaveFileWriter(_currentRecording.FileName, _waveSource.WaveFormat);
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
                File.Delete(_currentRecording.FileName);
            }
            _waveSource.StopRecording();
            _ruleTimer.Stop();
            _ruleTimer.Dispose();
        }


        /* Event Handlers */

        private void MainForm_Load(object sender, EventArgs e)
        {
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

            //TODO: FIX ME
            _currentOffender = new Offender();
            _currentOffender.Address = "6106 S. Arbor Ln.";
            _currentOffender.Name = "Scott Martin";
            _dataConnection
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
   }


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
                cbToggleRecord.Image = SBDIL.Properties.Resources.button_stop;
                numericThreshold.Enabled = false;
                StartRecording();
            }
            else
            {
                Logger.Info("Log stopped by user.");
                cbToggleRecord.Image = SBDIL.Properties.Resources.button_record;
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
            Logger.Info("Wrote Wav file to disk! Offender is clearly in violation of the statute and you now have proof!");
            _dataConnection.InsertRecording(_currentRecording);
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
                // Stop recording and throw away the current wav file.
                StopRecording(true);
                _currentRecording = null;

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
                _dataConnection.InsertLog(new Log(_currentOffender, Convert.ToDouble(levelDb)));

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



        // TODO: make this configurable through the interface
        /// <summary>
        /// Threshold of UI timer ticks between bark events
        /// </summary>
        private double _maxTicksBetweenBarks = .25 * 4 * 60; // 250 milliseconds * 4 * 60 = 1 minute

        /// <summary>
        /// Store number of UI timer ticks between bark events
        /// </summary>
        private int _ticksBetweenBarks;

        private Recording _currentRecording;
        private Offender _currentOffender;
        private DataConnection _dataConnection = new DataConnection();
        private bool _isRecording;
        private System.Windows.Forms.Timer _uiTimer;
        private System.Timers.Timer _ruleTimer;
        private WaveIn _waveSource;
        private WaveFileWriter _waveFile;
    }
}
