   M S B A M L   `   `   `  ���� H  DDaggerPlotter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DaggerPlotterDaggerPlotter  &     DaggerPlotter.DialogWizardWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:DaggerPlotter     X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � WindowStartupLocation$  CenterScreen=� � Icon$ Resources/icon.png�� � 
ResizeMode$ NoResize=� � Title$ Neuer Dialog��$	��600��$	��800��.��� .+�� -   $��CreationGrid��$y�Hidden=�.+�  Xceed.Wpf.Toolkit.Wizard  -   $��CreationWizard��   FinishButtonClosesWindow .    BackButtonContent$ Zurück��   CancelButtonContent$ 	Abbrechen��   FinishButtonContent$ 	Erstellen��   HelpButtonVisibility$ Hidden=�	   	CanFinish	 . 
   BackButtonVisibility$
 Hidden=�.@�"  Xceed.Wpf.Toolkit.WizardPage  -     Name$ 	IntroPage��   PageType$ Interior=�   Title$ Neuen Dialog erstellen��   Description$� �Folge den Anweisungen auf dieser Seite, um einen neuen Dialog zu erstellen. Ein VDTL-Dokument wird im Projektverzeichnis erstellt.��.���  � ColumnDefinitions �� $��* ��� $	��5px ��� $��2* �.+��� $��	Dialog-ID�� � ToolTip$� �Eine einzigartige Bennennung eines Dialoges. Bestimmt nur den Dateinamen und dient als Orientierungshilfe für den Nutzer. Im eigentlichen Spiel nicht einsehbar.���� -   $��TextBox_DialogIDTextBox��$��2��$��neuer_dialog��$��22��$	��Top=�� -   $��EditGrid��$y�Visible=�.+�  -   $��
EditWizard�� . $ Zurück��$ 	Abbrechen��$ 	Speichern��   HelpButtonContent$ Action-Tags��$ Visible=�	 . $
 Hidden=�.@�  -   $ 
EIntroPage��$ Exterior=�$ Dialog bearbeiten��$W QFolge den Anweisungen auf dieser Seite, um den ausgewählen Dialog zu bearbeiten.��   ExteriorPanelBackground
 �����   ExteriorPanelContent$� �Benutze Text-Transform-Tags,
um den Text auf bestimmte
Attribute anzupassen:

{ttg#m#f}
(Text Transform Gender):
Ist der Spieler männlich,
füge m an dieser Stelle ein,
ansonsten f.                                  

{tts#s#f}
(Text Transform Side)
Hat der Spieler sich
Sato angeschlossen, füge
s an dieser Stelle ein,
ansonsten f.                                  

Die restlichen Tags sind
Action-Tags, die man in den
entsprechenden Feldern
eintragen kann.��.��� -    �Name$
 Grid��.+��� $	��600��$	��300��$��0,0,0,50q�����  �� Color$ 	AliceBlue���� $��Dialog-Einträge��$��0, 20q�$��30��$	��200��$
��Left=�$	��Top=�$
��Bold"��� $��	Dialog-ID���� -	   $!��TextBox_EditDialogIDTextBox��$��2��$��22��$	��Top=�$��Right=�$	��450��{�.  
��������� $��Visible=�$��Disabled=�$	��300��$��
0,46,10,95q�.���� -
   $(��"StackPanel_DialogEntryButtonParent��$	��Top=� � 	Resources %   DaggerPlotter.DialogEntryButton �� 
TargetType �� Property �� Value��%,   '        �� "  .���  � ��$ 5q��� -   $!��Button_NewDialogEntryButton��$��Neuer Eintrag��$��26��$	��100��$��Bottom=�$
��Left=�$��
0,0,479,65q�