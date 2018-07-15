using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EmuController
{
	/// <summary>
	/// Summary description for WaitingForm.
	/// </summary>
	public class WaitingForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label MessageLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WaitingForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

    public string Message
    {
      get { return MessageLabel.Text; }
      set { MessageLabel.Text = value; }
    }

    public void ShowWithMessage(string message)
    {
      Message = message;
      Show();
      Refresh();
    }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.MessageLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // MessageLabel
      // 
      this.MessageLabel.Font = new System.Drawing.Font("Arial Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.MessageLabel.ForeColor = System.Drawing.Color.White;
      this.MessageLabel.Location = new System.Drawing.Point(8, 8);
      this.MessageLabel.Name = "MessageLabel";
      this.MessageLabel.Size = new System.Drawing.Size(608, 40);
      this.MessageLabel.TabIndex = 0;
      this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // WaitingForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
      this.ClientSize = new System.Drawing.Size(624, 53);
      this.ControlBox = false;
      this.Controls.Add(this.MessageLabel);
      this.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "WaitingForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.ResumeLayout(false);

    }
		#endregion
	}
}
