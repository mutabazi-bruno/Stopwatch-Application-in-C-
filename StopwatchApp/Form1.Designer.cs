namespace StopwatchApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Microsoft.Web.WebView2.WinForms.WebView2 webView;
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
        webView = new Microsoft.Web.WebView2.WinForms.WebView2();
        tickTimer = new System.Windows.Forms.Timer(components);

        ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
        SuspendLayout();

        // webView
        webView.Dock = DockStyle.Fill;
        webView.Location = new Point(0, 0);
        webView.Name = "webView";
        webView.Size = new Size(800, 600);
        webView.TabIndex = 0;
        webView.ZoomFactor = 1D;

        // tickTimer
        tickTimer.Interval = 1000;
        tickTimer.Tick += TickTimer_Tick;

        // Form1
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 600);
        Controls.Add(webView);
        Name = "Form1";
        Text = "Premium Stopwatch";
        StartPosition = FormStartPosition.CenterScreen;
        
        ((System.ComponentModel.ISupportInitialize)webView).EndInit();
        ResumeLayout(false);
    }

    #endregion
}
