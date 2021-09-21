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
    public partial class DialogWizardWindow : Window
    {
        public Dialog Dialog { get => dialog; }

        private Dialog dialog;
        private DialogTagsHelperWindow tagsHelperWindow;
        private List<DialogEntry> dialogEntries = new List<DialogEntry>();
        private List<DialogEntryButton> dialogEntryButtons = new List<DialogEntryButton>();

        public DialogWizardWindow()
        {
            InitializeComponent();

            EditGrid.Visibility = Visibility.Collapsed;
            //Height 200
            //Width 800
        }

        public DialogWizardWindow(Dialog _dialog)
        {
            InitializeComponent();

            dialog = _dialog;

            Title = "Dialog bearbeiten";
            Height = 600;
            CreationGrid.Visibility = Visibility.Collapsed;
            EditGrid.Visibility = Visibility.Visible;
            TextBox_EditDialogIDTextBox.Text = dialog.dialogID;
        }

        private void CreationWizard_Finish(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            dialog = new Dialog(TextBox_DialogIDTextBox.Text, new List<DialogEntry>());
        }

        private void EditWizard_Help(object sender, RoutedEventArgs e)
        {
            if (tagsHelperWindow != null)
            {
                tagsHelperWindow.Focus();
            }
            else
            {
                tagsHelperWindow = new DialogTagsHelperWindow();
                tagsHelperWindow.Show();
                tagsHelperWindow.Closed += TagsHelperWindow_Closed;
            }
        }

        private void EditWizard_Cancel(object sender, RoutedEventArgs e)
        {
            if (tagsHelperWindow != null)
                tagsHelperWindow.Close();
        }

        private void TagsHelperWindow_Closed(object sender, EventArgs e)
        {
            tagsHelperWindow = null;
        }

        private void NewDialogEntryButton_Click(object sender, RoutedEventArgs e)
        {
            DialogEntryWizardWindow wizardWindow = new DialogEntryWizardWindow(dialog.dialogEntryCount);
            wizardWindow.ShowDialog();
            if (wizardWindow.DialogResult.HasValue && wizardWindow.DialogResult.Value)
            {
                CreateDialogEntryButton(wizardWindow.Entry);
                dialog.entryList.Add(wizardWindow.Entry);
                dialog.UpdateFromList();
            }
        }

        private void CreateDialogEntryButton(DialogEntry source)
        {
            DialogEntryButton instance = new DialogEntryButton(source, this);
            instance.Height = 25;
            instance.Width = 525;
            instance.HorizontalContentAlignment = HorizontalAlignment.Left;
            dialogEntryButtons.Add(instance);
            StackPanel_DialogEntryButtonParent.Children.Add(instance);
        }

        public void OverrideDialogInList(DialogEntry _entry)
        {
            foreach (DialogEntry entry in dialogEntries)
            {
                if (entry.index == _entry.index)
                {
                    dialogEntries.Remove(entry);
                    break;
                }
            }

            dialogEntries.Add(_entry);
        }

        public void DeleteDialog(DialogEntry _entry, DialogEntryButton _button)
        { }
    }

    public class Dialog
    {
        public string dialogID;
        public int dialogEntryCount;
        public DialogEntry[] dialogEntries;
        public List<DialogEntry> entryList = new List<DialogEntry>();

        private const string VDTL_PREFIX = "VDTL*";

        public Dialog(string _id, List<DialogEntry> _entries)
        {
            dialogID = _id;
            dialogEntries = _entries.ToArray();
            dialogEntryCount = _entries.Count;
            entryList = _entries;
        }

        /// <summary>
        /// Wandelt gültigen VDTL-Code in einen Dialog um. Eine Syntaxüberprüfung findet nicht statt.
        /// </summary>
        /// <param name="vdtl">Gültiger VDTL-Code als string</param>
        public Dialog(string vdtl)
        {
            //HIER und Interface
            dialogID = GetArgument(vdtl, "DIALOGID");
            if (int.TryParse(GetArgument(vdtl, "DIALOGENTRYCOUNT"), out int count))
                dialogEntryCount = count;
        }

        /// <summary>
        /// Wandelt diesen Dialog in gültigen VDTL-Code um.
        /// </summary>
        public string ToVDTL()
        {
            dialogEntryCount = entryList.Count();
            dialogEntries = entryList.ToArray();

            string result = VDTL_PREFIX;
            result += $"<DIALOGID>{dialogID}</DIALOGID>";
            result += $"<DIALOGENTRYCOUNT>{dialogEntryCount}</DIALOGENTRYCOUNT>";
            result += $"<DIALOGENTRIES>";
            for (int i=0; i<dialogEntries.Length; i++)
            {
                result += $"<DIALOGENTRY>{dialogEntries[i].ToVDTL()}</DIALOGENTRY>";
            }
            result += $"</DIALOGENTRIES>";

            return result;
        }

        public void UpdateFromList()
        {
            dialogEntryCount = entryList.Count;
            dialogEntries = entryList.ToArray();
        }

        private string GetArgument(string vdtl, string tag)
        {
            string endTag = $"</{tag}>";
            tag = $"<{tag}>";

            if (!vdtl.Contains(tag) || !vdtl.Contains(endTag))
                return null;

            string result = vdtl.Split(new string[] { tag, endTag }, StringSplitOptions.None)[1];

            return result;
        }
    }

    public class DialogEntry
    {
        public int index;
        public string speaker;
        public DialogContent content;
        public VDTLTagList<string> actionPreTags = new VDTLTagList<string>();
        public VDTLTagList<string> actionPostTags = new VDTLTagList<string>();
        public Decision decision;
        public CameraPerspective perspectiveOverride;
        public CameraEffect cameraEffect;
        public VDTLTagList<EmotionOverrideData> emotionOverrides = new VDTLTagList<EmotionOverrideData>();

        public DialogEntry(int _index, string _speaker, DialogContent _content, VDTLTagList<string> _actionPreTags,
            VDTLTagList<string> _actionPostTags, Decision _decision, CameraPerspective _perspectiveOverride, 
            CameraEffect _cameraEffect, VDTLTagList<EmotionOverrideData> _emotionOverrides)
        {
            index = _index;
            speaker = _speaker;
            content = _content;
            actionPreTags = _actionPreTags;
            actionPostTags = _actionPostTags;
            decision = _decision;
            perspectiveOverride = _perspectiveOverride;
            cameraEffect = _cameraEffect;
            emotionOverrides = _emotionOverrides;
        }

        public string ToVDTL()
        {
            string result = $"<INDEX>{index}</INDEX>";
            result += $"<SPEAKER>{speaker}</SPEAKER>";
            result += $"<CONTENT>{content.ToVDTL()}</CONTENT>";
            result += $"<ACTIONPRETAGS>{actionPreTags.ToVDTL("ACTIONPRETAG")}</ACTIONPRETAGS>";
            result += $"<ACTIONPOSTTAGS>{actionPostTags.ToVDTL("ACTIONPOSTTAG")}</ACTIONPOSTTAGS>";
            result += $"<DECISION>{decision.ToVDTL()}</DECISION>";
            result += $"<PERSPECTIVEOVERRIDE>{perspectiveOverride.ToVDTL()}</PERSPECTIVEOVERRIDE>";
            result += $"<CAMERAEFFECT>{cameraEffect.ToVDTL()}</CAMERAEFFECT>";
            result += $"<EMOTIONOVERRIDES>{emotionOverrides.ToVDTL("EMOTIONOVERRIDE")}</EMOTIONOVERRIDES>";

            return result;
        }
    }

    public class Decision
    {
        public VDTLTagList<DecisionEntry> decisionEntries = new VDTLTagList<DecisionEntry>();

        public string ToVDTL()
        {
            return decisionEntries.ToVDTL("DECISIONENTRY");
        }
    }

    public class CameraPerspective
    {
        public ECameraPerspective perspective = ECameraPerspective.DEFAULT;
        public float[] offset = { 0f, 0f, 0f };

        public CameraPerspective(ECameraPerspective _perspective, float[] _offset)
        {
            perspective = _perspective;
            offset = _offset;
        }

        public string ToVDTL()
        {
            return $"<PERSPECTIVE>{(int)perspective}</PERSPECTIVE><OFFSETX>{offset[0]}</OFFSETX><OFFSETY>{offset[1]}</OFFSETY><OFFSETZ>{offset[2]}</OFFSETZ>";
        }
    }

    public class EmotionOverrideData
    {
        public VDTLTagList<float> emotionValues = new VDTLTagList<float>();
        public bool reset;
        public bool toggleBlink;

        public EmotionOverrideData(VDTLTagList<float> _emotionValues, bool _reset, bool _toggleBlink)
        {
            emotionValues = _emotionValues;
            reset = _reset;
            toggleBlink = _toggleBlink;
        }

        public string ToVDTL()
        {
            string result = $"<EMOTIONVALUES>{emotionValues.ToVDTL("EMOTIONVALUE")}</EMOTIONVALUES>";
            result += $"<RESET>{(reset ? $"{1}" : $"{0}")}</RESET>";
            result += $"<TOGGLEBLINK>{(toggleBlink ? $"{1}" : $"{0}")}</TOGGLEBLINK>";

            return result;
        }
    }

    public class CameraEffect
    {
        public ECameraEffectType effectType;
        public float delay;
        public float sustain;
        public float leave;
        public VDTLTagList<float> additionalValues;

        public CameraEffect(ECameraEffectType _effectType, float _delay, float _sustain, float _leave,
            VDTLTagList<float> _additionalValues)
        {
            effectType = _effectType;
            delay = _delay;
            sustain = _sustain;
            leave = _leave;
            additionalValues = _additionalValues;
        }

        public string ToVDTL()
        {
            string result = $"<EFFECTTYPE>{(int)effectType}</EFFECTTYPE>";
            result += $"<DELAY>{delay}</DELAY>";
            result += $"<SUSTAIN>{sustain}</SUSTAIN>";
            result += $"<LEAVE>{leave}</LEAVE>";
            result += $"<CAMERAEFFECTADDITIONALVALUES>{additionalValues.ToVDTL("CAMERAEFFECTADDITIONALVALUE")}</CAMERAEFFECTADDITIONALVALUES>";

            return result;
        }
    }

    public class DialogContent
    {
        public string audioClipPath;
        public string dialogText;

        public DialogContent(string _audioClipPath, string _dialogText)
        {
            audioClipPath = _audioClipPath;
            dialogText = _dialogText;
        }

        public string ToVDTL()
        {
            return $"<AUDIOCLIPPATH>{audioClipPath}</AUDIOCLIPPATH><DIALOGTEXT>{dialogText}</DIALOGTEXT>";
        }
    }

    public class DecisionEntry
    {
        public int index;
        public string content;
        public int linksToIndex;
        public DecisionType decisionType;

        public DecisionEntry(int _index, string _content, int _linksToIndex, DecisionType _decisionType)
        {
            index = _index;
            content = _content;
            linksToIndex = _linksToIndex;
            decisionType = _decisionType;
        }

        public string ToVDTL()
        {
            string result = $"<DECISIONENTRYINDEX>{index}</DECISIONENTRYINDEX>";
            result += $"<DECISIONENTRYCONTENT>{content}</DECISIONENTRYCONTENT>";
            result += $"<LINKSTOINDEX>{content}</LINKSTOINDEX>";
            result += $"<DECISIONENTRYDECISIONTYPE>{decisionType.ToVDTL()}</DECISIONENTRYDECISIONTYPE>";

            return result;
        }
    }

    public class DecisionType
    {
        public EDecisionType decisionType;
        public int additionalData;

        public DecisionType(EDecisionType _decisionType, int _additionalData)
        {
            decisionType = _decisionType;
            additionalData = _additionalData;
        }

        public string ToVDTL()
        {
            return $"<DECISIONTYPE>{(int)decisionType}</DECISIONTYPE><DECISIONTYPEADDITIONALDATA>{additionalData}</DECISIONTYPEADDITIONALDATA>";
        }
    }
    
    public class VDTLTagList<T> : List<T>
    {
        public string ToVDTL(string tagName)
        {
            string result = string.Empty;

            if (typeof(EmotionOverrideData).IsAssignableFrom(typeof(T)))
            {
                EmotionOverrideData[] content = (EmotionOverrideData[])Convert.ChangeType(ToArray(), typeof(EmotionOverrideData[]));
                for (int i = 0; i < content.Length; i++)
                    result += $"<{tagName}>{content[i].ToVDTL()}</{tagName}>";
            }
            else if (typeof(DecisionEntry).IsAssignableFrom(typeof(T)))
            {
                DecisionEntry[] content = (DecisionEntry[])Convert.ChangeType(ToArray(), typeof(DecisionEntry[]));
                for (int i = 0; i < content.Length; i++)
                    result += $"<{tagName}>{content[i].ToVDTL()}</{tagName}>";
            }
            else
            {
                T[] content = ToArray();
                for (int i = 0; i < content.Length; i++)
                    result += $"<{tagName}>{content[i]}</{tagName}>";
            }

            return result;
        }
    }

    public class DialogEntryButton : Button
    {
        public DialogEntry DialogEntry { get => dialogEntry; }

        private DialogEntry dialogEntry;
        private DialogWizardWindow mainWindow;
        private List<Button> submenuButtons = new List<Button>();

        public event EventHandler<DialogEntryButton> OnChanged;

        public DialogEntryButton(DialogEntry _dialogEntry, DialogWizardWindow _mainWindow) : base()
        {
            dialogEntry = _dialogEntry;
            mainWindow = _mainWindow;
            Content = $"{dialogEntry.speaker} : \"{dialogEntry.content.dialogText}\"";

            OnChanged?.Invoke(this, this);
        }

        protected override void OnClick()
        {
            base.OnClick();
            DestroySubmenu();

            if (dialogEntry != null)
            {
                DialogEntryWizardWindow wizardWindow = new DialogEntryWizardWindow(dialogEntry);
                wizardWindow.ShowDialog();
                if (wizardWindow.DialogResult.HasValue && wizardWindow.DialogResult.Value)
                {
                    dialogEntry = wizardWindow.Entry;
                    mainWindow.OverrideDialogInList(wizardWindow.Entry);

                    string targetContent = $"{dialogEntry.speaker} : \"{dialogEntry.content.dialogText}\"";
                    if ((string)Content != targetContent)
                        Content = targetContent;

                    OnChanged?.Invoke(this, this);
                }
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if (dialogEntry != null)
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
            if (dialogEntry == null) return;

            DestroySubmenu();

            if (MessageBox.Show($"Bist du sicher, dass du diesen Dialog-Eintrag (Index: {dialogEntry.index}) löschen möchtest?\nDies kann nicht rückgängig gemacht werden.",
                "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                mainWindow.DeleteDialog(dialogEntry, this);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dialogEntry == null) return;

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

    public enum ECameraPerspective
    {
        DEFAULT,
        TOP_VIEW,
        BOTTOM_VIEW
    }

    public enum ECameraEffectType
    {
        NONE,
        BAM,
        SHAKE
    }

    public enum EDecisionType
    {
        DEFAULT,
        LEAVE,
        SHOP,
        CARDS,
        LIE,
        FIGHT,
        BRIBE,
        QUEST
    }
}
