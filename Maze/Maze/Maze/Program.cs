using Maze;
using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    private static void Main(string[] args)
    {
        Board board = new Board();
        Player player = new Player();
        board.Initialize(25, player);
        player.Initialize(1, 1, board);

        Console.CursorVisible = false;

        const int WAIT_TICK = 1000 / 30;

        int lastTick = 0;
        while (true)
        {
            #region 프레임 관리
            int currentTick = System.Environment.TickCount;
            int deltaTick = currentTick - lastTick;
            if (deltaTick < WAIT_TICK)
            {
                continue;
            }
            lastTick = currentTick;
            #endregion

            // 입력

            // 로직
            player.Update(deltaTick);

            // 렌더링
            Console.SetCursorPosition(0, 0);
            board.Render();
        }
    }
}