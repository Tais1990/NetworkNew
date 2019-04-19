using System;
using System.Windows;
using System.Windows.Controls;

namespace NetworkNew.UserControls
{    
    public class MoveParticle : ItemsControl
    {
        Style DefaulStyle;
        private object GetResourse(string Namespace, string Resources)
        {
            foreach (ResourceDictionary resourceDictionary in Application.Current.Resources.MergedDictionaries)
            {
                if (resourceDictionary.Source.ToString().Equals(Namespace))
                {
                    return resourceDictionary[Resources];
                }
            }
            return null;
        }
        static MoveParticle()
        {
           DefaultStyleKeyProperty.OverrideMetadata(typeof(MoveParticle), new FrameworkPropertyMetadata(typeof(MoveParticle)));
            //System.Diagnostics.Debugger.Launch();
        }
        
        public override void OnApplyTemplate()
        {
            //System.Diagnostics.Debugger.Launch();
            
            base.OnApplyTemplate();
            //new Uri("Themes\Generic.xaml")
            //System.Diagnostics.Debugger.Launch();
            //this.ItemsSource;
            Style DefaultStyle = (Style)GetResourse("Themes\\Generic.xaml", "DefaultStyle");
            //foreach(this.Items)
        }

        public override void EndInit()
        {
            base.EndInit();
            //System.Diagnostics.Debugger.Launch();
        }

        public override void BeginInit()
        {
            //System.Diagnostics.Debugger.Launch();
            base.BeginInit();
        }







    }
}
