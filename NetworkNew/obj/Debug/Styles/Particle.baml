   M S B A M L   `   `   `  ����E  ANetworkNew, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:NetworkNew.StylesNetworkNew.Styles  5      5clr-namespace:NetworkNew.ModelsNetworkNew.Models  5      =#clr-namespace:NetworkNew.ViewModelsNetworkNew.ViewModels  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   Particle  �� 
TargetType �� Property  UltraLightBaseBrush �� Value  ParticleConfiguration  DarkBaseBrush  ItemsPanelTemplate1  ParticleConfigurationStyle �� Triggers �� Binding �� Value N� EnterActions �� TargetProperty W� From		 W� To
 g� Duration N� ExitActions 0 ,TemplateParticleConfigurationParticleGraphic �� DataType+    %NetworkNew.ViewModels.ParticleGraphic  geometryPath E� IsClosed E� 
StartPoint � Points  Storyboard1 g� RepeatBehavior T� PathGeometry T� Source -	 )ParticleConfigurationStyleParticleGraphic'   !NetworkNew.ViewModels.StateAction   ReStart �� RelativeSource �� AncestorType �� Path ��Name   Pause �� BeginStoryboardName   Play�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      )localclr-namespace:NetworkNew.Styles  5      )modelclr-namespace:NetworkNew.Models  5      1	viewmodel#clr-namespace:NetworkNew.ViewModels  5      %�  &	        7  &	 �     7  &	     &	 @    &	     7  &	 m    &	 �    7  7  &		 �    7  7  �� 5      "  A�6   .�5   
   ��  � ��8   6!   �� 5   
    � ��$ 50��6#   �� 5	   
    � ��$ 50��6"   5
      �� 5      "  A�6*   .�5   
   ��  � ��8   6!   �� 5   
    � ��$ 50��6#   �� 5   
    � ��$ 50��6"   5      �� 5      .R�5   
   �� 5      �� 5       5   
   �� 5       6   �� *Flag+ True63    5      �� 5      .��5      �� .��5      W� $ Opacity�6.   $	 1.0X�6R   $		 0.0X�6]   $
 0:0:1D�6f   5      5       5      �� 5       .��5!      �� .��5"      W� $ Opacity�6.   $		 1.0X�6R   $
 0:0:0D�6[   5#      5$      5&      .�5)   
   ��  �@��Top 6'   �� *Y+�� 5*   
    �@��Left 6(   �� *X+5,      �� 5/      "   6H   .R�50   
   � .+�51      A� 8��  52      53      54      B� 56      .��58   
   E�  .  $ 10,10&�6&   .��59      � ' �    ����  ��In
��� �In
6   5:      5;      �� 5=      .��5>   
   T� $ (Canvas.Top)�6#   $
 0:0:5D�5?   (   $ Forever��69   8   $ Y=�5@   U   5A      T� 5B   
   $ (Canvas.Left)�6#   $
 0:0:5D�5C   (   $ Forever��69   8  $ X=�5D   U   5E      5F      �� 5J       5K   
   �� 5L      # Z 5N       5M      ��  �� *FindAncestor+" �$ DataContext.StateAction� 5O      �� 5P      $ StoryboardTest��6&   .��5Q      �� .��5R      T� $ (Canvas.Top)�67   $
 0:0:5D�5S   (   $ Forever��69   8   $ Y=�5T   U   5U      T� 5V      $ (Canvas.Left)�67   $
 0:0:5D�5W   (   $ Forever��69   8  $ X=�5X   U   5Y      5Z      5[      5]      �� 5^      # Z 5`       5_      ��  �� *FindAncestor+" �$ DataContext.StateAction� 5a      ?� 5b      $ StoryboardTest��6&   5d      �� 5e      # Z 5g       5f      ��  �� *FindAncestor+" �$ DataContext.StateAction� 5h      �� 5i      $ StoryboardTest��6'   5k      .�5n   
   ��  �@��Top 6'   �� *	model.Y+�� 5o   
    �@��Left 6(   �� *	model.X+5q      5s      