namespace StopwatchApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    // UI controls
    private Label lblTimer;
    private Button btnStart;
    private Button btnPause;
    private Button btnResume;
    private Button btnReset;
    private Button btnStop;
    private Label lblStatus;
    private System.Windows.Forms.Timer tickTimer;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        // -- Timer display label (the big 00:00:00) --
        lblTimer = new Label();
        lblTimer.Text = "00:00:00";
        lblTimer.Font = new Font("Consolas", 48F, FontStyle.Bold);
        lblTimer.TextAlign = ContentAlignment.MiddleCenter;
        lblTimer.Dock = DockStyle.None;
        lblTimer.Location = new Point(60, 30);
        lblTimer.Size = new Size(460, 80);
        lblTimer.ForeColor = Color.FromArgb(30, 30, 30);

        // -- Start button --
        btnStart = new Button();
        btnStart.Text = "Start";
        btnStart.Location = new Point(30, 140);
        btnStart.Size = new Size(100, 40);
        btnStart.BackColor = Color.FromArgb(76, 175, 80);
        btnStart.ForeColor = Color.White;
        btnStart.FlatStyle = FlatStyle.Flat;
        btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStart.Click += BtnStart_Click;

        // -- Pause button --
        btnPause = new Button();
        btnPause.Text = "Pause";
        btnPause.Location = new Point(140, 140);
        btnPause.Size = new Size(100, 40);
        btnPause.BackColor = Color.FromArgb(255, 193, 7);
        btnPause.ForeColor = Color.White;
        btnPause.FlatStyle = FlatStyle.Flat;
        btnPause.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnPause.Enabled = false;
        btnPause.Click += BtnPause_Click;

        // -- Resume button --
        btnResume = new Button();
        btnResume.Text = "Resume";
        btnResume.Location = new Point(250, 140);
        btnResume.Size = new Size(100, 40);
        btnResume.BackColor = Color.FromArgb(33, 150, 243);
        btnResume.ForeColor = Color.White;
        btnResume.FlatStyle = FlatStyle.Flat;
        btnResume.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnResume.Enabled = false;
        btnResume.Click += BtnResume_Click;

        // -- Reset button --
        btnReset = new Button();
        btnReset.Text = "Reset";
        btnReset.Location = new Point(360, 140);
        btnReset.Size = new Size(100, 40);
        btnReset.BackColor = Color.FromArgb(158, 158, 158);
        btnReset.ForeColor = Color.White;
        btnReset.FlatStyle = FlatStyle.Flat;
        btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnReset.Enabled = false;
        btnReset.Click += BtnReset_Click;

        // -- Stop button --
        btnStop = new Button();
        btnStop.Text = "Stop";
        btnStop.Location = new Point(470, 140);
        btnStop.Size = new Size(100, 40);
        btnStop.BackColor = Color.FromArgb(244, 67, 54);
        btnStop.ForeColor = Color.White;
        btnStop.FlatStyle = FlatStyle.Flat;
        btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStop.Enabled = false;
        btnStop.Click += BtnStop_Click;

        // -- Status label at the bottom --
        lblStatus = new Label();
        lblStatus.Text = "Ready";
        lblStatus.Font = new Font("Segoe UI", 10F);
        lblStatus.Location = new Point(30, 200);
        lblStatus.Size = new Size(540, 30);
        lblStatus.ForeColor = Color.Gray;

        // -- The 1-second interval timer that drives the stopwatch --
        tickTimer = new System.Windows.Forms.Timer(components);
        tickTimer.Interval = 1000; // 1 second
        tickTimer.Tick += TickTimer_Tick;

        // -- Form setup --
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(600, 250);
        Text = "Stopwatch Application";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(245, 245, 245);

        Controls.Add(lblTimer);
        Controls.Add(btnStart);
        Controls.Add(btnPause);
        Controls.Add(btnResume);
        Controls.Add(btnReset);
        Controls.Add(btnStop);
        Controls.Add(lblStatus);
    }

    #endregion
}
