using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Simple_Desktop_Pet
{

	public partial class Form1 : Form
	{
		private int nOldWndLeft;
		private int nOldWndTop;
		private int nClickX;
		private int nClickY;
		private bool isDragging;

		Random rng = new Random();
		int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
		int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;
		private int goalPosLeft;
		private int goalPosTop;

		private int ImageIdx;
		private int AnimationIdx;
		const int frameInterval = 333;
		const int moveInterval = 3;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			isDragging = false;

			ImageIdx = 1;
			switchImage();

			AnimationIdx = 0;

			goalPosLeft = rng.Next() % ScreenWidth;
			goalPosTop = rng.Next() % ScreenHeight;

			timer1.Interval = frameInterval;
			timer1.Start();

			timer2.Interval = moveInterval;
			timer2.Start();

			toolTip1.ShowAlways = true;
			toolTip1.ToolTipTitle = ""; // NULL TITLE
			toolTip1.SetToolTip(pictureBox1, "Test_Message");

			notifyIcon1.Text = "Text_Message";
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			nOldWndLeft = this.Left;
			nOldWndTop = this.Top;
			nClickX = e.X;
			nClickY = e.Y;
			isDragging = true;
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDragging)
			{
				this.Left = e.X + nOldWndLeft - nClickX;
				this.Top = e.Y + nOldWndTop - nClickY;
				nOldWndLeft = this.Left;
				nOldWndTop = this.Top;
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			isDragging = false;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ImageIdx = 1;
			switchImage();
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			ImageIdx = 2;
			switchImage();
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			ImageIdx = 3;
			switchImage();
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			ImageIdx = 4;
			switchImage();
		}

		private void animationTestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ImageIdx = 5;
		}
	
		private void switchImage()
		{
			switch (ImageIdx)
			{
				case 1: pictureBox1.Image = Properties.Resources.IMAGE_1; break;
				case 2: pictureBox1.Image = Properties.Resources.IMAGE_2; break;
				case 3: pictureBox1.Image = Properties.Resources.IMAGE_3; break;
				case 4: pictureBox1.Image = Properties.Resources.IMAGE_4; break;
				default: break;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			AnimationIdx++;
			AnimationIdx = AnimationIdx % 2;
			if( ImageIdx == 5)
			{
				switch (AnimationIdx)
				{
					case 0: pictureBox1.Image = Properties.Resources.IMAGE_3; break;
					case 1: pictureBox1.Image = Properties.Resources.IMAGE_4; break;
					default: break;
				}
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			randomMove();
		}

		private void randomMove()
		{
			if (!isDragging)
			{
				String Message;
				Message = "Top:" + Convert.ToString(this.Top) + "\n"
						+ "Left:" + Convert.ToString(this.Left) + "\n"
						+ "GTop:" + Convert.ToString(goalPosTop) + "\n"
						+ "GLeft:" + Convert.ToString(goalPosLeft);

				// Message Update
				toolTip1.SetToolTip(pictureBox1, Message);
				notifyIcon1.Text = Message; // Can't Over 64 byte

				// Console Message
				Console.WriteLine("CurrentTop: "+Convert.ToString(this.Top));
				Console.WriteLine("CurrentLeft: "+Convert.ToString(this.Left));
				Console.WriteLine("Goal Top: "+Convert.ToString(goalPosTop));
				Console.WriteLine("Goal Left: "+Convert.ToString(goalPosLeft));

				if (this.Left == goalPosLeft && this.Top == goalPosTop)
				{
					goalPosLeft = rng.Next() % ScreenWidth;
					goalPosTop = rng.Next() % ScreenHeight;
				}

				if (this.Left < goalPosLeft) this.Left++;
				else if (this.Left > goalPosLeft) this.Left--;

				if (this.Top < goalPosTop) this.Top++;
				else if (this.Top > goalPosTop) this.Top--;
			}
		}	}


}
