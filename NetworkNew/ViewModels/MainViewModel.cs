using NetworkNew.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkNew.ViewModels
{
    /// <summary>
    /// состояние системы
    /// </summary>
    public enum State { NotBegin, Play, Pause}
    /// <summary>
    ///  переход между состояниями системы
    /// </summary>
    public enum StateAction { ReStart, Play, Pause, NotBegin}
    
    class MainViewModel : BaseVM
    {
        /// <summary>
        /// переход между состояниями системы
        /// </summary>        
        public StateAction StateAction { get; set; }
        /// <summary>
        /// состояние системы
        /// </summary>
        private State _state;
        /// <summary>
        /// состояние системы
        /// </summary>
        public State State
        {
            get { return _state; }
            set
            {                
                _state = value;
                RaisePropertyChanged("State");
                RaisePropertyChanged("OnMove");
                RaisePropertyChanged("StateAction");
            }
        }        

        public Configuration currentConfiguration;

        public MainViewModel()
        {
            History history = new History();
            currentConfiguration = history.history.FirstOrDefault();
            RaisePropertyChanged("currentConfiguration");
            this.State = State.NotBegin;
            this.StateAction = StateAction.NotBegin;
            // задаём изначальное положение частиц
            foreach (Particle particle in currentConfiguration.particles)
            {
                this._ParticleGraphics.Add(new ParticleGraphic(particle));
            }

            // реализуем движение
            foreach (Configuration conf in history.history)
            {
                for (int i = 0; i < conf.particles.Count; i++)
                {
                    this._ParticleGraphics[i].Points.Add(new Point(conf.particles[i].X, conf.particles[i].Y));
                }
            }           
            
            /*
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(1000).Wait();                    
                }
            });
            */
        }

        public bool OnMove => State.Equals(State.Play);


        public DelegateCommand ToPlay
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    // вычисление состояния перехода
                    this.StateAction = StateAction.Play;
                    // само состояние
                    this.State = State.Play;
                },
                (obj) => this.State.Equals(State.Pause));
            }
        }
        public DelegateCommand ToPause
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    // вычисление состояния перехода
                    this.StateAction = StateAction.Pause;
                    // само состояние
                    this.State = State.Pause;
                },
                (obj) => this.State.Equals(State.Play));
            }
        }
        public DelegateCommand ToReStart
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    // вычисление состояния перехода
                    this.StateAction = StateAction.ReStart;
                    // само состояние
                    this.State = State.Play;
                },
                (obj) => (this.State.Equals(State.Pause) || this.State.Equals(State.NotBegin)));
            }
        }

        private Collection<ParticleGraphic> _ParticleGraphics = new Collection<ParticleGraphic>();
        public ObservableCollection<ParticleGraphic> ParticleGraphics => new ObservableCollection<ParticleGraphic>(_ParticleGraphics);
        public CollectionParticleGraphic collectionParticleGraphic => new CollectionParticleGraphic((Collection<ParticleGraphic>)ParticleGraphics);

    }
    public class ParticleGraphic : BaseVM
    {
        public Particle model { get; set; }
        public Point StartPoint { get; set; }
        public Collection<Point> Points { get; set; }
        public ParticleGraphic(Particle particle)
        {
            this.model = particle;
            this.StartPoint = new Point(particle.X, particle.Y);
            this.Points = new Collection<Point>();            
        }
    }
    /// <summary>
    /// Инкапсляция коллекции графических объектов
    /// </summary>
    public class CollectionParticleGraphic
    {
        public Collection<ParticleGraphic> particles { get; set; }
        public CollectionParticleGraphic(Collection<ParticleGraphic> collection)
        {
            this.particles = collection;
        }
    }
}
