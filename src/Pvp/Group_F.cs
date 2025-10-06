using System;
using System.Threading;
using Godot;
public class Group_F
{
    private Label bigF;

    //Ball标签
    public Group_F(Label F)
    {
        this.bigF = F;
    }

    //大分增加  LeftF  RightF
    public void addBigF(string Name, bool Reset)
    {
        Label l = bigF.GetNode<Label>(Name);
        if (Reset == true)
        {
            l.Text = "0";
            return;
        }
        int oldF = int.Parse(l.Text);
        l.Text = $"{oldF + 1}";
    }
    public void addBigF(string Name)
    {
        addBigF(Name, false);
    }

    public void ResetBigf()
    {
          addBigF("LeftF", true);
          addBigF("RightF", true);
    }



    //小分移除 LeftF  RightF
    public void removeHigF(string Name, bool Reset)
    {
        Label l = bigF.GetNode<Label>(Name);
        GridContainer GroupF = l.GetChild<GridContainer>(0);

        for (int i = 0; i < GroupF.GetChildCount(); i++)
        {
            var Node = GroupF.GetChild<TextureRect>(i);
            //赋初值
            if (Reset == true)
            {
                Node.Visible = true;
                continue;
            }

            if (Node.Visible == true)
            {
                Node.Visible = false;
                break;
            }

        }
    }

    public void removeHigF(string Name)
    {
        removeHigF(Name, false);
    }
    public void ResetHigF()
    {
        removeHigF("LeftF", true);
        removeHigF("RightF", true);
    }

    // 判断小球有没有人胜利 返回胜利的大比分名称
    public string isBallVet()
    {
        string name = "";

        bool over = false;


        foreach (var item in bigF.GetChildren())
        {
            name = item.Name;
            int count = 0;
            foreach (TextureRect node in item.GetNode<GridContainer>("BallGroup").GetChildren())
            {
                if (node.Visible == false) count++;
            }

            if (count == item.GetNode<GridContainer>("BallGroup").GetChildCount())
            {
                over = true;
                break;
            }
        }

        //还没有结束
        if (over == false) return "null";

        //已经有结果了，返回胜利的一方
        if (name == "LeftF") return "RightF";
        return "LeftF";






    }
    public string isGameOver()
    {
        
        int leftF = int.Parse(bigF.GetNode<Label>("LeftF").Text);
        int rightF = int.Parse(bigF.GetNode<Label>("RightF").Text);

        return leftF == 1 || rightF == 1 ?(leftF>rightF?"LeftF":"RightF"): "null"; 
    }
    








}