using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;
using System.Data;
using System.Xml;

using EmuController.Controls;
using EmuController.PodControllers;
using EmuController.PodItems;
using EmuController.Properties;
using EmuController.Utility;

namespace EmuController
{
    public class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Label YearLabel;
        private System.Windows.Forms.Label YearValue;
        private System.Windows.Forms.Label ManufacturerLabel;
        private System.Windows.Forms.Label VideoValue;
        private System.Windows.Forms.Label VideoLabel;
        private System.Windows.Forms.Label ManufacturerValue;
        private System.Windows.Forms.Label InputValue;
        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.Label DescriptionValue;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel Divider;
        private System.Windows.Forms.PictureBox TitlePicture;

        private EmuController.Controls.PodList PodList;
        private SystemController _system;

        private static WaitingForm _waiting;
        private static UserActivityHook _hook;
        private static Image _blankImage;

        static MainForm()
        {
            _waiting = new WaitingForm();
            
            _hook = new UserActivityHook(false, true);
            _hook.KeyPress += new KeyPressEventHandler(Hook_KeyPress);

            _blankImage = new Bitmap(1, 1);
        }

        public MainForm()
        {            
            InitializeComponent();

            PodList = new PodList();
            PodList.Name = "PodList";
            PodList.Location = new Point(321, 8);
            PodList.Size = new Size(471, 634);
            PodList.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            PodList.SelectedIndexChanged += new EventHandler(PodList_SelectedIndexChanged);
            PodList.KeyPress += new KeyPressEventHandler(PodList_KeyPress);
            this.Controls.Add(PodList);

            ShowWaiting("Loading...");

            _system = new SystemController();
            PodList.RootController = _system;

            HideWaiting();
        }

        private static void Hook_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == '`')

        }

        public static void ShowWaiting(string message)
        {
            _waiting.ShowWithMessage(message);
        }

        public static void HideWaiting()
        {
            _waiting.Hide();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.YearLabel = new System.Windows.Forms.Label();
            this.YearValue = new System.Windows.Forms.Label();
            this.ManufacturerValue = new System.Windows.Forms.Label();
            this.ManufacturerLabel = new System.Windows.Forms.Label();
            this.VideoValue = new System.Windows.Forms.Label();
            this.VideoLabel = new System.Windows.Forms.Label();
            this.InputValue = new System.Windows.Forms.Label();
            this.InputLabel = new System.Windows.Forms.Label();
            this.Divider = new System.Windows.Forms.Panel();
            this.TitlePicture = new System.Windows.Forms.PictureBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.DescriptionValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // YearLabel
            // 
            this.YearLabel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YearLabel.ForeColor = System.Drawing.Color.Gray;
            this.YearLabel.Location = new System.Drawing.Point(4, 211);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(128, 23);
            this.YearLabel.TabIndex = 2;
            this.YearLabel.Text = "Year";
            // 
            // YearValue
            // 
            this.YearValue.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YearValue.ForeColor = System.Drawing.Color.LightGray;
            this.YearValue.Location = new System.Drawing.Point(57, 211);
            this.YearValue.Name = "YearValue";
            this.YearValue.Size = new System.Drawing.Size(208, 23);
            this.YearValue.TabIndex = 5;
            // 
            // ManufacturerValue
            // 
            this.ManufacturerValue.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManufacturerValue.ForeColor = System.Drawing.Color.LightGray;
            this.ManufacturerValue.Location = new System.Drawing.Point(69, 234);
            this.ManufacturerValue.Name = "ManufacturerValue";
            this.ManufacturerValue.Size = new System.Drawing.Size(208, 23);
            this.ManufacturerValue.TabIndex = 7;
            // 
            // ManufacturerLabel
            // 
            this.ManufacturerLabel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManufacturerLabel.ForeColor = System.Drawing.Color.Gray;
            this.ManufacturerLabel.Location = new System.Drawing.Point(4, 234);
            this.ManufacturerLabel.Name = "ManufacturerLabel";
            this.ManufacturerLabel.Size = new System.Drawing.Size(148, 23);
            this.ManufacturerLabel.TabIndex = 6;
            this.ManufacturerLabel.Text = "Studio";
            // 
            // VideoValue
            // 
            this.VideoValue.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoValue.ForeColor = System.Drawing.Color.LightGray;
            this.VideoValue.Location = new System.Drawing.Point(69, 280);
            this.VideoValue.Name = "VideoValue";
            this.VideoValue.Size = new System.Drawing.Size(208, 23);
            this.VideoValue.TabIndex = 13;
            // 
            // VideoLabel
            // 
            this.VideoLabel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoLabel.ForeColor = System.Drawing.Color.Gray;
            this.VideoLabel.Location = new System.Drawing.Point(4, 280);
            this.VideoLabel.Name = "VideoLabel";
            this.VideoLabel.Size = new System.Drawing.Size(128, 23);
            this.VideoLabel.TabIndex = 12;
            this.VideoLabel.Text = "Rating";
            // 
            // InputValue
            // 
            this.InputValue.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputValue.ForeColor = System.Drawing.Color.LightGray;
            this.InputValue.Location = new System.Drawing.Point(80, 257);
            this.InputValue.Name = "InputValue";
            this.InputValue.Size = new System.Drawing.Size(208, 23);
            this.InputValue.TabIndex = 15;
            // 
            // InputLabel
            // 
            this.InputLabel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputLabel.ForeColor = System.Drawing.Color.Gray;
            this.InputLabel.Location = new System.Drawing.Point(5, 257);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(128, 23);
            this.InputLabel.TabIndex = 14;
            this.InputLabel.Text = "Players";
            // 
            // Divider
            // 
            this.Divider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Divider.BackColor = System.Drawing.Color.Gray;
            this.Divider.Location = new System.Drawing.Point(313, 8);
            this.Divider.Name = "Divider";
            this.Divider.Size = new System.Drawing.Size(3, 666);
            this.Divider.TabIndex = 16;
            // 
            // TitlePicture
            // 
            this.TitlePicture.Location = new System.Drawing.Point(8, 8);
            this.TitlePicture.Name = "TitlePicture";
            this.TitlePicture.Size = new System.Drawing.Size(300, 200);
            this.TitlePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TitlePicture.TabIndex = 17;
            this.TitlePicture.TabStop = false;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.ForeColor = System.Drawing.Color.Gray;
            this.DescriptionLabel.Location = new System.Drawing.Point(4, 303);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(128, 23);
            this.DescriptionLabel.TabIndex = 19;
            this.DescriptionLabel.Text = "Description";
            // 
            // DescriptionValue
            // 
            this.DescriptionValue.AutoSize = true;
            this.DescriptionValue.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionValue.ForeColor = System.Drawing.Color.LightGray;
            this.DescriptionValue.Location = new System.Drawing.Point(12, 326);
            this.DescriptionValue.MaximumSize = new System.Drawing.Size(300, 0);
            this.DescriptionValue.Name = "DescriptionValue";
            this.DescriptionValue.Size = new System.Drawing.Size(0, 23);
            this.DescriptionValue.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 682);
            this.Controls.Add(this.DescriptionValue);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.TitlePicture);
            this.Controls.Add(this.Divider);
            this.Controls.Add(this.InputValue);
            this.Controls.Add(this.InputLabel);
            this.Controls.Add(this.VideoValue);
            this.Controls.Add(this.VideoLabel);
            this.Controls.Add(this.ManufacturerValue);
            this.Controls.Add(this.ManufacturerLabel);
            this.Controls.Add(this.YearValue);
            this.Controls.Add(this.YearLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emulator Controller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TitlePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Config.Initialize();

            Application.Run(new MainForm());
        }

        private void ReloadGameList()
        {
            ShowWaiting("Refreshing game list...");

            _system.Reload();
            PodList.RootController = _system;
            UpdateSummary();

            HideWaiting();
        }

        private void UpdateSummary()
        {
            //http://thegamesdb.net/api/GetGame.php?name=sonic%20the%20hedgehog&platform=Sega%20Genesis

            Image titleImage = null;
            //Image screenImage = null;
            string year = "";
            string manufacturer = "";
            string input = "";
            string video = "";
            string description = "";

            PodItem item = (PodItem)PodList.SelectedItem;

            if (item != null)
            {
                if (item is GameItem)
                {
                    XmlNode game = ((GameItem)item).Game;
                    GameSystem system = ((GameItem)item).GameSystem;

                    if (game.ParentNode.LocalName == "clones")
                        game = game.ParentNode.ParentNode;

                    string name = XmlBinder.Eval(game, "@name");
                    string cleanName = XmlBinder.Eval(game, "CleanName");
                    string baseDir = SystemHelper.GetDirectoryForGameSystem(system);

                    try
                    {
                        string filename = SystemHelper.GetImageFilename(baseDir + "images\\", cleanName + "_front");
                        if (filename != null)
                            titleImage = Image.FromFile(filename);
                    } 
                    catch { }

                    //try
                    //{
                    //    string filename = SystemHelper.GetImageFilename(baseDir + "images\\", cleanName + "_fan");
                    //    if (filename != null)
                    //        screenImage = Image.FromFile(filename);
                    //} 
                    //catch { }

                    year = XmlBinder.Eval(game, "ReleaseDate");
                    manufacturer = XmlBinder.Eval(game, "Publisher");
                    input = XmlBinder.Eval(game, "Players");
                    video = XmlBinder.Eval(game, "ESRB");
                    description = XmlBinder.Eval(game, "Overview");
                    //string players = XmlBinder.Eval(game, "input/@players");
                    //string buttons = XmlBinder.Eval(game, "input/@buttons");
                    //if (players.Length > 0 && buttons.Length > 0)
                    //    input = string.Format("{0}x {1} buttons", players, buttons);

                    //string width = XmlBinder.Eval(game, "video/@width");
                    //string height = XmlBinder.Eval(game, "video/@height");
                    //if (width.Length > 0 && buttons.Length > 0)
                    //    video = string.Format("{0}x{1}", width, height);
                }
                else if (item is SimpleItem)
                {
                    try
                    {
                        string icon = ((SimpleItem)item).Icon;
                        if (icon != null)
                            titleImage = (Image)Resources.ResourceManager.GetObject(icon);
                    }
                    catch { }
                }
            }

            TitlePicture.Image = (titleImage == null) ? _blankImage : titleImage;
            //PlayPicture.Image = (screenImage == null) ? _blankImage : screenImage;

            YearValue.Text = year;
            YearLabel.Visible = (YearValue.Text.Length > 0);
            ManufacturerValue.Text = manufacturer;
            ManufacturerLabel.Visible = (ManufacturerValue.Text.Length > 0);
            InputValue.Text = input;
            InputLabel.Visible = (InputValue.Text.Length > 0);
            VideoValue.Text = video;
            VideoLabel.Visible = (VideoValue.Text.Length > 0);
            DescriptionValue.Text = description;
            DescriptionLabel.Visible = (DescriptionValue.Text.Length > 0);
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            ReloadGameList();
        }

        private void MainForm_Activated(object sender, System.EventArgs e)
        {
            Cursor.Position = new Point(Width, Height);

            PodList.Focus();
        }

        private void PodList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateSummary();
        }

        private void PodList_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)27:
                    e.Handled = true;
                    Application.Exit();
                    break;
            }
        }
    }
}
