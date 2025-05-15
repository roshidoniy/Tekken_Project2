using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Tekken_Project2
{
    public partial class Form1 : Form
    {
        private List<Button> gameButtons;
        private List<int> buttonNumbers;
        private Button firstClicked;
        private Button secondClicked;
        private Timer resetDelayTimer;
        private Button restartButton;
        private Random rand;
        private Label matchLabel;
        private int matchedCards = 0;

        public Form1()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            // Clear current controls and game state
            this.Controls.Clear();
            firstClicked = null;
            secondClicked = null;
            matchedCards = 0;

            // Initialize list and random generator
            gameButtons = new List<Button>();
            buttonNumbers = new List<int>();
            rand = new Random();

            // Create pairs of numbers from 1 to 8
            for (int i = 1; i <= 8; i++)
            {
                buttonNumbers.Add(i);
                buttonNumbers.Add(i);
            }
            // Shuffle the numbers
            buttonNumbers = buttonNumbers.OrderBy(x => rand.Next()).ToList();

            // Create a 4x4 grid of buttons
            int gridRows = 4, gridCols = 4;
            int buttonSize = 80, spacing = 10;
            int startX = 10, startY = 10;
            for (int r = 0; r < gridRows; r++)
            {
                for (int c = 0; c < gridCols; c++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(buttonSize, buttonSize);
                    btn.Location = new Point(startX + c * (buttonSize + spacing), startY + r * (buttonSize + spacing));
                    btn.BackColor = Color.Black;
                    btn.ForeColor = Color.Black;
                    btn.Tag = buttonNumbers[r * gridCols + c];
                    btn.Click += GameButton_Click;
                    gameButtons.Add(btn);
                    this.Controls.Add(btn);
                }
            }

            // Create matchLabel to display match text above the restart button
            matchLabel = new Label();
            matchLabel.Text = "Matches: 0";
            matchLabel.Size = new Size(200, 30);
            matchLabel.Location = new Point(startX, restartButton.Location.Y - matchLabel.Size.Height - spacing);
            this.Controls.Add(matchLabel);

            // Create Restart button with red background
            restartButton = new Button();
            restartButton.Text = "Restart";
            restartButton.BackColor = Color.Red;
            restartButton.ForeColor = Color.White;
            restartButton.Size = new Size(80, 30);
            restartButton.Location = new Point(startX, startY + gridRows * (buttonSize + spacing) + spacing);
            restartButton.Click += (s, e) => StartNewGame();
            this.Controls.Add(restartButton);

            // Initialize timer for resetting non-matching buttons
            resetDelayTimer = new Timer();
            resetDelayTimer.Interval = 700;
            resetDelayTimer.Tick += ResetDelayTimer_Tick;
        }

        private void GameButton_Click(object sender, EventArgs e)
        {
            // Ignore if timer in progress
            if (resetDelayTimer.Enabled) return;

            Button button = sender as Button;
            if (button == null || button.Text != "") return; // already revealed

            // Reveal the number and change color to yellow
            button.Text = button.Tag.ToString();
            button.BackColor = Color.Yellow;
            button.ForeColor = Color.Black;

            if (firstClicked == null)
            {
                firstClicked = button;
                return;
            }
            else
            {
                secondClicked = button;
                if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
                {
                    // A match: disable both buttons
                    firstClicked.Enabled = false;
                    secondClicked.Enabled = false;
                    matchedCards += 2; // increment matched cards count by 2
                    matchLabel.Text = "Matches: " + matchedCards;
                    firstClicked = null;
                    secondClicked = null;
                }
                else
                {
                    // Not a match: start timer to flip them back
                    resetDelayTimer.Start();
                }
            }
        }

        private void ResetDelayTimer_Tick(object sender, EventArgs e)
        {
            resetDelayTimer.Stop();
            if (firstClicked != null)
            {
                firstClicked.Text = "";
                firstClicked.BackColor = Color.Black;
                firstClicked.ForeColor = Color.Black;
            }
            if (secondClicked != null)
            {
                secondClicked.Text = "";
                secondClicked.BackColor = Color.Black;
                secondClicked.ForeColor = Color.Black;
            }
            matchLabel.Text = "Matches: " + matchedCards;
            firstClicked = null;
            secondClicked = null;
        }
    }
}
