using NetworkNew.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NetworkNew.UserControls
{
    public class MoveParticleStyleSelector : StyleSelector
    {
        public Style CurrentStyle { get; set; }
        public override Style SelectStyle(object item, DependencyObject container)
        {
            Style newStyle = CurrentStyle;
            return newStyle;
        }
    }
}
