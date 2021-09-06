using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.spaccabolle.score
{
    public class Score
    {
        public static int tempFlyngPoint = 0;

        private int tempPoint = 0;
        private int number1;
        private int number2;
        private int number3;
        private int number4;

        public Score(int number1, int number2, int number3, int number4)
        {
            this.number1 = number1;
            this.number2 = number2;
            this.number3 = number3;
            this.number4 = number4;
        }

        public void AddPoint(int point, int flyngPoint)
        {
            this.tempPoint = (this.tempPoint + (point * 10));
            tempFlyngPoint = ((int)((tempFlyngPoint + Math.Pow(2, flyngPoint))));
            this.tempPoint = (this.tempPoint + tempFlyngPoint);
            for (int i = 0; (i <= 3); i++)
            {
                int definitive = (this.tempPoint / Score.Power(10, (3 - i)));
                
                switch (i)
                {
                    case 0:
                        this.number1 = definitive;
                        break;
                    case 1:
                        this.number2 = definitive;
                        break;
                    case 2:
                        this.number3 = definitive;
                        break;
                    case 3:
                        this.number4 = definitive;
                        break;
                }
                this.tempPoint = (this.tempPoint - (definitive * Score.Power(10, (3 - i))));
            }

            flyngPoint = 0;
        }

        public int GetNumber1()
        {
            return this.number1;
        }

        public int GetNumber2()
        {
            return this.number2;
        }

        public int GetNumber3()
        {
            return this.number3;
        }

        public int GetNumber4()
        {
            return this.number4;
        }

        public static int Power(int U, int V)
        {
            int risp = 1;

            for (int i = 1; i <= V; i++)
            {
                risp = (risp * U);
            }

            return risp;
        }

        public static void SetZero(Score score)
        {
            score.number1 = 0;
            score.number2 = 0;
            score.number3 = 0;
            score.number4 = 0;
        }

        public void Render(Graphics g)
        {
            g.drawImage(Assets.numbers[this.number1], 340, 600, 50, 50, null);
            g.drawImage(Assets.numbers[this.number2], 380, 600, 50, 50, null);
            g.drawImage(Assets.numbers[this.number3], 420, 600, 50, 50, null);
            g.drawImage(Assets.numbers[this.number4], 460, 600, 50, 50, null);
        }
    }
}