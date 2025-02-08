using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShapefilePreview
{
    public partial class Form1 : Form
    {
        private TableLayoutPanel tableLayoutPanel;
        private TextBox textBox1;
        private Button button1;
        private RichTextBox richTextBox1;
        private OpenFileDialog openFileDialog1;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            ClientSize = new Size(800, 450);
            Name = "Form1";
            Text = "Shapefile Preview";
            ResumeLayout(false);
        }

        private void InitializeCustomComponents()
        {
            // TableLayoutPanel
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Padding = new Padding(10);

            // TextBox
            textBox1 = new TextBox();
            textBox1.Dock = DockStyle.Fill;
            textBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // Button
            button1 = new Button();
            button1.Text = "Durchsuchen";
            button1.AutoSize = true;
            button1.Height = 30; // Feste Höhe
            button1.Width = 100; // Feste Breite
            button1.Click += new EventHandler(Button1_Click);

            // RichTextBox
            richTextBox1 = new RichTextBox();
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.MinimumSize = new Size(0, 80); // Mindesthöhe von 80 Pixeln

            // OpenFileDialog
            openFileDialog1 = new OpenFileDialog();

            // Add controls to the TableLayoutPanel
            tableLayoutPanel.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel.Controls.Add(button1, 1, 0);
            tableLayoutPanel.SetColumnSpan(richTextBox1, 2);
            tableLayoutPanel.Controls.Add(richTextBox1, 0, 1);

            // Add TableLayoutPanel to the form
            Controls.Add(tableLayoutPanel);

            // Resize event handler
            this.Resize += new EventHandler(Form1_Resize);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UpdateTextBox(openFileDialog1.FileName);
                string layerInfo = Helpers.ReadAndDisplayLayerInfo(openFileDialog1.FileName);
                richTextBox1.Text = layerInfo;
            }
        }


        private void UpdateTextBox(string filePath)
        {
            textBox1.Text = filePath;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Anpassen der TableLayoutPanel-Breite bei Formulargrößenänderung
            tableLayoutPanel.Width = this.ClientSize.Width - 20; // 20 für Padding
        }
    }
}
