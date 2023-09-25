using OpenCvSharp;
using OpenCvSharp.Extensions;
using Compunet.YoloV8;
using System.Diagnostics;
using Compunet.YoloV8.Data;

namespace DetectHub
{
    public partial class MainHub : Form
    {
        PictureBox image_output = new();
        public Bitmap outimg_bitmap;
        private VideoCapture capture;
        private YoloV8 predictor;
        public byte[] data;
        private int button_start_stop_counter = 1;
        public Button button_start_stop = new();
        public Button button_model_open = new();
        public Button button_screenshot = new();
        public OpenFileDialog openFileDialog1 = new();
        public string model_path;
        public TrackBar trackBar1 = new();
        public double confidence = .35;
        public Label confidence_label = new();
        public string[] webcams;
        public ComboBox webcam_combo = new();
        public Label name_model_label = new();
        public Label fps_counter = new();
        public Label cycle_time = new();
        public Label object_counter = new();
        public int objects_on_frame;
        public long[] ms_cycles = new long[10];
        public int ms_counter = 0;

        private void button_start_stop_Click(object sender, EventArgs e)
        {
            if (model_path != null)
            {
                button_start_stop_counter++;
                trackBar1.Enabled = false;
                webcam_combo.Enabled = false;
                button_model_open.Enabled = false;
                button_model_open.BackgroundImage = Properties.Resources.open1;
            }
            else
            {   
                MessageBox.Show("Необходимо загрузить .onnx модель для запуска.", "Ошибка запуска");
            }
            if (button_start_stop_counter % 2 == 1)
            {
                button_start_stop.BackgroundImage = Properties.Resources.start;
                trackBar1.Enabled = true;
                webcam_combo.Enabled = true;
                button_model_open.Enabled = true;
                button_model_open.BackgroundImage = Properties.Resources.open;
            }
            else
            {
                button_start_stop.BackgroundImage = Properties.Resources.stop;
            }
        }
        private void button_model_open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                model_path = openFileDialog1.FileName;
                try
                {
                    predictor = new YoloV8(model_path);
                    predictor.Parameters.Confidence = (float)confidence;
                    name_model_label.Text = Path.GetFileName(model_path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Указанна неверная .onnx модель.", "Ошибка загрузки");
                    model_path = null;
                    name_model_label.Text = "Модель не загружена";
                }
            }
        }
        private void button_screenshot_Click(object sender, EventArgs e)
        {
            string outimg_path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string screenshot_fileName = "screenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            if (!Directory.Exists($"{outimg_path}\\DetectHub"))
            {
                Directory.CreateDirectory($"{outimg_path}\\DetectHub");
            }
            outimg_bitmap.Save($"{outimg_path}\\DetectHub\\{screenshot_fileName}", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void button_screenshot_MouseDown(object sender, MouseEventArgs e)
        {
            button_screenshot.BackgroundImage = Properties.Resources.screenshot1;
        }

        private void button_screenshot_MouseUp(object sender, MouseEventArgs e)
        {
            button_screenshot.BackgroundImage = Properties.Resources.screenshot;
        }

        private void button_model_open_MouseDown(object sender, MouseEventArgs e)
        {
            button_model_open.BackgroundImage = Properties.Resources.open1;
        }

        private void button_model_open_MouseUp(object sender, MouseEventArgs e)
        {
            button_model_open.BackgroundImage = Properties.Resources.open;
        }

        private void button_start_stop_MouseDown(object sender, MouseEventArgs e)
        {
            if (button_start_stop_counter % 2 == 1)
            {
                button_start_stop.BackgroundImage = Properties.Resources.start1;
            }
            else
            {
                button_start_stop.BackgroundImage = Properties.Resources.stop1;
            }
        }

        private void button_start_stop_MouseUp(object sender, MouseEventArgs e)
        {
            if (button_start_stop_counter % 2 == 1)
            {

                button_start_stop.BackgroundImage = Properties.Resources.start;
            }
            else
            {
                button_start_stop.BackgroundImage = Properties.Resources.stop;
            }
        }

        private void cursor_enter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void cursor_leave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        public MainHub()
        {
            InitializeComponent();
            int width_window = 1200;
            int height_window = 800;
            try
            {
                capture = new VideoCapture(0, VideoCaptureAPIs.IMAGES);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удается получить доступ к камере", "Ошибка доступа");
            }
            capture.FrameWidth = 640;
            capture.FrameHeight = 480;

            this.Text = "DetectHub - Распознование объектов";
            this.Icon = Properties.Resources.icon;
            this.Size = new System.Drawing.Size(width_window, height_window);
            this.BackColor = Color.Gray;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(width_window, height_window);
            this.MaximumSize = new System.Drawing.Size(width_window, height_window);

            image_output.Left = (int)Math.Round(width_window * .4);
            image_output.Top = (int)Math.Round(height_window * .15);
            image_output.Width = 400 + 240;
            image_output.Height = 400 + 80;
            this.Controls.Add(image_output);

            button_start_stop.Location = new System.Drawing.Point(740, 630);
            button_start_stop.FlatStyle = FlatStyle.Flat;
            button_start_stop.FlatAppearance.BorderSize = 0;
            button_start_stop.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button_start_stop.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button_start_stop.Width = 126;
            button_start_stop.Height = 50;
            button_start_stop.MouseEnter += cursor_enter;
            button_start_stop.MouseLeave += cursor_leave;
            button_start_stop.Click += new EventHandler(button_start_stop_Click);
            button_start_stop.MouseDown += button_start_stop_MouseDown;
            button_start_stop.MouseUp += button_start_stop_MouseUp;
            button_start_stop.BackgroundImage = Properties.Resources.start;
            button_start_stop.BackgroundImageLayout = ImageLayout.Stretch;
            this.Controls.Add(button_start_stop);

            Label open_label = new();
            open_label.Text = "Загрузить .onnx модель";
            open_label.Location = new System.Drawing.Point(40, 150);
            open_label.Font = new Font("Arial", 12);
            open_label.ForeColor = Color.White;
            open_label.Width = 280;
            open_label.Height = 40;
            open_label.BackColor = ColorTranslator.FromHtml("#7c5bc2");
            this.Controls.Add(open_label);

            Label main_label = new();
            main_label.Text = "DetectHub";
            main_label.Location = new System.Drawing.Point(40, 40);
            main_label.Font = new Font("Arial", 20);
            main_label.ForeColor = Color.White;
            main_label.Width = 250;
            main_label.Height = 60;
            this.Controls.Add(main_label);


            name_model_label.Location = new System.Drawing.Point(40, 190);
            name_model_label.Text = "Модель не загружена";
            name_model_label.Font = new Font("Arial", 11);
            name_model_label.ForeColor = Color.White;
            name_model_label.Width = 280;
            name_model_label.Height = 40;
            name_model_label.BackColor = ColorTranslator.FromHtml("#7c5bc2");
            this.Controls.Add(name_model_label);

            button_model_open.Location = new System.Drawing.Point(320, 150);
            button_model_open.FlatStyle = FlatStyle.Flat;
            button_model_open.FlatAppearance.BorderSize = 0;
            button_model_open.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button_model_open.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button_model_open.Width = 80;
            button_model_open.Height = 30;
            button_model_open.MouseEnter += cursor_enter;
            button_model_open.MouseLeave += cursor_leave;
            button_model_open.Click += new EventHandler(button_model_open_Click);
            button_model_open.MouseDown += button_model_open_MouseDown;
            button_model_open.MouseUp += button_model_open_MouseUp;
            button_model_open.BackgroundImage = Properties.Resources.open;
            button_model_open.BackgroundImageLayout = ImageLayout.Stretch;
            button_model_open.BackColor = ColorTranslator.FromHtml("#7c5bc2");
            this.Controls.Add(button_model_open);

            button_screenshot.Location = new System.Drawing.Point(40, 340);
            button_screenshot.FlatStyle = FlatStyle.Flat;
            button_screenshot.FlatAppearance.BorderSize = 0;
            button_screenshot.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button_screenshot.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button_screenshot.Width = 220;
            button_screenshot.Height = 40;
            button_screenshot.MouseEnter += cursor_enter;
            button_screenshot.MouseLeave += cursor_leave;
            button_screenshot.Click += new EventHandler(button_screenshot_Click);
            button_screenshot.MouseDown += button_screenshot_MouseDown;
            button_screenshot.MouseUp += button_screenshot_MouseUp;
            button_screenshot.BackgroundImage = Properties.Resources.screenshot;
            button_screenshot.BackgroundImageLayout = ImageLayout.Stretch;
            button_screenshot.BackColor = ColorTranslator.FromHtml("#7c5bc2");
            this.Controls.Add(button_screenshot);


            Label taskbar_label = new();
            taskbar_label.Text = "Confidence";
            taskbar_label.Location = new System.Drawing.Point(40, 240);
            taskbar_label.Font = new Font("Arial", 12);
            taskbar_label.ForeColor = Color.White;
            taskbar_label.Width = 150;
            taskbar_label.Height = 40;
            taskbar_label.BackColor = ColorTranslator.FromHtml("#7c5bc2");
            this.Controls.Add(taskbar_label);

            fps_counter.Text = "FPS: ";
            fps_counter.Location = new System.Drawing.Point(40, 440);
            fps_counter.Font = new Font("Arial", 12);
            fps_counter.ForeColor = Color.White;
            fps_counter.Width = 150;
            fps_counter.Height = 40;
            this.Controls.Add(fps_counter);

            cycle_time.Text = "Время цикла: ";
            cycle_time.Location = new System.Drawing.Point(40, 480);
            cycle_time.Font = new Font("Arial", 12);
            cycle_time.ForeColor = Color.White;
            cycle_time.Width = 350;
            cycle_time.Height = 40;
            this.Controls.Add(cycle_time);

            object_counter.Text = "Объектов: ?";
            object_counter.Location = new System.Drawing.Point(40, 520);
            object_counter.Font = new Font("Arial", 12);
            object_counter.ForeColor = Color.White;
            object_counter.Width = 350;
            object_counter.Height = 40;
            this.Controls.Add(object_counter);


            confidence_label.Text = "0.35";
            confidence_label.Location = new System.Drawing.Point(40, 280);
            confidence_label.Font = new Font("Arial", 11);
            confidence_label.ForeColor = Color.White;
            confidence_label.Width = 60;
            confidence_label.Height = 40;
            confidence_label.BackColor = ColorTranslator.FromHtml("#7c5bc2");
            this.Controls.Add(confidence_label);


            trackBar1.Minimum = 5;
            trackBar1.Maximum = 100;
            trackBar1.TickFrequency = 5;
            trackBar1.LargeChange = 10;
            trackBar1.SmallChange = 1;
            trackBar1.Value = 35;
            trackBar1.Location = new System.Drawing.Point(220, 240);
            trackBar1.BackColor = ColorTranslator.FromHtml("#7c5bc2"); trackBar1.ForeColor = Color.White;
            trackBar1.Width = 200;
            this.Controls.Add(trackBar1);
            PictureBox first_element = new();
            first_element.Image = Properties.Resources.first_element;
            first_element.Left = 14;
            first_element.Top = 110;
            first_element.Width = 450;
            first_element.Height = 300;
            first_element.BackgroundImageLayout = ImageLayout.Stretch;
            this.Controls.Add(first_element);

            openFileDialog1.Filter = "ONNX Files (*.onnx)|*.onnx";

            webcam_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            webcam_combo.Location = new System.Drawing.Point(650, 50);
            webcam_combo.Width = 300;
            webcam_combo.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.Controls.Add(webcam_combo);

            ListWebcams();


            Application.Idle += CaptureFrame;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
        }

        private void Button_start_stop_MouseEnter(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void plot_one_box(int[] x, Mat img, string label = null, int line_thickness = 2)
        {
            int tl = line_thickness == 0 ? (int)(.002 * (img.Height + img.Width) / 2 + 1) : line_thickness;
            OpenCvSharp.Point c1 = new(x[0], x[1]);
            OpenCvSharp.Point c2 = new(x[2], x[3]);
            Cv2.Rectangle(img, c1, c2, Scalar.Orange, tl, LineTypes.AntiAlias);
            c2 = new OpenCvSharp.Point(c1.X + label.Length * 10.8, c1.Y - 18);
            Cv2.Rectangle(img, new OpenCvSharp.Point(c1.X - 1, c1.Y), c2, Scalar.Orange, -1, LineTypes.AntiAlias);
            Cv2.PutText(img, label, new OpenCvSharp.Point(c1.X + 2, c1.Y - 4), HersheyFonts.HersheyDuplex, .6, new Scalar(225, 255, 255), 1, LineTypes.AntiAlias);
        }
        Mat ProcessFrame(Mat img)
        {
            Bitmap bitmap = img.ToBitmap();
            MemoryStream ms = new();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            data = ms.ToArray();
            
            var result = predictor.Detect(data);
            int box_count;
            for (box_count = 0; box_count < result.Boxes.Count; box_count++)
            {
                Compunet.YoloV8.Data.IBoundingBox? box = result.Boxes[box_count];
                int[] x = { box.Bounds.X, box.Bounds.Y, box.Bounds.X + box.Bounds.Width, box.Bounds.Y + box.Bounds.Height };
                plot_one_box(x, img, $"{box.Class.Name}: {Math.Round(box.Confidence, 2)}");
            }
            object_counter.Text = $"Объектов: {box_count}";
            return img;
        }
        private void ListWebcams()
        {
            DirectShowLib.DsDevice[] devices = DirectShowLib.DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice);
            foreach (DirectShowLib.DsDevice device in devices)
            {
                webcam_combo.Items.Add(device.Name);
            }
            webcam_combo.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                capture.Release();
                capture = new VideoCapture(webcam_combo.SelectedIndex);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Не удается получить доступ к камере", "Ошибка доступа");
            }
        }
        private void CaptureFrame(object sender, EventArgs e)
        {
            ms_counter++;
            Stopwatch stopwatch = new();
            stopwatch.Start();
            Mat frame = capture.RetrieveMat();
            if (button_start_stop_counter % 2 == 0)
            {
                outimg_bitmap = BitmapConverter.ToBitmap(ProcessFrame(frame));
                image_output.Image = outimg_bitmap;
            }
            else
            {
                object_counter.Text = "Объектов: ?";
                outimg_bitmap = BitmapConverter.ToBitmap(frame);
                image_output.Image = outimg_bitmap;
                confidence = (double)trackBar1.Value / trackBar1.Maximum;
                confidence_label.Text = $"{confidence}";
                if (predictor != null)
                {
                    predictor.Parameters.Confidence = (float)confidence;
                }
            }
            GC.Collect();
            
            stopwatch.Stop();
            ms_cycles[ms_counter] = stopwatch.ElapsedMilliseconds;
            if (ms_counter == 9)
            {
                fps_counter.Text = $"FPS: {Convert.ToString(1000 / (ms_cycles.Sum() / 10))}";
                cycle_time.Text = $"Время цикла: {ms_cycles.Sum() / 10} ms";
                ms_counter = 0;
             }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}