﻿using System;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public class MainForm : Form
    {
        public MainForm()
        {
            // Create a button
            var button = new Button
            {
                Text = "Click Me!",
                Location = new System.Drawing.Point(100, 50),
            };
            

            // Wire up the button's click event
            button.Click += (sender, e) =>
            {
                // Show a message box when the button is clicked
                MessageBox.Show("Hello, Mono Windows Forms!");
            };

            // Add the button to the form
            Controls.Add(button);

            // Set the form properties
            Text = "Mono Windows Forms App";
            Size = new System.Drawing.Size(300, 150);
            StartPosition = FormStartPosition.CenterScreen;
        }
 
        [STAThread]
        public static void Main(string[] args)
        {
            // Create and run the Windows Forms application
            Application.Run(new MainForm());
        }
    }
} //6.0 2.7 5.1 1.6