   M S B A M L   `   `   `  ����H  DDaggerPlotter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DaggerPlotterDaggerPlotter  5   	   &     DaggerPlotter.DialogWizardWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DaggerPlotter     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Icon$ Resources/icon.png��5
   	    � 
ResizeMode$ NoResize=�5   	    � Title$ Neuer Dialog��5   	   $	��600��6   $	��800��6+   .��5      � .+�5   
   � -   5        $��CreationGrid��5      $y�Hidden=�6#   .+�5        Xceed.Wpf.Toolkit.Wizard  -   5        $��CreationWizard��5         FinishButtonClosesWindow .    BackButtonContent$ Zurück��6P      CancelButtonContent$ 	Abbrechen��6k      FinishButtonContent$ 	Erstellen��6�      HelpButtonVisibility$ Hidden=�6�   	   	CanFinish	 . 
   BackButtonVisibility$
 Hidden=�6�   .@�5      "  Xceed.Wpf.Toolkit.WizardPage  -   5          Name$ 	IntroPage��5   "      PageType$ Interior=�65      Title$ Neuen Dialog erstellen��6I      Description$� �Folge den Anweisungen auf dieser Seite, um einen neuen Dialog zu erstellen. Ein VDTL-Dokument wird im Projektverzeichnis erstellt.��6h   .��5      �  � ColumnDefinitions 5      �� 5      $��* �6/   �� 5      $	��5px �6/   �� 5      $��2* �6/   .+�5      �� $��	Dialog-ID��6     � ToolTip$� �Eine einzigartige Bennennung eines Dialoges. Bestimmt nur den Dateinamen und dient als Orientierungshilfe für den Nutzer. Im eigentlichen Spiel nicht einsehbar.��64   �� 5      -   5        $��TextBox_DialogIDTextBox��5   "   $��2��6A   $��neuer_dialog��6Q   $��22��6e   $	��Top=�6q   5      5      5      5      � 5"   
   -   5        $��EditGrid��5"      $y�Visible=�6    .+�5#        -   5        $��
EditWizard��5#       . $ Zurück��6L   $ 	Abbrechen��6g   $ 	Speichern��6�      HelpButtonContent$ Action-Tags��6�   $ Visible=�6�   	 . $
 Hidden=�6  .@�5$        -   5        $ 
EIntroPage��5$   "   $ Exterior=�66   $ Dialog bearbeiten��6J   $W QFolge den Anweisungen auf dieser Seite, um den ausgewählen Dialog zu bearbeiten.��6d      ExteriorPanelBackground
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
eintragen kann.��5%   "   .��5(      � -   5         �Name$
 Grid��5(      .+�5)      �� $	��600��6$   $	��300��60   $��0,0,0,50q�6=   ��5*      �� 5+   "    �� Color$ 	AliceBlue��62   5-      �� 5/      $��Dialog-Einträge��6    $��0, 20q�6:   $��30��6I   $	��200��6U   $
��Left=�6a   $	��Top=�6|   $
��Bold"�6�   �� 50      $��	Dialog-ID��6    �� 51      -	   5        $!��TextBox_EditDialogIDTextBox��51   "   $��2��6E   $��22��6U   $	��Top=�6a   $��Right=�6y   $	��450��6�   {�.  
�������6�   �� 52      $��Visible=�6'   $��Disabled=�6M   $	��300��6v   $��
0,46,10,95q�6�   .��53      �� -
   5        $(��"StackPanel_DialogEntryButtonParent��53   )   $	��Top=�6S    � 	Resources 54   "   %   DaggerPlotter.DialogEntryButton �� 
TargetType �� Property �� Value��%Q   '        �� 55   &   "  6,   .�56   *   ��  � ��$ 5q�6C   57   '   58   #   59      5:      �� 5;      -   5        $!��Button_NewDialogEntryButton��5;   !   $��Neuer Eintrag��6E   $��26��6]   $	��100��6i   $��Bottom=�6u   $
��Left=�6�   $��
0,0,479,65q�6�   6�   5<      5=      5>      5?      5@      5A      