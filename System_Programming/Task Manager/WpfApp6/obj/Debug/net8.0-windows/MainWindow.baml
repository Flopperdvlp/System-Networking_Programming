   M S B A M L   `   `   `  ����  WpfApp6    WpfApp6.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��ProcessesGrid��5         AutoGenerateColumns$ False��6(   $��10,48,10,10q�6D      
IsReadOnly$
 True��5         SelectionMode$ Single=�6      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5	      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5
      $ Ім'я��6%   $ * 6S    63   �� *ProcessName+6S   5      �� 5   
   -   5        $��Оновити��5      $
��Left=�6#   $��	10,19,0,0q�6>   $	��Top=�6Q   $	��100��6i   6u   �� 5   
   -   5        $��Завершити��5      $��Right=�6%   $��
0,19,312,0q�6A   $	��Top=�6U   $	��100��6m   6y   �� 5   
   -   5        $,��&Сортировать по имени��5      $
��Left=�60   $��
222,19,0,0q�6K   $	��Top=�6_   $	��132��6w   6�   �� 5   
   -   5        $.��(Информация о процессе��5      $
��Left=�61   $��
359,19,0,0q�6L   $	��Top=�6`   $	��156��6x   6�   5      5      