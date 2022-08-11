using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Rack_OGame
{
    public class RackO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Player[] Players { get; } 
        public Stack Stockpile { get; } 
        public Stack DiscardPile { get; } 

        public RackO()
        {
            Players = new Player[2];
            Stockpile = new Stack();
            DiscardPile = new Stack();
            InitializeGame();
        }

        public void InitializeGame()
        {
            List<int> list = new();
            int i;
            for (i = 0; i < 60; i++) list.Add(i + 1);

            Players[0] = new Player();
            Players[1] = new Player();
            GenerateStockpile(list);
            for (i = 0; i < (Players.Length * 10); i++) Players[i % Players.Length].AddCard(Stockpile.Pop());
            DiscardPile.Push(Stockpile.Pop());           
        }

        public void GenerateStockpile(List<int> list)
        {
            Random random = new();
            while (list.Count != 0)
            {
                int index = random.Next(list.Count);
                Stockpile.Push(new Node(list[index]));
                list.RemoveAt(index);
            }
        }

        public void Rerack()
        {
            while (DiscardPile.Size > 1) Stockpile.Push(DiscardPile.Pop());
        }

        public void ReplaceCard(int newCardValue, int index)
        {

        }
        
    }
}
