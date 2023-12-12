using OpenCvSharp;
using OpenCvSharp.Extensions;
using Compunet.YoloV8;
using System.Diagnostics;
using Compunet.YoloV8.Data;
using System.Runtime.InteropServices;

namespace DetectHub
{
    public partial class MainHub : Form
    {
        private VideoCapture capture;
        private YoloV8 predictor;
        private int buttonSSCounter = 1;
        private string? modelPath;
        private double confidence = .35;
        private int msCounter = 0;
        private int boxCount;
        private MemoryStream ms = new();
        private Stopwatch stopwatch = new();

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadCursorFromFile(string fileName);

        public MainHub()
        {
            InitializeComponent();
            try
            {
                capture = new VideoCapture(0, VideoCaptureAPIs.IMAGES);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot access the camera", "Access error");
            }
            capture.FrameWidth = 640;
            capture.FrameHeight = 480;

            ListWebcams();
            SetCursor();

            Application.Idle += CaptureFrame;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
        }

        private void SSButton_Click(object sender, EventArgs e)
        {
            if (modelPath != null)
            {
                buttonSSCounter++;
                confidenseTrackbar.Enabled = false;
                webcamCombobox.Enabled = false;
                modelOpenButton.Enabled = false;
                modelOpenButton.BackColor = Color.FromArgb(99, 163, 194);
            }
            else
            {
                MessageBox.Show("You need to load the .onnx model to run it.", "Launch error");
            }
            if (buttonSSCounter % 2 == 1)
            {
                startStopButton.BackgroundImage = Properties.Resources.start;
                confidenseTrackbar.Enabled = true;
                webcamCombobox.Enabled = true;
                modelOpenButton.Enabled = true;
                modelOpenButton.BackColor = Color.FromArgb(99, 163, 194);
            }
            else
            {
                startStopButton.BackgroundImage = Properties.Resources.stop;
            }
        }

        private void ModelOpenbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                modelPath = openFileDialog.FileName;
                try
                {
                    predictor = new YoloV8(modelPath);
                    predictor.Parameters.Confidence = (float)confidence;
                    modelNameLabel.Text = Path.GetFileName(modelPath);
                }
                catch (Exception)
                {
                    MessageBox.Show("The .onnx model is incorrect.", "Loading error");
                    modelPath = null;
                    modelNameLabel.Text = "Model not loaded";
                }
            }
        }
        private void ScreenshotButton_Click(object sender, EventArgs e)
        {
            string outimg_path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string screenshot_fileName = "screenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            if (!Directory.Exists($"{outimg_path}\\DetectHub"))
            {
                Directory.CreateDirectory($"{outimg_path}\\DetectHub");
            }
            imageOutput.Image.Save($"{outimg_path}\\DetectHub\\{screenshot_fileName}", System.Drawing.Imaging.ImageFormat.Png);
        }
        
        private void SetCursor()
        {
            IntPtr customCursor = LoadCursorFromFile("C:\\Windows\\Cursors\\aero_link.cur");
            if (customCursor != IntPtr.Zero)
            {
                foreach (Control control in Controls)
                {
                    if (control is Button)
                    {
                        ((Button)control).Cursor = new Cursor(customCursor);
                    }
                }
            }
            else
            {
                foreach (Control control in Controls)
                {
                    if (control is Button)
                    {
                        ((Button)control).Cursor = Cursors.Hand;
                    }
                }
            }
        }


        private void StartCursorEnter(object sender, EventArgs e)
        {
            if (buttonSSCounter % 2 == 1)
            {
                startStopButton.BackgroundImage = Properties.Resources.start_hover;
            }
            else
            {
                startStopButton.BackgroundImage = Properties.Resources.stop_hover;
            }
        }

        private void StartCursorLeave(object sender, EventArgs e)
        {
            if (buttonSSCounter % 2 == 1)
            {
                startStopButton.BackgroundImage = Properties.Resources.start;
            }
            else
            {
                startStopButton.BackgroundImage = Properties.Resources.stop;
            }
        }

        static void PlotOneBox(int[] x, Mat img, string label, int line_thickness = 2)
        {
            int tl = line_thickness == 0 ? (int)(.002 * (img.Height + img.Width) / 2 + 1) : line_thickness;
            OpenCvSharp.Point c1 = new(x[0], x[1]);
            OpenCvSharp.Point c2 = new(x[2], x[3]);
            Cv2.Rectangle(img, c1, c2, Scalar.Orange, tl, LineTypes.AntiAlias);
            c2 = new OpenCvSharp.Point(c1.X + label.Length * 10.8, c1.Y - 18);
            Cv2.Rectangle(img, new OpenCvSharp.Point(c1.X - 1, c1.Y), c2, Scalar.Orange, -1, LineTypes.AntiAlias);
            Cv2.PutText(img, label, new OpenCvSharp.Point(c1.X + 2, c1.Y - 4), HersheyFonts.HersheySimplex, .6, new Scalar(225, 255, 255), 1, LineTypes.AntiAlias);
        }

        Mat ProcessFrame(Mat img)
        {
            img.ToBitmap().Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            
            var result = predictor.Detect(ms.ToArray());
            for (boxCount = 0; boxCount < result.Boxes.Count; boxCount++)
            {
                IBoundingBox? box = result.Boxes[boxCount];
                int[] x = { box.Bounds.X, box.Bounds.Y, box.Bounds.X + box.Bounds.Width, box.Bounds.Y + box.Bounds.Height };
                PlotOneBox(x, img, $"{box.Class.Name}: {Math.Round(box.Confidence, 2)}");
            }
            objectsLabel.Text = $"Objects: {boxCount}";
            return img;
        }

        private void ListWebcams()
        {
            DirectShowLib.DsDevice[] devices = DirectShowLib.DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice);
            foreach (DirectShowLib.DsDevice device in devices)
            {
                webcamCombobox.Items.Add(device.Name);
            }
            webcamCombobox.SelectedIndex = 0;
        }

        private void WebcamCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                capture.Release();
                capture = new VideoCapture(webcamCombobox.SelectedIndex);
            }

            catch (Exception)
            {
                MessageBox.Show("Cannot access the camera", "Access error");
            }
        }

        private void CaptureFrame(object sender, EventArgs e)
        {
            msCounter++;
            stopwatch.Start();
            Mat frame = capture.RetrieveMat();
            if (buttonSSCounter % 2 == 0)
            {
                imageOutput.Image = BitmapConverter.ToBitmap(ProcessFrame(frame));
            }
            else
            {
                objectsLabel.Text = "Objects: ?";
                imageOutput.Image = BitmapConverter.ToBitmap(frame);
                confidence = (double)confidenseTrackbar.Value / confidenseTrackbar.Maximum;
                confidenceLabel.Text = $"{confidence}";
                if (predictor != null)
                {
                    predictor.Parameters.Confidence = (float)confidence;
                }
            }
            stopwatch.Stop();
            if (msCounter == 9)
            {
                long cycleTime = stopwatch.ElapsedMilliseconds;
                fpsLabel.Text = $"FPS: {Convert.ToString(1000 / (cycleTime / 10))}";
                cycleTimeLabel.Text = $"Cycle: {cycleTime / 10} ms";
                stopwatch.Reset();
                msCounter = 0;
            }
            GC.Collect();
        }
    }
}