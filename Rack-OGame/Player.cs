using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rack_OGame
{
    public class Player: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int[] Rack { get; set; }

        public Player()
        {
            Rack = new int[10];
        }

        public void AddCard(Node card)
        {
            for(int i = 0; i < Rack.Length; i++)
            {
                if(Rack[i] == 0)
                {
                    Rack[i] = card.Value;
                    break;
                }
            }
        }

        public bool HasNumericalSequence()
        {
            for(int i = 0; i < Rack.Length - 1; i++)
            {
                if (Rack[i] > Rack[i + 1]) return false;
            }
            return true;
        }

        public bool HasThreeCardRun()
        {
            int count = 0;
            for (int i = 0; i < Rack.Length - 1; i++)
            {
                if (Rack[i + 1] - Rack[i] == 1)
                {
                    count++;
                    if (count == 2) return true;
                }
                else count = 0;
            }
            return false;
        }
    }
}
