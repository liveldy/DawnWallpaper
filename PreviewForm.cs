using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxWMPLib;
using NAudio.Wave;
using Sunny.UI;

namespace DawnWallpaper
{
    public partial class PreviewForm : UIForm
    {
        private string[]? audioFiles;
        private int currentIndex = 0;
        private WaveOutEvent? waveOut;
        private AudioFileReader? audioFileReader;

        public PreviewForm()
        {
            InitializeComponent();
            InitializePreviewForm();
        }

        private void InitializePreviewForm()
        {
            string folderPath = Path.Combine(ControlForm.assetsDirectory, ControlForm.indexName, "audio");
            audioFiles = Directory.GetFiles(folderPath, "*.wav").OrderBy(f => f).ToArray();

            if (ControlForm.sound && audioFiles.Length > 0)
            {
                waveOut = new WaveOutEvent();
                waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
            }
        }

        private void InitializeMediaPlayer()
        {
            this.WindowState = FormWindowState.Maximized;
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.URL = Path.Combine(ControlForm.assetsDirectory, ControlForm.indexName, "video.mp4");
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.settings.mute = !ControlForm.bgm;
        }

        private void PlayNextAudio()
        {
            if (audioFiles.Length == 0)
                return;

            string currentFile = audioFiles[currentIndex];
            audioFileReader = new AudioFileReader(currentFile);

            waveOut.Init(audioFileReader);
            waveOut.Play();
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            currentIndex++;
            if (currentIndex >= audioFiles.Length)
            {
                currentIndex = 0;
            }
            if (waveOut != null && audioFileReader != null) PlayNextAudio();
        }

        private void PreviewW_Load(object sender, EventArgs e)
        {
            InitializeMediaPlayer();
            if (ControlForm.sound) PlayNextAudio();
        }

        private void PreviewW_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }
        }
    }
}
