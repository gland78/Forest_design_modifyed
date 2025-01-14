﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppTest
{
    public struct point
    {
        public double x;
        public double y;
    }
    public struct Coords
    {
        public point top;
        public point bottom;
        public point right;
        public point left;
    }

    public struct GUI
    {
        /// <summary>
        /// 원본 las파일 경로
        /// </summary>
        public string loadPath { get; set; }
        /// <summary>
        /// 결과 폴더 경로
        /// </summary>
        public string resultPath { get; set; }
        /// <summary>
        /// 표준지 중심 x좌표
        /// </summary>
        public double centerX { get; set; }
        /// <summary>
        /// 표준지 중심 y좌표
        /// </summary>
        public double centerY { get; set; }
        /// <summary>
        /// 표준지를 원으로 cropping할때 원의 반지름
        /// </summary>
        public double radius { get; set; }
        /// <summary>
        /// 표준지를 정사각형으로 cropping할때 한변의 길이
        /// </summary>
        public double xMin { get; set; }
        public double xMax { get; set; }
        public double yMin { get; set; }
        public double yMax { get; set; }
        /// <summary>
        /// 표준지를 다각형으로 cropping할때 다각형의 꼭짓점을 저장해둔 List
        /// </summary>
        public string pointList { get; set; }
    }

    struct LasSize
    {
        public double maxx;
        public double maxy;
        public double minx;
        public double miny;
        public double minz;
        public double maxz;
    }
}
