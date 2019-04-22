using NetworkNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NetworkNew.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MoveParticle.xaml
    /// </summary>
    public partial class MoveParticle : UserControl
    {
        public MoveParticle()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Коллекция шариков для дижения
        /// </summary>
        public ObservableCollection<ParticleGraphic> Source
        {
            get { return (ObservableCollection<ParticleGraphic>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                "Source", typeof(ObservableCollection<ParticleGraphic>), typeof(MoveParticle),
                new PropertyMetadata(new ObservableCollection<ParticleGraphic>(), (o, args) => ((MoveParticle)o).UpdateValues()));        
        
        private BeginStoryboard GetBeginStoryboard(int type, Shape shape, ParticleGraphic particle = null)
        {
            if (type.Equals(0))
            {
                // выцветание
                Storyboard storyboardOpacity = new Storyboard();
                DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(2)));
                Storyboard.SetTarget(doubleAnimation, shape);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Opacity)"));
                storyboardOpacity.Children.Add(doubleAnimation);
                storyboardOpacity.Name = "StoryboardOpacity";

                BeginStoryboard beginStoryboardOpacity = new BeginStoryboard();
                beginStoryboardOpacity.Storyboard = storyboardOpacity;
                beginStoryboardOpacity.Name = "begin" + storyboardOpacity.Name;
                return beginStoryboardOpacity;
            }
            if (type.Equals(1))
            {
                // простое маленькое движение
                Storyboard storyboardMove = new Storyboard();
                DoubleAnimation da = new DoubleAnimation(0, 200, new Duration(TimeSpan.FromSeconds(3)));
                Storyboard.SetTarget(da, shape);
                Storyboard.SetTargetProperty(da, new PropertyPath(Canvas.TopProperty));
                storyboardMove.Children.Add(da);
                da = new DoubleAnimation(0, 200, new Duration(TimeSpan.FromSeconds(3)));
                Storyboard.SetTarget(da, shape);
                Storyboard.SetTargetProperty(da, new PropertyPath(Canvas.LeftProperty));
                storyboardMove.Children.Add(da);
                storyboardMove.Name = "beginStoryboardMove";

                BeginStoryboard beginStoryboardMove = new BeginStoryboard();
                beginStoryboardMove.Storyboard = storyboardMove;
                beginStoryboardMove.Name = "begin" + storyboardMove.Name;
                return beginStoryboardMove;
            }
            if (type.Equals(2) && particle != null)
            {
                int TimeMove = 50;
                // наше движение
                // вычисление пути
                PathGeometry animationPath = new PathGeometry();
                PathFigure pFigure = new PathFigure();
                pFigure.StartPoint = particle.StartPoint;
                PolyLineSegment pBezierSegment = new PolyLineSegment();

                foreach (Point point in particle.Points)
                {
                    pBezierSegment.Points.Add(point);
                }

                pFigure.Segments.Add(pBezierSegment);
                animationPath.Figures.Add(pFigure);
                // Freeze the PathGeometry for performance benefits.
                animationPath.Freeze();

                // движение
                // поток движения
                Storyboard storyboard = new Storyboard();

                DoubleAnimationUsingPath animationX = new DoubleAnimationUsingPath()
                {
                    PathGeometry = animationPath,
                    Duration = new Duration(TimeSpan.FromSeconds(TimeMove)),
                    Source = PathAnimationSource.X
                };
                Storyboard.SetTarget(animationX, shape);
                Storyboard.SetTargetProperty(animationX, new PropertyPath(Canvas.LeftProperty));
                storyboard.Children.Add(animationX);

                DoubleAnimationUsingPath animationY = new DoubleAnimationUsingPath()
                {
                    PathGeometry = animationPath,
                    Duration = new Duration(TimeSpan.FromSeconds(100)),
                    Source = PathAnimationSource.Y,
                };
                Storyboard.SetTarget(animationY, shape);
                Storyboard.SetTargetProperty(animationY, new PropertyPath(Canvas.TopProperty));
                storyboard.Children.Add(animationY);
                storyboard.Name = "StoryboardMain";

                BeginStoryboard beginStoryboard = new BeginStoryboard();                
                beginStoryboard.Storyboard = storyboard;
                beginStoryboard.Name = "begin" + storyboard.Name;
                return beginStoryboard;
            }

            return null;
        }

        private TriggerBase GetTrigger(int type, Shape shape)
        {
            if (type.Equals(0))
            {
                // на щелчок по кнопке
                return new EventTrigger(EventManager.GetRoutedEvents()[9]);
            }
            if (type.Equals(1))
            {
                // на изменение значения, на паузу - сомое простое
                DataTrigger dataTrigger = new DataTrigger()
                {
                    Binding = new Binding() {
                        Path = new PropertyPath("StateAction")
                    }              
                };
                dataTrigger.Value = StateAction.Pause;
                return dataTrigger;
            }
            if (type.Equals(2))
            {
                // на изменение значения, внутри данного класса
                DataTrigger dataTrigger = new DataTrigger()
                {
                    Binding = new Binding()
                    {
                        Path = new PropertyPath("StateAction")
                    }
                };
                dataTrigger.Value = StateAction.ReStart;
                return dataTrigger;
            }
            return null;
        }

        private DataTrigger dataTriggerChangeState(PropertyPath propertyPath, object beginValue)
        {
            DataTrigger dataTrigger = new DataTrigger()
            {
                Binding = new Binding()
                {
                    Path = propertyPath
                }
            };
            dataTrigger.Value = beginValue;
            return dataTrigger;
        }

        private enum TypeAction {
            /// <summary>
            /// Начала движени я с 0
            /// </summary>
            Begin,
            /// <summary>
            /// начало движения и предыдущего места
            /// </summary>
            Play,
            /// <summary>
            /// поуза в воспроизведении
            /// </summary>
            Pause };
        /// <summary>
        /// для случаев, когда нам необходимо задать движение на изменение какого-нибудь поля
        /// </summary>
        /// <param name="type"></param>
        /// <param name="shape"></param>
        /// <param name="particle"></param>
        /// <returns></returns>
        private Dictionary<TypeAction, TriggerAction> GetComplexAction(int type, Shape shape, ParticleGraphic particle)
        {
            // коллеция движений
            Dictionary<TypeAction, TriggerAction> result = new Dictionary<TypeAction, TriggerAction>();            
            BeginStoryboard beginStoryboard = GetBeginStoryboard(type, shape, particle);
            result.Add(TypeAction.Begin, beginStoryboard);            
            PauseStoryboard pauseStoryboard = new PauseStoryboard() { BeginStoryboardName = beginStoryboard.Name };
            result.Add(TypeAction.Pause, pauseStoryboard);
            ResumeStoryboard resumeStoryboard = new ResumeStoryboard() { BeginStoryboardName = beginStoryboard.Name };
            result.Add(TypeAction.Play, resumeStoryboard);            
            return result;
        }

        private Dictionary<TypeAction, DataTrigger> GetComplexTrigger(int type, Shape shape)
        {
            // возвращаем коллекцию триггеров
            Dictionary<TypeAction, DataTrigger> result = new Dictionary<TypeAction, DataTrigger>();
            // старт движения
            if (type.Equals(0))
            {
                // старт движения
                DataTrigger trigger = dataTriggerChangeState(new PropertyPath("StateAction"), StateAction.ReStart);
                result.Add(TypeAction.Begin, trigger);
                
                // возобновление движения
                trigger = dataTriggerChangeState(new PropertyPath("StateAction"), StateAction.Play);
                result.Add(TypeAction.Play, trigger);
                
                // приостановка движения
                trigger = dataTriggerChangeState(new PropertyPath("StateAction"), StateAction.Pause);  
                result.Add(TypeAction.Pause, trigger);                
            }
            return result;
        }       

        void UpdateValues()
        {
            foreach (ParticleGraphic particle in this.Source)
            {
                // сам элемент для отрисовки
                Ellipse ellipse = new Ellipse();
                // изначальное положение частицы
                ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                ellipse.Width = 50;
                ellipse.Height = 50;
                Canvas.SetTop(ellipse, particle.StartPoint.X);
                Canvas.SetLeft(ellipse, particle.StartPoint.Y);

                var style = new Style(typeof(Ellipse));

                // триггер простой или комплексный
                bool isSimple = false;
                int typeBeginStoryboard = 2;
                int typeTrigger = 0;

                if (isSimple)
                {
                    BeginStoryboard beginStoryboard = GetBeginStoryboard(typeBeginStoryboard, ellipse, particle);
                    TriggerBase trigger = GetTrigger(typeTrigger, ellipse);

                    if (trigger.GetType().Equals(typeof(EventTrigger)))
                    {
                        ((EventTrigger)trigger).Actions.Add(beginStoryboard);
                    }
                    else
                    {
                        ((DataTrigger)trigger).EnterActions.Add(beginStoryboard);
                    }
                    // The name of the Storyboard must be registered so the actions can find it
                    style.RegisterName(beginStoryboard.Storyboard.Name, beginStoryboard.Storyboard);
                    style.Triggers.Add(trigger);
                }
                else
                {
                    Dictionary<TypeAction, TriggerAction> actions = GetComplexAction(typeBeginStoryboard, ellipse, particle);
                    Dictionary<TypeAction, DataTrigger> triggers = GetComplexTrigger(typeTrigger, ellipse);
                    // инициатор движения
                    TriggerAction triggerAction;
                    if (!actions.TryGetValue(TypeAction.Begin, out triggerAction) || triggerAction == null || !triggerAction.GetType().Equals(typeof(BeginStoryboard)))
                        return;
                    style.RegisterName(((BeginStoryboard)triggerAction).Name, ((BeginStoryboard)triggerAction));
                    foreach (KeyValuePair<TypeAction, DataTrigger> pair in triggers)
                    {
                        TriggerAction action = null;
                        if (actions.TryGetValue(pair.Key, out action) && action != null)
                        {
                            pair.Value.EnterActions.Add(action);
                        }
                        style.Triggers.Add(pair.Value);
                    }
                }
                // подключение триггера к стилю создаваемого объекта
                
                ellipse.Style = style;
                ellipse.MouseDown += (object sender, MouseButtonEventArgs e) => {                    
                    //storyboardMove.Begin();
                };
                MyCanva.Children.Add(ellipse);
            }
        }
    }
}

