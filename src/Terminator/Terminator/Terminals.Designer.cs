using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Terminator.Models;

namespace Terminator
{
    partial class Terminals
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Terminals));
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 539);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Terminator";
            this.ResumeLayout(false);
            this.Padding = new Padding(20);

            int x = 0, y = 10;
            int buttonWidth = 100, buttonHeight = 100;

            foreach(var group in GetConfigGroups())
            {
                // Add group box
                var label = new Label();
                label.Text = group.Name;
                label.Location = new System.Drawing.Point(x, y);
                this.Controls.Add(label);
                y += 30;

                foreach(var launch in group.LaunchConfigs)
                {
                    var b = new Button();
                    b.Location = new System.Drawing.Point(x, y);
                    b.Name = $"button_{group.Name.Replace(" ","")}_{launch.Name.Replace(" ", "")}";
                    b.Text = launch.Name;
                    b.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                    b.Padding = new Padding(1);
                    b.BackColor = Color.FromArgb(210, 41, 61, 71);
                    b.TextAlign = ContentAlignment.BottomCenter;

                    b.BackgroundImage = GetLaunchBgImage(launch.ProgramType);
                    b.BackgroundImageLayout = ImageLayout.Stretch;

                    b.Click += new System.EventHandler(Launch_Click);

                    this.Controls.Add(b);
                    y += buttonHeight + 5;
                }
            }

        }

        private void Launch_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Test.ps1");
            var commandText = $"-noexit {filePath}";
            var process = new Process();           
            var startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "powershell.exe";
            startInfo.Arguments = commandText;
            startInfo.UseShellExecute = true;

            process.StartInfo = startInfo;

            process.Start();


        }

        private Image GetLaunchBgImage(ProgramType type)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            string resourcePrefix = "Terminator.Resources.";
            string filename = "";
            
            switch (type)
            {
                case ProgramType.Powershell:
                    filename = "powershell.png";
                    break;
                case ProgramType.GitBash:
                    filename = "gitbash.png";
                    break;
                case ProgramType.Cmd:
                    filename = "cmd.png";
                    break;
                case ProgramType.VisualStudioCode:
                    filename = "vscode.png";
                    break;

            }

            var imageStream = assembly.GetManifestResourceStream($"{resourcePrefix}{filename}");
            return Image.FromStream(imageStream);
        }

        private IEnumerable<LaunchConfigGroup> GetConfigGroups()
        {
            var groups = new List<LaunchConfigGroup>();

            groups.Add(new LaunchConfigGroup
            {
                Name = "Cinnamon",
                LaunchConfigs = new List<LaunchConfig>
                {
                    new LaunchConfig
                    {
                        Name = "List directory",
                        ProgramType = ProgramType.Powershell,
                        CommandText = "ls"
                    },

                    new LaunchConfig
                    {
                        Name = "List directory",
                        ProgramType = ProgramType.GitBash,
                        CommandText = "ls"
                    },

                    new LaunchConfig
                    {
                        Name = "List directory",
                        ProgramType = ProgramType.Cmd,
                        CommandText = "dir"
                    },

                    new LaunchConfig
                    {
                        Name = "Launch window",
                        ProgramType = ProgramType.VisualStudioCode,
                        CommandText = @"C:\Users\Justin\Code\Terminator"
                    }
                }
            });

            return groups;
        }

        #endregion
    }
}

