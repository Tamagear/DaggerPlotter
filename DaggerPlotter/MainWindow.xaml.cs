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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Diagnostics;
using System.Windows.Media;
using System.IO.Compression;

namespace DaggerPlotter
{
    public partial class MainWindow : Window
    {
        private string currentWorkingPath = string.Empty;
        private List<Quest> quests = new List<Quest>();
        private List<QuestButton> questButtons = new List<QuestButton>();
        private Dictionary<QuestButton, Button> graphButtons = new Dictionary<QuestButton, Button>();
        private Dictionary<Button, double[]> graphButtonPositions = new Dictionary<Button, double[]>();
        private List<Dialog> dialogs = new List<Dialog>();
        private List<DialogButton> dialogButtons = new List<DialogButton>();

        private bool graphButtonisMoving;
        private Point? graphButtonPosition;

        private const string VERSION_NUMBER = "0.0.3";
        private const string PROJECT_SETTINGS = "\\projectsettings.set";
        private const string PROJECT_SETTINGS_INITIAL_TEXT = "DaggerPlotter Settings (c) Tim B., 2021*";

        public MainWindow()
        {
            InitializeComponent();

            Text_VersionText.Text = $"Version {VERSION_NUMBER}";
        }

        /// <summary>
        /// Aktiviert die Haupt-Interface-Knöpfe
        /// </summary>
        private void EnableMainButtons()
        {
            Button_Main_NewQuest.IsEnabled = true;
            ///Button_Main_NewDialogue.IsEnabled = true;
        }

        /// <summary>
        /// Deaktiviert die Haupt-Interface-Knöpfe.
        /// </summary>
        private void DisableMainButtons()
        {
            Button_Main_NewQuest.IsEnabled = false;
            Button_Main_NewDialogue.IsEnabled = false;
        }

        /// <summary>
        /// Deaktiviert alle Untermenüs der Toolbar.
        /// </summary>
        private void DisableAllSubmenus()
        {
            Button_Data_New.Visibility = Visibility.Hidden;
            Button_Data_Close.Visibility = Visibility.Hidden;
            Button_Data_Open.Visibility = Visibility.Hidden;
            Button_Data_ShowInExplorer.Visibility = Visibility.Hidden;
            Button_Data_Save.Visibility = Visibility.Hidden;
            Button_Data_SaveAs.Visibility = Visibility.Hidden;
            Button_Data_Import.Visibility = Visibility.Hidden;
            Button_Data_Export.Visibility = Visibility.Hidden;
            Button_Data_Quit.Visibility = Visibility.Hidden;

            Button_Edit_GoBack.Visibility = Visibility.Hidden;
            Button_Edit_GoForward.Visibility = Visibility.Hidden;
            Button_Edit_Cut.Visibility = Visibility.Hidden;
            Button_Edit_Copy.Visibility = Visibility.Hidden;
            Button_Edit_Paste.Visibility = Visibility.Hidden;
            Button_Edit_Search.Visibility = Visibility.Hidden;
            Button_Edit_Mark.Visibility = Visibility.Hidden;

            Button_Help_Information.Visibility = Visibility.Hidden;
            Button_Help_VersionInformation.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Erstellt ein neues Projekt. Alte Daten werden überschrieben.
        /// </summary>
        /// <param name="settings">Die Einstellungen für das neue Projekt.</param>
        private void CreateNewProject(ProjectSettings settings)
        {
            string topLevelPath = Directory.GetParent(settings.projectPath).FullName;
            if (!Directory.Exists(topLevelPath))            
                Directory.CreateDirectory(topLevelPath);

            if (!Directory.Exists(settings.projectPath))
                Directory.CreateDirectory(settings.projectPath);

            string projectSettingsPath = settings.projectPath + PROJECT_SETTINGS;

            File.WriteAllText(projectSettingsPath, PROJECT_SETTINGS_INITIAL_TEXT, Encoding.GetEncoding(1252));

            MessageBox.Show($"Erfolgreich {settings.projectName} erstellt!", "Erfolgreich Erstellt");

            OpenProject(settings.projectPath);
        }

        /// <summary>
        /// Öffnet ein Projekt am angegebenen Pfad.
        /// </summary>
        /// <param name="projectPath">Der Pfad zum Verzeichnis, in dem das Projekt liegt.</param>
        private void OpenProject(string projectPath)
        {
            if (!string.IsNullOrEmpty(currentWorkingPath))
                CloseCurrentProject();

            if (!Directory.Exists(projectPath))
            {
                MessageBox.Show("Das angegebene Projekt konnte nicht gefunden werden.", "Fehler");
            }
            else if (!File.Exists(projectPath + PROJECT_SETTINGS))
            {
                MessageBox.Show("Bei dem ausgewählten Pfad handelt es sich nicht um ein valides DaggerPlotter-Projekt.");
            }
            else
            {
                currentWorkingPath = projectPath;
                Text_PathText.Text = projectPath;
                EnableMainButtons();

                ScanProjectFolder();
            }
        }

        /// <summary>
        /// Schließt das aktuelle Projekt.
        /// </summary>
        private void CloseCurrentProject()
        {
            currentWorkingPath = string.Empty;
            Text_PathText.Text = string.Empty;
            DisableMainButtons();
            DeleteAllQuestButtons();
            DeleteAllDialogButtons();
        }

        /// <summary>
        /// Speichert das komplette Projekt.
        /// </summary>
        public void Save()
        {
            //Quests
            for(int i=0; i<quests.Count; i++)
            {
                Quest source = quests[i];
                string targetPath = currentWorkingPath + $"\\{source.questID}.vqtl";
                if (File.Exists(targetPath))
                {
                    File.WriteAllText(targetPath, source.ToVQTL(), Encoding.GetEncoding(1252));
                    OverrideQuestInList(source);
                }
                else
                    MessageBox.Show("Es wurde versucht, eine nicht-existente Datei zu speichern. Es wird empfohlen, die Software neuzustarten.", "Fehler");
            }

            string setData = PROJECT_SETTINGS_INITIAL_TEXT;
            //Graph Button Positions
            for (int i = 0; i < questButtons.Count; i++)
            {
                QuestButton btn = questButtons[i];
                if (graphButtons.TryGetValue(btn, out Button target) && graphButtonPositions.TryGetValue(target, out double[] pos))
                    setData += $"<QUEST>{btn.Quest.questID}<X>{pos[0]}</X><Y>{pos[1]}</Y></QUEST>";
            }

            File.WriteAllText(currentWorkingPath + PROJECT_SETTINGS, setData);
            MessageBox.Show("Gespeichert.", "Erfolgreiches Speichern");
        }

        private void Save(string path)
        {
            Save();
        }

        /// <summary>
        /// Erstellt eine neue VQTL-Datei im Stammverzeichnis.
        /// </summary>
        /// <param name="source">Die Quest, deren Daten in VQTL übertragen werden</param>
        private void CreateVQTL(Quest source)
        {
            if (source == null)
            {
                MessageBox.Show("Die Quest, die erstellt werden sollte, ist null.", "Fehler");
                return;
            }

            string targetPath = currentWorkingPath + $"\\{source.questID}.vqtl";
            if (File.Exists(targetPath))
            {
                MessageBox.Show("Es wurde versucht, eine bereits existierende VQTL-Datei neu zu erzeugen.", "Fehler");
            }
            else
            {
                File.WriteAllText(targetPath, source.ToVQTL(), Encoding.GetEncoding(1252));
                quests.Add(source);
                CreateQuestButton(source, out QuestButton button);

                MessageBox.Show("Erfolgreich Quest erstellt. Eine VQTL-Datei wurde im Projektverzeichnis erstellt.", "Erfolg");
            }
        }

        /// <summary>
        /// Erstellt eine neue VDTL-Datei im Stammverzeichnis.
        /// </summary>
        /// <param name="source">Der Dialog, dessen Daten in VDTL übertragen werden</param>
        private void CreateVDTL(Dialog source)
        {
            if (source == null)
            {
                MessageBox.Show("Der Dialog, der erstellt werden sollte, ist null.", "Fehler");
                return;
            }

            string targetPath = currentWorkingPath + $"\\{source.dialogID}.vdtl";
            if (File.Exists(targetPath))
            {
                MessageBox.Show("Es wurde versucht, eine bereits existierende VDTL-Datei neu zu erzeugen.", "Fehler");
            }
            else
            {
                File.WriteAllText(targetPath, source.ToVDTL(), Encoding.GetEncoding(1252));
                dialogs.Add(source);
                CreateDialogButton(source, out DialogButton button);

                MessageBox.Show("Erfolgreich Dialog erstellt. Eine VDTL-Datei wurde im Projektverzeichnis erstellt.", "Erfolg");
            }
        }

        /// <summary>
        /// Aktualisiert die Quest in der internen Liste.
        /// </summary>
        public void OverrideQuestInList(Quest _quest)
        {
            foreach(Quest quest in quests)
            {
                if (quest.questID.Equals(_quest.questID))
                {
                    quests.Remove(quest);
                    break;
                }
            }

            quests.Add(_quest);            
        }

        /// <summary>
        /// Aktualisiert den Dialog in der internen Liste.
        /// </summary>
        public void OverrideDialogInList(Dialog _dialog)
        {
            foreach (Dialog dialog in dialogs)
            {
                if (dialog.dialogID.Equals(_dialog.dialogID))
                {
                    dialogs.Remove(dialog);
                    break;
                }
            }

            dialogs.Add(_dialog);
        }

        /// <summary>
        /// Überprüft das Stammverzeichnis auf noch nicht regristrierte Quests und Dialoge.
        /// </summary>
        private void ScanProjectFolder()
        {
            //Quests
            string[] files = Directory.GetFiles(currentWorkingPath, "*.vqtl");
            foreach(string file in files)
            {
                bool alreadyRegistered = false;
                foreach (Quest quest in quests)
                {
                    if (quest.questID.Equals(file.Replace(".vqtl", string.Empty)))
                    {
                        alreadyRegistered = true;
                        break;
                    }
                }

                if (!alreadyRegistered)
                {
                    Quest targetQuest = new Quest(File.ReadAllText(file, Encoding.GetEncoding(1252)));
                    quests.Add(targetQuest);
                    CreateQuestButton(targetQuest, out QuestButton button);

                }
            }

            //QuestButtonPositions
            string[] set = File.ReadAllText(currentWorkingPath + PROJECT_SETTINGS).Split(new string[] { "<QUEST>", "</QUEST>" }, StringSplitOptions.None);
            for (int i=0; i<set.Length; i++)
            {
                string questID = set[i].Split(new string[] { "<X>" }, StringSplitOptions.None)[0];
                foreach(QuestButton questButton in questButtons)
                {
                    if (questButton.Quest.questID.Equals(questID) && graphButtons.TryGetValue(questButton, out Button button))
                    {
                        if (!double.TryParse(set[i].Split(new string[] { "<X>", "</X>" }, StringSplitOptions.None)[1], out double x) ||
                            !double.TryParse(set[i].Split(new string[] { "<Y>", "</Y>" }, StringSplitOptions.None)[1], out double y))
                        {
                            MessageBox.Show("Parsen fehlgeschlagen");
                            continue;
                        }
                        else
                        {
                            button.RenderTransform = new TranslateTransform(x, y);
                            break;
                        }
                    }
                }
            }

            //Dialoge
            files = Directory.GetFiles(currentWorkingPath, "*.vdtl");
            foreach (string file in files)
            {
                bool alreadyRegistered = false;
                foreach (Dialog dialog in dialogs)
                {
                    if (dialog.dialogID.Equals(file.Replace(".vdtl", string.Empty)))
                    {
                        alreadyRegistered = true;
                        break;
                    }
                }

                if (!alreadyRegistered)
                {
                    Dialog targetDialog = new Dialog(File.ReadAllText(file, Encoding.GetEncoding(1252)));
                    dialogs.Add(targetDialog);
                    CreateDialogButton(targetDialog, out DialogButton button);
                }
            }

            DeleteDuplicateButtons();
            SetQuestButtonPositions();
        }

        /// <summary>
        /// Setzt die Positionen der Knopfübersicht links auf die korrekten Koordinaten.
        /// </summary>
        private void SetQuestButtonPositions()
        {
            for (int i = questButtons.Count - 1; i >= 0; i--)
            {
                QuestButton button = questButtons[i];
                button.Margin = new Thickness(0f, 0f, 0f, i * 25f);
            }
        }

        /// <summary>
        /// Setzt die Positionen der Knopfübersicht links auf die korrekten Koordinaten.
        /// </summary>
        private void SetDialogButtonPositions()
        {
            for (int i = dialogButtons.Count - 1; i >= 0; i--)
            {
                DialogButton button = dialogButtons[i];
                button.Margin = new Thickness(110f, 0f, 0f, i * 25f);
            }
        }

        /// <summary>
        /// Erstellt und linkt einen Knopf, um bereits erstellte Quests zu bearbeiten.
        /// </summary>
        private void CreateQuestButton(Quest source, out QuestButton instance)
        {
            instance = new QuestButton(source, this);            
            instance.Height = 25;
            instance.Width = 100;
            instance.VerticalAlignment = VerticalAlignment.Bottom;
            instance.HorizontalAlignment = HorizontalAlignment.Left;
            questButtons.Add(instance);
            SetQuestButtonPositions();
            Grid.Children.Add(instance);
            CreateGraphButton(instance, 0f, 0f);            
        }

        /// <summary>
        /// Erstellt und linkt einen Knopf, um bereits erstellte Dialoge zu bearbeiten.
        /// </summary>
        private void CreateDialogButton(Dialog source, out DialogButton instance)
        {
            instance = new DialogButton(source, this);
            instance.Height = 25;
            instance.Width = 100;
            instance.VerticalAlignment = VerticalAlignment.Bottom;
            instance.HorizontalAlignment = HorizontalAlignment.Left;
            dialogButtons.Add(instance);
            SetDialogButtonPositions();
            Grid.Children.Add(instance);
        }

        /// <summary>
        /// Erstellt und linkt einen Knopf, der im Spannungsgraph herumgeschoben werden kann.
        /// </summary>
        private void CreateGraphButton(QuestButton origin, float _x, float _y)
        {
            Button instance = new Button();
            instance.Height = 25;
            instance.Width = 100;
            instance.VerticalAlignment = VerticalAlignment.Bottom;
            instance.HorizontalAlignment = HorizontalAlignment.Right;
            instance.Margin = new Thickness(_x, _y, 0f, 0f);
            instance.PreviewMouseDown += GraphButton_PreviewMouseDown;
            instance.PreviewMouseUp += GraphButton_PreviewMouseUp;
            instance.PreviewMouseMove += GraphButton_PreviewMouseMove;
            Grid.Children.Add(instance);
            graphButtons.Add(origin, instance);

            QuestButton_OnChanged(origin, origin);
            origin.OnChanged += QuestButton_OnChanged;            
        }
        
        /// <summary>
        /// Ein Callback, was aufgerufen wird, wenn sich der Inhalt eines QuestButtons verändert.
        /// </summary>
        private void QuestButton_OnChanged(object sender, QuestButton _button)
        {
            if (graphButtons.TryGetValue(_button, out Button button))
            {
                button.Content = _button.Content;
                button.Background = _button.Background;
            }
        }

        /// <summary>
        /// Löscht doppelte Buttons.
        /// </summary>
        private void DeleteDuplicateButtons()
        {
            List<string> usedIDs = new List<string>();
            foreach(QuestButton button in questButtons)
            {
                string currentID = button.Quest.questID;
                if (usedIDs.Contains(currentID))                
                    button.Visibility = Visibility.Collapsed;                
                else
                    usedIDs.Add(currentID);
            }
        }

        /// <summary>
        /// Löscht alle QuestButtons und setzt die Liste der Buttons zurück.
        /// </summary>
        private void DeleteAllQuestButtons()
        {
            foreach (QuestButton button in questButtons)
            {
                if (graphButtons.TryGetValue(button, out Button _btn))
                    _btn.Visibility = Visibility.Collapsed;

                button.Visibility = Visibility.Collapsed;
            }

            questButtons.Clear();
            graphButtons.Clear();
            graphButtonPositions.Clear();
        }

        /// <summary>
        /// Löscht alle DialogButtons und setzt die Liste der Buttons zurück.
        /// </summary>
        private void DeleteAllDialogButtons()
        {
            foreach (DialogButton button in dialogButtons)
            {
                button.Visibility = Visibility.Collapsed;
            }

            dialogButtons.Clear();
        }

        /// <summary>
        /// Löscht die Quest aus dem Verzeichnis.
        /// </summary>
        public void DeleteQuest(Quest _quest, QuestButton _button)
        {
            if (graphButtons.TryGetValue(_button, out Button btn))
            {
                btn.Visibility = Visibility.Collapsed;
                graphButtons.Remove(_button);
            }

            _button.Visibility = Visibility.Collapsed;

            if (quests.Contains(_quest))
                quests.Remove(_quest);

            if (questButtons.Contains(_button))
                questButtons.Remove(_button);

            string[] files = Directory.GetFiles(currentWorkingPath, "*.vqtl");
            foreach (string file in files)
            {
                string[] s = file.Replace(".vqtl", string.Empty).Split('\\');
                string fileName = s[s.Length - 1];
                if (_quest.questID.Equals(fileName))
                {
                    File.Delete(file);
                    break;
                }                
            }

            SetQuestButtonPositions();
        }

        /// <summary>
        /// Löscht den Dialog aus dem Verzeichnis.
        /// </summary>
        public void DeleteDialog(Dialog _dialog, DialogButton _button)
        {
            _button.Visibility = Visibility.Collapsed;

            if (dialogs.Contains(_dialog))
                dialogs.Remove(_dialog);

            if (dialogButtons.Contains(_button))
                dialogButtons.Remove(_button);

            string[] files = Directory.GetFiles(currentWorkingPath, "*.vdtl");
            foreach (string file in files)
            {
                string[] s = file.Replace(".vdtl", string.Empty).Split('\\');
                string fileName = s[s.Length - 1];
                if (_dialog.dialogID.Equals(fileName))
                {
                    File.Delete(file);
                    break;
                }
            }

            SetDialogButtonPositions();
        }

        /// <summary>
        /// Hält einen Wert zwischen Minimum und Maximum.
        /// </summary>
        private double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;

            return value;
        }

        #region Toolbar Button Methods

        private void Data_Click(object sender, RoutedEventArgs e)
        {
            bool isAlreadyOpen = Button_Data_New.Visibility == Visibility.Visible;

            DisableAllSubmenus();

            if (!isAlreadyOpen)
            {
                Button_Data_New.Visibility = Visibility.Visible;
                Button_Data_Close.Visibility = Visibility.Visible;
                Button_Data_Open.Visibility = Visibility.Visible;
                Button_Data_ShowInExplorer.Visibility = Visibility.Visible;
                Button_Data_Save.Visibility = Visibility.Visible;
                Button_Data_SaveAs.Visibility = Visibility.Visible;
                Button_Data_Import.Visibility = Visibility.Visible;
                Button_Data_Export.Visibility = Visibility.Visible;
                Button_Data_Quit.Visibility = Visibility.Visible;

                bool loadedProject = !string.IsNullOrEmpty(currentWorkingPath);
                Button_Data_Close.IsEnabled = loadedProject;
                Button_Data_ShowInExplorer.IsEnabled = loadedProject;
                Button_Data_Save.IsEnabled = loadedProject;
                Button_Data_SaveAs.IsEnabled = loadedProject;
                Button_Data_Export.IsEnabled = loadedProject;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            bool isAlreadyOpen = Button_Edit_GoBack.Visibility == Visibility.Visible;

            DisableAllSubmenus();

            if (!isAlreadyOpen)
            {
                Button_Edit_GoBack.Visibility = Visibility.Visible;
                Button_Edit_GoForward.Visibility = Visibility.Visible;
                Button_Edit_Cut.Visibility = Visibility.Visible;
                Button_Edit_Copy.Visibility = Visibility.Visible;
                Button_Edit_Paste.Visibility = Visibility.Visible;
                Button_Edit_Search.Visibility = Visibility.Visible;
                Button_Edit_Mark.Visibility = Visibility.Visible;

                bool loadedProject = !string.IsNullOrEmpty(currentWorkingPath);

                Button_Edit_GoBack.IsEnabled = loadedProject;
                Button_Edit_GoForward.IsEnabled = loadedProject;
                Button_Edit_Cut.IsEnabled = loadedProject;
                Button_Edit_Copy.IsEnabled = loadedProject;
                Button_Edit_Paste.IsEnabled = loadedProject;
                Button_Edit_Search.IsEnabled = loadedProject;
                Button_Edit_Mark.IsEnabled = loadedProject;
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            bool isAlreadyOpen = Button_Help_Information.Visibility == Visibility.Visible;

            DisableAllSubmenus();

            if (!isAlreadyOpen)
            {
                Button_Help_Information.Visibility = Visibility.Visible;
                Button_Help_VersionInformation.Visibility = Visibility.Visible;
            }
        }

        private void Data_New_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            NewWizardWindow wizardWindow = new NewWizardWindow();
            wizardWindow.ShowDialog();
            if (wizardWindow.DialogResult.HasValue && wizardWindow.DialogResult.Value)
            {
                if (string.IsNullOrEmpty(wizardWindow.Settings.projectName))
                {
                    MessageBox.Show("Der eingebene Projektname war leer. Bitte versuche es erneut.", "Fehler");
                }
                else if (Directory.Exists(wizardWindow.Settings.projectPath))
                {
                    MessageBox.Show("Ein Projekt mit dem eingegebenen Namen existiert bereits. Bitte versuche es erneut.", "Fehler");
                }
                else
                {
                    CreateNewProject(wizardWindow.Settings);
                }
            }
        }

        private void Data_Open_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
            {
                OpenProject(fbd.SelectedPath);
            }
        }

        private void Data_Close_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            CloseCurrentProject();
            MessageBox.Show("Das aktuelle Projekt wurde geschlossen.", "Erfolg");
        }

        private void Data_ShowInExplorer_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            if (!string.IsNullOrEmpty(currentWorkingPath))
                Process.Start(currentWorkingPath);
            else
                MessageBox.Show("Es konnte kein valider Datenpfad gefunden werden.", "Fehler");
        }

        private void Data_Save_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            if (!string.IsNullOrEmpty(currentWorkingPath))
                Save();
            else
                MessageBox.Show("Es konnte kein offenes Projekt gefunden werden.", "Fehler");
        }

        private void Data_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            string newPath = currentWorkingPath;

            if (!string.IsNullOrEmpty(currentWorkingPath))
                Save(newPath);
            else
                MessageBox.Show("Es konnte kein offenes Projekt gefunden werden.", "Fehler");
        }

        private void Data_Import_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            using (System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.Filter = "zip files (*.zip)|*.zip";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = ofd.FileName;
                    string extractPath = Directory.GetParent(path).FullName;
                    int i = 0;
                    while (Directory.Exists($"{extractPath}\\import_{i}"))
                        i++;

                    extractPath = $"{extractPath}\\import_{i}";
                    Directory.CreateDirectory(extractPath);

                    ZipFile.ExtractToDirectory(path, extractPath);
                    OpenProject(extractPath);
                    MessageBox.Show($"Erfolgreich nach {extractPath} entpackt und importiert.", "Erfolg");
                }
            }
        }

        private void Data_Export_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
            {
                string finalPath = $"{fbd.SelectedPath}\\export.zip";
                ZipFile.CreateFromDirectory(currentWorkingPath, finalPath);
                MessageBox.Show($"Erfolgreich zum angegebenen Pfad ({fbd.SelectedPath}) exportiert.");
            }
            else
            {
                MessageBox.Show($"Beim Exportieren zum angegebenen Pfad ({fbd.SelectedPath}) ist ein Fehler aufgetreten.\nAchte darauf, dass das angegebene Verzeichnis existiert.", "Export-Fehler");
            }
        }

        private void Data_Quit_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            Application.Current.Shutdown();
        }

        private void Edit_GoBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_GoForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Cut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Paste_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Mark_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Help_Information_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            MessageBox.Show($"Dies ist der DaggerPlotter, eine Software, die Tim schrieb, um Alexander bei seinem Schreibprozess " +
                $"zu unterstützen.\n\nMit dieser Software lassen sich Quests und Dialoge einfach und übersichtlich planen und erstellen. " +
                $"Neben Tools zur Planung an sich unterstützt diese Software außerdem eine Überprüfung auf Repetivität in Form von " +
                $"Questbausteinen, die insbesonders beim Erstellen nicht-linearer Quests zum Einsatz kommen.\n\n" +
                $"Diese Software ist für den Gebrauch der erstellten Daten für das Spiel VIVACIOUS ((c)Daggerbunny Studios) vorgesehen.\n" +
                $"Die verwendete Sprache wird VDTL (Vivacious Dialogue Textbased Language) für die Dialoge und VQTL (Vivacious Quest Textbased Language) für die Quests genannt. " +
                $"Schöpfer beider Sprachen ist Tim B., die in ihrer Struktur an XML angelehnt sind.\n\n\nFür weitere Informationen wenden Sie sich bitte an Tim B..", "Informationen");
        }

        private void Help_VersionInformation_Click(object sender, RoutedEventArgs e)
        {
            DisableAllSubmenus();

            MessageBox.Show($"DaggerPlotter Version {VERSION_NUMBER}\n(c)Tim B., 2021", "Versionshinweise");
        }

        #endregion

        #region Main Button Methods

        private void NewQuest_Click(object sender, RoutedEventArgs e)
        {
            QuestWizardWindow wizardWindow = new QuestWizardWindow();
            wizardWindow.ShowDialog();
            if (wizardWindow.DialogResult.HasValue && wizardWindow.DialogResult.Value)
            {
                CreateVQTL(wizardWindow.Quest);
            }
        }

        private void NewDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogWizardWindow wizardWindow = new DialogWizardWindow();
            wizardWindow.ShowDialog();
            if (wizardWindow.DialogResult.HasValue && wizardWindow.DialogResult.Value)
            {
                CreateVDTL(wizardWindow.Dialog);
            }
        }

        private void GraphButton_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            if (graphButtonPosition == null)
            {
                Point p = Mouse.GetPosition(Grid);
                graphButtonPosition = p;
            }

            graphButtonisMoving = true;
        }

        private void GraphButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            graphButtonisMoving = false;
        }

        private void GraphButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!graphButtonisMoving) return;

            var mousePoint = Mouse.GetPosition(Grid);

            var offsetX =  - mousePoint.X +1525;
            var offsetY =  - mousePoint.Y +840;

            offsetX = Clamp(offsetX, 0, 750);
            offsetY = Clamp(offsetY, 0, 425);

            Button btn = (Button)sender;

            if (graphButtonPositions.TryGetValue(btn, out double[] pos))
                graphButtonPositions.Remove(btn);

            graphButtonPositions.Add(btn, new double[] { -offsetX, -offsetY });

            btn.RenderTransform = new TranslateTransform(-offsetX, -offsetY);
        }

        #endregion
    }

    public class QuestButton : Button
    {
        public Quest Quest { get => quest; }

        private Quest quest;
        private MainWindow mainWindow;
        private List<Button> submenuButtons = new List<Button>();

        public event EventHandler<QuestButton> OnChanged;        

        public QuestButton(Quest _quest, MainWindow _mainWindow) : base()
        {
            quest = _quest;
            mainWindow = _mainWindow;
            Content = quest.questName;
            switch (quest.buildType)
            {
                case TaskBuildType.SCENE:
                    Background = Brushes.CornflowerBlue;
                    break;
                case TaskBuildType.ACTION:
                    Background = Brushes.PaleVioletRed;
                    break;
                case TaskBuildType.NO_GAMEPLAY:
                    Background = Brushes.DarkSeaGreen;
                    break;
            }

            OnChanged?.Invoke(this, this);
        }

        protected override void OnClick()
        {
            base.OnClick();
            DestroySubmenu();

            if (quest != null)
            {
                QuestWizardWindow wizardWindow = new QuestWizardWindow(quest);
                wizardWindow.ShowDialog();
                if (wizardWindow.DialogResult.HasValue && wizardWindow.DialogResult.Value)
                {
                    quest = wizardWindow.Quest;
                    mainWindow.OverrideQuestInList(wizardWindow.Quest);
                    mainWindow.Save();

                    if ((string)Content != quest.questName)
                        Content = quest.questName;

                    switch (quest.buildType)
                    {
                        case TaskBuildType.SCENE:
                            Background = Brushes.CornflowerBlue;
                            break;
                        case TaskBuildType.ACTION:
                            Background = Brushes.PaleVioletRed;
                            break;
                        case TaskBuildType.NO_GAMEPLAY:
                            Background = Brushes.DarkSeaGreen;
                            break;
                    }

                    OnChanged?.Invoke(this, this);
                }
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if (quest != null)
            {
                Button editButton = new Button();
                editButton.Height = 20;
                editButton.Width = 20;
                editButton.Content = "Bearbeiten";
                editButton.VerticalAlignment = VerticalAlignment.Bottom;
                editButton.HorizontalAlignment = HorizontalAlignment.Left;
                editButton.Margin = AddThickness(Margin, new Thickness(90f, 0f, 0f, 20f));
                editButton.Click += EditButton_Click;
                submenuButtons.Add(editButton);
                mainWindow.Grid.Children.Add(editButton);

                Button deleteButton = new Button();
                deleteButton.Height = 20;
                deleteButton.Width = 20;
                deleteButton.Content = "Löschen";
                deleteButton.VerticalAlignment = VerticalAlignment.Bottom;
                deleteButton.HorizontalAlignment = HorizontalAlignment.Left;
                deleteButton.Margin = AddThickness(Margin, new Thickness(110f, 0f, 0f, 20f));
                deleteButton.Click += DeleteButton_Click;
                submenuButtons.Add(deleteButton);
                mainWindow.Grid.Children.Add(deleteButton);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (quest == null) return;

            DestroySubmenu();

            if (MessageBox.Show($"Bist du sicher, dass du die Quest {quest.questName} (ID: {quest.questID}) löschen möchtest?\nDies kann nicht rückgängig gemacht werden.",
                "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                mainWindow.DeleteQuest(quest, this);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (quest == null) return;

            DestroySubmenu();
            OnClick();
        }

        private void DestroySubmenu()
        {
            foreach (Button btn in submenuButtons)
                btn.Visibility = Visibility.Collapsed;

            submenuButtons.Clear();
        }

        public static Thickness AddThickness(Thickness t1, Thickness t2)        
            => new Thickness(t1.Left + t2.Left, t1.Top + t2.Top, t1.Right + t2.Right, t1.Bottom + t2.Bottom);        
    }

    public class DialogButton : Button
    {
        public Dialog Dialog { get => dialog; }

        private Dialog dialog;
        private MainWindow mainWindow;
        private List<Button> submenuButtons = new List<Button>();

        public event EventHandler<DialogButton> OnChanged;

        public DialogButton(Dialog _dialog, MainWindow _mainWindow) : base()
        {
            dialog = _dialog;
            mainWindow = _mainWindow;
            Content = dialog.dialogID;

            OnChanged?.Invoke(this, this);
        }

        protected override void OnClick()
        {
            base.OnClick();
            DestroySubmenu();

            if (dialog != null)
            {
                DialogWizardWindow wizardWindow = new DialogWizardWindow(dialog);
                wizardWindow.ShowDialog();
                if (wizardWindow.DialogResult.HasValue && wizardWindow.DialogResult.Value)
                {
                    dialog = wizardWindow.Dialog;
                    mainWindow.OverrideDialogInList(wizardWindow.Dialog);
                    mainWindow.Save();

                    if ((string)Content != dialog.dialogID)
                        Content = dialog.dialogID;

                    OnChanged?.Invoke(this, this);
                }
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if (dialog != null)
            {
                Button editButton = new Button();
                editButton.Height = 20;
                editButton.Width = 20;
                editButton.Content = "Bearbeiten";
                editButton.VerticalAlignment = VerticalAlignment.Bottom;
                editButton.HorizontalAlignment = HorizontalAlignment.Left;
                editButton.Margin = QuestButton.AddThickness(Margin, new Thickness(90f, 0f, 0f, 20f));
                editButton.Click += EditButton_Click;
                submenuButtons.Add(editButton);
                mainWindow.Grid.Children.Add(editButton);

                Button deleteButton = new Button();
                deleteButton.Height = 20;
                deleteButton.Width = 20;
                deleteButton.Content = "Löschen";
                deleteButton.VerticalAlignment = VerticalAlignment.Bottom;
                deleteButton.HorizontalAlignment = HorizontalAlignment.Left;
                deleteButton.Margin = QuestButton.AddThickness(Margin, new Thickness(110f, 0f, 0f, 20f));
                deleteButton.Click += DeleteButton_Click;
                submenuButtons.Add(deleteButton);
                mainWindow.Grid.Children.Add(deleteButton);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dialog == null) return;

            DestroySubmenu();

            if (MessageBox.Show($"Bist du sicher, dass du diesen Dialog (ID: {dialog.dialogID}) löschen möchtest?\nDies kann nicht rückgängig gemacht werden.",
                "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                mainWindow.DeleteDialog(dialog, this);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dialog == null) return;

            DestroySubmenu();
            OnClick();
        }

        private void DestroySubmenu()
        {
            foreach (Button btn in submenuButtons)
                btn.Visibility = Visibility.Collapsed;

            submenuButtons.Clear();
        }
    }
}
