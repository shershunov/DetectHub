namespace DetectHub
{
    partial class MainHub
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webcamCombobox = new ComboBox();
            modelNameLabel = new Label();
            fpsLabel = new Label();
            cycleTimeLabel = new Label();
            objectsLabel = new Label();
            confidenceLabel = new Label();
            confidenseTrackbar = new TrackBar();
            startStopButton = new Button();
            modelOpenButton = new Button();
            screenshotButton = new Button();
            openFileDialog = new OpenFileDialog();
            openLabel = new Label();
            taskbarLabel = new Label();
            firstElement = new PictureBox();
            titleLabel = new Label();
            imageOutput = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)confidenseTrackbar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)firstElement).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageOutput).BeginInit();
            SuspendLayout();
            // 
            // webcamCombobox
            // 
            webcamCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            webcamCombobox.Location = new Point(660, 36);
            webcamCombobox.Name = "webcamCombobox";
            webcamCombobox.Size = new Size(300, 33);
            webcamCombobox.TabIndex = 3;
            webcamCombobox.SelectedIndexChanged += WebcamCombobox_SelectedIndexChanged;
            // 
            // modelNameLabel
            // 
            modelNameLabel.BackColor = Color.FromArgb(45, 45, 50);
            modelNameLabel.Font = new Font("Dubai", 10F);
            modelNameLabel.ForeColor = Color.FromArgb(243, 243, 247);
            modelNameLabel.Location = new Point(36, 170);
            modelNameLabel.Name = "modelNameLabel";
            modelNameLabel.Size = new Size(280, 40);
            modelNameLabel.TabIndex = 10;
            modelNameLabel.Text = "The model is not loaded";
            // 
            // fpsLabel
            // 
            fpsLabel.Font = new Font("Dubai", 10F);
            fpsLabel.ForeColor = Color.FromArgb(243, 243, 247);
            fpsLabel.Location = new Point(36, 420);
            fpsLabel.Name = "fpsLabel";
            fpsLabel.Size = new Size(150, 40);
            fpsLabel.TabIndex = 6;
            fpsLabel.Text = "FPS: ";
            // 
            // cycleTimeLabel
            // 
            cycleTimeLabel.Font = new Font("Dubai", 10F);
            cycleTimeLabel.ForeColor = Color.FromArgb(243, 243, 247);
            cycleTimeLabel.Location = new Point(36, 460);
            cycleTimeLabel.Name = "cycleTimeLabel";
            cycleTimeLabel.Size = new Size(350, 40);
            cycleTimeLabel.TabIndex = 5;
            cycleTimeLabel.Text = "Cycle: ";
            // 
            // objectsLabel
            // 
            objectsLabel.Font = new Font("Dubai", 10F);
            objectsLabel.ForeColor = Color.FromArgb(243, 243, 247);
            objectsLabel.Location = new Point(36, 500);
            objectsLabel.Name = "objectsLabel";
            objectsLabel.Size = new Size(350, 40);
            objectsLabel.TabIndex = 4;
            objectsLabel.Text = "Objects: ?";
            // 
            // confidenceLabel
            // 
            confidenceLabel.BackColor = Color.FromArgb(45, 45, 50);
            confidenceLabel.Font = new Font("Dubai", 10F);
            confidenceLabel.ForeColor = Color.FromArgb(243, 243, 247);
            confidenceLabel.Location = new Point(36, 260);
            confidenceLabel.Name = "confidenceLabel";
            confidenceLabel.Size = new Size(60, 40);
            confidenceLabel.TabIndex = 1;
            confidenceLabel.Text = "0.35";
            // 
            // confidenseTrackbar
            // 
            confidenseTrackbar.BackColor = Color.FromArgb(45, 45, 50);
            confidenseTrackbar.LargeChange = 10;
            confidenseTrackbar.Location = new Point(154, 224);
            confidenseTrackbar.Maximum = 100;
            confidenseTrackbar.Minimum = 5;
            confidenseTrackbar.Name = "confidenseTrackbar";
            confidenseTrackbar.Size = new Size(200, 69);
            confidenseTrackbar.TabIndex = 0;
            confidenseTrackbar.TickFrequency = 5;
            confidenseTrackbar.Value = 35;
            // 
            // startStopButton
            // 
            startStopButton.BackgroundImage = Properties.Resources.start;
            startStopButton.BackgroundImageLayout = ImageLayout.Stretch;
            startStopButton.FlatAppearance.BorderSize = 0;
            startStopButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            startStopButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            startStopButton.FlatStyle = FlatStyle.Flat;
            startStopButton.Location = new Point(753, 590);
            startStopButton.Name = "startStopButton";
            startStopButton.Size = new Size(126, 50);
            startStopButton.TabIndex = 13;
            startStopButton.Click += SSButton_Click;
            startStopButton.MouseEnter += StartCursorEnter;
            startStopButton.MouseLeave += StartCursorLeave;
            // 
            // modelOpenButton
            // 
            modelOpenButton.BackColor = Color.FromArgb(0, 154, 232);
            modelOpenButton.BackgroundImageLayout = ImageLayout.None;
            modelOpenButton.FlatAppearance.BorderSize = 0;
            modelOpenButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 188, 255);
            modelOpenButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 188, 255);
            modelOpenButton.FlatStyle = FlatStyle.Flat;
            modelOpenButton.Font = new Font("Dubai", 8F);
            modelOpenButton.ForeColor = Color.FromArgb(243, 243, 247);
            modelOpenButton.Location = new Point(253, 134);
            modelOpenButton.Name = "modelOpenButton";
            modelOpenButton.Size = new Size(80, 30);
            modelOpenButton.TabIndex = 9;
            modelOpenButton.Text = "OPEN";
            modelOpenButton.UseVisualStyleBackColor = false;
            modelOpenButton.Click += ModelOpenbutton_Click;
            // 
            // screenshotButton
            // 
            screenshotButton.BackColor = Color.FromArgb(0, 154, 232);
            screenshotButton.BackgroundImageLayout = ImageLayout.None;
            screenshotButton.FlatAppearance.BorderSize = 0;
            screenshotButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 188, 255);
            screenshotButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 188, 255);
            screenshotButton.FlatStyle = FlatStyle.Flat;
            screenshotButton.Font = new Font("Dubai", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            screenshotButton.ForeColor = Color.FromArgb(243, 243, 247);
            screenshotButton.Location = new Point(36, 320);
            screenshotButton.Name = "screenshotButton";
            screenshotButton.Size = new Size(220, 40);
            screenshotButton.TabIndex = 8;
            screenshotButton.Text = "SCREENSHOT";
            screenshotButton.UseVisualStyleBackColor = false;
            screenshotButton.Click += ScreenshotButton_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "ONNX Files (*.onnx)|*.onnx";
            // 
            // openLabel
            // 
            openLabel.BackColor = Color.FromArgb(45, 45, 50);
            openLabel.Font = new Font("Dubai", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            openLabel.ForeColor = Color.FromArgb(243, 243, 247);
            openLabel.Location = new Point(36, 130);
            openLabel.Name = "openLabel";
            openLabel.Size = new Size(280, 40);
            openLabel.TabIndex = 12;
            openLabel.Text = "Load .onnx model";
            // 
            // taskbarLabel
            // 
            taskbarLabel.BackColor = Color.FromArgb(45, 45, 50);
            taskbarLabel.Font = new Font("Dubai", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taskbarLabel.ForeColor = Color.FromArgb(243, 243, 247);
            taskbarLabel.Location = new Point(36, 220);
            taskbarLabel.Name = "taskbarLabel";
            taskbarLabel.Size = new Size(150, 40);
            taskbarLabel.TabIndex = 7;
            taskbarLabel.Text = "Confidence";
            // 
            // firstElement
            // 
            firstElement.BackgroundImageLayout = ImageLayout.Stretch;
            firstElement.Image = Properties.Resources.first_element;
            firstElement.Location = new Point(10, 90);
            firstElement.Name = "firstElement";
            firstElement.Size = new Size(450, 300);
            firstElement.TabIndex = 2;
            firstElement.TabStop = false;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Dubai", 23.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.FromArgb(243, 243, 247);
            titleLabel.Location = new Point(10, 9);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(250, 60);
            titleLabel.TabIndex = 11;
            titleLabel.Text = "DetectHub";
            // 
            // imageOutput
            // 
            imageOutput.Location = new Point(496, 90);
            imageOutput.Name = "imageOutput";
            imageOutput.Size = new Size(640, 480);
            imageOutput.SizeMode = PictureBoxSizeMode.StretchImage;
            imageOutput.TabIndex = 14;
            imageOutput.TabStop = false;
            // 
            // MainHub
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(35, 35, 40);
            ClientSize = new Size(1178, 664);
            Controls.Add(imageOutput);
            Controls.Add(confidenseTrackbar);
            Controls.Add(confidenceLabel);
            Controls.Add(webcamCombobox);
            Controls.Add(objectsLabel);
            Controls.Add(cycleTimeLabel);
            Controls.Add(fpsLabel);
            Controls.Add(taskbarLabel);
            Controls.Add(screenshotButton);
            Controls.Add(modelOpenButton);
            Controls.Add(modelNameLabel);
            Controls.Add(titleLabel);
            Controls.Add(openLabel);
            Controls.Add(startStopButton);
            Controls.Add(firstElement);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = Properties.Resources.icon;
            MaximizeBox = false;
            MaximumSize = new Size(1200, 720);
            MinimumSize = new Size(1200, 720);
            Name = "MainHub";
            Text = "DetectHub - Object detection";
            ((System.ComponentModel.ISupportInitialize)confidenseTrackbar).EndInit();
            ((System.ComponentModel.ISupportInitialize)firstElement).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageOutput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label openLabel;
        private System.Windows.Forms.PictureBox firstElement;
        private System.Windows.Forms.Label taskbarLabel;
        private System.Windows.Forms.Label modelNameLabel;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.Label cycleTimeLabel;
        private System.Windows.Forms.Label objectsLabel;
        private System.Windows.Forms.Label confidenceLabel;
        private System.Windows.Forms.ComboBox webcamCombobox;
        private System.Windows.Forms.TrackBar confidenseTrackbar;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Button modelOpenButton;
        private System.Windows.Forms.Button screenshotButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        #endregion

        private PictureBox imageOutput;
    }
}