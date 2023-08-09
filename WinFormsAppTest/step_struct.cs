using System;
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

    [Serializable]
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

    [Serializable]
    public struct Crop
    {
        public double buffer { get; set; }
    }

    [Serializable]
    public struct Subsampling
    {
        public double cellSize { get; set; }
    }

    [Serializable]
    public struct Outlier
    {
        /// <summary>
        /// confing파일에서 mean_k입니다.
        /// </summary>
        public double mean_k { get; set; }
        /// <summary>
        /// config파일에서 method입니다.
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// confing 파일에서 multiplier입니다.
        /// </summary>
        public double Multiplier { get; set; }

    }

    [Serializable]
    public struct GroundSeg
    {
        public string scalar { get; set; }
        public string cellSize { get; set; }
        public string slope { get; set; }
        public string windowSize { get; set; }
        public string threshold { get; set; }
    }

    [Serializable]
    public struct TSlice
    {
        public double minHeight { get; set; }
        public double maxHeight { get; set; }
    }

    [Serializable]
    public struct CSlice
    {
        public double minHeight { get; set; }
        public double maxHeight { get; set; }
    }

    [Serializable]
    public struct CrownSeg
    {
        public int CrownNN { get; set; }
    }

    [Serializable]
    public struct Measure
    {
        public int MeasureNN { get; set; }
        public double minRad { get; set; }
        public double maxRad { get; set; }
        public int iterations { get; set; }
    }

    [Serializable]
    public struct SegmentStem
    {
        public double smoothness { get; set; }
        public double minDBH { get; set; }
        public double maxDBH { get; set; }
        public double HeightThreshold { get; set; }
    }
}
