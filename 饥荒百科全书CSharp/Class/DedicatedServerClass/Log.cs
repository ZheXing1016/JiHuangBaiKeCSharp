using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒Log
{
    /// <summary>
    /// 2016.12.1 编写 饥荒百科日志操作文件Log
    /// </summary>
    class Log
    {
        #region 【字段，属性，构造】
        // 当前路径
        private static string currentPath = System.Environment.CurrentDirectory;

        // UTF-8 无 BOM ,格式还是和饥荒统一吧
        private static UTF8Encoding utf8NoBom = new UTF8Encoding(false);     
        #endregion

        #region 【写入文件】
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="path"></param>
        public static void Write(string text)
        {
         

            // 以年月日为文件名
            string logFileName= DateTime.Now.ToString("yyyy年MM月dd日");

            // 不存在，创建---【当前文件夹下的Log文件夹下以年月日文件名的.log文件】
            string logFilePath = currentPath + @"\Log\" + logFileName + ".log";

            // 先创建目录
            if (!Directory.Exists(currentPath + @"\Log"))
            {
                Directory.CreateDirectory(currentPath + @"\Log");
            }

            // 再创建文件
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath);
            }

            // 到现在才写入
            FileStream fs = new FileStream(logFilePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, utf8NoBom);

            sw.WriteLine("【"+System.DateTime.Now.ToString()+"】:"+text);

            // 关闭
            sw.Dispose();
            sw.Close();
            fs.Dispose();
            fs.Close();


        }

        #endregion


    }
}
