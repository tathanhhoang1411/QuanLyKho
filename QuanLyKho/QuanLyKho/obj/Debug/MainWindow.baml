   M S B A M L   `   `   `  ����D  @QuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QuanLyKho	QuanLyKho  5   	   O,clr-namespace:QuanLyKho.UserControlQuanLyKhoQuanLyKho.UserControlQuanLyKho  5	   	       QuanLyKho.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:QuanLyKho     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   5uc,clr-namespace:QuanLyKho.UserControlQuanLyKho  5	   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5        $��
mainWindow��5   	      MaterialDesignBody#���   5   	   $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � 
ResizeMode$ 	CanResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    
 MainVM � DataContext# [ 5   	    � Title$$ Phần mềm quản lý kho��5   	   $	��800��6*   $
��1300��68   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5      6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 6$   �� *LoadedWindowCommand+   CommandParameter 5   $   ��  �� ElementName$ 
mainWindow��5      .��5      �  � RowDefinitions 5   
   �� 5       $
��auto �6   6,   �� 5!      $
��auto �6   6,   .+�5#   
   � $��0��6   .+�5$      1   +QuanLyKho.UserControlQuanLyKho.ControlBarUC  
 � Tag 6   �� *Title+$ 
mainWindow��6N   5%      � 5&   
   $��1��6   $��0 5 0 5q�6   .+�5'      �� $��Visible=�6   .��5(      �  � ColumnDefinitions 5)      �� 5*      $
��auto �6'   66   �� 5+      6*    � 	Resources 5.       �� 
TargetType �� Property �� Value��%�   '��       �� 5/      " ��6    .�50      ��  � ��$
 auto��67   6F   �� 51       � ��
 �����6O   �� 52       � ��$ 5q�67   6C   �� 53       � ��$ 12(�69   6F   �� 54       � ��$
 Bold"�6;   6J   55      56      .+�57      �� $��0��6#   $
��Auto=�63   .��58      ��  �� Orientation$ Vertical=�6%   $��Center=�6<   .+�5:      ��  � ToolTip$ Nhập��6%   $��80��5;   %     MaterialDesignRaisedButton#��[ 5<   %   $��5q�5=   %   ��5>   %   �� *InputWindowCommand+.��5?   "   �� $ Vertical=�6-   .+�5@   &   �� $��
Nhập kho��6-   6B   '  !MaterialDesignThemes.Wpf.PackIcon  5A   &   $��Center=�6>   $��24��6[   $��24��6f      Kind$  ArrowDownBoldCircleOutline=�6s   5B   '   5C   #   5D      �� 5E      $ Xuất��6%   $��80��5F   &   #��[ 5G   %   $��5q�5H   %   ��5I   %   �� *OutputWindowCommand+.��5J   "   �� $ Vertical=�6-   .+�5K   &   �� $��0��6,   $��
Xuất kho��69   6N     5L   &   $��24��6>   $��24��6I   $��Center=�6V   $ ArrowUpBoldCircleOutline=�6t   5M   '   5N   #   5O      �� 5P      $ 	Vật tư��6%   $��80��5Q   &   #��[ 5R   %   $��5q�5S   %   ��5T   &   �� *VattuWindowCommand+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�.��5U   "   �� $ Vertical=�6-   .+�5V   &   �� $��0��6,   $��	Vật tư��69   6L     5W   &   $��24��6>   $��24��6I   $��Center=�6V   $	 Dns=�6t   5X   '   5Y   #   5Z      �� 5[      $ 
Đơn vị��6%   $��80��5\   &   #��[ 5]   %   $��5q�5^   %   ��5_   %   �� *UnitWindowCommand+.��5`   "   �� $ Vertical=�6-   .+�5a   &   �� $��0��6,   $��Đơn vị đo��69   6O     5b   &   $��24��6>   $��24��6I   $��Center=�6V   $	 Dna=�6t   5c   '   5e   #   5f      �� 5g      $ Nhà cung cấp��6%   $��80��5h   &   #��[ 5i   %   $��5q�5j   %   ��5k   %   �� *SupplierWindowCommand+.��5l   "   �� $ Vertical=�6-   .+�5m   &   �� $��0��6,   $��Nhà cung cấp��69   6R     5n   &   $��24��6>   $��24��6I   $��Center=�6V   $ Factory=�6t   5o   '   5p   #   5q      �� 5r      $ Khách hàng��6%   $��80��5s   &   #��[ 5t   %   $��5q�5u   %   ��5v   %   �� *CustomerWindowCommand+.��5w   "   �� $ Vertical=�6-   .+�5x   &   �� $��0��6,   $��Khách hàng��69   6P     5y   &   $��24��6>   $��24��6I   $��Center=�6V   $
 Face=�6t   5z   '   5{   #   5|      �� 5}      $ Tài khoản��6%   $��80��5~   &   #��[ 5   %   $��5q�5�   %   ��5�   %   �� *AccountsWindowCommand+.��5�   "   �� $ Vertical=�6-   .+�5�   &   �� $��0��6,   $��Tài khoản��69   6O     5�   &   $��24��6>   $��24��6I   $��Center=�6V   $ Account=�6t   5�   '   5�   #   5�      5�      5�      � 5�      $��1��6    5�      �� 5�      61   �� 5�      61   �� 5�      61    5�      �� 5�      $
��auto �6,   6=   �� 5�      $
��auto �6,   6<   �� 5�      $
��auto �6,   6<   �� 5�      $
��auto �6,   6<   �� 5�      $
��auto �6,   6<   �� 5�      $
��auto �6,   6<    5�      ��%�   '��       �� 5�      " ��6$   .�5�   "   ��  � ��
 �����6O   �� 5�   "    � ��$ Center=�6H   6Y   �� 5�   "    � ��$ Center=�6F   6W   5�      5�      .+�5�      �� $ 
Horizontal=�6%   $��3��6>   .+�5�      �� $��5q�5�   #   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên vật tư��5�         MaterialDesignFilledTextBox#��[ 5�       ��5�   "   �� *
SearchName+$ PropertyChanged=�$ TwoWay=�.��5�   U   5�      �� 5�      $ Lọc��5�       $��5q�5�       3	  -MaterialDesignThemes.Wpf.ButtonProgressAssist 	  IsIndeterminate .  	  IsIndicatorVisible .  	  Value$ -1Q�5�       $��Lọc��5�       #��[ 5�       
��� E��{�5�       �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL�5�      5�      �� 5�      $��1��6$   $��3��61   6H   � 5�      $��0��6   $��2��6/   .+�5�      #
  MaterialDesignThemes.Wpf.Card
  
�������$��5q�5�   3   $
��auto��6>    ! PrimaryHueDarkForegroundBrush#���  5�   !   $��0q�5�   !   .��5�   "   �� $ Vertical=�6-   .+�5�   &   �� $��Lượng nhập��6,   6C   �� 5�   &   $��10000��6,   6>   5�   #   5�      5�      � 5�      $��1��6   $��2��6/   .+�5�      
  
�������$��5q�5�   3   $
��auto��6>   #���  5�   !   $��0q�5�   !   .��5�   "   �� $ Vertical=�6-   .+�5�   &   �� $��Lượng xuất��6,   6C   �� 5�   &   $��10000��6,   6>   5�   #   5�      5�      � 5�      $��2��6   $��2��6/   .+�5�      
  
�������$��5q�5�   3   $
��auto��6>   #���  5�   !   $��0q�5�   !   .��5�   "   �� $ Vertical=�6-   .+�5�   &   �� $ ��Tổng vật tư tồn kho��6,   6L   �� 5�   &   $��9999999��6,   6@   5�   #   5�      5�      �� 5�      $��0��6$   $��3��64   $��3��6A   5�      
  5�      $��4��6.   $��3��6;   .��5�      �� $
��Auto=�6+   $
��Auto=�6N   .��5�   "   �� $��Center=�6+   $��Center=�5�   +   ��5�   +   �� *
ListTonKho+ 5�   &   ��%}   '��       �� 5�   *   " ��60   .�5�   .   ��  � ��$ 15(�6I   6V   �� 5�   .    � ��
 ��=H�6c   5�   +   5�   '    �� View 5�   &   �� 5�   *   .P�5�   .   �� $	��STT��6=     �� DisplayMemberBinding  6J   �� *STT+6q   �� 5�   .   $��Tên vật tư��6=     6Q   �� *	Vattu.Ten+6~   �� 5�   .   $��
Hiện có��6=     6N   �� *Count+6x   �� 5�   .   $��Đơn vị tính��6=     6R   �� *DonViDo.Ten+6�   5�   +   5�   #   5�      5�      
  5�      $��5��5�      $��3��5�      $��Bottom=�5�      $��Right=�5�      .��5�      �� $ Vertical=�6)    5�   "   ��%}   '��       �� 5�   &   " ��6,   .�5�   .   ��  � ��$ 15(�6I   6V   �� 5�   .    � ��
 �  ��6Y   5�   +   5�   #   .+�5�   "   �� $1��+Thông tin tài khoản đang đăng nhập��6(   6W   �� 5�   "   $��	0 -40 0 0q�6+   ��5�   !   �� *ListAccInfo+$ TwoWay=�$ PropertyChanged=� 5�   &   �� 5�   *   ��.  .P�5�   .   ��   5   -   �� *TaiKhoan.HoVaTen+5  /   �� 5  .     5  -   �� *TaiKhoan.SDT+5  /   �� 5  .     5  -   �� *RoleTaiKhoan.Ten+5  /   5  +   5
  #   �� 5  "   $A ;Load lại danh sách thông tin tài khoản đăng nhập��6)   $
��auto��5  &   $��
Làm mới��5  )   #��[ 5  %   $��5q�5  %   $	��100��5  )   ��5  %   �� *LoadWindowCommand+$ TwoWay=�$ PropertyChanged=�5  #   5     5     5     5     5     5     5     5     