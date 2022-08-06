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
        public Button[] Slots { get; } 
        public Button[] Piles { get; }
        public int DrawnCard { get; }

        public MainWindow()
        {
            InitializeComponent();
            Slots = new Button[] { FiveSlot, TenSlot, FifteenSlot, TwentySlot, TwentyFiveSlot, ThirtySlot, ThirtyFiveSlot, FortySlot, FortyFiveSlot, FiftySlot };
            Piles = new Button[] { StockpileButton, DiscardPileButton };
            GameLogic.InitializeGame(Slots);
        }

        private void Slot_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            for (int i = 0; i < Slots.Length; i++)
            {
                if (button == Slots[i])
                {
                    
                }
            }
        }

        private void Stockpile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DiscardPile_Click(object sender, RoutedEventArgs e)
        {

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
