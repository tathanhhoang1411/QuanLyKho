   M S B A M L   `   `   `  ����D  @QuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QuanLyKho	QuanLyKho  5   	   O,clr-namespace:QuanLyKho.UserControlQuanLyKhoQuanLyKho.UserControlQuanLyKho  5	   
       QuanLyKho.VattuWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:QuanLyKho     5   	   ;uc,clr-namespace:QuanLyKho.UserControlQuanLyKho     5	   
   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5        $��vattuWindow��5   	      MaterialDesignBody#���   5   	   $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � 
ResizeMode$ 	CanResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	     VattuVM � DataContext# [ 5   	    � Title$ 	Vật tư��5   	   $	��600��6   $
��1000��6%   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      1   +QuanLyKho.UserControlQuanLyKho.ControlBarUC  
 � Tag 6   �� *Title+	 �� ElementName$	 vattuWindow��6O   5      � 5    
   $��1��6   
 � ColumnDefinitions
 5!      �� 5"      $	��250��6#   64   �� 5#      $
��auto �6#   62   �� 5$      6%   .+�5(      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�62   .��5)      �� $��Center=�6   $��Center=�6:   .+�5*      �� $��5q�5,      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên vật tư��5-       % !MaterialDesignFloatingHintTextBox#��[ 5.      ��5+      �� *
TenVatTu+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�.��5.   T   5/      �� 50      $��5q�52      $ 
Đơn vị��53        MaterialDesignComboBox#��[ 54       �� DisplayMemberPath$ DonViDo.Ten��55       �� Text 51      �� *
TenDonViDo+$ PropertyChanged=�$ TwoWay=���56      �� *
ListUnit+$ PropertyChanged=�$ TwoWay=� �� SelectedItem 57      �� *SelectedItemUnit+$ TwoWay=�$ PropertyChanged=�59      �� 5;      $��5q�5=      $ Nhà cung cấp��5>      #��[ 5?      $ NhaCungCap.Ten��5@       5<      �� *TenNhaCungCap+$ PropertyChanged=�$ TwoWay=���5A      �� *
ListSupp+$ PropertyChanged=�$ TwoWay=� 5B      �� *SelectedItemSupp+$ TwoWay=�$ PropertyChanged=�5D      5F      5G        5H      $��1��6"   $��5q�62   .��5I      �� $��Center=�5J      $��Center=�5K      .+�5L      �� $��Thêm��5N      $��5q�5O      $	��100��5P      ��5M      �� *
AddCommand+$ TwoWay=�$ PropertyChanged=�5Q      �� 5R      $��Sửa��5T      $��5q�5U      $	��100��5V      ��5S      �� *EditCommand+$ TwoWay=�$ PropertyChanged=�5W      �� 5X      $��Tắt��5Z      $��5q�5[      $	��100��5\      ��5Y      �� *DeleteCommand+$ TwoWay=�$ PropertyChanged=�5]      �� 5^      $��Kích hoạt��5`      $��5q�5a      $	��100��5b      ��5_      �� *UnDeleteCommand+$ TwoWay=�$ PropertyChanged=�5c      �� 5d      $��
Làm mới��5f      $��5q�5g      $	��100��5h      ��5e      �� *ReloadCommand+$ TwoWay=�$ PropertyChanged=�5i      5j      5k        5m      $��2��5n      $��5q�5o      $��Stretch=�5p      .��5q      �� $
��Auto=�5r      .��5s      �� .+�5t      ��  �� Orientation$ 
Horizontal=�6%   .+�5u      �� $��5q�5v   #   $ Tên vật tư��5x         MaterialDesignFilledTextBox#��[ 5y       ��5w   "   �� *
SearchName+$ PropertyChanged=�$ TwoWay=�.��5y   U   5z      �� 5{      $��5q�5|   #   $ 
Đơn vị��5~       #��[ 5       ��5}   "   �� *SearchDonVi+$ PropertyChanged=�$ TwoWay=�.��5   U   5�      �� 5�      $ 
Horizontal=�6)   .+�5�   "   d�  ( $MaterialDesignActionDarkToggleButton#��[ 6/    d� 	IsChecked$
 TrueY�6n   6�   �� 5�   "   $��Trạng thái��6(   $��Center=�6=   6Z   5�      5�      �� 5�      $��Center=�6#   ��5�      �� *
ListVattus+$ PropertyChanged=�$ TwoWay=� 5�      �� *SelectedItem+$ TwoWay=�$ PropertyChanged=� � 	Resources 5�       �� 
TargetType �� Property �� Value��%}   '��       �� 5�   "   " ��6(   .�5�   &   ��  � ��$ 15(�6A   6N   �� 5�   &    � ��
 ��=H�6[   5�   #   5�       �� View 5�      �� 5�   "   .P�5�   &   �� $	��STT��65    �� DisplayMemberBinding 6B   �� *STT+6i   �� 5�   &   $��	Vật tư��65    6E   �� *	VatTu.Ten+6r   �� 5�   &   $��Đơn vị tính��65    6J   �� *DonViDo.Ten+6y   �� 5�   &   $��Trạng thái��65    6I   �� *	TrangThai+6v   �� 5�   &   $��
Hiện có��65    6F   �� *Count+6o   5�   #   5�      5�      5�      5�      5�      5�      5�      