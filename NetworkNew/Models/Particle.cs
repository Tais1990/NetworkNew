using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkNew.Models
{
    public class Particle : BaseVM
    {        
        public double X { get; set; }
        public double Y { get; set; }

        public Particle(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Particle(Particle particle)
        {
            this.X = particle.X;
            this.Y = particle.Y;
        }

        public Particle Move(double x, double y)
        {
            this.X = (this.X + x + 300) % 300;
            this.Y = (this.Y + y + 300) % 300;
            return this;
        }
    }
    class Configuration : BaseVM
    {
        public Collection<Particle> particles { get; set; }
        public int currentTime { get; set; }

        public Configuration()
        {
            this.particles = new Collection<Particle>();
            this.particles.Add(new Particle(10, 10));
            this.particles.Add(new Particle(50, 100));
            this.currentTime = 0;
        }

        public Configuration Move()
        {
            Random rnd = new Random();
            int maxRendom = 100;
            for (int i = 0; i < this.particles.Count; i++)
            {
                this.particles[i].Move(rnd.Next((-1) * maxRendom, maxRendom), rnd.Next((-1) * maxRendom, maxRendom));
            }
            this.currentTime++;
            return this;
        }
        public Configuration(Configuration configuration)
        {
            this.particles = new Collection<Particle>();
            for (int i = 0; i < configuration.particles.Count; i++)
            {
                this.particles.Add(new Particle(configuration.particles[i]));
            }            
            this.currentTime = configuration.currentTime;
        }
    }

    class History : BaseVM
    {
        private int EndTime = 100;
        public Collection<Configuration> history = new Collection<Configuration>();
        public History()
        {
            history.Add(new Configuration());            
            Configuration tmp;
            for (int i = 0; i < EndTime; i++)
            {                
                tmp = this.history.LastOrDefault();
                Configuration con = new Configuration(tmp);
                history.Add(con.Move());
            }
        }
    }
}
