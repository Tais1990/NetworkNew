using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkNew.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Svg.xaml
    /// </summary>
    public partial class Svg : UserControl
    {
        public Svg()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Цвет Иконки
        /// </summary>
        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        /// <summary>
        /// Цвет Иконки
        /// </summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color", typeof(Brush), typeof(Svg),
                new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 0, 0)), (o, args) => ((Svg)o).UpdateValues()));
        /// <summary>
        /// Размер Иконки
        /// </summary>
        public int HeightSvg
        {
            get { return (int)GetValue(HeightSvgProperty); }
            set { SetValue(HeightSvgProperty, value); }
        }
        /// <summary>
        /// Размер Иконки
        /// </summary>
        public static readonly DependencyProperty HeightSvgProperty =
            DependencyProperty.Register(
                "HeightSvg", typeof(int), typeof(Svg),
                new PropertyMetadata(10, (o, args) => ((Svg)o).UpdateValues()));

        /// <summary>
        /// Отсутпы для иконки
        /// </summary>
        public int MarginSvg
        {
            get { return (int)GetValue(MarginSvgProperty); }
            set { SetValue(MarginSvgProperty, value); }
        }
        /// <summary>
        /// Отсутпы для иконки
        /// </summary>
        public static readonly DependencyProperty MarginSvgProperty =
            DependencyProperty.Register(
                "MarginSvg", typeof(int), typeof(Svg),
                new PropertyMetadata(0, (o, args) => ((Svg)o).UpdateValues()));

        /// <summary>
        /// Строка с кодом картинки
        /// </summary>
        public Geometry DataSvg
        {
            get { return (Geometry)GetValue(DataSvgProperty); }
            set { SetValue(DataSvgProperty, value); }
        }
        /// <summary>
        /// Строка с кодом картинки
        /// </summary>
        public static readonly DependencyProperty DataSvgProperty =
            DependencyProperty.Register(
                "DataSvg", typeof(Geometry), typeof(Svg),
                new PropertyMetadata(Geometry.Empty, (o, args) => ((Svg)o).UpdateValues()));

        void UpdateValues()
        {
            Outward.Height = HeightSvg;
            Outward.Width = HeightSvg;
            Inside.Margin = new Thickness(MarginSvg);
            DataPath.Fill = Color;
            DataPath.Data = DataSvg;
        }
    }
}
