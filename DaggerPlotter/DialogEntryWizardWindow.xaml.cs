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
    public partial class DialogEntryWizardWindow : Window
    {
        public DialogEntry Entry { get => entry; }

        private DialogEntry entry;
        private int targetIndex;

        public DialogEntryWizardWindow(int _targetIndex)
        {
            InitializeComponent();
            targetIndex = _targetIndex;
        }

        public DialogEntryWizardWindow(DialogEntry _entry)
        {
            entry = _entry;
            targetIndex = entry.index;

            //Die Textboxen setzen
        }

        private void Wizard_Finish(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            DialogContent content = new DialogContent(TextBox_ContentAudioClipPathTextBox.Text, TextBox_ContentTextTextBox.Text);

            VDTLTagList<string> preTags = new VDTLTagList<string>();
            VDTLTagList<string> postTags = new VDTLTagList<string>();

            Decision decision = new Decision();
            CameraPerspective perspectiveOverride = new CameraPerspective(ECameraPerspective.DEFAULT, new float[] { 0f, 0f, 0f });
            CameraEffect cameraEffect = new CameraEffect(ECameraEffectType.NONE, 0f, 0f, 0f, null);
            VDTLTagList<EmotionOverrideData> emotionOverrideData = new VDTLTagList<EmotionOverrideData>();

            entry = new DialogEntry(targetIndex, TextBox_SpeakerTextBox.Text, content, preTags, postTags, decision,
                perspectiveOverride, cameraEffect, emotionOverrideData);
        }
    }
}
