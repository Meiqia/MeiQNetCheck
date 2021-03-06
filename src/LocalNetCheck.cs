﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MeiQNetCheck
{
    /**
     *@className   LocalNetCheck
     *@brief       对本地网络配置进行检测
     *@date        2016.9.2
     */
    class LocalNetCheck : IDisposable
    {
        [System.Runtime.InteropServices.DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        private string _message;     //holds the message from net check
        private const int INTERNET_CONNECTION_MODEM = 1;  //Local system uses a modem to connect to the Internet
        private const int INTERNET_CONNECTION_LAN = 2;   //Local system uses a local area network to connect to the Internet
        private const int INTERNET_CONNECTION_PROXY = 4; //Local system uses a proxy server to connect to the Internet
        private const int INTERNET_CONNECTION_CONFIGURED = 64; //Local system has a valid connection to the Internet, but it might or might not be currently connected
        private FormMeiQ _form1;
        private PingServerNetCheck _pingCheck;
        //非托管资源是否回收完毕
        bool _disposed;

        public LocalNetCheck( FormMeiQ form1 )
        {
            _message = "";
            _form1 = form1;
            _pingCheck = new PingServerNetCheck(form1);
        }

        ~LocalNetCheck()
        {
            Dispose(false);
        }

        //实现Dispose()接口
        public void Dispose()
        {
            Dispose(true);
            //.NET Framework 类库
            // GC..::.SuppressFinalize 方法
            //请求系统不要调用指定对象的终结器。
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //如果已经被回收，就中断执行
            if (_disposed) return;
            if (disposing)
            {
                _pingCheck.Dispose();
            }
            _disposed = true;
        }

        /**
         * @brief   通过InternetGetConnectedState接口对本地网络情况进行监测 and holds result to _message
         * @detail  引入InternetGetConnetedState接口需要在类中引入其他动态库
         * @param
         * @return
         */
        public bool checkLocalNetStat()
        {
            int checkStateFlag = 0;
            if (InternetGetConnectedState(ref checkStateFlag, 0))
            {
                if ((checkStateFlag & 0x47) != 0)
                {
                    if (_pingCheck.checkNetPing("www.jd.com") || _pingCheck.checkNetPing("www.baidu.com") || _pingCheck.checkNetPing("www.qq.com"))
                    {
                        _message = "[" + System.DateTime.UtcNow.AddHours(8) + "]本地网络连接：【本地网络配置正常】" + System.Environment.NewLine;
                        Log.logToTextBox(_message,_form1);
                        _pingCheck.Dispose();
                        return true;
                    }
                    else
                    {
                        _pingCheck.Dispose();
                    }
                }
            }
            _message = "[" + System.DateTime.UtcNow.AddHours(8) + "]【Error: 本地未连接网络，请检查本地网络配置】" + System.Environment.NewLine;
            Log.logToTextBox(_message,_form1);
            return false;
        }

        /**
         * @brief 获取本地网络连接的信息
         * @param
         * @return
         */
        public string netMessage
        {
            set { _message = value; }
            get { return _message; }
        }
    }

}
