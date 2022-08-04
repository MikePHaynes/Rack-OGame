﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rack_OGame
{
    public class GameLogic
    {
        private static Player[] Players { get; } = new Player[2];
        private static Stack Stockpile { get; } = new();
        private static Stack DiscardPile { get; } = new();

        public static void InitializeGame()
        {
            List<int> list = new();
            for (int i = 0; i < 60; i++) list.Add(i + 1);

            Players[0] = new Player();
            Players[1] = new Player();
            GenerateStockpile(list);
            for (int i = 0; i < (Players.Length * 10); i++) Players[i % Players.Length].AddCard(Stockpile.Pop());
            DiscardPile.Push(Stockpile.Pop());
        }

        public static void GenerateStockpile(List<int> list)
        {
            Random random = new();
            while (list.Count != 0)
            {
                int index = random.Next(list.Count);
                Stockpile.Push(new Node(list[index]));
                list.RemoveAt(index);
            }
        }
        
    }
}