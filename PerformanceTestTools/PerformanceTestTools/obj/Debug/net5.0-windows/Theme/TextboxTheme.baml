   M S B A M L   `   `   `  ����O  KPerformanceTestTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:PerformanceTestTools.CorePerformanceTestTools.Core  5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ModernTextbox7    1PerformanceTestTools.Core.WaterMarkTextBoxMonitor �� Property �� Value �� 
TargetType � 	Resources  Key g� Duration	 W� To �� TargetProperty �� 
TargetName	 W� From
 �� CornerRadius ��Name ��Name    WatermarkText J� Opacity J� IsHitTestVisible �� Triggers P� Property P� Value �� 
TargetName ]� 
Conditions �� Property �� Value N� EnterActions  enterGotFocus N� ExitActions  exitGotFocus  enterHasText  exitHasText�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      0Core'clr-namespace:PerformanceTestTools.Core  5      %�  &	        7  7  7  7  �� 5      "  ��6   .�5   
   ��  �@  IsMonitoring$
 True��6F   �� 5   
    �@  WatermarkText$ ID��6G   �� 5   
    � �� 5	      �� 5
      " ��6"    5      ���� 5      enterGotFocus .��5      W� $ 0:0:0.1D�6.    .76A   $ Opacity�6I   $ Message��6m   5      �� 5      exitGotFocus .��5      W� $ 0:0:0.1D�6.   $ Opacity�6A   $ Message��6e   5      �� 5      enterHasText .��5      W� $ 0:0:0.1D�6.   	 .76A    06K   $ Opacity�6R   $ Message��6v   5      �� 5      exitHasText .��5      W� $ 0:0:0.1D�6.   $ Opacity�6A   $ Message��6e   5      5      .R�5      �� $��Bd��6   $
 10��5      ���#353340$	��200��5      $��40��6)   ._�5      � .+�5      �� $ PART_ContentHost��6+   $��Center=�6E   $��1q�6`   �� 5      $ Message��6&   $��1q�5    &   #��z 5!   &   $��0q�5"   &   $ 1Q�5#   &   
������ $��Center=�5%   &   $ False��5&   &   $��5q�5'   &   ���#cfcfcf5(   &   5)      5*       5+      P� 5,       � {�$ false��67   . �5-      �� $ Bd��6%    � �� �#3533406K   �� 5.       � �� �#cfcfcf6;   5/      P� 50       � {�$
 True��67   . �51      �� $ Bd��6B    �@J�Opacity$ 1Q�68   6B   52      P� 53       �@J�IsMouseOver$
 True��69   . �54      ��  � �� �	#FF5C9BF76C   55      P� 56       �@J�IsKeyboardFocused$
 True��6?   . �57      ��  � �� �	#FF5C9BF76C   58      ]� 59       5:      �� 5;   "    �@  HasText$ False��6\   �� 5<   "    �@J�	IsFocused$
 True��6A    5>      �� 5?   "   8��  62    5A      �� 5B   "   8�� 62   5D      P� 5E       �@  HasText$
 True��6R    5F      �� 5G   "   8�� 62    5I      �� 5J   "   8�� 62   5L      5N      5P      5Q      5R      