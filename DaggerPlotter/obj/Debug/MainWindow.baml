   M S B A M L   `   `   `  ����H  DDaggerPlotter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DaggerPlotterDaggerPlotter  5   	       DaggerPlotter.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DaggerPlotter     5   	     � WindowStyle$  ThreeDBorderWindow=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ CanMinimize=�5
   	    � Icon$ Resources/icon.png��5   	    � Title$ DaggerPlotter��5   	   $	��900��6   $
��1600��6,   .��5      � -   5        $
��Grid��5      .+�5   
   �� $��Resources/bg.png��6   $��Center=�6*   $��Center=�6G   $��UniformToFill=�6b   6|   �� 5   
   $
��1600��6   $��30��6!   $	��Top=�6-   ��5      �� 5       �� Color$ 	AliceBlue��6"   5      �� 5   
   $��Resources/graphbg.png��6   $	��450��6/   $��Right=�6<   $��Bottom=�6X   �� 5   
   -   5        $��Text_PathText��5      $��30��6)   $	��Top=�65    �� TextAlignment$ Right=�6M   $��20(�6c   $��Italic$�6q   6�   �� 5   
   -   5        $��Text_VersionText��5      $	��Top=�6,   $��Right=�6D   $��20(�6`   
�������
���OO/�$��0, 30q�6�   �� 5   
   $��Hinzufügen��6   $��0, 75q�6%   $
��Bold"�64   $	��100��6F   $��25��6R   $	��Top=�6^   $
��Left=�6v   
���   �6�   �� 5   
   -   5        $��Button_Main_NewQuest��5      $��
Neue Quest��6.   $	��100��6C   $��25��6O   $	��Top=�6[   $
��Left=�6s   $��0, 100q�6�   {�.  6�   �� 5   
   -   5        $��Button_Main_NewDialogue��5      $��Neuer Dialog��61   $	��100��6H   $��25��6T   $	��Top=�6`   $
��Left=�6x   $��0, 125q�6�   {�.  6�   �� 5   
   -   5        $��Datei��5      $	��100��6!   $��30��6-   $	��Top=�69   $
��Left=�6Q   $��20(�6l   6�   �� 5   
   -   5        $��
Bearbeiten��5      $	��100��6&   $��30��62   $	��Top=�6>   $
��Left=�6V   $��18(�6q   $��100,0q�6   6�   �� 5    
   -   5        $��Hilfe��5       $	��100��6!   $��30��6-   $	��Top=�69   $
��Left=�6Q   $��20(�6l   $��200,0q�6z   6�   �� 5!   
   -	   5        $��Button_Data_New��5!      $	��Neu��6)   $	��100��67   $��30��6C   $	��Top=�6O   $
��Left=�6g   $
��0,30q�6�   $y�Hidden=�6�   6�   �� 5"   
   -
   5        $��Button_Data_Open��5"      $��Öffnen��6*   $	��100��6;   $��30��6G   $	��Top=�6S   $
��Left=�6k   $
��0,60q�6�   $y�Hidden=�6�   6�   �� 5#   
   -   5        $��Button_Data_Close��5#      $��Projekt schließen��6+   $	��100��6G   $��30��6S   $	��Top=�6_   $
��Left=�6w   $
��0,90q�6�   $y�Hidden=�6�   6�   �� 5$   
   -   5        $ ��Button_Data_ShowInExplorer��5$      $��Im Explorer öffnen��64   $	��100��6Q   $��30��6]   $	��Top=�6i   $
��Left=�6�   $��0,120q�6�   $y�Hidden=�6�   6�   �� 5%   
   -   5        $��Button_Data_Save��5%      $��	Speichern��6*   $	��100��6>   $��30��6J   $	��Top=�6V   $
��Left=�6n   $��0,150q�6�   $y�Hidden=�6�   6�   �� 5&   
   -   5        $��Button_Data_SaveAs��5&      $��Speichern unter...��6,   $	��100��6I   $��30��6U   $	��Top=�6a   $
��Left=�6y   $��0,180q�6�   $y�Hidden=�6�   6�   �� 5'   
   -   5        $��Button_Data_Import��5'      $��Importieren��6,   $	��100��6B   $��30��6N   $	��Top=�6Z   $
��Left=�6r   $��0,210q�6�   $y�Hidden=�6�   6�   �� 5(   
   -   5        $��Button_Data_Export��5(      $��Exportieren��6,   $	��100��6B   $��30��6N   $	��Top=�6Z   $
��Left=�6r   $��0,240q�6�   $y�Hidden=�6�   6�   �� 5)   
   -   5        $��Button_Data_Quit��5)      $��Beenden��6*   $	��100��6<   $��30��6H   $	��Top=�6T   $
��Left=�6l   $��0,270q�6�   $y�Hidden=�6�   6�   �� 5*   
   -   5        $��Button_Edit_GoBack��5*      $��Schritt zurück��6,   $	��100��6E   $��30��6Q   $	��Top=�6]   $
��Left=�6u   $��100,30q�6�   $y�Hidden=�6�   6�   �� 5+   
   -   5        $��Button_Edit_GoForward��5+      $��Schritt vorwärts��6/   $	��100��6J   $��30��6V   $	��Top=�6b   $
��Left=�6z   $��100,60q�6�   $y�Hidden=�6�   6�   �� 5,   
   -   5        $��Button_Edit_Cut��5,      $��Ausschneiden��6)   $	��100��6@   $��30��6L   $	��Top=�6X   $
��Left=�6p   $��100,90q�6�   $y�Hidden=�6�   6�   �� 5-   
   -   5        $��Button_Edit_Copy��5-      $��Kopieren��6*   $	��100��6=   $��30��6I   $	��Top=�6U   $
��Left=�6m   $��100,120q�6�   $y�Hidden=�6�   6�   �� 5.   
   -   5        $��Button_Edit_Paste��5.      $��	Einfügen��6+   $	��100��6>   $��30��6J   $	��Top=�6V   $
��Left=�6n   $��100,150q�6�   $y�Hidden=�6�   6�   �� 5/   
   -   5        $��Button_Edit_Search��5/      $��Suchen��6,   $	��100��6=   $��30��6I   $	��Top=�6U   $
��Left=�6m   $��100,180q�6�   $y�Hidden=�6�   6�   �� 50   
   -   5        $��Button_Edit_Mark��50      $��Alles markieren��6*   $	��100��6D   $��30��6P   $	��Top=�6\   $
��Left=�6t   $��100,210q�6�   $y�Hidden=�6�   6�   �� 51   
   -   5        $��Button_Help_Information��51      $
��Info��61   $	��100��6@   $��30��6L   $	��Top=�6X   $
��Left=�6p   $��200,30q�6�   $y�Hidden=�6�   6�   �� 52   
   -   5        $$��Button_Help_VersionInformation��52      $��Versionshinweise��68   $	��100��6S   $��30��6_   $	��Top=�6k   $
��Left=�6�   $��200,60q�6�   $y�Hidden=�6�   6�   53      54      