   M S B A M L   `   `   `  ����D  @QuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QuanLyKho	QuanLyKho  5   	   O,clr-namespace:QuanLyKho.UserControlQuanLyKhoQuanLyKho.UserControlQuanLyKho  5	   
       QuanLyKho.UnitWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:QuanLyKho     5   	   ;uc,clr-namespace:QuanLyKho.UserControlQuanLyKho     5	   
   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5        $��
unitWindow��5   	      MaterialDesignBody#���   5   	   $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � 
ResizeMode$ 	CanResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    
 UnitVM � DataContext# [ 5   	    � Title$ Đơn vị đo��5   	   $	��450��6   $	��800��6(   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      1   +QuanLyKho.UserControlQuanLyKho.ControlBarUC  
 � Tag 6   �� *Title+	 �� ElementName$	 
unitWindow��6N   5      � 5    
   $��1��6   
 � ColumnDefinitions
 5!      �� 5"      $	��250��6#   64   �� 5#      $
��auto �6#   62   �� 5$      6%   .+�5(      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�62   .��5)      �� $��Center=�6   $��Center=�6:   .+�5*      �� $��5q�5,      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên đơn vị đo��5-       % !MaterialDesignFloatingHintTextBox#��[ 5.      ��5+      �� *
TenDonViDo+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�.��5.   T   5/      50      51        52      $��1��6"   $��5q�62   .��53      �� $��Center=�54      $��Center=�55      .+�56      �� $��Thêm��58      $��5q�59      $	��100��5:      ��57      �� *
AddCommand+5;      �� 5C      $��Tắt��5E      $��5q�5F      $	��100��5G      ��5D      �� *DeleteCommand+5H      �� 5I      $��Kích hoạt��5K      $��5q�5L      $	��100��5M      ��5J      �� *UnDeleteCommand+5N      5O      5P        5R      $��2��5S      $��5q�5T      $��Stretch=�5U      .��5V      �� $
��Auto=�5W      .��5X      �� $��Center=�6   ��5Y      �� *ListDonViDoes+$ TwoWay=�$ PropertyChanged=� �� SelectedItem 5Z      �� *SelectedItem+$ TwoWay=�$ PropertyChanged=� � 	Resources 5[       �� 
TargetType �� Property �� Value��%}   '��       �� 5\      " ��6$   .�5]   "   ��  � ��$ 15(�6=   6J   �� 5^   "    � ��
 ��=H�6W   5_      5`       �� View 5a      �� 5b      .P�5c   "   �� $	��STT��62    �� DisplayMemberBinding 6?   �� *STT+6f   �� 5d   "   $��Tên đơn vị đo��61    6I   �� *DonViDo.Ten+6x   �� 5e   "   $��Trạng thái��61    6E   �� *	TrangThai+6r   5f      5h      5i      5j      5k      5l      5m      