   M S B A M L   `   `   `  ����H  DDaggerPlotter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DaggerPlotterDaggerPlotter  5   	   *    $DaggerPlotter.DialogTagsHelperWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DaggerPlotter     5   	     � WindowStyle$  
ToolWindow=�5   	    � Icon$ Resources/icon.png��5	   	    � 
ResizeMode$ CanMinimize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Action-Tag-Hilfe��5   	   $	��500��6"   $	��850��6/   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��4* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   �� 5"      $��* �6   �� 5#      $��* �6   �� 5$      $��* �6   .+�5&   
   �� $��BEFEHL��6   �� 5'   
   $��	BEDEUTUNG��6   $��1��6$   �� 5(   
   $��BESCHREIBUNG��6   $��2��6'   �� 5)   
   $��{item#i<#c>}��6   $��1��6/   �� 5*   
   $��	Gain Item��6   $��1��6$   $��1��61   �� 5+   
   $A��;Gibt dem Spieler c mal (default : 1) das Item mit der ID i.��6   $��1��6V   $��2��6c   �� 5,   
   $��{bs#t}��6   $��2��6#   �� 5-   
   $��Blackscreen��6   $��2��6&   $��1��63   �� 5.   
   $?��9Faded einen Blackscreen ein, der t Sekunden lang anhält.��6   $��2��6S   $��2��6`   �� 5/   
   $��{en#o}��6   $��3��6#   �� 50   
   $��Enable Object��6   $��3��6(   $��1��65   �� 51   
   $X��RRuft o.gameObject.setActive(true) auf. O ist der Name des Objekts in dieser Szene.��6   $��3��6m   $��2��6z   �� 52   
   $��{ds#o}��6   $��4��6#   �� 53   
   $��Disable Object��6   $��4��6)   $��1��66   �� 54   
   $Y��SRuft o.gameObject.setActive(false) auf. O ist der Name des Objekts in dieser Szene.��6   $��4��6n   $��2��6{   �� 55   
   $��{jmp#l}��6   $��5��6$   �� 56   
   $��Jump to line��6   $��5��6'   $��1��64   �� 57   
   $8��2Springt NACH dieser Dialogzeile zur Dialogzeile l.��6   $��5��6M   $��2��6Z   �� 58   
   $��{shop}��6   $��6��6#   �� 59   
   $��	Open Shop��6   $��6��6$   $��1��61   �� 5:   
   $&�� Öffnet den Laden des Besitzers.��6   $��6��6:   $��2��6G   5;      5<      