   M S B A M L   `   `   `  ����  UNO    UNO.Views.WaitingRoom   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Waiting Room��5   	   $	��400��6   $	��600��6+    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   �� 5
      $
��Auto �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��0��6,   $��0,0,0,20q�69   .+�5      �� -   5         ��Name$ lblRoomInfo��5      $��18(�6-   $
��Bold"�6;    �� TextAlignment$ Center=�6M   5      �� 5   
   $��Players in Room��6   $��1��6,   $��14(�69   .��5      �� .��5      �� -   5         ��Name$ 
lstPlayers��5      $��16(�6.   5      5      �� 5   
   $��2��6   $��Center=�6"   $��0,20,0,0q�6?   .+�5      �� -   5         ��Name$ btnStart��5      $��
Start Game��6'   $	��150��6<   $��40��6H   6T   5      5      5      