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

namespace NetworkNew.ViewModels
{
    public enum State { NotBegin, Play, Pause}
    
    class MainViewModel : BaseVM
    {   
        private State _state;        
        public State State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
                RaisePropertyChanged("OnMove");
            }
        }        

        public Configuration currentConfiguration;

        public MainViewModel()
        {
            History history = new History();
            currentConfiguration = new Configuration();
            RaisePropertyChanged("currentConfiguration");
            this.State = State.NotBegin;
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
                    this.State = State.Play;
                    foreach (ParticleGraphic particle in this._ParticleGraphics)
                    {
                        particle.Change = true;
                        particle.RaisePropertyChanged("Change");
                    }
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
                    this.State = State.Pause;
                    foreach (ParticleGraphic particle in this._ParticleGraphics)
                    {
                        particle.Change = false;
                        particle.RaisePropertyChanged("Change");
                    }
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
                    this.State = State.Play;
                    foreach (ParticleGraphic particle in this._ParticleGraphics)
                    {
                        particle.Change = true;
                        particle.RaisePropertyChanged("Change");
                    }
                },
                (obj) => (this.State.Equals(State.Pause) || this.State.Equals(State.NotBegin)));
            }
        }

        private Collection<ParticleGraphic> _ParticleGraphics = new Collection<ParticleGraphic>();



        //private ObservableCollection<Particle> Particles => new ObservableCollection<Particle>(currentConfiguration.particles);
        public ObservableCollection<ParticleGraphic> ParticleGraphics => new ObservableCollection<ParticleGraphic>(_ParticleGraphics);

        

    }
    public class ParticleGraphic : BaseVM
    {
        public string StartPoint { get; set; }
        public string Points { get; set; }
        public bool Change { get; set; }

        public ParticleGraphic(Particle particle)
        {
            this.StartPoint = string.Format(@"{0}, {1}", particle.X, particle.Y);
            this.Points = string.Format(@"{0},{1}", particle.X + 100, particle.Y + 100);
            this.Change = false;
        }

        public void move()
        {
            this.Change = true;
        }
    }
}
