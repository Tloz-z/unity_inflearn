using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        Random mRand = new Random();
        Board mBoard;

        public void Initialize(int posX, int posY, int destX, int destY, Board board)
        {
            PosX = posX;
            PosY = posY;

            mBoard = board;
        }

        const int MOVE_TICK = 100;
        int mSumTick = 0;
        public void Update(int deltaTick)
        {
            mSumTick += deltaTick;
            if (mSumTick >= MOVE_TICK)
            {
                mSumTick = 0;

                int randValue = mRand.Next(0, 5);
                switch (randValue)
                {
                    case 0: //상
                        if (mBoard.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                            PosY -= 1;
                        break;
                    case 1:  //하
                        if (mBoard.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                            PosY += 1;
                        break;
                    case 2:  //좌
                        if (mBoard.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                            PosX -= 1;
                        break;
                    case 3:  //우
                        if (mBoard.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                            PosX += 1;
                        break;
                }
            }
        }
    }
}
