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
        private readonly Player[] Players;      
        private readonly Button[] Slots;
        private readonly Button[] Piles;
        private readonly Stack Stockpile = new();
        private readonly Stack DiscardPile = new();
        private int DrawnCard;

        public MainWindow()
        {
            InitializeComponent();
            Players = new Player[] { new Player(), new Player() };
            Slots = new Button[] { FiveSlot, TenSlot, FifteenSlot, TwentySlot, TwentyFiveSlot, ThirtySlot, ThirtyFiveSlot, FortySlot, FortyFiveSlot, FiftySlot };
            Piles = new Button[] { StockpileButton, DiscardPileButton };
            DataContext = Players[0];
            DisableSlots();
            DisablePiles();
            InitializeGame();
            EnablePiles();
        }

        private void InitializeGame()
        {
            List<int> list = Enumerable.Range(1, 60).ToList();
            GenerateStockpile(list);
            for (int i = 0; i < (Players.Length * 10); i++) Players[i % Players.Length].AddCard(Stockpile.Pop()!);
            DiscardPile.Push(Stockpile.Pop()!);
            DiscardPileButton.Content = DiscardPile.Peek();
            EnablePiles();
        }

        private void GenerateStockpile(List<int> list)
        {
            Random random = new();
            while (list.Count != 0)
            {
                int index = random.Next(list.Count);
                Stockpile.Push(new Node(list[index]));
                list.RemoveAt(index);
            }
        }

        private void Slot_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            int toDiscard = (int)button!.Content;
            button.Content = DrawnCard;
            for (int i = 0; i < Slots.Length; i++)
            {
                if (button == Slots[i])
                {
                    Players[0].Rack[i] = DrawnCard;
                    break;
                }
            }
            if (Players[0].HasNumericalSequence() && Players[0].HasThreeCardRun()) ShowWinner(Winner.Player);
            else 
            {
                DiscardPileButton.Content = toDiscard;
                DiscardPile.Push(new Node(toDiscard));
                DisableSlots();
                CPUTurn();
            }            
        }

        private void Stockpile_Click(object sender, RoutedEventArgs e)
        {
            DisablePiles();
            DrawnCard = Stockpile.Pop()!.Value;
            MessageBlock.Text = $"Drew a {DrawnCard}";
            if (Stockpile.Size == 0) Rerack();
            EnableSlots();
        }

        private void DiscardPile_Click(object sender, RoutedEventArgs e)
        {
            DisablePiles();
            DrawnCard = DiscardPile.Pop()!.Value;
            MessageBlock.Text = $"Drew a {DrawnCard}";
            EnableSlots();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Rerack()
        {
            while (DiscardPile.Size > 1) Stockpile.Push(DiscardPile.Pop()!);
        }

        async private void CPUTurn()
        {
            MessageBlock.Text = "Player: CPU";
            await Task.Delay(TimeSpan.FromSeconds(3));
            Random random = new();
            int choice;
            int draw = random.Next(2);
            if (draw == 0)
            {
                choice = Stockpile.Pop()!.Value;
                if (Stockpile.Size == 0) Rerack();
            }
            else choice = DiscardPile.Pop()!.Value;
            CPUFindSlot(choice);
            if (Players[1].HasNumericalSequence()) ShowWinner(Winner.CPU);
            else 
            {
                MessageBlock.Text = "Player: You";
                EnablePiles();
            }           
        }

        private void CPUFindSlot(int choice)
        {
            int smallestDifference = 60;
            int smallestDiffIndex = 0;
            for (int i = 0; i < Players[1].Rack.Length; i++)
            {
                if (Math.Abs((i + 1) * 5 - choice) < smallestDifference)
                {
                    smallestDiffIndex = i;
                    smallestDifference = Math.Abs((i + 1) * 5 - choice);
                }
            }
            int toReplace = Players[1].Rack[smallestDiffIndex];
            Players[1].Rack[smallestDiffIndex] = choice;
            DiscardPile.Push(new Node(toReplace));
            DiscardPileButton.Content = DiscardPile.Peek();
        }

        private void ShowWinner(Winner winner)
        {
            GamePanel.Visibility = Visibility.Hidden;
            ResultTextBox.Text += winner.ToString();
            EndScreenPanel.Visibility = Visibility.Visible;
        }

        private void DisableSlots()
        {
            foreach (var slot in Slots)
            {
                slot.IsEnabled = false;
                slot.Foreground = Brushes.Red;
            }
        }

        private void EnableSlots()
        {
            foreach (var slot in Slots)
            {
                slot.IsEnabled = true;
                slot.Foreground = Brushes.White;
            }
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
