using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KemonoLangInterpreter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string FileName = ofd.FileName;
            StreamReader sr = new StreamReader(FileName);
            string str=sr.ReadToEnd();
            List<string> orders = new List<string>();
            int start = 0;
            for(int i=0;i<str.Length;i++)
            {
                if (str[i] == '！')
                {
                    orders.Add(str.Substring(start,i-start+1));
                    start = i + 1;
                }
            }

            VirtualMachine v = new VirtualMachine();
            v.run(orders.ToArray());

            Console.WriteLine("FileName = "+FileName);
            Console.ReadLine();
        }
    }

    class VirtualMachine
    {
        byte[] Memory = new byte[256];
        int Address = 0;
        int OrderNo = 0;
        string []Orders;
        public void run(string[] Orders)
        {
            OrderNo = 0;
            this.Orders = Orders;
            while(true)
            {
                if (Orders[OrderNo] == "﻿たのしー！"|| Orders[OrderNo] == "たのしー！")
                    ++Address;
                if (Orders[OrderNo] == "たーのしー！"|| Orders[OrderNo] == "﻿たーのしー！")
                    ++Memory[Address];
                if (Orders[OrderNo] == "すごーい！")
                    --Address;
                if (Orders[OrderNo] == "すっごーい！"|| Orders[OrderNo] == "﻿すっごーい！")
                    --Memory[Address];

                if (Orders[OrderNo] == "なにこれなにこれ！"|| Orders[OrderNo] == "﻿なにこれなにこれ！")
                    Console.Write(Convert.ToChar(Memory[Address]));
                if (Orders[OrderNo] == "おもしろーい！")
                    Memory[Address] =  (byte)Console.Read();

                if (Orders[OrderNo] == "うわー！" || Orders[OrderNo] == "﻿うわー！")
                {
                    if (Memory[Address] == 0)
                    {
                        OrderNo = GetPearW_i(OrderNo);
                        continue;
                    }
                }
                if (Orders[OrderNo] == "わーい！"|| Orders[OrderNo] == "わーい！")
                {
                    if (Memory[Address] != 0)
                    {
                        OrderNo = GetPearUwa_(OrderNo);
                        continue;
                    }
                }

                ++OrderNo;
                if (OrderNo == Orders.Length) return;
            }
        }
        public int GetPearUwa_(int n)
        {
            int deep = 0;
            for(int i=n;i>=0;i--)
            {
                if (Orders[i] == "わーい！"|| Orders[i] == "わーい！")
                    --deep;
                if (Orders[i] == "うわー！"||Orders[i] == "﻿うわー！")
                {
                    ++deep;
                    if (deep == 0)
                        return i;
                }
            }
            return -1;
        }
        public int GetPearW_i(int n)
        {
            int deep = 0;
            for (int i = n ; i <= Orders.Length; i++)
            {
                if (Orders[i] == "わーい！"|| Orders[i] == "わーい！")
                {     --deep;
                    if (deep == 0)
                        return i;
                }

                if (Orders[i] == "うわー！" || Orders[i] == "﻿うわー！")
                    ++deep;
                 
            }
            return -1;
        }
    }
}
