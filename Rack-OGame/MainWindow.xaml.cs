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

namespace Rack_OGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Player[] Players = new Player[2];      
        private readonly Button[] Slots;
        private readonly Button[] Piles;

        private Stack Stockpile = new();
        private Stack DiscardPile = new();
        private int DrawnCard;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new RackO();
            Slots = new Button[] { FiveSlot, TenSlot, FifteenSlot, TwentySlot, TwentyFiveSlot, ThirtySlot, ThirtyFiveSlot, FortySlot, FortyFiveSlot, FiftySlot };
            Piles = new Button[] { StockpileButton, DiscardPileButton };
            DisableSlots();
            DisablePiles();
            
            EnablePiles();
        }

        private void Slot_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            int toDiscard = (int)button.Content;
            button.Content = DrawnCard;
            for (int i = 0; i < Slots.Length; i++)
            {
                if (button == Slots[i])
                {
                    Players[0].Rack[i] = DrawnCard;
                    break;
                }
            }
        }

        private void Stockpile_Click(object sender, RoutedEventArgs e)
        {
            DisablePiles();
            DrawnCard = Stockpile.Pop().Value;
            if (Stockpile.Size == 0) Rerack();
            EnableSlots();
        }

        private void DiscardPile_Click(object sender, RoutedEventArgs e)
        {
            DisablePiles();
            DrawnCard = DiscardPile.Pop().Value;
            EnableSlots();
        }

        public void Rerack()
        {
            while (DiscardPile.Size > 1) Stockpile.Push(DiscardPile.Pop());
        }

        private void DisableSlots()
        {
            foreach (var slot in Slots) slot.IsEnabled = false;
        }

        private void EnableSlots()
        {
            foreach (var slot in Slots) slot.IsEnabled = true;
        }

        private void DisablePiles()
        {
            foreach (var pile in Piles) pile.IsEnabled = false;
        }

        private void EnablePiles()
        {
            foreach (var pile in Piles) pile.IsEnabled = true;
        }
    }
}
