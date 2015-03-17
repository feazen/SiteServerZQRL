using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL
{
    public class SmsInfo
    {
        private int keyID;
        private string loginMessage;
        private string findPassWord;
        private string mobileYYFG;
        private string tJYYD;
        private string jXSXYYD;
        private string jXSQRCS;
        private string gLYQRCS;
        private string jXSFFCS;
        private string gLYFFCS;
        private string fGWC;
        private string sDSH;
        private string gGXX;

        public int KeyID
        {
            set { keyID = value; }
            get { return keyID; }
        }

        public string LoginMessage
        {
            set { loginMessage = value; }
            get { return loginMessage; }
        }

        public string FindPassWord
        {
            set { findPassWord = value; }
            get { return findPassWord; }
        }

        public string MobileYYFG
        {
            set { mobileYYFG = value; }
            get { return mobileYYFG; }
        }
        public string TJYYD
        {
            set { tJYYD = value; }
            get { return tJYYD; }
        }
        public string JXSXYYD
        {
            set { jXSXYYD = value; }
            get { return jXSXYYD; }
        }
        public string JXSQRCS
        {
            set { jXSQRCS = value; }
            get { return jXSQRCS; }
        }
        public string GLYQRCS
        {
            set { gLYQRCS = value; }
            get { return gLYQRCS; }
        }
        public string JXSFFCS
        {
            set { jXSFFCS = value; }
            get { return jXSFFCS; }
        }
        public string GLYFFCS
        {
            set { gLYFFCS = value; }
            get { return gLYFFCS; }
        }
        public string FGWC
        {
            set { fGWC = value; }
            get { return fGWC; }
        }
        public string SDSH
        {
            set { sDSH = value; }
            get { return sDSH; }
        }
        public string GGXX
        {
            set { gGXX = value; }
            get { return gGXX; }
        }

    }
}
