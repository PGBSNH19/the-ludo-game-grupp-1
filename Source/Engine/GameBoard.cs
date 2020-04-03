﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineClasses
{
    public class GameBoard
    {
        public string[,] Placements { get; private set; } = new string[11, 11];
        public Dictionary<string, int> Bases { get; private set; }
        public Dictionary<string, int> Goal { get; private set; }
        public List<GameSquare> StartingSquares { get; set; }
        public List<GameSquare> BoardRoute { get; set; }
        public GameBoard()
        {
            Placements = new string[,] { 
                { "x","x","x","x","a","a","a","x","x","x","x"}, 
                { "x","x","x","x","a","r","a","x","x","x","x"}, 
                { "x","x","x","x","a","r","a","x","x","x","x"}, 
                { "x","x","x","x","a","r","a","x","x","x","x"}, 
                { "a","a","a","a","a","r","a","a","a","a","a"}, 
                { "a","b","b","b","b","G","y","y","y","y","a"}, 
                { "a","a","a","a","a","g","a","a","a","a","a"},
                { "x","x","x","x","a","g","a","x","x","x","x"}, 
                { "x","x","x","x","a","g","a","x","x","x","x"}, 
                { "x","x","x","x","a","g","a","x","x","x","x"}, 
                { "x","x","x","x","a","a","a","x","x","x","x"} 
            };
        }

        public GameSquare ValidateStartingSquare(GamePiece gamePiece)
        {

            return StartingSquares.Where(g => g.Color == gamePiece.Player.Color).FirstOrDefault();
        }
        
        public bool ValidateNextSquare(GamePiece gamePiece)
        {
            GameSquare gameSquare = BoardRoute.Where(b => b.XCoordinate == gamePiece.XCoord && b.YCoordinate == gamePiece.YCoord).First()
            if(gameSquare.Color != null)
            {
                
                return 
            }
            
        }
    }
}
