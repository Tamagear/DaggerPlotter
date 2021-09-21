using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DaggerPlotter
{
    /// <summary>
    /// Interaktionslogik für NewWizardWindow.xaml
    /// </summary>
    public partial class NewWizardWindow : Window
    {
        public ProjectSettings Settings { get => settings; }

        private ProjectSettings settings;
        public NewWizardWindow()
        {
            InitializeComponent();

            settings = new ProjectSettings();
            SetSavePathText(TextBox_ProjectNameTextBox.Text);
        }

        private void SetSavePathText(string projectName)
        {
            if (TextBox_SavePathTextBox != null)
                TextBox_SavePathTextBox.Text = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\DaggerPlotter-Projekte\\{projectName}";
        }

        private void TextBox_ProjectNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetSavePathText(TextBox_ProjectNameTextBox.Text);
        }

        private void Wizard_Finish(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            settings.projectName = TextBox_ProjectNameTextBox.Text;
            settings.projectPath = TextBox_SavePathTextBox.Text;
        }
    }

    public struct ProjectSettings
    {
        public string projectName;
        public string projectPath;        
    }
}
