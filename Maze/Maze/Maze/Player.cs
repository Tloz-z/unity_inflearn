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

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        int mDir = (int)Dir.Up;
        List<int[]> mPoints = new List<int[]>();
        public void Initialize(int posX, int posY, Board board)
        {
            PosX = posX;
            PosY = posY;
            mBoard = board;

            //SearchByRightHand();
            SearchByBFS();
        }

        void SearchByBFS()
        {
            int[] deltaY = new int[] { -1, 0, 1, 0 };
            int[] deltaX = new int[] { 0, -1, 0, 1 };

            bool[,] found = new bool[mBoard.Size, mBoard.Size];
            int[,,] parent = new int[mBoard.Size, mBoard.Size, 2];

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { PosY, PosX });
            found[PosY, PosX] = true;
            parent[PosY, PosX, 0] = PosY;  
            parent[PosY, PosX, 1] = PosX;

            while (queue.Count > 0)
            {
                int[] pos = queue.Dequeue();
                int nowY = pos[0];
                int nowX = pos[1];

                for (int i = 0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];

                    if (nextX < 0 || nextX > mBoard.Size - 1 || nextY < 0 || nextY > mBoard.Size - 1)
                        continue;
                    if (mBoard.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    if (found[nextY, nextX] == true)
                        continue;
                    queue.Enqueue(new int[] {nextY, nextX});
                    found[nextY, nextX] = true;
                    parent[nextY, nextX, 0] = nowY;
                    parent[nextY, nextX, 1] = nowX;
                }
            }

            int y = mBoard.DestY;
            int x = mBoard.DestX;
            while (parent[y, x, 0] != y || parent[y, x, 1] != x)
            {
                mPoints.Add(new int[] { y, x });
                y = parent[y, x, 0];
                x = parent[y, x, 1];
            }
            mPoints.Add(new int[] { y, x });
            mPoints.Reverse();
        }

        void SearchByRightHand()
        {
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            mPoints.Add(new int[] { PosY, PosX });
            while (PosY != mBoard.DestY || PosX != mBoard.DestX)
            {
                if (mBoard.Tile[PosY + rightY[mDir], PosX + rightX[mDir]] == Board.TileType.Empty)
                {
                    mDir = (mDir - 1 + 4) % 4;
                    PosX += frontX[mDir];
                    PosY += frontY[mDir];
                    mPoints.Add(new int[] { PosY, PosX });
                }
                else if (mBoard.Tile[PosY + frontY[mDir], PosX + frontX[mDir]] == Board.TileType.Empty)
                {
                    PosX += frontX[mDir];
                    PosY += frontY[mDir];
                    mPoints.Add(new int[] { PosY, PosX });
                }
                else
                {
                    mDir = (mDir + 1 + 4) % 4;
                }
            }
        }

        const int MOVE_TICK = 100;
        int mSumTick = 0;
        int mLastIndex = 0;
        public void Update(int deltaTick)
        {
            if (mLastIndex >= mPoints.Count)
                return;

            mSumTick += deltaTick;
            if (mSumTick >= MOVE_TICK)
            {
                mSumTick = 0;

                PosY = mPoints[mLastIndex][0];
                PosX = mPoints[mLastIndex][1];
                mLastIndex++;
            }
        }
    }
}
