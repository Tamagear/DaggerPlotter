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
    public partial class QuestWizardWindow : Window
    {
        public Quest Quest { get => quest; }

        private Quest quest;
        private TaskBuildType buildType;
        private TaskType taskType;

        public QuestWizardWindow()
        {
            InitializeComponent();

            buildType = TaskBuildType.SCENE;
            taskType = TaskType.TALK;
        }

        public QuestWizardWindow(Quest _quest)
        {
            InitializeComponent();

            Title = "Quest bearbeiten";
            IntroPage.Title = "Quest bearbeiten";
            IntroPage.Description = "Folge den Anweisungen auf dieser Seite, um die ausgewählte Quest zu bearbeiten.";
            quest = _quest;
            Wizard.FinishButtonContent = "Speichern";

            TextBox_QuestNameTextBox.Text = quest.questName;
            TextBox_QuestIDTextBox.Text = quest.questID;
            TextBox_QuestIDTextBox.Background = Brushes.LightGray;
            TextBox_QuestIDTextBox.IsReadOnly = true;
            Toggle_MainQuestToggle.IsChecked = quest.isMainQuest;
            TextBox_QuestParentIDTextBox.Text = quest.parentQuestID;
            TextBox_QuestDescriptionTextBox.Text = quest.questDescription;
            TextBox_QuestBackgroundTextBox.Text = quest.questBackground;
            Slider_LevelSlider.Value = quest.recommendedLevel;
            Label_LevelLabel.Content = $"{quest.recommendedLevel}";
            TextBox_QuestGiverTextBox.Text = quest.questGiver;
            Toggle_GoBackToggle.IsChecked = quest.needsToDeliver;
            taskType = quest.task.taskType;
            switch (taskType)
            {
                case TaskType.TALK:
                    DropDown_TaskTalk_Click(null, null);
                    break;
                case TaskType.INTERACT:
                    DropDown_TaskInteract_Click(null, null);
                    break;
                case TaskType.DEAL_DAMAGE:
                    DropDown_TaskDealDamage_Click(null, null);
                    break;
                case TaskType.KILL:
                    DropDown_TaskKill_Click(null, null);
                    break;
                case TaskType.COLLECT:
                    DropDown_TaskCollect_Click(null, null);
                    break;
                case TaskType.EQUIP:
                    DropDown_TaskEquip_Click(null, null);
                    break;
                case TaskType.CONSUME:
                    DropDown_TaskConsume_Click(null, null);
                    break;
                case TaskType.DUNGEON:
                    DropDown_TaskDungeon_Click(null, null);
                    break;
                case TaskType.TRIGGER_ENTER:
                    DropDown_TaskTriggerEnter_Click(null, null);
                    break;
                case TaskType.TRIGGER_EXIT:
                    DropDown_TaskTriggerExit_Click(null, null);
                    break;
            }
            TextBox_TaskTargetTextBox.Text = quest.task.taskGoalObject;
            TextBox_TaskTargetCountTextBox.Text = $"{quest.task.taskGoalCount}";
            TextBox_RewardMoneyTextBox.Text = $"{quest.moneyReward}";
            TextBox_RewardItemTextBox.Text = quest.itemReward;
            buildType = quest.buildType;
            switch (buildType)
            {
                case TaskBuildType.SCENE:
                    Dropdown_SceneType_Click(null, null);
                    break;
                case TaskBuildType.ACTION:
                    Dropdown_ActionType_Click(null, null);
                    break;
                case TaskBuildType.NO_GAMEPLAY:
                    Dropdown_NoGameplayType_Click(null, null);
                    break;
            }
        }

        private void Wizard_Finish(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            bool goalCountParse = int.TryParse(TextBox_TaskTargetCountTextBox.Text, out int goalCount);
            bool moneyParse = int.TryParse(TextBox_RewardMoneyTextBox.Text, out int money);

            if (!goalCountParse)
            {
                MessageBox.Show("Die Anzahl der Wiederholungen wurde nicht als Ganzzahl angegeben.", "Fehler");
                return;
            }

            if (!moneyParse)
            {
                MessageBox.Show("Das als Belohnung erhaltene Geld wurde nicht als Ganzzahl angegeben.", "Fehler");
                return;
            }

            quest = new Quest(TextBox_QuestNameTextBox.Text, Toggle_MainQuestToggle.IsChecked.Value,
                TextBox_QuestIDTextBox.Text, TextBox_QuestParentIDTextBox.Text, TextBox_QuestDescriptionTextBox.Text,
                TextBox_QuestBackgroundTextBox.Text, (int)Slider_LevelSlider.Value, TextBox_QuestGiverTextBox.Text,
                Toggle_GoBackToggle.IsChecked.Value, new QuestTask(taskType, TextBox_TaskTargetTextBox.Text, goalCount),
                money, TextBox_RewardItemTextBox.Text, buildType);
        }

        private void Dropdown_SceneType_Click(object sender, RoutedEventArgs e)
        {
            DropDown_BuildTypeDropDown.Content = "Scene-Baustein";
            DropDown_BuildTypeDropDown.Background = Brushes.CornflowerBlue;
            DropDown_BuildTypeDropDown.IsOpen = false;

            buildType = TaskBuildType.SCENE;
        }

        private void Dropdown_ActionType_Click(object sender, RoutedEventArgs e)
        {
            DropDown_BuildTypeDropDown.Content = "Action-Baustein";
            DropDown_BuildTypeDropDown.Background = Brushes.PaleVioletRed;
            DropDown_BuildTypeDropDown.IsOpen = false;

            buildType = TaskBuildType.ACTION;
        }

        private void Dropdown_NoGameplayType_Click(object sender, RoutedEventArgs e)
        {
            DropDown_BuildTypeDropDown.Content = "No-Gameplay-Baustein";
            DropDown_BuildTypeDropDown.Background = Brushes.DarkSeaGreen;
            DropDown_BuildTypeDropDown.IsOpen = false;

            buildType = TaskBuildType.NO_GAMEPLAY;
        }

        private void Slider_LevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label_LevelLabel.Content = $"{(int)Slider_LevelSlider.Value}";
        }
        
        private void DropDown_TaskTalk_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Sprich mit";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.TALK;
        }

        private void DropDown_TaskInteract_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Interagiere mit";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.INTERACT;
        }

        private void DropDown_TaskDealDamage_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Füge Schaden zu an";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.DEAL_DAMAGE;
        }

        private void DropDown_TaskKill_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Besiege";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.KILL;
        }

        private void DropDown_TaskCollect_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Sammle";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.COLLECT;
        }

        private void DropDown_TaskEquip_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Rüste aus";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.EQUIP;
        }

        private void DropDown_TaskConsume_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Verbrauche";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.CONSUME;
        }

        private void DropDown_TaskDungeon_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Bezwinge den Dungeon";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.DUNGEON;
        }

        private void DropDown_TaskTriggerEnter_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Betrete den Trigger";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.TRIGGER_ENTER;
        }

        private void DropDown_TaskTriggerExit_Click(object sender, RoutedEventArgs e)
        {
            DropDown_TaskTypeDropDown.Content = "Verlasse den Trigger";
            DropDown_TaskTypeDropDown.IsOpen = false;

            taskType = TaskType.TRIGGER_EXIT;
        }
    }

    public class Quest
    {
        public string questName;
        public bool isMainQuest;
        public string questID;
        public string parentQuestID;
        public string questDescription;
        public string questBackground;
        public int recommendedLevel;
        public string questGiver;
        public bool needsToDeliver;
        public QuestTask task;
        public int moneyReward;
        public string itemReward;
        public TaskBuildType buildType;
        public bool invalid;

        private const string VQTL_PREFIX = "VQTL*";

        /// <summary>
        /// Erstellt eine Quest mit den gegebenen Parametern.
        /// </summary>
        public Quest(string _name, bool _main, string _id, string _parentID, string _description, string _background,
            int _level, string _giver, bool _deliver, QuestTask _task, int _money, string _items, TaskBuildType _buildType)
        {
            questName = _name;
            isMainQuest = _main;
            questID = _id;
            parentQuestID = _parentID;
            questDescription = _description;
            questBackground = _background;
            recommendedLevel = _level;
            questGiver = _giver;
            needsToDeliver = _deliver;
            task = _task;
            moneyReward = _money;
            itemReward = _items;
            buildType = _buildType;
            invalid = false;
        }

        /// <summary>
        /// Wandelt gültigen VQTL-Code in eine Quest um. Eine Syntaxüberprüfung findet nicht statt.
        /// </summary>
        /// <param name="vqtl">Gültiger VQTL-Code als string</param>
        public Quest(string vqtl)
        {
            if (!vqtl.StartsWith(VQTL_PREFIX))
            {
                MessageBox.Show($"Übergebener Code war nicht als VQTL-Code markiert!\n\n{vqtl}", "Import-Fehler");
                invalid = true;
            }
            else
            {                
                questName = GetArgument(vqtl, "QUESTNAME");
                isMainQuest = GetArgument(vqtl, "ISMAINQUEST").Equals("1");
                questID = GetArgument(vqtl, "QUESTID");
                parentQuestID = GetArgument(vqtl, "PARENTQUESTID");
                questDescription = GetArgument(vqtl, "QUESTDESCRIPTION");
                questBackground = GetArgument(vqtl, "QUESTBACKGROUND");
                if (int.TryParse(GetArgument(vqtl, "RECOMMENDEDLEVEL"), out int level))
                    recommendedLevel = level;
                questGiver = GetArgument(vqtl, "QUESTGIVER");
                needsToDeliver = GetArgument(vqtl, "NEEDSTODELIVER").Equals("1");
                if (int.TryParse(GetArgument(vqtl, "MONEYREWARD"), out int money))
                    moneyReward = money;
                itemReward = GetArgument(vqtl, "ITEMREWARD");
                if (int.TryParse(GetArgument(vqtl, "BUILDTYPE"), out int buildIndex))
                    buildType = (TaskBuildType)buildIndex;

                TaskType _type = TaskType.TALK;
                int _goalCount = -1;

                if (int.TryParse(GetArgument(vqtl, "TASKTYPE"), out int taskIndex))
                    _type = (TaskType)taskIndex;
                string _goalObject = GetArgument(vqtl, "TASKGOALOBJECT");
                if (int.TryParse(GetArgument(vqtl, "TASKGOALCOUNT"), out int count))
                    _goalCount = count;

                task = new QuestTask(_type, _goalObject, _goalCount);

                invalid = false;
            }
        }

        /// <summary>
        /// Wandelt diese Quest in gültigen VQTL-Code um.
        /// </summary>
        public string ToVQTL()
        {
            if (invalid) return null;

            string result = VQTL_PREFIX;
            result += $"<QUESTNAME>{questName}</QUESTNAME>";
            result += $"<ISMAINQUEST>{(isMainQuest ? $"{1}" : $"{0}")}</ISMAINQUEST>";
            result += $"<QUESTID>{questID}</QUESTID>";
            result += $"<PARENTQUESTID>{parentQuestID}</PARENTQUESTID>";
            result += $"<QUESTDESCRIPTION>{questDescription}</QUESTDESCRIPTION>";
            result += $"<QUESTBACKGROUND>{questBackground}</QUESTBACKGROUND>";
            result += $"<RECOMMENDEDLEVEL>{recommendedLevel}</RECOMMENDEDLEVEL>";
            result += $"<QUESTGIVER>{questGiver}</QUESTGIVER>";
            result += $"<NEEDSTODELIVER>{(needsToDeliver ? $"{1}" : $"{0}")}</NEEDSTODELIVER>";
            result += $"<QUESTTASK>{task.ToString()}</QUESTTASK>";
            result += $"<MONEYREWARD>{moneyReward}</MONEYREWARD>";
            result += $"<ITEMREWARD>{itemReward}</ITEMREWARD>";
            result += $"<BUILDTYPE>{(int)buildType}</BUILDTYPE>";

            return result;
        }

        private string GetArgument(string vqtl, string tag)
        {
            string endTag = $"</{tag}>";
            tag = $"<{tag}>";

            if (!vqtl.Contains(tag) || !vqtl.Contains(endTag))
                return null;

            string result = vqtl.Split(new string[] { tag, endTag }, StringSplitOptions.None)[1];

            return result;
        }
    }

    public class QuestTask
    {
        public TaskType taskType;
        public string taskGoalObject;
        public int taskGoalCount;

        public QuestTask(TaskType _type, string _goalObject, int _goalCount)
        {
            taskType = _type;
            taskGoalObject = _goalObject;
            taskGoalCount = _goalCount;
        }

        public override string ToString()
        {
            string result = $"<TASKTYPE>{(int)taskType}</TASKTYPE>";
            result += $"<TASKGOALOBJECT>{taskGoalObject}</TASKGOALOBJECT>";
            result += $"<TASKGOALCOUNT>{taskGoalCount}</TASKGOALCOUNT>";

            return result;
        }
    }

    public enum TaskBuildType
    {
        SCENE,
        ACTION,
        NO_GAMEPLAY
    }

    public enum TaskType
    {
        TALK,
        INTERACT,
        DEAL_DAMAGE,
        KILL,
        COLLECT,
        EQUIP,
        CONSUME,
        DUNGEON,
        TRIGGER_ENTER,
        TRIGGER_EXIT
    }
}