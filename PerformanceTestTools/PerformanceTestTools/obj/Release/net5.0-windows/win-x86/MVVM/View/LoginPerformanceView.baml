   M S B A M L   `   `   `  ���� O  KPerformanceTestTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:PerformanceTestTools.MVVM.ViewPerformanceTestTools.MVVM.View  E'clr-namespace:PerformanceTestTools.CorePerformanceTestTools.Core  9    3PerformanceTestTools.MVVM.View.LoginPerformanceView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:PerformanceTestTools.MVVM.View     8Core'clr-namespace:PerformanceTestTools.Core     .���� .+��� $��로그인 테스트�����#112d4e$��28(�$
��Left=�$��0,0,0,0q���   �� Orientation$  
Horizontal=�$��0,0,0,10q�.+��� $	��350��$	��200������  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,2&�.���  � Color$ #5bc3ff�� � Offset$	 0.0Q�� $ #3aa0ff��$ 1Q� J� Clip ��  �� RadiusX$ 10Q� �� RadiusY$ 10Q� �� Rect$ 0,0,350,200�._�� .+��� .+��� $��로그인 정보�����#112d4e$��20(�$��
20,10,10,0q��� $M��G테스트를 위해서 만든 아이디/패스워드를 입력하세요�����#112d4e$��12(�$��
20,10,10,0q��� -   	 ��Name$	 UserID��   ModernTextbox#��[  $
��Left=�$��15q�G� -   
 G�Name$
 UserPW��  Passwordbox#��[ $
��Left=�$
��15 0q�$��5q����#cfcfcf+  %PerformanceTestTools.Core.ImageButton  -     Name$ startBtn��+   %PerformanceTestTools.Core.ImageButton   Click$ startBtn_Click��   Image$* $/Images/outline_login_black_48dp.png��$��
200,50,0,0q�$	��100��$	��100���� $	��300��$	��200�� �� CornerRadius$ 10��$��45,0,0,0q����� $	 0,0&�$	 1,2&�.��� $ #ffc554��$	 0.0Q�� $ #ffc73a��$ 1Q� �� $ 10Q�$ 10Q�$ 0,0,350,200�._�� .+��� .+��� $��스레드 정보�����#112d4e$��20(�$��
20,10,10,0q��� $9��3스레드 개수를 입력하세요
최대 2000개 �����#112d4e$��12(�$��
20,10,10,0q��� $  
Horizontal=�$��	20 10 0 0q�.+��� -   $	 ThreadCount��  ThreadTextbox#��[  �� 	MaxLength$ 4���� $  Vertical=�.+�  -   $ UpCount��$ UpCount_Click��$2 ,/Images/outline_arrow_drop_up_black_18dp.png��$��5,2,0,0q�  -   $ 	DownCount��$ DownCount_Click��$4 ./Images/outline_arrow_drop_down_black_18dp.png��$��5,0,0,0q��� $��85��$	��100��$+��%/Images/outline_memory_black_48dp.png��$��Right=�$��Bottom=� J� RenderTransformOrigin$ 0.5,0.5&�$��	0,0,22,29q���  � 	Resources  �� 
TargetType �� Property �� Value��%3   '��       �� " ��.���  � ��$ 15,0,0,0q�.+��� $��결과�����#112d4e$��20(�$
��Left=�$��0,0,0,10q��� $  
Horizontal=�.+��� $	��150��$	��150�����#3361e0$ 10��$��0q�._��� $��
성공 : 0��$��35(����#dbe2ef �� TextAlignment$ Center=�$��Center=��� $	��150��$	��150�����#e54747$ 10��._��� $��
실패 : 0��$��35(����#dbe2ef$ Center=�$��Center=��� $	��150��$	��150�����#112d4e$ 10��._��� -    ��Name$ Logview�����#112d4e$��3q�$��로그��  LogViewButton#��[ 