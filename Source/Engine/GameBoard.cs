﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineClasses
{
    public class GameBoard
    {
        public Dictionary<string, int> Bases { get; private set; }
        public List<BoardSquare> Board { get; set; }
        public GameBoard()
        {
            this.Board = new List<BoardSquare>();

            this.Board.AddRange(AddPlayerSection("Red"));
            this.Board.AddRange(AddSharedSection());

            this.Board.AddRange(AddPlayerSection("Blue"));
            this.Board.AddRange(AddSharedSection());

            this.Board.AddRange(AddPlayerSection("Yellow"));
            this.Board.AddRange(AddSharedSection());

            this.Board.AddRange(AddPlayerSection("Green"));
            this.Board.AddRange(AddSharedSection());
        }

        private List<BoardSquare> AddPlayerSection(string color)
        {
            List<BoardSquare> section = new List<BoardSquare>();
            section.Add(new BoardSquare(this.Board.Count + section.Count, color, true, false));
            for (int i = 0; i < 4; i++)
            {
                section.Add(new BoardSquare(this.Board.Count + section.Count, color, false, false));
            }
            section.Add(new BoardSquare(this.Board.Count + section.Count, color, false, true));

            return section;
        }

        private List<BoardSquare> AddSharedSection()
        {
            List<BoardSquare> section = new List<BoardSquare>();
            for (int i = 0; i < 9; i++)
            {
                section.Add(new BoardSquare(this.Board.Count + section.Count, "White", false, false));
            }

            return section;
        }

        /// <summary>
        /// Return starting square based on player color.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public BoardSquare GetStartingSquare(Player player)
        {
            return Board.Where(b => b.Color == player.Color && b.StartingSquare).FirstOrDefault();
        }

        /// <summary>
        /// Returns gamesquare based on game pieces position (int).
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public BoardSquare GetCurrentSquare(GamePiece gamePiece)
        {
            return Board.Where(b => b.BoardSquareNumber == gamePiece.BoardSquareNumber.Value).FirstOrDefault();
        }

        /// <summary>
        /// Returns the square after the square the game piece is currently at.
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public BoardSquare GetNextSquare(GamePiece gamePiece)
        {
            return Board.Where(b => b.BoardSquareNumber == gamePiece.BoardSquareNumber.Value + 1).FirstOrDefault();
        }

        /// <summary>
        /// Returns the next square the game piece can legally stand on according to game rules.
        /// </summary>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public BoardSquare FindNextValidSquare(GamePiece gamePiece)
        {
            int i = 1;

            while (Board[gamePiece.BoardSquareNumber.Value + i].Color != gamePiece.Player.Color ||
                    Board[gamePiece.BoardSquareNumber.Value + i].Color != "White")
            {
                i++;
            }

            return Board[gamePiece.BoardSquareNumber.Value + i];
        }


        /// <summary>
        /// Places gamePiece in squares' list of game pieces and sets all pieces of different colors to base.
        /// </summary>
        /// <param name="gamePiece"></param>
        public void PlaceGamePiece(GamePiece gamePiece)
        {
            BoardSquare boardSquare = Board.Where(bs => bs.BoardSquareNumber == gamePiece.BoardSquareNumber.Value).First();
            boardSquare.GamePieces.Add(gamePiece);
        }

    }
}
