   M S B A M L   `   `   `  ����K  GAssignment4_MVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Assignment4_MVVM.ViewAssignment4_MVVM.View  5   	   2    ,Assignment4_MVVM.View.MembershipUpdateDelete   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Assignment4_MVVM.View     5   	   -   5        $��wndMembershipUpdate��5        � Title$  Update/Delete Membership��5   	      childwindowStyle#��[  6*    � WindowStartupLocation$ CenterScreen=�6T   .��5	      �  � DataContext 6   �� *MemberViewModel+  Locator �� Source# [  � ColumnDefinitions 5
   
   �� 5      $��1* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6     
labelStyle#��[ 6-   .��6Q   First Name:6^   �� 5   
   $��1��6   $��0��6   #��[ 6-   .��6Q   
Last Name:6]   �� 5   
   $��2��6   $��0��6   #��[ 6-   .��6Q   Email:6Y   �� 5   
   $��0��6   $��1��6     textboxStyle#��[ 6/   ��5   	   �� *	FirstName+6&   �� 5   
   $��1��6   $��1��6   #��[ 6/   ��5   	   �� *
LastName+6%   �� 5   
   $��2��6   $��1��6   #��[ 6/   ��5   	   �� *Email+6"   � 5   
   $��3��6   $��2��6!     wrapPanelMainStyle#��[ 65   .+�5      ��   buttonStyle#��[ 6   ��6:   �� *UpdateCommand+.��6\   Update5      �� 5       #��[ 6   ��6:   �� *DeleteCommand+.��6\   Delete5!      �� 5"      #��[ 6   ��6:   �� *ExitWindowCommand+ �� Mode$ OneWay=���5#      ��  �� ElementName$ wndMembershipUpdate��.��6J   Cancel5$      5%      5&      5'      