   M S B A M L   `   `   `  ���� H  DDaggerPlotter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DaggerPlotterDaggerPlotter      DaggerPlotter.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:DaggerPlotter       � WindowStyle$  ThreeDBorderWindow=� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=� � Icon$ Resources/icon.png�� � Title$ DaggerPlotter��$	��900��$
��1600��.��� -   $
��Grid��.+��� $��Resources/bg.png��$��Center=�$��Center=�$��UniformToFill=��� $
��1600��$��30��$	��Top=�����  �� Color$ 	AliceBlue���� $��Resources/graphbg.png��$	��450��$��Right=�$��Bottom=��� -   $��Text_PathText��$��30��$	��Top=� �� TextAlignment$ Right=�$��20(�$��Italic$��� -   $��Text_VersionText��$	��Top=�$��Right=�$��20(�
�������
���OO/�$��0, 30q��� $��Hinzufügen��$��0, 75q�$
��Bold"�$	��100��$��25��$	��Top=�$
��Left=�
���   ��� -   $��Button_Main_NewQuest��$��
Neue Quest��$	��100��$��25��$	��Top=�$
��Left=�$��0, 100q�{�.  �� -   $��Button_Main_NewDialogue��$��Neuer Dialog��$	��100��$��25��$	��Top=�$
��Left=�$��0, 125q�{�.  �� -   $��Datei��$	��100��$��30��$	��Top=�$
��Left=�$��20(��� -   $��
Bearbeiten��$	��100��$��30��$	��Top=�$
��Left=�$��18(�$��100,0q��� -   $��Hilfe��$	��100��$��30��$	��Top=�$
��Left=�$��20(�$��200,0q��� -	   $��Button_Data_New��$	��Neu��$	��100��$��30��$	��Top=�$
��Left=�$
��0,30q�$y�Hidden=��� -
   $��Button_Data_Open��$��Öffnen��$	��100��$��30��$	��Top=�$
��Left=�$
��0,60q�$y�Hidden=��� -   $��Button_Data_Close��$��Projekt schließen��$	��100��$��30��$	��Top=�$
��Left=�$
��0,90q�$y�Hidden=��� -   $ ��Button_Data_ShowInExplorer��$��Im Explorer öffnen��$	��100��$��30��$	��Top=�$
��Left=�$��0,120q�$y�Hidden=��� -   $��Button_Data_Save��$��	Speichern��$	��100��$��30��$	��Top=�$
��Left=�$��0,150q�$y�Hidden=��� -   $��Button_Data_SaveAs��$��Speichern unter...��$	��100��$��30��$	��Top=�$
��Left=�$��0,180q�$y�Hidden=��� -   $��Button_Data_Import��$��Importieren��$	��100��$��30��$	��Top=�$
��Left=�$��0,210q�$y�Hidden=��� -   $��Button_Data_Export��$��Exportieren��$	��100��$��30��$	��Top=�$
��Left=�$��0,240q�$y�Hidden=��� -   $��Button_Data_Quit��$��Beenden��$	��100��$��30��$	��Top=�$
��Left=�$��0,270q�$y�Hidden=��� -   $��Button_Edit_GoBack��$��Schritt zurück��$	��100��$��30��$	��Top=�$
��Left=�$��100,30q�$y�Hidden=��� -   $��Button_Edit_GoForward��$��Schritt vorwärts��$	��100��$��30��$	��Top=�$
��Left=�$��100,60q�$y�Hidden=��� -   $��Button_Edit_Cut��$��Ausschneiden��$	��100��$��30��$	��Top=�$
��Left=�$��100,90q�$y�Hidden=��� -   $��Button_Edit_Copy��$��Kopieren��$	��100��$��30��$	��Top=�$
��Left=�$��100,120q�$y�Hidden=��� -   $��Button_Edit_Paste��$��	Einfügen��$	��100��$��30��$	��Top=�$
��Left=�$��100,150q�$y�Hidden=��� -   $��Button_Edit_Search��$��Suchen��$	��100��$��30��$	��Top=�$
��Left=�$��100,180q�$y�Hidden=��� -   $��Button_Edit_Mark��$��Alles markieren��$	��100��$��30��$	��Top=�$
��Left=�$��100,210q�$y�Hidden=��� -   $��Button_Help_Information��$
��Info��$	��100��$��30��$	��Top=�$
��Left=�$��200,30q�$y�Hidden=��� -   $$��Button_Help_VersionInformation��$��Versionshinweise��$	��100��$��30��$	��Top=�$
��Left=�$��200,60q�$y�Hidden=�