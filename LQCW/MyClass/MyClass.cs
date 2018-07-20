using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using INIFILE;
using System.IO;
using System.IO.Ports;
 

namespace _LQCW.MyClass
    {
        class Myclass
        {
        #region DataGridView布局
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">Lora地址</param>
        /// <param name="b">通道号</param>
        /// <param name="c">缆号</param>
        /// <param name="d">芯片位置号</param>
        /// <param name="e">温度</param>
       
        #endregion

        #region 串口参数预置
        /// <summary>
        /// 串口参数
        /// </summary>
        /// <param name="Baudrate"></param>
        /// <param name="databits"></param>
        /// <param name="stop"></param>
        /// <param name="PARITY"></param>
        public void DecomSerf(ComboBox Baudrate, ComboBox databits, ComboBox stop, ComboBox PARITY)
        {
            //----------------------串口预置--------------------------//
            switch (Profile.G_BAUDRATE)
            {
                case "300":
                    Baudrate.SelectedIndex = 0;
                    break;
                case "600":
                    Baudrate.SelectedIndex = 1;
                    break;
                case "1200":
                    Baudrate.SelectedIndex = 2;
                    break;
                case "2400":
                    Baudrate.SelectedIndex = 3;
                    break;
                case "4800":
                    Baudrate.SelectedIndex = 4;
                    break;
                case "9600":
                    Baudrate.SelectedIndex = 5;
                    break;
                case "19200":
                    Baudrate.SelectedIndex = 6;
                    break;
                case "38400":
                    Baudrate.SelectedIndex = 7;
                    break;
                case "115200":
                    Baudrate.SelectedIndex = 8;
                    break;
                default:
                    {
                        MessageBox.Show("波特率参数错误！");
                        return;
                    }
            }
            switch (Profile.G_DATABITS)
            {
                case "5":
                    databits.SelectedIndex = 0;
                    break;
                case "6":
                    databits.SelectedIndex = 1;
                    break;
                case "7":
                    databits.SelectedIndex = 2;
                    break;
                case "8":
                    databits.SelectedIndex = 3;
                    break;
                default:
                    {
                        MessageBox.Show("数据位参数错误！");
                        return;
                    }

            }

            switch (Profile.G_STOP)
            {
                case "1":
                    stop.SelectedIndex = 0;
                    break;
                case "1.5":
                    stop.SelectedIndex = 1;
                    break;
                case "2":
                    stop.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("停止位参数错误！");
                        return;
                    }

            }
            switch (Profile.G_PARITY)
            {
                case "wu":
                    PARITY.SelectedIndex = 0;
                    break;
                case "ODD":
                    PARITY.SelectedIndex = 1;
                    break;
                case "EVEN":
                    PARITY.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("停止位参数错误！");
                        return;
                    }

            }
        }
        #endregion

        #region 获取串口
        public void COMCHECK(ComboBox com)
        {
            string[] strCOM = SerialPort.GetPortNames(); //check COM
            if (strCOM == null)
            {
                MessageBox.Show("本机没有串口！");
                return;
            }

            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                com.Items.Add(s);
            }
        }
        #endregion

        #region 数据保存至本地
        public void Save_file(string aa, string times,string xinxi)
        {
            DateTime dt = DateTime.Now;
            Directory.CreateDirectory(@"C:\" + "LQCW");
            StreamWriter SW = new StreamWriter(@"C:\" + "LQCW" + "\\" + "_" + xinxi + ".txt", true, Encoding.UTF8);
            SW.Write(dt + "\r\n" + aa + "\r\n");
            SW.Flush();
            SW.Close();
        }
        #endregion

        #region buffer 数据缓存机制
        

        #endregion

    }
}

