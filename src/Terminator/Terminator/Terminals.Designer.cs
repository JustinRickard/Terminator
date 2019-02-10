﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Terminator.Models;
using Terminator.Utils;

namespace Terminator
{
    partial class Terminals
    {
        private Config _config;
        private Dictionary<string, LaunchConfig> _buttonNameLaunchConfigMappings;
        private ComboBox _groupDropdown;
        private Label _groupTitle;
        private List<Button> _launchButtons;

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
            this.Name = "Terminals";
            this.Text = "Terminator";
            this.ResumeLayout(false);
            this.Padding = new Padding(20);
            this.Width = 300;

            _config = new ConfigService().GetConfig();
            _buttonNameLaunchConfigMappings = new Dictionary<string, LaunchConfig>();

            var group = _config.LaunchConfigGroups.FirstOrDefault();

            int x = 5, y = 10;
            int buttonWidth = 100, buttonHeight = 100;

            var _groupDropdown = new ComboBox();
            _groupDropdown.Height = 40;
            _groupDropdown.Name = "dd_group";
            _groupDropdown.DisplayMember = "Name";
            _groupDropdown.ValueMember = "Name";
            foreach(var groupToAdd in _config.LaunchConfigGroups)
            {
                _groupDropdown.Items.Add(groupToAdd);
            }
            _groupDropdown.SelectedValue = _config.LaunchConfigGroups.FirstOrDefault();
            _groupDropdown.SelectedIndexChanged += new EventHandler(SelectGroup_Click);

            y += 30;

            this.Controls.Add(_groupDropdown);


            // Add group box
            _groupTitle = new Label();
            _groupTitle.Text = group.Name;
            _groupTitle.Location = new System.Drawing.Point(x, y);
            _groupTitle.Font = new Font("Eras Demi ITC", 16, FontStyle.Bold);
            _groupTitle.ForeColor = Color.FromArgb(0, 145, 12, 12);
            _groupTitle.Width = 300;
            this.Controls.Add(_groupTitle);
            y += 30;

            _launchButtons = new List<Button>();

            foreach (var launch in group.LaunchConfigs)
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

                _buttonNameLaunchConfigMappings.Add(b.Name, launch);

                b.Click += GetEventHandler(launch.ProgramType);

                _launchButtons.Add(b);
                y += buttonHeight + 5;
            }

            this.Controls.AddRange(_launchButtons.ToArray());
        }

        private EventHandler GetEventHandler(ProgramType type)
        {
            switch(type)
            {
                case ProgramType.Powershell:
                    return new EventHandler(LaunchPowershell_Click);
                default: return null;
            }
            
        }

        private void SelectGroup_Click(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            var selectedGroup = (LaunchConfigGroup)combo.SelectedItem;

            UpdateItemsForGroup(selectedGroup);
        }

        private void UpdateItemsForGroup(LaunchConfigGroup group)
        {
            _groupTitle.Text = group.Name;
        }

        private void LaunchPowershell_Click(object sender, EventArgs e)
        {         
            var button = (Button)sender;
            var launchConfig = _buttonNameLaunchConfigMappings[button.Name];

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Execute.ps1");
            var directory = $"{_config.BaseDirectory}\\{launchConfig.Path}";
            var command = $"'cd {directory}'";
            var arguments = $"-noexit {filePath} -cmd {command}";
            var process = new Process();           
            var startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "powershell.exe";
            startInfo.Arguments = arguments;
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
                        Path = "ls"
                    },

                    new LaunchConfig
                    {
                        Name = "List directory",
                        ProgramType = ProgramType.GitBash,
                        Path = "ls"
                    },

                    new LaunchConfig
                    {
                        Name = "List directory",
                        ProgramType = ProgramType.Cmd,
                        Path = "dir"
                    },

                    new LaunchConfig
                    {
                        Name = "Launch window",
                        ProgramType = ProgramType.VisualStudioCode,
                        Path = @"C:\Users\Justin\Code\Terminator"
                    }
                }
            });

            return groups;
        }

        #endregion
    }
}

