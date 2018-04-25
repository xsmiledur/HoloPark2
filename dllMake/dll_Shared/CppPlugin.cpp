#include "opencv2/opencv.hpp"
#include "CppPlugin.h"


extern "C" {
	int __stdcall Add(int a, int b) {
		return a + b;
	}
	int __stdcall Sum(unsigned char* data, int height, int width)
	{
		cv::Mat mat(height, width, CV_8U, data);
		cv::Scalar sum_4ch = cv::sum(mat);
		return static_cast<int>(sum_4ch[0]);
	}
}