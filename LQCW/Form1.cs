using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace _LQCW
{

    public partial class Form1 : Form
    {
        
        SerialPort Sp1 = new SerialPort(); //初始化串口实例
        MyClass.Myclass MyClass = new _LQCW.MyClass.Myclass();
        public Form1()
        {
            InitializeComponent();
        }
   

        private void Form1_Load(object sender, EventArgs e)
        {

          

            MyClass.Myclass MyClass = new _LQCW.MyClass.Myclass();
            INIFILE.Profile.LoadProfile(); //加载本地Config文件

           

            MyClass.DecomSerf(cbBaudRate, cbDataBits, cbStop, cbParity);

            MyClass.COMCHECK(cbSerial);

            Sp1.BaudRate = 115200;
            Control.CheckForIllegalCrossThreadCalls = false;
            Sp1.DataReceived += new SerialDataReceivedEventHandler(Sp1_DataReceived);
            Sp1.DtrEnable = true;
            Sp1.RtsEnable = true;
            Sp1.ReadTimeout =100000 ; //读取时间设定为2s
            Sp1.Close();
            this.dataGridView1.AllowUserToAddRows = false;

            





        }
        //---------------------------------------------------------------------------------------------------------------------//
        #region 串口数据接收 1

        void Sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(300);

            int len1 = 0;
            int len2 = 0;
            int len3 = 0;
            int len4 = 0;

           
            byte[] temperature_data_1 = null;
            byte[] temperature_data_2 = null;
            byte[] temperature_data_3 = null;
            byte[] temperature_data_4 = null;

            DataTable table = new DataTable();


            DataColumn c1 = new DataColumn("LoRa地址:端口", typeof(string));
            DataColumn c2 = new DataColumn("缆号", typeof(string));
            DataColumn c3 = new DataColumn("地址", typeof(string));
            DataColumn c4 = new DataColumn("温度（℃）", typeof(string));
            DataColumn c5 = new DataColumn("湿度（%RH）", typeof(string));

            

            table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.Columns.Add(c3);
            table.Columns.Add(c4);
            table.Columns.Add(c5);




            if (Sp1.IsOpen)
            {
                DateTime dt = DateTime.Now;

                //byte[] byteRead = new byte[Sp1.BytesToRead]; //获取Sp1.数据个数
                List<byte> buffer = new List<byte>(4096 * 2);
                try
                {
                    
                    Byte[] receivedData = new Byte[Sp1.BytesToRead];
                    Sp1.Read(receivedData, 0, receivedData.Length);
                    buffer.AddRange(receivedData);  //将数据缓存至List<>
                    int n = buffer.Count;
                    ushort cd = 0;
                    Sp1.DiscardInBuffer();
                    cd = (ushort)(cd ^ buffer[2]); cd = (ushort)(cd << 8); cd = (ushort)(cd ^ buffer[3]);

                    string strrcv = null;

                    for (int i = 0; i < receivedData.Length; i++)
                    {
                        strrcv += receivedData[i].ToString("x2");
                    }
                    txtReceive.Text += strrcv + "\r\n";
                    MyClass.Save_file(strrcv, dt.ToString(), "Sp1");


                    #region 通讯协议解析

                    if (buffer[0] == 0x5F && buffer[1] == 0x5F && buffer[n - 1] == 0xAA && buffer[n - 2] == 0x55)
                    {

                        if (n < cd)
                        {
                            return;
                        }

                        if (buffer[11] == 0x01)
                        {
                            buffer.RemoveRange(0, n);
                        }
                        else
                        {
                            if (buffer[11] == 0x03)
                            {
                                #region 目标ID 网关号
                                ushort a = 0;
                                a = (ushort)(a ^ buffer[4]); a = (ushort)(a << 8); a = (ushort)(a ^ buffer[5]);
                                string TargetID = a.ToString("X2");
                                gatawayID.Text = TargetID;
                                #endregion;  

                                #region 源ID 采集端号
                                Int32 b = 0;
                                b = (Int32)(b ^ buffer[6]); b = (Int32)(b << 8); b = (Int32)(b ^ buffer[7]); b = (Int32)(b << 8); b = (Int32)(b ^ buffer[8]); b = (Int32)(b << 8); b = (Int32)(b ^ buffer[9]);
                                string SourceID = b.ToString("X2");

                                #endregion

                                #region 序列号  
                                string SerialNum = Convert.ToString(buffer[10], 10);

                                #endregion

                                #region 通道个数  COM
                                string Passageway = Convert.ToString(buffer[12], 10);

                                #endregion 

                                #region 电池电量
                                string battery = Convert.ToString(buffer[13], 10);
                                BatteryPower.Text = battery.ToString() + "%";
                                #endregion

                                #region 通道数位0
                                if (Convert.ToInt32(Passageway)==0)
                                {
                                    DataRow r1 = table.NewRow();
                                    r1["LoRa地址:端口"] = SourceID.ToString();
                                    r1["缆号"] = "--";
                                    r1["地址"] = "--";
                                    r1["温度（℃）"] = "--";
                                    r1["湿度（%RH）"] = "--";
                                    table.Rows.Add(r1);
                                    dataGridView1.DataSource = table;
                                    dataGridView1.AutoResizeRows();
                                    gatawayID.Text = TargetID.ToString();
                                    COM.Text = "无";COM2.Text = "";COM3.Text = "";COM4.Text = "";
                                    session1.Text = "无";session2.Text = "";session3.Text = "";session4.Text = "";

                                    return;

                                }
                                #endregion

                                #region 一通道
                                if (Convert.ToInt16(Passageway) == 1)
                                {
                                    string[] Lanhao = new string[32];
                                    string[] weizhi = new string[32];
                                    string[] wendu1 = new string[32];
                                    string PassagewayNum1 = Convert.ToString(buffer[14], 10);//第一通道，通道号 

                                    COM.Text = PassagewayNum1;

                                    len1 = buffer[15];  //第一通道参数组数

                                    session1.Text = len1.ToString();
                                  
                                    int serfNum = len1 * 4;//参数组数总个数

                                    temperature_data_1 = new byte[serfNum];

                                    buffer.CopyTo(16, temperature_data_1, 0, serfNum); //将第一通道参数组数复制至参数数组

                                    //-----------------------------------//温度参数//--------------------------------------------------//

                                    for (int i = 0, j = 0; i < temperature_data_1.Length; i += 4, j++)   //电缆号
                                    {

                                        string Cablenum = Convert.ToString(temperature_data_1[i], 10);
                                        Lanhao[j] = Cablenum.ToString(); //→coble


                                    }

                                    for (int i = 1, j = 0; i < temperature_data_1.Length; i += 4, j++) //位置号
                                    {

                                        string localNum = Convert.ToString(temperature_data_1[i], 10);
                                        weizhi[j] = localNum;



                                    }
                                    for (int i = 2, j = 0; i < temperature_data_1.Length; i += 4, j++) //温度
                                    {
                                        if (temperature_data_1[i] == 0x3f && temperature_data_1[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_1[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_1[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_1[i], temperature_data_1[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                           

                                        }                                     
                                    }

                                    for (int i = 0; i < len1; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum1;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        this.dataGridView1.DataSource = table;
                                    }));
                                    
                                    for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                                    {
                                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                                    }
                                    dataGridView1.AutoResizeRows();
                                    return;

                                }
                                #endregion

                                #region 双通道


                                if (Convert.ToInt16(Passageway) == 2)
                                {
                                    string[] Lanhao = new string[32];
                                    string[] weizhi = new string[32];
                                    string[] wendu1 = new string[32];
                                    string PassagewayNum1 = Convert.ToString(buffer[14], 10);//第一通道，通道号
                                    COM.Text = PassagewayNum1;
                                    len1 = buffer[15];  //第一通道参数组数
                                    session1.Text = len1.ToString();
                                    int serfNum = len1 * 4;//参数组数总个数
                                    temperature_data_1 = new byte[serfNum];
                                    buffer.CopyTo(16, temperature_data_1, 0, serfNum); //将第一通道参数组数复制至参数数组
                                                                                       //-----------------------------------//温度参数//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_1[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_1[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    
                                        for (int i = 2, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                        {
                                            if (temperature_data_1[i] == 0x3f && temperature_data_1[i + 1] == 0xff)
                                            {
                                                wendu1[j] = "--";
                                            }
                                            else
                                            {
                                                //short c = 0;
                                                //c = (short)(c ^ temperature_data_1[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_1[i + 1]);
                                                //string d = Convert.ToString(c, 10);
                                                //double wendu = Convert.ToInt32(d);
                                                //double temperature = wendu / 16;
                                                //wendu1[j] = temperature.ToString();
                                                StringBuilder temperature_data = new StringBuilder();
                                                byte[] zz = new byte[2] { temperature_data_1[i], temperature_data_1[i + 1] };

                                                for (int k = 0; k < 2; k++)
                                                {
                                                    string zh = Convert.ToString(zz[k], 2);
                                                    string tp = zh.PadLeft(8, '0');
                                                    temperature_data.Append(tp);
                                                }
                                                char[] c1c = new char[16];
                                                temperature_data.CopyTo(0, c1c, 0, 16);
                                                string canshutyep = null;
                                                string wenduint = null;
                                                string wendudouble = null;
                                                string zhengfu = null;
                                                for (int m = 0; m < 4; m++)
                                                {
                                                    canshutyep += c1c[m].ToString();
                                                    
                                                }

                                                for (int p = 5; p < 12; p++)
                                                {
                                                    wenduint += c1c[p].ToString();
                                                    
                                                }
                                                for (int q = 12; q < 16; q++)
                                                {
                                                    wendudouble += c1c[q].ToString();
                                                    
                                                }
                                                if (c1c[4].ToString() == "1")
                                                {
                                                    zhengfu = "-";
                                                }
                                                else
                                                {
                                                    zhengfu = "+";
                                                }
                                                wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                               
                                            }

                                        }
                                   


                                    for (int i = 0; i < len1; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum1;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    dataGridView1.DataSource = table;

                                    string PassagewayNum2 = Convert.ToString(buffer[14 + serfNum + 2]); //第二通道，通道号
                                    COM2.Text = PassagewayNum2.ToString();
                                    len2 = buffer[15 + serfNum + 2]; //第二通道参数组数
                                    session2.Text = len2.ToString();
                                    int serfNum1 = len2 * 4; //第二通道参数总个数
                                    temperature_data_2 = new byte[serfNum1];

                                    buffer.CopyTo(16 + serfNum + 2, temperature_data_2, 0, serfNum1);

                                    //-----------------------------------//温度参数//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_2[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_2[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        if (temperature_data_2[i] == 0x3f && temperature_data_2[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_2[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_2[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_2[i], temperature_data_2[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                           
                                        }

                                    }
                                    for (int i = 0; i < len2; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum2;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        this.dataGridView1.DataSource = table;
                                    }));
                                    for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                                    {
                                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                                    }
                                    dataGridView1.AutoResizeRows();
                                    return;
                                }
                                #endregion

                                #region 三通道
                                if (Convert.ToInt16(Passageway) == 3)
                                {
                                    string[] Lanhao = new string[32];
                                    string[] weizhi = new string[32];
                                    string[] wendu1 = new string[32];
                                    string PassagewayNum1 = Convert.ToString(buffer[14], 10);//第一通道，通道号
                                    COM.Text = PassagewayNum1;
                                    len1 = buffer[15];  //第一通道参数组数
                                    session1.Text = len1.ToString();
                                    int serfNum = len1 * 4;//参数组数总个数
                                    temperature_data_1 = new byte[serfNum];
                                    buffer.CopyTo(16, temperature_data_1, 0, serfNum); //将第一通道参数组数复制至参数数组
                                                                                       //-----------------------------------//温度参数1//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_1[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_1[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {
                                        if (temperature_data_1[i] == 0x3f && temperature_data_1[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_1[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_1[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_1[i], temperature_data_1[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                        }

                                    }
                                    for (int i = 0; i < len1; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum1;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    dataGridView1.DataSource = table;
                                    dataGridView1.AutoResizeRows();



                                    string PassagewayNum2 = Convert.ToString(buffer[14 + serfNum + 2]); //第二通道，通道号
                                    COM2.Text = PassagewayNum2.ToString();
                                    len2 = buffer[15 + serfNum + 2]; //第二通道参数组数
                                    session2.Text = len2.ToString();
                                    int serfNum1 = len2 * 4; //第二通道参数总个数
                                    temperature_data_2 = new byte[serfNum1];

                                    buffer.CopyTo(16 + serfNum + 2, temperature_data_2, 0, serfNum1);

                                    //-----------------------------------//温度参数//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_2[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_2[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        if (temperature_data_2[i] == 0x3f && temperature_data_2[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_2[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_2[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_2[i], temperature_data_2[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);

                                        }

                                    }
                                    for (int i = 0; i < len2; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum2;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    dataGridView1.DataSource = table;
                                    dataGridView1.AutoResizeRows();



                                    string PassagewayNmu3 = Convert.ToString(buffer[14 + serfNum + serfNum1 + 4]);
                                    COM3.Text = PassagewayNmu3;
                                    len3 = buffer[15 + serfNum + serfNum1 + 4];
                                    session3.Text = len3.ToString();
                                    int serfNum2 = len3 * 4;
                                    temperature_data_3 = new byte[serfNum2];
                                    buffer.CopyTo(16 + serfNum + serfNum1 + 4, temperature_data_3, 0, serfNum2);
                                    //-----------------------------------//温度参数3//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_3.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_3[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_3.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_3[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_3.Length; i += 4, j++)
                                    {

                                        if (temperature_data_3[i] == 0x3f && temperature_data_3[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_3[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_3[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_3[i], temperature_data_3[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                           
                                        }
                                    }


                                    for (int i = 0; i < len3; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNmu3;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        this.dataGridView1.DataSource = table;
                                    }));
                                    dataGridView1.AutoResizeRows();
                                    for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                                    {
                                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                                    }
                                    return;
                                }
                                #endregion

                                #region 四通道
                                if (Convert.ToInt16(Passageway) == 4)
                                {
                                    string[] Lanhao = new string[32];
                                    string[] weizhi = new string[32];
                                    string[] wendu1 = new string[32];
                                    string PassagewayNum1 = Convert.ToString(buffer[14], 10);//第一通道，通道号
                                    COM.Text = PassagewayNum1;
                                    len1 = buffer[15];  //第一通道参数组数
                                    session1.Text = len1.ToString();
                                    int serfNum = len1 * 4;//参数组数总个数
                                    temperature_data_1 = new byte[serfNum];
                                    buffer.CopyTo(16, temperature_data_1, 0, serfNum); //将第一通道参数组数复制至参数数组

                                    for (int i = 0, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_1[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_1[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_1.Length; i += 4, j++)
                                    {

                                        if (temperature_data_1[i] == 0x3f && temperature_data_1[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_1[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_1[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_1[i], temperature_data_1[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                            
                                        }


                                    }
                                    for (int i = 0; i < len1; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum1;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    dataGridView1.DataSource = table;

                                    dataGridView1.AutoResizeRows();

                                    string PassagewayNum2 = Convert.ToString(buffer[14 + serfNum + 2]); //第二通道，通道号
                                    COM2.Text = PassagewayNum2;
                                    len2 = buffer[15 + serfNum + 2]; //第二通道参数组数
                                    session2.Text = len2.ToString(); 
                                    int serfNum1 = len2 * 4; //第二通道参数总个数
                                    temperature_data_2 = new byte[serfNum1];
                                    buffer.CopyTo(16 + serfNum + 2, temperature_data_2, 0, serfNum1);
                                    //-----------------------------------//温度参数2//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_2[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_2[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_2.Length; i += 4, j++)
                                    {

                                        if (temperature_data_2[i] == 0x3f && temperature_data_2[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_2[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_2[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_2[i], temperature_data_2[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                            
                                        }

                                    }
                                    for (int i = 0; i < len2; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum2;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    dataGridView1.DataSource = table;

                                    dataGridView1.AutoResizeRows();


                                    string PassagewayNum3 = Convert.ToString(buffer[14 + serfNum + serfNum1 + 4]);
                                    COM3.Text = PassagewayNum3;
                                    len3 = buffer[15 + serfNum + serfNum1 + 4];
                                    session3.Text = len3.ToString();
                                    int serfNum2 = len3 * 4;
                                    temperature_data_3 = new byte[serfNum2];
                                    buffer.CopyTo(16 + serfNum + serfNum1 + 4, temperature_data_3, 0, serfNum2);
                                    //-----------------------------------//温度参数3//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_3.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_3[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_3.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_3[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_3.Length; i += 4, j++)
                                    {

                                        if (temperature_data_3[i] == 0x3f && temperature_data_3[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_3[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_3[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_3[i], temperature_data_3[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                            
                                        }

                                    }
                                    for (int i = 0; i < len3; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum3;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    dataGridView1.DataSource = table;

                                    dataGridView1.AutoResizeRows();


                                    string PassagewayNum4 = Convert.ToString(buffer[14 + serfNum + serfNum1 + serfNum2 + 6]);
                                    COM4.Text = PassagewayNum4;
                                    len4 = buffer[15 + serfNum + serfNum1 + serfNum2 + 6];
                                    session4.Text = len4.ToString();
                                    int serfNum3 = len4 * 4;
                                    temperature_data_4 = new byte[serfNum3];
                                    buffer.CopyTo(16 + serfNum + serfNum1 + serfNum2 + 6, temperature_data_4, 0, serfNum3);
                                    //-----------------------------------//温度参数3//--------------------------------------------------//
                                    for (int i = 0, j = 0; i < temperature_data_4.Length; i += 4, j++)
                                    {
                                        string Cablenum = Convert.ToString(temperature_data_4[i], 10);
                                        Lanhao[j] = Cablenum;
                                    }
                                    for (int i = 1, j = 0; i < temperature_data_4.Length; i += 4, j++)
                                    {
                                        string localNum = Convert.ToString(temperature_data_4[i], 10);
                                        weizhi[j] = localNum;
                                    }
                                    for (int i = 2, j = 0; i < temperature_data_4.Length; i += 4, j++)
                                    {

                                        if (temperature_data_4[i] == 0x3f && temperature_data_4[i + 1] == 0xff)
                                        {
                                            wendu1[j] = "--";
                                        }
                                        else
                                        {
                                            //short c = 0;
                                            //c = (short)(c ^ temperature_data_4[i]); c = (short)(c << 8); c = (short)(c ^ temperature_data_4[i + 1]);
                                            //string d = Convert.ToString(c, 10);
                                            //double wendu = Convert.ToInt32(d);
                                            //double temperature = wendu / 16;
                                            //wendu1[j] = temperature.ToString();
                                            StringBuilder temperature_data = new StringBuilder();
                                            byte[] zz = new byte[2] { temperature_data_4[i], temperature_data_4[i + 1] };

                                            for (int k = 0; k < 2; k++)
                                            {
                                                string zh = Convert.ToString(zz[k], 2);
                                                string tp = zh.PadLeft(8, '0');
                                                temperature_data.Append(tp);
                                            }
                                            char[] c1c = new char[16];
                                            temperature_data.CopyTo(0, c1c, 0, 16);
                                            string canshutyep = null;
                                            string wenduint = null;
                                            string wendudouble = null;
                                            string zhengfu = null;
                                            for (int m = 0; m < 4; m++)
                                            {
                                                canshutyep += c1c[m].ToString();

                                            }

                                            for (int p = 5; p < 12; p++)
                                            {
                                                wenduint += c1c[p].ToString();

                                            }
                                            for (int q = 12; q < 16; q++)
                                            {
                                                wendudouble += c1c[q].ToString();

                                            }
                                            if (c1c[4].ToString() == "1")
                                            {
                                                zhengfu = "-";
                                            }
                                            else
                                            {
                                                zhengfu = "+";
                                            }
                                            wendu1[j] = zhengfu + Convert.ToInt32(wenduint, 2).ToString() + "." + Convert.ToInt32(wendudouble, 2);
                                            
                                        }

                                    }
                                    for (int i = 0; i < len4; i++)
                                    {

                                        DataRow r1 = table.NewRow();
                                        r1["LoRa地址:端口"] = SourceID + ":" + PassagewayNum4;
                                        r1["缆号"] = Lanhao[i];
                                        r1["地址"] = weizhi[i];
                                        r1["温度（℃）"] = wendu1[i];
                                        r1["湿度（%RH）"] = "--";
                                        table.Rows.Add(r1);
                                    }
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        this.dataGridView1.DataSource = table;
                                    }));
                                    for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                                    {
                                        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                                    }
                                    dataGridView1.AutoResizeRows();
                                    return;
                                }
                                #endregion




                            }
                        }
                    }
                    #endregion
                }
                catch (System.Exception ex)
                {
                    //MessageBox.Show(ex + ex.StackTrace);
                }


            }
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
            #endregion

      

        }





        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (!Sp1.IsOpen)
            {
                try
                {
                    string serialName = cbSerial.SelectedItem.ToString();
                    Sp1.PortName = serialName;
                    //串口设置
                    string strBaudRate = cbBaudRate.Text;
                    string strDateBits = cbDataBits.Text;
                    string strStopBits = cbStop.Text;
                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                    Int32 idateBits = Convert.ToInt32(strDateBits);

                    Sp1.BaudRate = iBaudRate;
                    Sp1.DataBits = idateBits;
                    Sp1.StopBits = StopBits.One;
                    Sp1.Parity = Parity.None;

                    if (Sp1.IsOpen == true)
                    {
                        Sp1.Close();
                    }
                    tsSpNum.Text = "串口号：" + Sp1.PortName + "|";
                    tsBaudRate.Text = "波特率：" + Sp1.BaudRate + "|";
                    tsDataBits.Text = "数据位：" + Sp1.DataBits + "|";
                    tsStopBits.Text = "停止位：" + Sp1.StopBits + "|";
                    tsParity.Text = "校验位:" + Sp1.Parity + "|";

                    cbSerial.Enabled = false;
                    cbBaudRate.Enabled = false;
                    cbStop.Enabled = false;
                    cbDataBits.Enabled = false;
                    cbParity.Enabled = false;

                    Sp1.Open();

                    btnSwitch.Text = "关闭串口";
                }
                catch(System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    tmSend.Enabled = false;
                }
            }
            else
            {
                tsSpNum.Text = "串口号：未指定|";
                tsBaudRate.Text = "波特率：未指定|";
                tsDataBits.Text = "数据位：未指定|";
                tsStopBits.Text = "停止位：未指定|";
                tsParity.Text = "校验位:未指定|";

                cbSerial.Enabled = false;
                cbBaudRate.Enabled = false;
                cbStop.Enabled = false;
                cbDataBits.Enabled = false;
                cbParity.Enabled = false;

                cbSerial.Enabled = true;
                cbBaudRate.Enabled = true;
                cbStop.Enabled = true;
                cbDataBits.Enabled = true;
                cbParity.Enabled = true;

                Sp1.Close();
                btnSwitch.Text = "打开串口";
                tmSend.Enabled = false;

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ScrollBars = ScrollBars.Both;
        }
    }
}
