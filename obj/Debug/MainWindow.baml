   M S B A M L   `   `   `  ����J  FFinalTextEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TextEditor
TextEditor  5   	       TextEditor.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TextEditor     5   	   -   5        $��
mainWindow��5   )     � Title$-  'FinalTextEditor 2019 Fall FINAL PROJECT��5   	   $	��500��69   $
��1000��6F   .��5
      � .+�5   
   ]� .+�5      o� $	��Top=�6   .@�5      m� $��Menu Of TextEditor��6   .@�5      m� -   5         m�Name$
 save��5   .   $��_Save��6   6<   m� 5      -   5        $ saveAs��5   1   $��_Save As��6   6A   m� 5      -   5        $
 help��5   .   $��_Help��6   6<   5      5      5      �� 5   
   -   5         ��Name$
 cls1��5      $
��Left=�6   $��1,20,0,0q�69   $
��Auto��6K   �� 5   
   -   5        $
 cls2��5      $��Center=�6   $��1,20,0,0q�6;   $
��Auto��6M   �� 5   
   -   5        $
 cls3��5      $��Right=�6   $��1,20,0,0q�6:   $
��Auto��6L   �� 5   
   -   5         ��Name$ ArrowBox��5      ���#ff3333$
��Left=�69   $��23��6T   $��1,48,0,0q�6`   $	��Top=�6r   $��92��6�   $��
=========>��6�   �� 5   
   -	   5        $ virtualtext��5      $��Right=�6'   $��23��6C   $��0,48,0,0q�6O    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��894��6�   6�   �� 5   
   -
   5        $��scrollViewer��5   �   $��Visible=�6   $��Visible=�6=   $��	1,71,0,27q�6e   $
��Auto��6x   $��Stretch=�6�   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ gridinfo��5   0      HeadersVisibility$
 None=�6      AutoGenerateColumns .  ���#ffff4d��6^   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      	   
Foreground
	 �   �,  &System.Windows.Controls.DataGridColumn
   
IsReadOnly
 .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6Y   �� *	lineNum+6x     5      $	 595 6)    65   �� *data+6Q     5      
	 �P��$ 90 6<    6G   �� *suffix+6e   5!      5"      5$      5%      