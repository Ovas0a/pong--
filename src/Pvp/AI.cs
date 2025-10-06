using System;
using System.Runtime.CompilerServices;
using Godot;
public static class AI
{

    //容差
    private static float aioffset = 90;
    //判断误差
    private static float BallErro=20f;
    //ai思考间隔 0.3s
    private static float Time = 0.1f;
    //思考计时器
    private static float Timer = 0f;
    //上一次判断的移动
    private static int oldMove;
    //上一次判断需要抵达的坐标
    private static float oldBallY;
    
    






    //ai逻辑，每tike执行。
    // retun -1 上移动，
    // retun 1，下移动，
    // return 0 不移动。
    public static int Link(Node2D paddle, float delta)
    {

        Timer -= delta;
        //还在思考
        if (Timer > 0)
        {
            if (oldBallY > paddle.Position.Y - 90 && oldBallY < paddle.Position.Y + 90) return 0;
            return oldMove;

        }
        Timer = Time;
        

        float paddleY = paddle.Position.Y+(float)GD.RandRange(-BallErro,BallErro);

        // 球的 Y 位置
        float ballY = Ball.ThisNode.Position.Y;
        oldBallY = ballY;

        // 判断是否应该向上或向下移动
        if (ballY < paddleY - aioffset)
        {
            oldMove = -1;
            // 球在上方，向上移动
            return oldMove;
        }
        else if (ballY > paddleY + aioffset)
        {
            oldMove = 1;
            // 球在下方，向下移动
            return oldMove;

        }
        else
        {
            oldMove = 0;
            // 在范围内，不移动
            return oldMove;
        }
        
    }
    

}