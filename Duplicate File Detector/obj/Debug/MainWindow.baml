   M S B A M L   `   `   `  ����R  NDuplicate File Detector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Duplicate_File_DetectorDuplicate_File_Detector  5   	   (    "Duplicate_File_Detector.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Duplicate_File_Detector     5   	     � Title$  Duplicate File Detector��5   	   $	��450��6)   $	��800��66   .��5	      �  � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ MyStackPanel��5      $��0��5      $��	10,10,0,0q�5       �� Orientation$ 
Horizontal=�5      $��Center=�5      $��Center=�5      .+�5      �� -   5         ��Name$ Folder_Name��5      $��0,0,0,0q�5      $��20(�5      $��Folder Name:��6   �� 5      -   5         ��Name$ txtFilePath��5      $	��200��5      $��15,5,0,5q�5      $��Center=�5       $��18(�5!       J� 	AllowDrop . ��5#      �� *
FolderName+62   �� 5&      -   5        $��
btnSearch1��5&      
���   �$��1q�5(      $	��100��5)      $��50,5,0,5q�5*      $��18(�5+      {�5,      �� *EnableSearchBtn+��5.      �� *ButtonContent+50      �� 53   
   -   5         ��Name$ gridSplitter��53      $��1��6-   $
��1000��6:   $��3��6G   � 56   
   $��2��6   .+�58      �� -   5        $ label��58      $��Duplicate Files:��59      $
��Left=�5:      $��	10,10,0,0q�5;      $	��Top=�5<      $	��153��5=      $��43��6    $��18(�5>      �� 5B      $��10,50,10,10q�6=   $��Stretch=�6S   $
��Auto��5C      $
��Auto��6%   ��5B      �� *ListOfDupFile+��5D      �� 5E      .R�5F      �� $��border��6!   
����ح�$��1q�6G    �� Padding$ 5q�6[   $��5q�6g   ._�5G      �� .+�5H   "   �� $ 
Horizontal=�6-   .+�5I   &   �� $��14(�60   $��
CheckSum: ��6?   6V   �� 5J   &   $��14(�60   ��6>   �� 	 �� Path$	 CheckSum�5K   #   �� 5L   "   -   5        $��Stretch=�5M   +   
�������$
��Auto��5N   +   ��5L   +   �� $	 	FilesPath���5P   &   �� 5Q   *   .R�5R   .   �� .+�5S   2   �� ��6<   �� 5T   /   5U   +   5W   #   5X      5Y      5Z      5\      5]      5_      5`      