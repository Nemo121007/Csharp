   M S B A M L   `   `   `  ����J  FHotelAccounting, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:HotelAccountingHotelAccounting  5   	        HotelAccounting.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:HotelAccounting     5   	     � Title$+  %Счет за услуги отеля��5   	   $	��150��6&   $	��300��63   .��5	      ��  �� Orientation$ Vertical=�6   .+�5
   
   ]� .+�5      �� $	��100��6   $��Right=�6"   ��5      �� *Price+ �� ValidatesOnExceptions$
 True�� �� UpdateSourceTrigger$ PropertyChanged=��� 5      .��6   !Цена за одну ночь6'   5      ]� 5   
   .+�5      �� $	��100��6   $��Right=�6"   ��5      �� *NightsCount+$
 True��$ PropertyChanged=��� 5      .��6   !Количество ночей6&   5      ]� 5   
   .+�5      �� $	��100��6   $��Right=�6"   ��5      �� *
Discount+$
 True��$ PropertyChanged=��� 5      .��6   Скидка6   5      ]� 5   
   .+�5      �� $	��100��6   $��Right=�6"   ��5      �� *Total+$
 True��$ PropertyChanged=��� 5      .��6   
Итого6   5      5      5      