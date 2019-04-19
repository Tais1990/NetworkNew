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
            currentConfiguration = new Configuration();
            RaisePropertyChanged("currentConfiguration");
            this.State = State.NotBegin;
            this.StateAction = StateAction.NotBegin;
            foreach (Particle particle in currentConfiguration.particles)
            {
                this._ParticleGraphics.Add(new ParticleGraphic(particle));
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

    }
    public class ParticleGraphic : BaseVM
    {
        public Particle model { get; set; }
        public Point StartPoint { get; set; }
        public string Points { get; set; }
        public ParticleGraphic(Particle particle)
        {
            this.model = particle;
            this.StartPoint = new Point(particle.X, particle.Y);
            this.Points = string.Format(@"{0},{1} {2},{3}", particle.X + 100, particle.Y + 100, particle.X + 50, particle.Y + 50);
        }        
    }
}
