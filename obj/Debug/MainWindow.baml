   M S B A M L   `   `   `  ����>  :NEB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NEBNEB  5   	       NEB.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:NEB     5   	     � Title$  Продажи��5   	   $	��400��6   $	��600��6&   $	��400��66   $	��650��6F   $	��618��6R   $
��1200��6a   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��SaleGrid��5
         AutoGenerateColumns .  $��140,10,9.6,39.4q�6o      SelectionMode$ Single=�6�   ��6#   ��  �� Source$ SalesCollection��   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      MinWidth$ 30Q�61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *ID+  5      $ 
Товар��6%   $	 130Q�64    6C   �� *Product.Title+  5      $ Продавший��6%   $	 130Q�68    6G   �� *	Manager+  5      $ 
Время��6%   $	 100Q�64    6C   �� *
SoldTime+  5      $ 
Сумма��6%   $ 60Q�64    6B   �� *
SumMoney+5      �� 5   
   $*��$Общая сумма продаж: ��6   $��142,0,0,8.4q�6/   $��26��6D   $��Bottom=�6P   $
��Left=�6k   $	��137��6�   �� 5   
   -   5        $	��Sum��5      $�� ��6   $��294,0,0,8.4q�6&   $��26��6;   $��Bottom=�6G   $
��Left=�6b   $	��182��6}   �� 5   
   -   5        $��AddSale��5      $%��Добавить продажу��6    $
��Left=�6;   $��	10,10,0,0q�6V   $	��Top=�6i   $	��114��6�   $��31��6�   6�   �� 5   
   -   5        $��EditSale��5      $$��Отредактировать��6!   $
��Left=�6;   $��	10,84,0,0q�6V   $	��Top=�6i   $	��114��6�   $��32��6�   6�   �� 5   
   -   5        $��OpenCatalogs��5      $!��К справочникам��6%   $
��Left=�6>   $��31��6Y   $��
10,163,0,0q�6e   $	��Top=�6y   $	��114��6�   6�   5      5      