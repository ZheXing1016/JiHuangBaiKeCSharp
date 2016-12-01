﻿using ServerTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 饥荒Log;

namespace 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer
{
    /// <summary>
    /// 2016.12.1  写 基本设置类
    /// 给我一个cluster.ini地址，还你一片天空
    /// 添加字段和属性后，记得在构造函数附一个初始值
    /// </summary>
    class Basic : INotifyPropertyChanged
    {
        #region 字段和属性

        private UTF8Encoding utf8NoBom = new UTF8Encoding(false);

        private bool isFileToProperty=false;
        private List<string> gameStyle;
        private string gameStyleText;
        private string houseName;
        private string describe;
        private List<string> gameMode;
        private string gameModeText;
        private int limitNumOfPeople;
        private string secret;
        private bool isCave;
        private bool isConsole;
        private bool isPause;
        private bool isPVP;
        private string clusterIni_FilePath;

        public string HouseName
        {
            get
            {
             
                return houseName;

            }

            set
            {
                
                houseName = value;
                NotifyPropertyChange("HouseName");
            }
        }

        public string Describe
        {
            get
            {
                return describe;
            }

            set
            {
                describe = value;
                NotifyPropertyChange("Describe");
            }
        }

  

        public int LimitNumOfPeople
        {
            get
            {
                return limitNumOfPeople;
            }

            set
            {
                limitNumOfPeople = value;
                NotifyPropertyChange("LimitNumOfPeople");
            }
        }

        public string Secret
        {
            get
            {
                return secret;
            }

            set
            {
                secret = value;
                NotifyPropertyChange("Secret");
            }
        }

        public bool IsCave
        {
            get
            {
                return isCave;
            }

            set
            {
                isCave = value;
                NotifyPropertyChange("IsCave");
            }
        }

        public bool IsConsole
        {
            get
            {
                return isConsole;
            }

            set
            {
                isConsole = value;
                NotifyPropertyChange("IsConsole");
            }
        }

        public bool IsPause
        {
            get
            {
                return isPause;
            }

            set
            {
                isPause = value;
                NotifyPropertyChange("IsPause");
            }
        }

        public bool IsPVP
        {
            get
            {
                return isPVP;
            }

            set
            {
                isPVP = value;
                NotifyPropertyChange("IsPVP");
            }
        }

        public string GameStyleText
        {
            get
            {
                return gameStyleText;
            }

            set
            {
                gameStyleText = value;
                NotifyPropertyChange("GameStyleText");
            }
        }

        public string GameModeText
        {
            get
            {
                return gameModeText;
            }

            set
            {
                gameModeText = value;
                NotifyPropertyChange("GameModeText");
            }
        }

        public List<string> GameMode
        {
            get
            {
                return gameMode;
            }
            set
            {

                gameMode = value;
                NotifyPropertyChange("GameMode");
            }

         
        }

        public List<string> GameStyle
        {
            get
            {
                return gameStyle;
            }
            set
            {

                gameStyle = value;
                NotifyPropertyChange("GameStyle");
            }

           
        }

        #endregion

        #region 构造函数 

        /// <summary>
        /// clusterIni的地址，目前地址只能通过构造函数传入，以后有需要再改
        /// </summary>
        /// <param name="clusterIni_FilePath"></param>
        public Basic(string clusterIni_FilePath) {

            // 游戏风格【字段赋值】
            gameStyle = new List<string>();
            gameStyle.Add("合作");
            gameStyle.Add("交际");
            gameStyle.Add("竞争");
            gameStyle.Add("疯狂");
            gameStyleText = "合作";
            NotifyPropertyChange("GameStyle");
           
            // 游戏模式【字段赋值】
            gameMode = new List<string>();
            gameMode.Add("生存");
            gameMode.Add("无尽");
            gameMode.Add("荒野");
            gameModeText = "无尽";
            NotifyPropertyChange("GameMode");
         

            // 其他先全部赋值，防止为空
            houseName = "qq群：351765204";
            describe = "qq群：351765204";
            limitNumOfPeople = 6;
            secret = "333";
            isCave = false;
            isConsole = true;
            isPause = true;
            isPVP = false;


            if (File.Exists(clusterIni_FilePath))
            {
                this.clusterIni_FilePath = clusterIni_FilePath;
                // 从文件读，给字段赋值
                FileToProperty(clusterIni_FilePath);


            }
            else {

                Debug.WriteLine("cluster.ini文件不存在");
            }
                     
        }
        #endregion

        #region 方法
        /// <summary>
        /// 从文件读，给字段赋值
        /// </summary>
        private void FileToProperty(string clusterIniPath) {

            // 改变记号（这个记号可能以后保存的时候会有用）
            isFileToProperty = true;

  //          Log.Write("从cluster.ini文件读取数据给基本设置字段赋值—开始");
            // 标记！：这里没有判断文件是否存在，在外面判断了，以后再看用不用修改

            //读取基本设置
            INIhelper iniTool = new INIhelper(clusterIniPath, utf8NoBom);


            //读取游戏风格
            string yx_fengge = iniTool.ReadValue("NETWORK", "cluster_intention");
            if (yx_fengge == "cooperative") { GameStyleText = "合作"; };
            if (yx_fengge == "social") { GameStyleText = "交际"; };
            if (yx_fengge == "competitive") { GameStyleText = "竞争"; };
            if (yx_fengge == "madness") { GameStyleText = "疯狂"; };

            //读取房间名称
            string fj_name = iniTool.ReadValue("NETWORK", "cluster_name");
            HouseName = fj_name;
            //读取描述
            string fj_miaoshu = iniTool.ReadValue("NETWORK", "cluster_description");
            Describe = fj_miaoshu;
            //读取游戏模式
            string yx_moshi = iniTool.ReadValue("GAMEPLAY", "game_mode");
            if (yx_moshi == "endless") { GameModeText= "无尽"; };
            if (yx_moshi == "survival") { GameModeText = "生存"; };
            if (yx_moshi == "wilderness") { GameModeText = "荒野"; };
            //读取人数限制
            string yx_renshu = iniTool.ReadValue("GAMEPLAY", "max_players");
            LimitNumOfPeople = int.Parse(yx_renshu);
            //读取密码
            string yx_mima = iniTool.ReadValue("NETWORK", "cluster_password");
            Secret = yx_mima;


            //读取是否启用控制台[标记：这里没有变成小写]
            string yx_kongzhitai = iniTool.ReadValue("MISC", "console_enabled");
            if (yx_kongzhitai == "true") { IsConsole= true; };
            if (yx_kongzhitai == "false") {IsConsole= false; };

            //读取无人时暂停[标记：这里没有变成小写]
            string yx_zhanting = iniTool.ReadValue("GAMEPLAY", "pause_when_empty");
            if (yx_zhanting == "true") { IsPause = true; };
            if (yx_zhanting == "false") { IsPause = false; };
            //读取PVP[标记：这里没有变成小写]
            string yx_pvp = iniTool.ReadValue("GAMEPLAY", "pvp");
            if (yx_pvp == "true") { IsPVP = true; };
            if (yx_pvp == "false") { IsPVP = false; };

            // 读取是否开启洞穴[标记：这里没有变成小写]
            string yx_cave = iniTool.ReadValue("SHARD", "shard_enabled");
            if (yx_cave == "true") { IsCave = true; };
            if (yx_cave == "false") { IsCave = false; };

            isFileToProperty = false;

      //  Log.Write("从cluster.ini文件读取数据给基本设置字段赋值—结束");

        }

        #endregion


        #region  标记：【接口】,还是第一次用接口

        public event PropertyChangedEventHandler PropertyChanged;
        //PropertyChangedEventArgs类型，这个类用于传递更改值的属性的名称，实现向客户端已经更改的属性发送更改通知。属性的名称为字符串类型。 
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //根据PropertyChanged事件的委托类，实现PropertyChanged事件： 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));


                // 保存
                if (isFileToProperty==false)
                {
                    INIhelper ini1 = new INIhelper(@"C:\Users\yy\Documents\Klei\DoNotStarveTogether\yyServer" + @"\cluster.ini", utf8NoBom);

                    if (propertyName == "HouseName")
                    {
                        ini1.write("NETWORK", "cluster_name", HouseName, utf8NoBom);
                    }

                    if (propertyName == "Describe")
                    {
                        ini1.write("NETWORK", "cluster_description", Describe, utf8NoBom);
                    }
                    if (propertyName == "LimitNumOfPeople")
                    {
                        ini1.write("GAMEPLAY", "max_players", LimitNumOfPeople.ToString(), utf8NoBom);
                    }
                    if (propertyName == "Secret")
                    {
                        ini1.write("NETWORK", "cluster_password", Secret, utf8NoBom);
                    }
                    if (propertyName == "IsCave")
                    {
                        ini1.write("SHARD", "shard_enabled", IsCave.ToString().ToLower(), utf8NoBom);
                    }
                    if (propertyName == "IsConsole")
                    {
                        ini1.write("MISC", "console_enabled", IsConsole.ToString().ToLower(), utf8NoBom);
                    }
                    if (propertyName == "IsPause")
                    {
                        ini1.write("GAMEPLAY", "pause_when_empty", IsPause.ToString().ToLower(), utf8NoBom);
                    }
                    if (propertyName == "IsPVP")
                    {
                        ini1.write("GAMEPLAY", "pvp", IsPVP.ToString().ToLower(), utf8NoBom);
                    }
                    if (propertyName == "GameStyleText")
                    {
                        if (GameStyleText == "合作") { ini1.write("NETWORK", "cluster_intention", "cooperative", utf8NoBom); };
                        if (GameStyleText == "交际") { ini1.write("NETWORK", "cluster_intention", "social", utf8NoBom); };
                        if (GameStyleText == "竞争") { ini1.write("NETWORK", "cluster_intention", "competitive", utf8NoBom); };
                        if (GameStyleText == "疯狂") { ini1.write("NETWORK", "cluster_intention", "madness", utf8NoBom); };
                    }
                    if (propertyName == "GameModeText")
                    {
                        if (GameModeText  == "无尽") { ini1.write ("GAMEPLAY", "game_mode", "endless", utf8NoBom); };
                        if (GameModeText  == "生存") { ini1.write ("GAMEPLAY", "game_mode", "survival", utf8NoBom); };
                        if (GameModeText  == "荒野") { ini1.write ("GAMEPLAY", "game_mode", "wilderness", utf8NoBom); };
                    }
                }
          
            }
        }

        #endregion












    }
}
