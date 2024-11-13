using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DawnWallpaper
{
    public partial class WallpaperForm : Form
    {

        private string[]? audioFiles;
        private int currentIndex = 0;
        private WaveOutEvent? waveOut;
        private AudioFileReader? audioFileReader;

        public event Action<string>? TextChangedEvent;

        public WallpaperForm()
        {
            InitializeComponent();
            InitializePreviewForm();
        }

        private void WallpaperForm_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.enableContextMenu = false;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.settings.mute = !ControlForm.bgm;
            if (ControlForm.sound) PlayNextAudio();
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

        private void PlayNextAudio()
        {
            if (audioFiles.Length == 0)
                return;

            string currentFile = audioFiles[currentIndex];
            audioFileReader = new AudioFileReader(currentFile);

            waveOut.Init(audioFileReader);
            waveOut.Play();

            string updateAudio = "(" + currentIndex + "/" + audioFiles.Length + ")";
            TextChangedEvent?.Invoke(updateAudio);
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

        private void WallpaperForm_FormClosing(object sender, FormClosingEventArgs e)
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
