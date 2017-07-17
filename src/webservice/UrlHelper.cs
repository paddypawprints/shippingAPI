using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace com.pb.shippingapi
{ 
    public static class UrlHelper
    {
        public static void URLAdd( StringBuilder url, string s)
        {
            // This is a cheerful encoder. Replace when the .net core framework URL Encoder is available.
            var bytes = Encoding.UTF8.GetBytes(s);

            foreach( var b in bytes )
            {
                switch(Convert.ToChar(b))
                {
                    case ' ': url.Append("%20");break;
                    case '!': url.Append("%21");break;
                    case '#': url.Append("%23");break;
                    case '$': url.Append("%24");break;
                    case '%': url.Append("%25");break;
                    case '&': url.Append("%26");break;
                    case '\'': url.Append("%27");break;
                    case '(': url.Append("%28");break;
                    case ')': url.Append("%29");break;
                    case '*': url.Append("%2A");break;
                    case '+': url.Append("%2B");break;
                    case ',': url.Append("%2C");break;
                    case '/': url.Append("%2F");break;
                    case ':': url.Append("%3A");break;
                    case ';': url.Append("%3B");break;
                    case '=': url.Append("%3D");break;
                    case '?': url.Append("%3F");break;
                    case '@': url.Append("%40");break;
                    case '[': url.Append("%5B");break;
                    case ']': url.Append("%5D");break;
                    default:  url.Append(Convert.ToChar(b)); break;
                }
            }
        }
        public static void Encode(StringBuilder buffer, char[] data)
        {
            char[] tbl = {
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P',
                'Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f',
                'g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v',
                'w','x','y','z','0','1','2','3','4','5','6','7','8','9','+','/' };

            int pad = 0;
            for (int i = 0; i < data.Length; i += 3) {

                int b = ((data[i] & 0xFF) << 16) & 0xFFFFFF;
                if (i + 1 < data.Length) {
                    b |= (data[i+1] & 0xFF) << 8;
                } else {
                    pad++;
                }
                if (i + 2 < data.Length) {
                    b |= (data[i+2] & 0xFF);
                } else {
                    pad++;
                }

                for (int j = 0; j < 4 - pad; j++) {
                    int c = (b & 0xFC0000) >> 18;
                    buffer.Append(tbl[c]);
                    b <<= 6;
                }
            }
            for (int j = 0; j < pad; j++) {
                buffer.Append("=");
            }
        }

        public static byte[] Decode(String data)
        {
            int[] tbl = {
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63, 52, 53, 54,
                55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1, -1, 0, 1, 2,
                3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1, -1, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47,
                48, 49, 50, 51, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            char[] bytes = data.ToCharArray();
            MemoryStream buffer = new MemoryStream();
            for (int i = 0; i < bytes.Length; ) {
                int b = 0;
                if (tbl[bytes[i]] != -1) {
                    b = (tbl[bytes[i]] & 0xFF) << 18;
                }
                // skip unknown characters
                else {
                    i++;
                    continue;
                }

                int num = 0;
                if (i + 1 < bytes.Length && tbl[bytes[i+1]] != -1) {
                    b = b | ((tbl[bytes[i+1]] & 0xFF) << 12);
                    num++;
                }
                if (i + 2 < bytes.Length && tbl[bytes[i+2]] != -1) {
                    b = b | ((tbl[bytes[i+2]] & 0xFF) << 6);
                    num++;
                }
                if (i + 3 < bytes.Length && tbl[bytes[i+3]] != -1) {
                    b = b | (tbl[bytes[i+3]] & 0xFF);
                    num++;
                }

                while (num > 0) {
                    int c = (b & 0xFF0000) >> 16;
                    buffer.WriteByte((byte)c);
                    b <<= 8;
                    num--;
                }
                i += 4;
            }
            return buffer.ToArray();
        }
    }
}
